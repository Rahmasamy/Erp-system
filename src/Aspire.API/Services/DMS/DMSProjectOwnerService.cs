using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;

namespace Aspire.Services;

public class DMSProjectOwnerService()
{


// Code-Player-Start-DTO //
    public static async Task<DMSProjectOwner> Create(DMSProjectOwner entity, ApplicationDbContext context, ITenantProviderService _tenantService)
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
        context.DMSProjectOwners.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public static async Task<DMSProjectOwner> Update(DMSProjectOwner entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        DMSProjectOwner? obj = context.DMSProjectOwners.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Populate the Object Will be Saved
        obj.AccountManagerUsrId = entity.AccountManagerUsrId is null ? obj.AccountManagerUsrId : entity.AccountManagerUsrId;
        obj.CurrencyId = entity.CurrencyId is null ? obj.CurrencyId : entity.CurrencyId;
        obj.Domain = entity.Domain is null ? obj.Domain : entity.Domain;
        obj.Website = entity.Website is null ? obj.Website : entity.Website;
        obj.CRNo = entity.CRNo is null ? obj.CRNo : entity.CRNo;
        obj.VATRegNo = entity.VATRegNo is null ? obj.VATRegNo : entity.VATRegNo;
        obj.RegistrationNo = entity.RegistrationNo is null ? obj.RegistrationNo : entity.RegistrationNo;
        obj.IndustryId = entity.IndustryId is null ? obj.IndustryId : entity.IndustryId;
        obj.SectorId = entity.SectorId is null ? obj.SectorId : entity.SectorId;
        obj.Employees_num = entity.Employees_num is null ? obj.Employees_num : entity.Employees_num;
        obj.AnnualRevenue_val = entity.AnnualRevenue_val is null ? obj.AnnualRevenue_val : entity.AnnualRevenue_val;
        obj.Phone = entity.Phone is null ? obj.Phone : entity.Phone;
        obj.Mobile = entity.Mobile is null ? obj.Mobile : entity.Mobile;
        obj.Email = entity.Email is null ? obj.Email : entity.Email;
        obj.CountryId = entity.CountryId is null ? obj.CountryId : entity.CountryId;
        obj.City = entity.City is null ? obj.City : entity.City;
        obj.State = entity.State is null ? obj.State : entity.State;
        obj.Address = entity.Address is null ? obj.Address : entity.Address;
        obj.Street = entity.Street is null ? obj.Street : entity.Street;
        obj.ZipCode = entity.ZipCode is null ? obj.ZipCode : entity.ZipCode;
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