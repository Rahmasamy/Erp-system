using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;

namespace Aspire.Services;

public class PnlService()
{
    public static async Task<(Pnl pnl, Quote? quote)> Send_For_Approval(Pnl entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";

        Pnl? obj = context.Pnls.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
        {
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";
            return (entity, null);
        }

        if (string.IsNullOrWhiteSpace(entity.ActionComment))
        {
            entity.Msg = "A comment is required to send this Pnl for approval";
            return (entity, null);
        }

        // Perform the Business Logic BEFOR UPDATING the Pnl Object in the Database
        obj.ActionComment = entity.ActionComment;
        obj.Msg = "OK";
        obj.UpdatedByUsrId = _tenantService.GetUserId();
        obj.UpdateDate = DateTime.UtcNow;

        Quote quote = new Quote
        {
            TenantId = obj.TenantId,
            CreatedByUsrId = _tenantService.GetUserId(),
            CreateDate = DateTime.UtcNow,
            Name = obj.Name,
            NameAr = obj.NameAr,
            CustomerId = obj.CustomerId,
            AccountManagerUsrId = obj.AccountManagerUsrId,
            CurrencyId = obj.CurrencyId,
            DealId = obj.DealId,
            PnlId = obj.Id,
            IssueDate = DateTime.UtcNow,
            ActionComment = entity.ActionComment,
            Msg = "OK"
        };
        context.Quotes.Add(quote);
        await context.SaveChangesAsync();

        List<LineItem> pnlLineItems = context.LineItems.Where(li => li.EntityName == "Pnl" && li.ObjectId == obj.Id).ToList();

        foreach (LineItem sourceLI in pnlLineItems)
            context.LineItems.Add(DealService.CloneLineItem(sourceLI, "Quote", quote.Id));

        if (pnlLineItems.Count > 0)
            await context.SaveChangesAsync();

        // Log The Action
        await Utilities.DBLog.Action(obj.Id, "Pnl", "Send_For_Approval", obj.ActionComment, context, _tenantService);

        return (obj, quote);
    }




// Code-Player-Start-DTO //
    public static async Task<Pnl> Create(Pnl entity, ApplicationDbContext context, ITenantProviderService _tenantService)
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
        context.Pnls.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public static async Task<Pnl> Update(Pnl entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        Pnl? obj = context.Pnls.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Populate the Object Will be Saved
        obj.AccountManagerUsrId = entity.AccountManagerUsrId is null ? obj.AccountManagerUsrId : entity.AccountManagerUsrId;
        obj.IssueDate = entity.IssueDate is null ? obj.IssueDate : entity.IssueDate;
        obj.CurrencyId = entity.CurrencyId is null ? obj.CurrencyId : entity.CurrencyId;
        obj.CustomerId = entity.CustomerId is null ? obj.CustomerId : entity.CustomerId;
        obj.DealId = entity.DealId is null ? obj.DealId : entity.DealId;
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