using Amazon.S3.Model;
using Aspire.Context;
using Aspire.Data;
using Aspire.Enums;
using Aspire.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Aspire.Services;

public class DocumentWorkItemService()
{

//    const stages = computed(() => {
//    const rows: any[][] = [];
//    let currentStage: any[] | null = null;
//    let currentStepNo: number | null = null;

//    for (const step of steps.value) {
//    if (step.stepNo !== currentStepNo) {
//      currentStage = [step];
//      rows.push(currentStage);
//      currentStepNo = step.stepNo;
//    } else {
//      currentStage!.push(step);
//    }
//  }

//  return rows;
//});



// Document workflow: 5 stages (Document Controller, Area Manager, Discipline, Project Manager, Document Controller).
// IsOpen marks the CURRENT stage: exactly one work item per document is open until the workflow completes,
// EXCEPT the discipline stage, where every discipline work item (DisciplineId set) is open at the same time:
// each discipline's engineer changes the status of their own item, and only the LAST discipline (highest StepNo)
// can move the workflow to the next stage, once every discipline has a completion status.
public const string StatusPending = "Pending";
    public const string StatusInProgress = "In-Progress";
    public const string StatusInActive = "In-Active";
    public const int SlaDays = 1;

    // A stage can move forward once its assignee has made a decision (Approved, Rejected, Returned, ...) -
    // only these two "not yet decided" statuses block Submit & Next.
    public static readonly string[] UndecidedStatuses = [StatusPending, StatusInProgress];

    // Serialises every workflow write on ONE document: the Document row is locked (held until the transaction ends),
    // so duplicate / concurrent requests queue up and re-read the committed state. A single-row lock cannot deadlock,
    // unlike locking several work item rows in whatever order each request happens to scan them.
    private static async Task LockDocument(long? documentId, ApplicationDbContext context)
    {
        if (context.Database.IsNpgsql())
            await context.Database.ExecuteSqlInterpolatedAsync($"SELECT 1 FROM \"Documents\" WHERE \"Id\" = {documentId} FOR UPDATE");
    }

    // Server-side authorization of every workflow write: the work item must be the current stage AND assigned to the caller
    private static string? CheckCurrentAssignee(DocumentWorkItem item, long? userId)
    {
        if (item.IsOpen != true)
            return "This work item is not the current stage of the workflow and is read-only";
        if (userId is null || item.ActionUsrId != userId)
            return "Only the user assigned to the current work item can do this";
        return null;
    }

    private static bool IsAreaManager(DocumentWorkItem item) =>
        item.DisciplineId is null && (item.ActionRole == "Area Manager" || item.Name == "Area Manager");

    private static bool IsDocumentController(DocumentWorkItem item) =>
        item.DisciplineId is null && (item.ActionRole == "Document Controller" || item.Name == "Document Controller");

// Code-Player-Start-DTO //
    public static async Task<DocumentWorkItem> Create(DocumentWorkItem entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        // The five work items are created together with the Document (DocumentService.Create), never one by one
        entity.Msg = "Work items are created automatically with the document and cannot be created manually";
        return await Task.FromResult(entity);
    }

    // Changes the STATUS (and comment) of the current work item. It never activates another stage and never
    // changes the parent Document's status - that only happens in MovetoNextStage, once the outcome (continue /
    // return to previous stage / return to contractor) has actually been decided and applied.
    public static async Task<DocumentWorkItem> Update(DocumentWorkItem entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        //await DocumentService.EnsureDocumentSchemaAsync(context);

        entity.Msg = "OK";
        long? tenantId = _tenantService.GetTenantId();

        var head = await context.DocumentWorkItems.AsNoTracking()
            .Where(e => e.Id == entity.Id && e.TenantId == tenantId)
            .Select(e => new { e.DocumentId })
            .FirstOrDefaultAsync();

        if (head is null || head.DocumentId is null)
        {
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";
            return entity;
        }

        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            await LockDocument(head.DocumentId.Value, context);

            // Re-read AFTER the lock so a concurrent Move to Next Stage is seen
            DocumentWorkItem obj = await context.DocumentWorkItems.FirstAsync(e => e.Id == entity.Id);

            string? denied = CheckCurrentAssignee(obj, _tenantService.GetUserId());
            if (denied is not null)
            {
                obj.Msg = denied;
                return obj;
            }

            // Only the status and the comment can change here; assignee, stage, dates and open state are workflow-controlled
            if (entity.EntityStatusId is not null && entity.EntityStatusId != obj.EntityStatusId)
            {
                EntityStatus? status = await context.EntityStatuses
                    .FirstOrDefaultAsync(s => s.Id == entity.EntityStatusId && s.EntityName == "DocumentWorkItem" && s.TenantId == tenantId);

                if (status is null)
                {
                    obj.Msg = "This status is not a valid work item status";
                    return obj;
                }
                if (status.Name == StatusInActive)
                {
                    obj.Msg = "A work item in progress cannot be set back to " + StatusInActive;
                    return obj;
                }

                obj.EntityStatusId = status.Id;
            }
            if (entity.ActionComment is not null)
            {
                obj.ActionComment = entity.ActionComment;
            }

            // Sync the Document's own "Project Manager Comments" (shown on the Document banner/details)
            // whenever the PM stage's comment changes, or - if some other stage is being updated - keep
            // whatever the PM already said. This is display sync only; it never touches Document status.
            if (obj.ActionRole == "Project Manager" || obj.Name == "Project Manager")
            {
                if (!string.IsNullOrWhiteSpace(entity.ActionComment))
                {
                    Document? doc = await context.Documents.FirstOrDefaultAsync(d => d.Id == head.DocumentId.Value);
                    if (doc != null)
                        doc.ActionComment = entity.ActionComment;
                }
            }

            obj.Msg = "OK";
            obj.UpdatedByUsrId = _tenantService.GetUserId();
            obj.UpdateDate = DateTime.UtcNow;
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return obj;
        }
        catch (DbUpdateException)
        {
            await transaction.RollbackAsync();
            entity.Msg = "The workflow was changed by another request, please reload and try again";
            return entity;
        }
    }

    public static async Task<DocumentWorkItem> MovetoNextStage(DocumentWorkItem entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";

        var head = await context.DocumentWorkItems.AsNoTracking()
            .Where(e => e.Id == entity.Id).Select(e => new { e.DocumentId }).FirstOrDefaultAsync();

        await using var transaction = await context.Database.BeginTransactionAsync();
        long docId = head.DocumentId ?? 0;

        try
        {
            
            await LockDocument(docId, context);

            List<DocumentWorkItem> items = await context.DocumentWorkItems
                .Where(e => e.DocumentId == docId).OrderBy(e => e.StepNo).ToListAsync();

            DocumentWorkItem current = items.First(e => e.Id == entity.Id);

            string? denied = CheckCurrentAssignee(current, _tenantService.GetUserId());
            if (denied is not null)
            {
                entity.Msg = denied;
                return entity;
            }

            EntityStatus? currentStatus = await context.EntityStatuses.FirstOrDefaultAsync(s => s.Id == current.EntityStatusId);
            int finalStepNo = items.Max(e => e.StepNo ?? 0);

            // **********************************************************
            EntityStatus? pending = await context.EntityStatuses.FirstOrDefaultAsync(s => s.EntityName == "DocumentWorkItem" && s.Name == StatusPending);

            ActionRoutingEnum route = (ActionRoutingEnum)int.Parse(current.Actions.Substring(current.Actions.IndexOf(entity.Action) + entity.Action.Length + 1, 1));
            current.IsOpen = false;
            current.Action = entity.Action;
            current.ActionComment = entity.ActionComment;
            current.CompleteDate = DateTime.UtcNow;
            current.IsSLABreach = current.CompleteDate > current.DueDate;
            current.SLABreach_dec = DateAndTime.DateDiff(DateInterval.Hour, current.CompleteDate ?? DateTime.Now, current.DueDate ?? DateTime.Now, FirstDayOfWeek.Sunday);

            var parallelItems = items.GroupBy(x => x.StepNo).Where(g => g.Count() > 1).SelectMany(g => g).ToList();
            bool Move = true;
            if (parallelItems.Contains(current))
            {
                List<DocumentWorkItem> currentParallelItems = items.Where(x => x.StepNo == current.StepNo).ToList();
                foreach (var item in currentParallelItems)
                    if (item.IsOpen)
                    {
                        Move = false;
                        break;
                    }
            }

            int goToStepNo = -1;
            if(Move)
            {
                if (route == ActionRoutingEnum.ContinueToNextStep)
                    goToStepNo = (current.StepNo ?? 0) + 1;
                else if (route == ActionRoutingEnum.BackToPreviousStep)
                    goToStepNo = (current.StepNo ?? 0) - 1;
                else if (route == ActionRoutingEnum.GoToStep)
                    goToStepNo = int.Parse(current.Action.Split(':')[1].Split('-')[0]);
                else if (route == ActionRoutingEnum.ReturnToContractor)
                    goToStepNo = 1;

                List<DocumentWorkItem>? goToItems = items.Where(e => e.StepNo == goToStepNo).ToList();
                foreach (DocumentWorkItem itm in goToItems)
                {
                    itm.IsOpen = true;
                    itm.EntityStatusId = pending.Id;
                    itm.StartDate = DateTime.UtcNow;
                    itm.DueDate = DateTime.UtcNow.AddHours(double.Parse(itm.SLA_dec.ToString()));
                }

                if (current.StepNo == finalStepNo)
                {
                    Document? doc = await context.Documents.FirstOrDefaultAsync(d => d.Id == docId);
                }
            }

            // ************************************************************


            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            await Utilities.DBLog.Action(docId, "Document", entity.Action, entity.ActionComment, context, _tenantService);
            return entity;
        }
        catch (DbUpdateException)
        {
            await transaction.RollbackAsync();
            entity.Msg = "The workflow was changed by another request, please reload and try again";
            return entity;
        }
    }

    public static string OnDelete(long Id, ApplicationDbContext context)
    {
        // Work items are the audit trail of the document workflow
        return "Work items cannot be deleted";
    }
// Code-Player-End-DTO //
}
