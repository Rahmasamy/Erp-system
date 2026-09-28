using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Aspire.Services;

public class CustomerService()
{
    public static async Task<Customer> Block(Customer entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        Customer? obj = await context.Customers.FirstOrDefaultAsync(e => e.Id == entity.Id);

        if (obj is null)
        {
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";
            return entity;
        }

        // Perform the Business Logic BEFOR UPDATING the Customer Object in the Database
        obj.IsBlock = true;
        obj.ActionComment = entity.ActionComment;
        obj.UpdatedByUsrId = _tenantService.GetUserId();
        obj.UpdateDate = DateTime.UtcNow;
        await context.SaveChangesAsync();

        // Log The Action
        await Utilities.DBLog.Action(obj.Id, "Customer", "Block", obj.ActionComment ?? string.Empty, context, _tenantService);

        obj.Msg = "OK";
        return obj;
    }


    public static async Task<Customer> UnBlock(Customer entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        Customer? obj = await context.Customers.FirstOrDefaultAsync(e => e.Id == entity.Id);

        if (obj is null)
        {
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";
            return entity;
        }

        // Perform the Business Logic BEFOR UPDATING the Customer Object in the Database
        obj.IsBlock = false;
        obj.ActionComment = entity.ActionComment;
        obj.UpdatedByUsrId = _tenantService.GetUserId();
        obj.UpdateDate = DateTime.UtcNow;
        await context.SaveChangesAsync();

        // Log The Action
        await Utilities.DBLog.Action(obj.Id, "Customer", "UnBlock", obj.ActionComment ?? string.Empty, context, _tenantService);

        obj.Msg = "OK";
        return obj;
    }


    public static async Task<Customer> AssignPriceList(Customer entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        Customer? obj = await context.Customers.FirstOrDefaultAsync(e => e.Id == entity.Id);

        if (obj is null)
        {
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";
            return entity;
        }

        if (entity.PriceListId is null || !await context.PriceLists.AnyAsync(p => p.Id == entity.PriceListId))
            entity.Msg = "Select a valid price list for this customer organization";

        if (entity.Msg != "OK")
            return entity;

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database
        obj.PriceListId = entity.PriceListId;
        obj.ActionComment = entity.ActionComment;
        obj.UpdatedByUsrId = _tenantService.GetUserId();
        obj.UpdateDate = DateTime.UtcNow;
        await context.SaveChangesAsync();

        // Log The Action
        await Utilities.DBLog.Action(obj.Id, "Customer", "AssignPriceList", obj.ActionComment ?? string.Empty, context, _tenantService);

        obj.Msg = "OK";
        return obj;
    }


    public static async Task<Customer> OpeningBalance(Customer entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";

        if (entity.OpeningBalance_val <= 0)
        {
            entity.Msg = "Opening balance must be greater than zero";
            return entity;
        }

        if (entity.OpeningBalance_val > 999999999)
        {
            entity.Msg = "Opening balance cannot exceed 999,999,999";
            return entity;
        }

        Customer? obj = context.Customers.FirstOrDefault(e => e.Id == entity.Id);

        if (obj is null)
        {
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";
            return entity;
        }

        obj.OpeningBalance_val = entity.OpeningBalance_val;
        obj.ActionComment = entity.ActionComment;
        obj.UpdatedByUsrId = _tenantService.GetUserId();
        obj.UpdateDate = DateTime.UtcNow;
        await context.SaveChangesAsync();

        await Utilities.DBLog.Action(obj.Id, "Customer", "OpeningBalance", obj.ActionComment ?? string.Empty, context, _tenantService);

        obj.Msg = "OK";
        return obj;
    }




// Code-Player-Start-DTO //
    public static async Task<Customer> Create(Customer entity, ApplicationDbContext context, ITenantProviderService _tenantService)
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
        context.Customers.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public static async Task<Customer> Update(Customer entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        Customer? obj = context.Customers.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Populate the Object Will be Saved
        obj.PriceListId = entity.PriceListId is null ? obj.PriceListId : entity.PriceListId;
        obj.calc_PipelineAmount_val = entity.calc_PipelineAmount_val is null ? obj.calc_PipelineAmount_val : entity.calc_PipelineAmount_val;
        obj.calc_TotalPrice_val = entity.calc_TotalPrice_val is null ? obj.calc_TotalPrice_val : entity.calc_TotalPrice_val;
        obj.calc_TotalCost_val = entity.calc_TotalCost_val is null ? obj.calc_TotalCost_val : entity.calc_TotalCost_val;
        obj.calc_Discount_val = entity.calc_Discount_val is null ? obj.calc_Discount_val : entity.calc_Discount_val;
        obj.calc_ManagementBuffer_val = entity.calc_ManagementBuffer_val is null ? obj.calc_ManagementBuffer_val : entity.calc_ManagementBuffer_val;
        obj.calc_Discount_prc = entity.calc_Discount_prc is null ? obj.calc_Discount_prc : entity.calc_Discount_prc;
        obj.calc_PlannedGP_prc = entity.calc_PlannedGP_prc is null ? obj.calc_PlannedGP_prc : entity.calc_PlannedGP_prc;
        obj.calc_ActualGP_prc = entity.calc_ActualGP_prc is null ? obj.calc_ActualGP_prc : entity.calc_ActualGP_prc;
        obj.calc_ManagementBuffer_prc = entity.calc_ManagementBuffer_prc is null ? obj.calc_ManagementBuffer_prc : entity.calc_ManagementBuffer_prc;
        obj.calc_Amount_val = entity.calc_Amount_val is null ? obj.calc_Amount_val : entity.calc_Amount_val;
        obj.Amount_rat = entity.Amount_rat is null ? obj.Amount_rat : entity.Amount_rat;
        obj.calc_Amount_tax = entity.calc_Amount_tax is null ? obj.calc_Amount_tax : entity.calc_Amount_tax;
        obj.calc_Amount_tot = entity.calc_Amount_tot is null ? obj.calc_Amount_tot : entity.calc_Amount_tot;
        obj.calc_Budget_val = entity.calc_Budget_val is null ? obj.calc_Budget_val : entity.calc_Budget_val;
        obj.calc_PlannedGP_val = entity.calc_PlannedGP_val is null ? obj.calc_PlannedGP_val : entity.calc_PlannedGP_val;
        obj.calc_ActualCost_val = entity.calc_ActualCost_val is null ? obj.calc_ActualCost_val : entity.calc_ActualCost_val;
        obj.calc_ActualGP_val = entity.calc_ActualGP_val is null ? obj.calc_ActualGP_val : entity.calc_ActualGP_val;
        obj.CostToComplete_val = entity.CostToComplete_val is null ? obj.CostToComplete_val : entity.CostToComplete_val;
        obj.calc_RecgnzdRevn_val = entity.calc_RecgnzdRevn_val is null ? obj.calc_RecgnzdRevn_val : entity.calc_RecgnzdRevn_val;
        obj.calc_UnRecgnzdRevn_val = entity.calc_UnRecgnzdRevn_val is null ? obj.calc_UnRecgnzdRevn_val : entity.calc_UnRecgnzdRevn_val;
        obj.calc_Billed_val = entity.calc_Billed_val is null ? obj.calc_Billed_val : entity.calc_Billed_val;
        obj.calc_UnBilled_val = entity.calc_UnBilled_val is null ? obj.calc_UnBilled_val : entity.calc_UnBilled_val;
        obj.calc_DueInv_val = entity.calc_DueInv_val is null ? obj.calc_DueInv_val : entity.calc_DueInv_val;
        obj.calc_Collected_val = entity.calc_Collected_val is null ? obj.calc_Collected_val : entity.calc_Collected_val;
        obj.calc_UnCollected_val = entity.calc_UnCollected_val is null ? obj.calc_UnCollected_val : entity.calc_UnCollected_val;
        obj.calc_Planned_val = entity.calc_Planned_val is null ? obj.calc_Planned_val : entity.calc_Planned_val;
        obj.calc_UnPlanned_val = entity.calc_UnPlanned_val is null ? obj.calc_UnPlanned_val : entity.calc_UnPlanned_val;
        obj.AccountManagerUsrId = entity.AccountManagerUsrId is null ? obj.AccountManagerUsrId : entity.AccountManagerUsrId;
        obj.CurrencyId = entity.CurrencyId is null ? obj.CurrencyId : entity.CurrencyId;
        obj.Domain = entity.Domain is null ? obj.Domain : entity.Domain;
        obj.Website = entity.Website is null ? obj.Website : entity.Website;
        obj.CRNo = entity.CRNo is null ? obj.CRNo : entity.CRNo;
        obj.VATRegNo = entity.VATRegNo is null ? obj.VATRegNo : entity.VATRegNo;
        obj.IndustryId = entity.IndustryId is null ? obj.IndustryId : entity.IndustryId;
        obj.SectorId = entity.SectorId is null ? obj.SectorId : entity.SectorId;
        obj.Employees_num = entity.Employees_num is null ? obj.Employees_num : entity.Employees_num;
        obj.AnnualRevenue_val = entity.AnnualRevenue_val is null ? obj.AnnualRevenue_val : entity.AnnualRevenue_val;
        obj.CountryId = entity.CountryId is null ? obj.CountryId : entity.CountryId;
        obj.City = entity.City is null ? obj.City : entity.City;
        obj.State = entity.State is null ? obj.State : entity.State;
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
