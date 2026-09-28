using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Aspire.Services;

public class LeadService()
{
    public static async Task<Lead> Qualify(
        Lead entity,
        string qualificationOption,
        ApplicationDbContext context,
        ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        Lead? obj = context.Leads.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database

        // if (obj is null)
        // {
        //     entity.Msg = "This Id (" + entity.Id + ") does not exist in the database.";
        //     return entity;
        // }

        if (obj.CustomerId.HasValue)
        {
            entity.Msg = "This lead has already been converted to a customer.";
            return entity;
        }

        if (qualificationOption is not ("B2C" or "B2B" or "B2BContact"))
        {
            entity.Msg = "Please select B2C, B2B, or B2B + Linked Contact.";
            return entity;
        }

        bool isB2C = qualificationOption == "B2C";

        bool createContact =
            qualificationOption == "B2C" ||
            qualificationOption == "B2BContact";

        string customerTypeName = isB2C
            ? "Individual (B2C)"
            : "Company (B2B)";

        long? tenantId = _tenantService.GetTenantId();

        EntityType? customerType = await context.EntityTypes
            .FirstOrDefaultAsync(e =>
                e.TenantId == tenantId &&
                e.EntityName == "Customer" &&
                e.Name == customerTypeName);

        if (customerType is null)
        {
            entity.Msg = $"Customer type '{customerTypeName}' was not found.";
            return entity;
        }

        EntityStatus? qualifiedStatus = await context.EntityStatuses
            .FirstOrDefaultAsync(e =>
                e.TenantId == tenantId &&
                e.EntityName == "Lead" &&
                e.Name == "Qualified");

        if (qualifiedStatus is null)
        {
            entity.Msg = "Qualified lead status was not found.";
            return entity;
        }

        EntityStatus? customerStatus = await context.EntityStatuses
            .FirstOrDefaultAsync(e =>
                e.TenantId == tenantId &&
                e.EntityName == "Customer" &&
                e.Name == "Registered");

        if (customerStatus is null)
        {
            entity.Msg = "Registered customer status was not found.";
            return entity;
        }

        EntityStatus? contactStatus = null;

        if (createContact)
        {
            contactStatus = await context.EntityStatuses
                .FirstOrDefaultAsync(e =>
                    e.TenantId == tenantId &&
                    e.EntityName == "Contact" &&
                    e.Name == "Active");

            if (contactStatus is null)
            {
                entity.Msg = "Active contact status was not found.";
                return entity;
            }
        }

        if (string.IsNullOrWhiteSpace(obj.Name))
        {
            entity.Msg = "The lead name is required to create a customer.";
            return entity;
        }

        string contactName = string.Join(
            " ",
            new[] { obj.FirstName, obj.LastName }
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .Select(value => value!.Trim()));

        if (createContact && string.IsNullOrWhiteSpace(contactName))
        {
            entity.Msg = "First name or last name is required to create a contact.";
            return entity;
        }

        await using var transaction =
            await context.Database.BeginTransactionAsync();

        try
        {
            Customer customer = new Customer
            {
                // General customer information comes from the Lead entity argument.
                Name = obj.Name,
                NameAr = obj.NameAr ?? obj.Name,

                EntityTypeId = customerType.Id,
                EntityStatusId = customerStatus.Id,
                DataConfidentialityId = obj.DataConfidentialityId,

                AccountManagerUsrId = obj.AccountManagerUsrId,
                CurrencyId = obj.CurrencyId,
                CountryId = obj.CountryId,
                City = obj.City,

                Description = obj.Description,
                DescriptionAr = obj.DescriptionAr,

                ActionComment = entity.ActionComment,
                IsActive = true
            };

            if (isB2C)
            {
                // Individual customer information comes from the B2C lead section.
                customer.Name = obj.FirstName + " " + obj.LastName;
                customer.Email = obj.Email;
                customer.Mobile = obj.Mobile;
            }
            else
            {
                // Business customer information comes from the B2B lead section.
                customer.Domain = obj.Domain;
                customer.Website = obj.Website;
                customer.IndustryId = obj.IndustryId;
                customer.SectorId = obj.SectorId;
                customer.Employees_num = obj.Employees_num;
                customer.AnnualRevenue_val = obj.AnnualRevenue_val;
                customer.Phone = obj.Phone;
                customer.Email = obj.CompanyEmail;
            }

            customer = await CustomerService.Create(
                customer,
                context,
                _tenantService);

            if (customer.Msg != "OK")
            {
                await transaction.RollbackAsync();
                entity.Msg = customer.Msg;
                return entity;
            }

            Contact? contact = null;

            if (createContact)
            {
                // Contact information comes from the B2C section of the Lead entity.
                contact = new Contact
                {
                    ObjectId = customer.Id,
                    AccountManagerId = obj.AccountManagerUsrId,

                    Name = contactName,
                    NameAr = contactName,

                    FirstName = obj.FirstName,
                    LastName = obj.LastName,
                    Email = obj.Email,
                    Mobile = obj.Mobile,
                    JobTitle = obj.JobTitle,
                    GenderId = obj.GenderId,

                    CountryId = obj.CountryId,
                    City = obj.City,

                    EntityStatusId = contactStatus!.Id,
                    DataConfidentialityId = obj.DataConfidentialityId,

                    ActionComment = entity.ActionComment,
                    IsActive = true
                };

                contact = await ContactService.Create(
                    contact,
                    context,
                    _tenantService);

                if (contact.Msg != "OK")
                {
                    await transaction.RollbackAsync();
                    entity.Msg = contact.Msg;
                    return entity;
                }
            }

            // Link the lead to the created records and mark it as Qualified.
            obj.CustomerId = customer.Id;
            obj.ContactId = contact?.Id;
            obj.EntityStatusId = qualifiedStatus.Id;
            obj.ActionComment = entity.ActionComment;
            obj.UpdatedByUsrId = _tenantService.GetUserId();
            obj.UpdateDate = DateTime.UtcNow;
            obj.Msg = "OK";

            await context.SaveChangesAsync();
            await transaction.CommitAsync();

            // Return the created record IDs to the frontend.
            entity.CustomerId = customer.Id;
            entity.ContactId = contact?.Id;
            entity.EntityStatusId = qualifiedStatus.Id;
            entity.Msg = "OK";

            await Utilities.DBLog.Action(obj.Id,
                "Lead",
                "Qualify",
                obj.ActionComment,
                context,
                _tenantService);

            return entity;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public static async Task<Lead> Disqualify(Lead entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        Lead? obj = await context.Leads.FirstOrDefaultAsync(e => e.Id == entity.Id);

        if (obj is null)
        {
            entity.Msg = "This Id (" + entity.Id + ") does not exist in the database.";
            return entity;
        }
        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database

        long? tenantId = _tenantService.GetTenantId();

        EntityStatus? disqualifiedStatus = await context.EntityStatuses
            .FirstOrDefaultAsync(e =>
                e.TenantId == tenantId &&
                e.EntityName == "Lead" &&
                e.Name == "Un-Qualified");

        if (disqualifiedStatus is null)
        {
            entity.Msg = "Un-Qualified lead status was not found.";
            return entity;
        }

        obj.EntityStatusId = disqualifiedStatus.Id;
        obj.ActionComment = entity.ActionComment;
        obj.UpdatedByUsrId = _tenantService.GetUserId();
        obj.UpdateDate = DateTime.UtcNow;
        obj.Msg = "OK";

        await context.SaveChangesAsync();

        entity.EntityStatusId = disqualifiedStatus.Id;
        entity.ActionComment = obj.ActionComment;
        entity.Msg = "OK";
        // Log The Action
        await Utilities.DBLog.Action(obj.Id,
            "Lead",
            "Disqualify",
            obj.ActionComment,
            context,
            _tenantService
        );

        return entity;
    }

    // Code-Player-Start-DTO //
    public static async Task<Lead> Create(Lead entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        // Perform the Business Logic BEFOR CREATING the WorkItem Object in the Database
        entity.TenantId = _tenantService.GetTenantId();
        
        entity.CreatedByUsrId = _tenantService.GetUserId();
        entity.CreateDate = DateTime.UtcNow;
        entity.Msg = "OK";
        context.Leads.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public static async Task<Lead> Update(Lead entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        Lead? obj = context.Leads.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Populate the Object Will be Saved
        obj.IssueDate = entity.IssueDate is null ? obj.IssueDate : entity.IssueDate;
        obj.FollowUpDate = entity.FollowUpDate is null ? obj.FollowUpDate : entity.FollowUpDate;
        obj.IndustryId = entity.IndustryId is null ? obj.IndustryId : entity.IndustryId;
        obj.SectorId = entity.SectorId is null ? obj.SectorId : entity.SectorId;
        obj.AccountManagerUsrId = entity.AccountManagerUsrId is null ? obj.AccountManagerUsrId : entity.AccountManagerUsrId;
        obj.CurrencyId = entity.CurrencyId is null ? obj.CurrencyId : entity.CurrencyId;
        obj.CountryId = entity.CountryId is null ? obj.CountryId : entity.CountryId;
        obj.City = entity.City is null ? obj.City : entity.City;
        obj.FirstName = entity.FirstName is null ? obj.FirstName : entity.FirstName;
        obj.LastName = entity.LastName is null ? obj.LastName : entity.LastName;
        obj.Email = entity.Email is null ? obj.Email : entity.Email;
        obj.JobTitle = entity.JobTitle is null ? obj.JobTitle : entity.JobTitle;
        obj.Mobile = entity.Mobile is null ? obj.Mobile : entity.Mobile;
        obj.GenderId = entity.GenderId is null ? obj.GenderId : entity.GenderId;
        obj.Domain = entity.Domain is null ? obj.Domain : entity.Domain;
        obj.Website = entity.Website is null ? obj.Website : entity.Website;
        obj.Phone = entity.Phone is null ? obj.Phone : entity.Phone;
        obj.CompanyEmail = entity.CompanyEmail is null ? obj.CompanyEmail : entity.CompanyEmail;
        obj.EntityStatusId = entity.EntityStatusId is null ? obj.EntityStatusId : entity.EntityStatusId;
        obj.EntityTypeId = entity.EntityTypeId is null ? obj.EntityTypeId : entity.EntityTypeId;
        obj.DataConfidentialityId = entity.DataConfidentialityId is null ? obj.DataConfidentialityId : entity.DataConfidentialityId;
        obj.Name = entity.Name is null ? obj.Name : entity.Name;
        obj.NameAr = entity.NameAr is null ? obj.NameAr : entity.NameAr;
        obj.Description = entity.Description is null ? obj.Description : entity.Description;
        obj.DescriptionAr = entity.DescriptionAr is null ? obj.DescriptionAr : entity.DescriptionAr;
        obj.IsActive = entity.IsActive is null ? obj.IsActive : entity.IsActive;
        obj.ActionComment = entity.ActionComment is null ? obj.ActionComment : entity.ActionComment;
        obj.PhotoUrl = entity.PhotoUrl is null ? obj.PhotoUrl : entity.PhotoUrl;

        obj.Msg = "OK";
        obj.UpdatedByUsrId = _tenantService.GetUserId();
        obj.UpdateDate = DateTime.UtcNow;
        await context.SaveChangesAsync();
        return obj;
    }

    public static string OnDelete(long Id, ApplicationDbContext context)
    {
        string msg = "OK";
        // Perform the Check Before DELETE
        // Update the "Msg" if there is any Validation Error Should Stop the DELETE Action


        return msg;
    }
    // Code-Player-End-DTO //
}