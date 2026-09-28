using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;

namespace Aspire.Services;

public class InvoicePaymentARService()
{


// Code-Player-Start-DTO //
    public static async Task<InvoicePaymentAR> Create(InvoicePaymentAR entity, ApplicationDbContext context, ITenantProviderService _tenantService)
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
        context.InvoicePaymentARs.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public static async Task<InvoicePaymentAR> Update(InvoicePaymentAR entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        InvoicePaymentAR? obj = context.InvoicePaymentARs.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Populate the Object Will be Saved
        obj.PaymentARId = entity.PaymentARId is null ? obj.PaymentARId : entity.PaymentARId;
        obj.InvoiceARId = entity.InvoiceARId is null ? obj.InvoiceARId : entity.InvoiceARId;
        obj.LineItemId = entity.LineItemId is null ? obj.LineItemId : entity.LineItemId;
        obj.Amount_ar = entity.Amount_ar is null ? obj.Amount_ar : entity.Amount_ar;
        obj.Amount_cur = entity.Amount_cur is null ? obj.Amount_cur : entity.Amount_cur;
        obj.Amount_rat = entity.Amount_rat is null ? obj.Amount_rat : entity.Amount_rat;
        obj.Amount_tax = entity.Amount_tax is null ? obj.Amount_tax : entity.Amount_tax;
        obj.Amount_tot = entity.Amount_tot is null ? obj.Amount_tot : entity.Amount_tot;
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