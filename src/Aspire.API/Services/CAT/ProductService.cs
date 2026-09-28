using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;

namespace Aspire.Services;

public class ProductService()
{


// Code-Player-Start-DTO //
    public static async Task<Product> Create(Product entity, ApplicationDbContext context, ITenantProviderService _tenantService)
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
        context.Products.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public static async Task<Product> Update(Product entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        Product? obj = context.Products.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Populate the Object Will be Saved
        obj.SKUNo = entity.SKUNo is null ? obj.SKUNo : entity.SKUNo;
        obj.PN = entity.PN is null ? obj.PN : entity.PN;
        obj.UOMId = entity.UOMId is null ? obj.UOMId : entity.UOMId;
        obj.DescriptionSales = entity.DescriptionSales is null ? obj.DescriptionSales : entity.DescriptionSales;
        obj.DescriptionPurchase = entity.DescriptionPurchase is null ? obj.DescriptionPurchase : entity.DescriptionPurchase;
        obj.WarningSales = entity.WarningSales is null ? obj.WarningSales : entity.WarningSales;
        obj.WarningPurchase = entity.WarningPurchase is null ? obj.WarningPurchase : entity.WarningPurchase;
        obj.DeliveryProofId = entity.DeliveryProofId is null ? obj.DeliveryProofId : entity.DeliveryProofId;
        obj.ProductCategoryId = entity.ProductCategoryId is null ? obj.ProductCategoryId : entity.ProductCategoryId;
        obj.MarketUnitId = entity.MarketUnitId is null ? obj.MarketUnitId : entity.MarketUnitId;
        obj.TradingTypeId = entity.TradingTypeId is null ? obj.TradingTypeId : entity.TradingTypeId;
        obj.BillingFrequencyId = entity.BillingFrequencyId is null ? obj.BillingFrequencyId : entity.BillingFrequencyId;
        obj.TaxRateId = entity.TaxRateId is null ? obj.TaxRateId : entity.TaxRateId;
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