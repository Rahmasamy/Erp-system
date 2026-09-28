using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;

namespace Aspire.Services;

public class InvoiceAPService()
{
    public static async Task<InvoiceAP> Billed(InvoiceAP entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        InvoiceAP? obj = context.InvoiceAPs.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Log The Action
        await Utilities.DBLog.Action(obj.Id, "{Entity Name}", "{Action Name}", obj.ActionComment, context, _tenantService);

        return entity;
    }


    public static async Task<InvoiceAP> RevokeBilling(InvoiceAP entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        InvoiceAP? obj = context.InvoiceAPs.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Log The Action
        await Utilities.DBLog.Action(obj.Id, "{Entity Name}", "{Action Name}", obj.ActionComment, context, _tenantService);

        return entity;
    }


    public static async Task<InvoiceAP> Submitted(InvoiceAP entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        InvoiceAP? obj = context.InvoiceAPs.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Log The Action
        await Utilities.DBLog.Action(obj.Id, "{Entity Name}", "{Action Name}", obj.ActionComment, context, _tenantService);

        return entity;
    }


    public static async Task<InvoiceAP> RevokeSubmission(InvoiceAP entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        InvoiceAP? obj = context.InvoiceAPs.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Log The Action
        await Utilities.DBLog.Action(obj.Id, "{Entity Name}", "{Action Name}", obj.ActionComment, context, _tenantService);

        return entity;
    }


    public static async Task<InvoiceAP> Collected(InvoiceAP entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        InvoiceAP? obj = context.InvoiceAPs.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Log The Action
        await Utilities.DBLog.Action(obj.Id, "{Entity Name}", "{Action Name}", obj.ActionComment, context, _tenantService);

        return entity;
    }


    public static async Task<InvoiceAP> RevokeCollection(InvoiceAP entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        InvoiceAP? obj = context.InvoiceAPs.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Log The Action
        await Utilities.DBLog.Action(obj.Id, "{Entity Name}", "{Action Name}", obj.ActionComment, context, _tenantService);

        return entity;
    }




// Code-Player-Start-DTO //
    public static async Task<InvoiceAP> Create(InvoiceAP entity, ApplicationDbContext context, ITenantProviderService _tenantService)
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
        context.InvoiceAPs.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public static async Task<InvoiceAP> Update(InvoiceAP entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        InvoiceAP? obj = context.InvoiceAPs.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Populate the Object Will be Saved
        obj.AccountManagerUsrId = entity.AccountManagerUsrId is null ? obj.AccountManagerUsrId : entity.AccountManagerUsrId;
        obj.IssueDate = entity.IssueDate is null ? obj.IssueDate : entity.IssueDate;
        obj.SupplierId = entity.SupplierId is null ? obj.SupplierId : entity.SupplierId;
        obj.ProjectId = entity.ProjectId is null ? obj.ProjectId : entity.ProjectId;
        obj.PurchaseOrderId = entity.PurchaseOrderId is null ? obj.PurchaseOrderId : entity.PurchaseOrderId;
        obj.IncotermId = entity.IncotermId is null ? obj.IncotermId : entity.IncotermId;
        obj.BankAccountId = entity.BankAccountId is null ? obj.BankAccountId : entity.BankAccountId;
        obj.PaymentTermId = entity.PaymentTermId is null ? obj.PaymentTermId : entity.PaymentTermId;
        obj.DueDate = entity.DueDate is null ? obj.DueDate : entity.DueDate;
        obj.SubmissionDate = entity.SubmissionDate is null ? obj.SubmissionDate : entity.SubmissionDate;
        obj.PaymentDate = entity.PaymentDate is null ? obj.PaymentDate : entity.PaymentDate;
        obj.Amount_cur = entity.Amount_cur is null ? obj.Amount_cur : entity.Amount_cur;
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