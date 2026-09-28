using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;

namespace Aspire.Services;

public class DealService()
{
    public static async Task<Deal> MovetoNextStage(Deal entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";

        Deal? obj = context.Deals.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
        {
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";
            return entity;
        }

        if (obj.CloseDate is not null)
        {
            entity.Msg = "This Deal is already closed (Won or Lost) and cannot move stages";
            return entity;
        }

        if (string.IsNullOrWhiteSpace(entity.ActionComment))
        {
            entity.Msg = "A comment is required to move to the next stage";
            return entity;
        }

        Stage? currentStage = context.Stages.Where(s => s.Id == obj.StageId).FirstOrDefault();

        if (currentStage is null)
        {
            entity.Msg = "The Deal's current stage (" + obj.StageId + ") is not exist in the database";
            return entity;
        }

        Stage? nextStage = context.Stages
            .Where(s => s.PipelineId == obj.PipelineId && s.Pipeline_prc > currentStage.Pipeline_prc)
            .OrderBy(s => s.Pipeline_prc)
            .FirstOrDefault();

        if (nextStage is null)
        {
            entity.Msg = "This Deal is already on the final stage of its pipeline";
            return entity;
        }

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database
        obj.StageId = nextStage.Id;
        obj.calc_PipelineAmount_val = obj.calc_Amount_val * (nextStage.Pipeline_prc / 100);
        obj.ActionComment = entity.ActionComment;
        obj.Msg = "OK";
        obj.UpdatedByUsrId = _tenantService.GetUserId();
        obj.UpdateDate = DateTime.UtcNow;
        await context.SaveChangesAsync();

        // Log The Action
        await Utilities.DBLog.Action(obj.Id, "Deal", "MovetoNextStage", obj.ActionComment, context, _tenantService);

        return obj;
    }


    public static async Task<(Deal deal, Pnl? pnl)> CreatePnl(Deal entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";

        Deal? obj = context.Deals.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
        {
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";
            return (entity, null);
        }

        if (string.IsNullOrWhiteSpace(entity.ActionComment))
        {
            entity.Msg = "A comment is required to create a Pnl from this Deal";
            return (entity, null);
        }

        // Perform the Business Logic BEFOR UPDATING the Deal Object in the Database
        obj.ActionComment = entity.ActionComment;
        obj.Msg = "OK";
        obj.UpdatedByUsrId = _tenantService.GetUserId();
        obj.UpdateDate = DateTime.UtcNow;

        Pnl pnl = new Pnl
        {
            TenantId = obj.TenantId,
            CreatedByUsrId = _tenantService.GetUserId(),
            CreateDate = DateTime.UtcNow,
            Name = obj.Name,
            NameAr = obj.NameAr,
            CustomerId = obj.CustomerId,
            AccountManagerUsrId = obj.AccountManagerUsrId,
            CurrencyId = obj.CurrencyId,
            DealId = obj.Id,
            IssueDate = DateTime.UtcNow,
            // Description is the field the Details tab actually renders as the Pnl's notes;
            // ActionComment is kept too, for parity with the action log, but isn't shown anywhere.
            Description = entity.ActionComment,
            ActionComment = entity.ActionComment,
            Msg = "OK"
        };
        context.Pnls.Add(pnl);
        await context.SaveChangesAsync();

        List<LineItem> dealLineItems = context.LineItems.Where(li => li.EntityName == "Deal" && li.ObjectId == obj.Id).ToList();

        foreach (LineItem sourceLI in dealLineItems)
            context.LineItems.Add(CloneLineItem(sourceLI, "Pnl", pnl.Id));

        if (dealLineItems.Count > 0)
            await context.SaveChangesAsync();

        // Log The Action
        await Utilities.DBLog.Action(obj.Id, "Deal", "CreatePnl", obj.ActionComment, context, _tenantService);

        return (obj, pnl);
    }


    public static async Task<Deal> Won(Deal entity, string option, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";

        Deal? obj = context.Deals.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
        {
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";
            return entity;
        }

        if (obj.CloseDate is not null)
        {
            entity.Msg = "This Deal is already closed (Won or Lost)";
            return entity;
        }

        if (string.IsNullOrWhiteSpace(entity.ActionComment))
        {
            entity.Msg = "A comment is required to mark this Deal as Won";
            return entity;
        }

        if (option != "CustomerPO" && option != "SalesOrder" && option != "Both")
        {
            entity.Msg = "Option must be one of CustomerPO, SalesOrder or Both";
            return entity;
        }

        bool createCustomerPO = option == "CustomerPO" || option == "Both";
        bool createSalesOrder = option == "SalesOrder" || option == "Both";

        CustomerPO? customerPO = null;
        SalesOrder? salesOrder = null;

        // Explicit transaction: winning the deal and creating its order document(s) are one atomic
        // action (FR-010) — a failure anywhere in here must leave the deal unwon and nothing created.
        await using var transaction = await context.Database.BeginTransactionAsync();

        // Perform the Business Logic BEFOR UPDATING the Deal Object in the Database
        obj.AwardDate = DateTime.UtcNow;
        obj.CloseDate = DateTime.UtcNow;
        obj.ActionComment = entity.ActionComment;
        obj.Msg = "OK";
        obj.UpdatedByUsrId = _tenantService.GetUserId();
        obj.UpdateDate = DateTime.UtcNow;

        if (createCustomerPO)
        {
            customerPO = new CustomerPO
            {
                TenantId = _tenantService.GetTenantId(),
                CreatedByUsrId = _tenantService.GetUserId(),
                CreateDate = DateTime.UtcNow,
                Name = obj.Name,
                NameAr = obj.NameAr,
                CustomerId = obj.CustomerId,
                AccountManagerUsrId = obj.AccountManagerUsrId,
                DealId = obj.Id,
                ActionComment = entity.ActionComment,
                Msg = "OK"
            };
            context.CustomerPOs.Add(customerPO);
        }

        if (createSalesOrder)
        {
            salesOrder = new SalesOrder
            {
                TenantId = _tenantService.GetTenantId(),
                CreatedByUsrId = _tenantService.GetUserId(),
                CreateDate = DateTime.UtcNow,
                Name = obj.Name,
                NameAr = obj.NameAr,
                CustomerId = obj.CustomerId,
                AccountManagerUsrId = obj.AccountManagerUsrId,
                DealId = obj.Id,
                ActionComment = entity.ActionComment,
                Msg = "OK"
            };
            context.SalesOrders.Add(salesOrder);
        }

        // Save the Deal + header row(s) first so generated Ids are available for cross-linking and line-item ObjectId values.
        await context.SaveChangesAsync();

        if (option == "Both")
        {
            customerPO.SalesOrderId = salesOrder.Id;
            salesOrder.CustomerPOId = customerPO.Id;
            await context.SaveChangesAsync();
        }

        List<LineItem> dealLineItems = context.LineItems.Where(li => li.EntityName == "Deal" && li.ObjectId == obj.Id).ToList();

        foreach (LineItem sourceLI in dealLineItems)
        {
            if (customerPO is not null)
                context.LineItems.Add(CloneLineItem(sourceLI, "CustomerPO", customerPO.Id));

            if (salesOrder is not null)
                context.LineItems.Add(CloneLineItem(sourceLI, "SalesOrder", salesOrder.Id));
        }

        await context.SaveChangesAsync();
        await transaction.CommitAsync();

        // Log The Action
        await Utilities.DBLog.Action(obj.Id, "Deal", "Won", obj.ActionComment, context, _tenantService);

        return obj;
    }


    public static async Task<Deal> Lost(Deal entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";

        Deal? obj = context.Deals.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
        {
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";
            return entity;
        }

        if (obj.CloseDate is not null)
        {
            entity.Msg = "This Deal is already closed (Won or Lost)";
            return entity;
        }

        if (string.IsNullOrWhiteSpace(entity.ActionComment))
        {
            entity.Msg = "A comment is required to mark this Deal as Lost";
            return entity;
        }

        if (entity.DealLostReasonId is null)
        {
            entity.Msg = "A lost reason is required to mark this Deal as Lost";
            return entity;
        }

        if (entity.CompetitorId is null)
        {
            entity.Msg = "A competitor is required to mark this Deal as Lost";
            return entity;
        }

        if (entity.CompetitorWonAmount_val == 0)
        {
            entity.Msg = "A won amount is required to mark this Deal as Lost";
            return entity;
        }

        if (entity.CompetitorWonAmount_val < 0 || entity.CompetitorWonAmount_val > 999999999)
        {
            entity.Msg = "The won amount cannot exceed 999,999,999";
            return entity;
        }

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database
        EntityStatus? lostStatus = context.EntityStatuses
            .Where(s => s.EntityName == "Deal" && s.Name == "Lost")
            .FirstOrDefault();

        if (lostStatus is null)
        {
            lostStatus = new EntityStatus(obj.TenantId, "Deal", "Lost", "عربي", "danger");
            context.EntityStatuses.Add(lostStatus);
            await context.SaveChangesAsync();
        }

        obj.DealLostReasonId = entity.DealLostReasonId;
        obj.CompetitorId = entity.CompetitorId;
        obj.CompetitorWonAmount_val = entity.CompetitorWonAmount_val;
        obj.LostNotes = entity.ActionComment;
        obj.ActionComment = entity.ActionComment;
        obj.CloseDate = DateTime.UtcNow;
        obj.EntityStatusId = lostStatus.Id;
        obj.Msg = "OK";
        obj.UpdatedByUsrId = _tenantService.GetUserId();
        obj.UpdateDate = DateTime.UtcNow;
        await context.SaveChangesAsync();

        // Log The Action
        await Utilities.DBLog.Action(obj.Id, "Deal", "Lost", obj.ActionComment, context, _tenantService);

        return obj;
    }


    internal static LineItem CloneLineItem(LineItem source, string entityName, long? objectId)
    {
        return new LineItem
        {
            TenantId = source.TenantId,
            CreateDate = DateTime.UtcNow,
            Name = source.Name,
            NameAr = source.NameAr,
            EntityName = entityName,
            ObjectId = objectId,
            ProductId = source.ProductId,
            ProductCategoryId = source.ProductCategoryId,
            MarketUnitId = source.MarketUnitId,
            Quantity_dec = source.Quantity_dec,
            UnitPrice_val = source.UnitPrice_val,
            calc_UnitCost_val = source.calc_UnitCost_val,
            calc_TotalCost_val = source.calc_TotalCost_val,
            calc_TotalPrice_val = source.calc_TotalPrice_val,
            DiscountTypeId = source.DiscountTypeId,
            Discount_val = source.Discount_val,
            ManagementBuffer_val = source.ManagementBuffer_val,
            calc_Amount_val = source.calc_Amount_val,
            CurrencyId = source.CurrencyId,
            TaxRateId = source.TaxRateId,
            calc_Tax_val = source.calc_Tax_val,
            calc_Total_val = source.calc_Total_val,
            calc_Budget_val = source.calc_Budget_val,
            calc_PlannedGP_val = source.calc_PlannedGP_val
        };
    }


    // Code-Player-Start-DTO //
    public static async Task<Deal> Create(Deal entity, ApplicationDbContext context, ITenantProviderService _tenantService)
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
        context.Deals.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public static async Task<Deal> Update(Deal entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        Deal? obj = context.Deals.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Populate the Object Will be Saved
        obj.AccountManagerUsrId = entity.AccountManagerUsrId is null ? obj.AccountManagerUsrId : entity.AccountManagerUsrId;
        obj.IssueDate = entity.IssueDate is null ? obj.IssueDate : entity.IssueDate;
        obj.StartDate = entity.StartDate is null ? obj.StartDate : entity.StartDate;
        obj.EndDate = entity.EndDate is null ? obj.EndDate : entity.EndDate;
        obj.CustomerId = entity.CustomerId is null ? obj.CustomerId : entity.CustomerId;
        obj.CurrencyId = entity.CurrencyId is null ? obj.CurrencyId : entity.CurrencyId;
        obj.PipelineId = entity.PipelineId is null ? obj.PipelineId : entity.PipelineId;
        obj.StageId = entity.StageId is null ? obj.StageId : entity.StageId;
        obj.ConfidenceId = entity.ConfidenceId is null ? obj.ConfidenceId : entity.ConfidenceId;
        obj.IsEtimad = entity.IsEtimad is null ? obj.IsEtimad : entity.IsEtimad;
        obj.EtimadNo = entity.EtimadNo is null ? obj.EtimadNo : entity.EtimadNo;
        obj.BidRefNo = entity.BidRefNo is null ? obj.BidRefNo : entity.BidRefNo;
        obj.AwardLetterRefNo = entity.AwardLetterRefNo is null ? obj.AwardLetterRefNo : entity.AwardLetterRefNo;
        obj.ContractRefNo = entity.ContractRefNo is null ? obj.ContractRefNo : entity.ContractRefNo;
        obj.PORefNo = entity.PORefNo is null ? obj.PORefNo : entity.PORefNo;
        obj.IsLG = entity.IsLG is null ? obj.IsLG : entity.IsLG;
        obj.LGRefNo = entity.LGRefNo is null ? obj.LGRefNo : entity.LGRefNo;
        obj.LGValue_dec = entity.LGValue_dec is null ? obj.LGValue_dec : entity.LGValue_dec;
        obj.LGExpiryDate = entity.LGExpiryDate is null ? obj.LGExpiryDate : entity.LGExpiryDate;
        obj.PaymentDays_num = entity.PaymentDays_num is null ? obj.PaymentDays_num : entity.PaymentDays_num;
        obj.TechnicalNotes = entity.TechnicalNotes is null ? obj.TechnicalNotes : entity.TechnicalNotes;
        obj.FinantialNotes = entity.FinantialNotes is null ? obj.FinantialNotes : entity.FinantialNotes;
        obj.AccountNotes = entity.AccountNotes is null ? obj.AccountNotes : entity.AccountNotes;
        obj.LostNotes = entity.LostNotes is null ? obj.LostNotes : entity.LostNotes;
        obj.CloseDate = entity.CloseDate is null ? obj.CloseDate : entity.CloseDate;
        obj.AwardDate = entity.AwardDate is null ? obj.AwardDate : entity.AwardDate;
        obj.CompetitorId = entity.CompetitorId is null ? obj.CompetitorId : entity.CompetitorId;
        obj.DealLostReasonId = entity.DealLostReasonId is null ? obj.DealLostReasonId : entity.DealLostReasonId;
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