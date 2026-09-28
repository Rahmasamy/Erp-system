using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Aspire.Services;

public class DocumentService()
{


// Code-Player-Start-DTO //
    public static async Task<EntityStatus> GetOrAddDocumentStatus(string name, string color, long? tenantId, ApplicationDbContext context)
    {
        EntityStatus? status = await context.EntityStatuses
            .FirstOrDefaultAsync(e => e.TenantId == tenantId && e.EntityName == "Document" && e.Name == name);

        if (status is null)
        {
            status = new EntityStatus(tenantId ?? 0, "Document", name, "عربي", color);
            context.EntityStatuses.Add(status);
            await context.SaveChangesAsync();
        }

        return status;
    }

    public static async Task<Document> Create(Document entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        try
        {
            entity.TenantId = _tenantService.GetTenantId();
            entity.CreatedByUsrId = _tenantService.GetUserId();
            entity.CreateDate = DateTime.UtcNow;
            entity.Version = 1;
            entity.Msg = "OK";
            long? tenantId = entity.TenantId;

            // Auto-assign ContractorId if the logged in user is a contractor / external user and ContractorId is not specified
            if (entity.ContractorId == null)
            {
                long? currentUserId = _tenantService.GetUserId();
                if (currentUserId.HasValue)
                {
                    ExternalUser? extUsr = await context.ExternalUsers.FirstOrDefaultAsync(x => x.AppUsrId == currentUserId.Value);
                    if (extUsr != null && extUsr.ContractorId.HasValue)
                    {
                        entity.ContractorId = extUsr.ContractorId;
                    }
                }
            }

            // Initial status of newly created document is always "Draft"
            EntityStatus draftStatus = await GetOrAddDocumentStatus("Draft", "secondary", tenantId, context);
            entity.EntityStatusId = draftStatus.Id;

            context.Documents.Add(entity);
            await context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex) {
            return entity;
        }

    }

    public static async Task EnsureDocumentSchemaAsync(ApplicationDbContext context)
    {
        try
        {
            await context.Database.ExecuteSqlRawAsync(@"
                ALTER TABLE IF EXISTS ""DocumentTypes"" ADD COLUMN IF NOT EXISTS ""IsAllowVersioning"" boolean DEFAULT false;
                ALTER TABLE IF EXISTS ""DocumentTypes"" ADD COLUMN IF NOT EXISTS ""MaxFilesCount_num"" integer;
                ALTER TABLE IF EXISTS ""DocumentTypes"" ADD COLUMN IF NOT EXISTS ""IsUploadOnly"" boolean DEFAULT false;
                ALTER TABLE IF EXISTS ""DocumentTypes"" ADD COLUMN IF NOT EXISTS ""Prefixes"" text;
                ALTER TABLE IF EXISTS ""DocumentTypes"" ADD COLUMN IF NOT EXISTS ""FileTypes"" text;
                ALTER TABLE IF EXISTS ""Documents"" ADD COLUMN IF NOT EXISTS ""Prefix"" text;
                ALTER TABLE IF EXISTS ""Documents"" ADD COLUMN IF NOT EXISTS ""ActionComment"" text;
                ALTER TABLE IF EXISTS ""Documents"" ADD COLUMN IF NOT EXISTS ""OldId"" text;
                ALTER TABLE IF EXISTS ""Documents"" ADD COLUMN IF NOT EXISTS ""OldWorkflowId"" bigint;
                ALTER TABLE IF EXISTS ""Documents"" ADD COLUMN IF NOT EXISTS ""CurrentStatus"" text;
                ALTER TABLE IF EXISTS ""Documents"" ADD COLUMN IF NOT EXISTS ""CurrentVersion"" numeric;
                ALTER TABLE IF EXISTS ""Documents"" ADD COLUMN IF NOT EXISTS ""IsArchived"" boolean DEFAULT false;
                ALTER TABLE IF EXISTS ""Documents"" ADD COLUMN IF NOT EXISTS ""IsUploadOnly"" boolean DEFAULT false;
                ALTER TABLE IF EXISTS ""Documents"" ADD COLUMN IF NOT EXISTS ""IsDeleted"" boolean DEFAULT false;
                ALTER TABLE IF EXISTS ""Documents"" ADD COLUMN IF NOT EXISTS ""DeleteDate"" timestamp without time zone;
                ALTER TABLE IF EXISTS ""Documents"" ADD COLUMN IF NOT EXISTS ""DeletedByUsrId"" bigint;
                ALTER TABLE IF EXISTS ""Documents"" ADD COLUMN IF NOT EXISTS ""PermanentDeleteEligibleDate"" timestamp without time zone;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""ContractorId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""DMSProjectId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""DocumentTypeId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""DocumentId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""DisciplineId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""ActionUsrId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""ActionRole"" text;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""DMSActionId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""StepNo"" integer;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""IsOpen"" boolean DEFAULT true;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""StartDate"" timestamp without time zone;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""DueDate"" timestamp without time zone;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""ActionDate"" timestamp without time zone;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""ActionComment"" text;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""EntityStatusId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""EntityTypeId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""DataConfidentialityId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""Name"" text;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""NameAr"" text;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""Description"" text;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""DescriptionAr"" text;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""No"" text;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""TenantId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""IsActive"" boolean;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""Msg"" text;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""PhotoUrl"" text;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""CreatedByUsrId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""UpdatedByUsrId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""CreateDate"" timestamp without time zone;
                ALTER TABLE IF EXISTS ""DBLogs"" ADD COLUMN IF NOT EXISTS ""TenantId"" bigint;
                ALTER TABLE IF EXISTS ""DBLogs"" ADD COLUMN IF NOT EXISTS ""ObjectId"" bigint;
                ALTER TABLE IF EXISTS ""DBLogs"" ADD COLUMN IF NOT EXISTS ""EntityName"" text;
                ALTER TABLE IF EXISTS ""DBLogs"" ADD COLUMN IF NOT EXISTS ""Action"" text;
                ALTER TABLE IF EXISTS ""DBLogs"" ADD COLUMN IF NOT EXISTS ""ActionComment"" text;
                ALTER TABLE IF EXISTS ""DBLogs"" ADD COLUMN IF NOT EXISTS ""ActionUsrId"" bigint;
                ALTER TABLE IF EXISTS ""DBLogs"" ADD COLUMN IF NOT EXISTS ""ActionDate"" timestamp without time zone;
                ALTER TABLE IF EXISTS ""DocumentTypeDisciplines"" ADD COLUMN IF NOT EXISTS ""TaskName"" text;
                ALTER TABLE IF EXISTS ""DocumentTypeDisciplines"" ADD COLUMN IF NOT EXISTS ""Actions"" text;
                ALTER TABLE IF EXISTS ""DocumentTypeDisciplines"" ADD COLUMN IF NOT EXISTS ""Seq"" integer;
                ALTER TABLE IF EXISTS ""DocumentTypeDisciplines"" ADD COLUMN IF NOT EXISTS ""Allocation"" integer;
                ALTER TABLE IF EXISTS ""DocumentTypeDisciplines"" ADD COLUMN IF NOT EXISTS ""SLA_dec"" numeric;
            ");
        }
        catch
        {
            // Ignore if DDL fails or columns already exist
        }
    }

    public static async Task<Document> SubmitDocument(long id, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        //await EnsureDocumentSchemaAsync(context);

        Document? doc = await context.Documents.FirstOrDefaultAsync(d => d.Id == id);

        if (doc is null)
        {
            return new Document { Msg = $"Document with Id ({id}) was not found" };
        }

        if (doc.DocumentTypeId.HasValue)
        {
            DocumentType? docType = await context.DocumentTypes.FirstOrDefaultAsync(x => x.Id == doc.DocumentTypeId.Value);
            if (docType != null && docType.IsUploadOnly == true)
            {
                doc.Msg = "Upload-only document types do not require a review workflow.";
                return doc;
            }
        }

        // Check if current status allows submission
        EntityStatus? currentStatus = doc.EntityStatusId.HasValue 
            ? await context.EntityStatuses.FirstOrDefaultAsync(s => s.Id == doc.EntityStatusId.Value) : null;

        if (currentStatus != null && currentStatus.Name != "Draft")
        {
            doc.Msg = $"Only documents in 'Draft' status can be submitted. Current status is '{currentStatus.Name}'.";
            return doc;
        }

        if (doc.DMSProjectId is null)
        {
            doc.Msg = "A Project is required to submit the document";
            return doc;
        }

        if (doc.DocumentTypeId is null)
        {
            doc.Msg = "A Document Type is required to submit the document";
            return doc;
        }

        bool hasAttachments = await context.EntityAttachments.AnyAsync(a => 
            a.EntityName == "Document" && 
            a.ObjectId == doc.Id);

        if (!hasAttachments)
        {
            doc.Msg = "At least one attachment is required to submit the document for review.";
            return doc;
        }

        DMSProject? project = await context.DMSProjects.FirstOrDefaultAsync(x => x.Id == doc.DMSProjectId);
        if (project is null)
        {
            doc.Msg = "The project assigned to this document was not found";
            return doc;
        }

        List<string> errors = [];


        if (errors.Count > 0)
        {
            doc.Msg = string.Join("; ", errors);
            return doc;
        }

        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            long PndingStatusId = context.EntityStatuses.FirstOrDefault(e => e.EntityName == "DocumentWorkItem" && e.Name == "Pending").Id;
            long InActvStatusId = context.EntityStatuses.FirstOrDefault(e => e.EntityName == "DocumentWorkItem" && e.Name == "In-Active").Id;
            long SubmitStatusId = context.EntityStatuses.FirstOrDefault(e => e.EntityName == "DocumentWorkItem" && e.Name == "Submitted").Id;

            doc.EntityStatusId = context.EntityStatuses.FirstOrDefault(e => e.EntityName == "Document" && e.Name == "Under Review").Id;
            doc.UpdatedByUsrId = _tenantService.GetUserId();
            doc.UpdateDate = DateTime.UtcNow;

            // Remove any Work Items if present from a previous run
            var staleItems = await context.DocumentWorkItems.Where(wi => wi.DocumentId == doc.Id).ToListAsync();
            if (staleItems.Count > 0)
                context.DocumentWorkItems.RemoveRange(staleItems);

            DateTime now = DateTime.UtcNow;

            // *********************************************************
            List<DocumentTypeDiscipline> DocDsps = context.DocumentTypeDisciplines.Where(x => x.DocumentTypeId == doc.DocumentTypeId).OrderBy(s => s.Seq).ToList();

            int StepCounter = 0; int? Seq = -1;
            foreach (DocumentTypeDiscipline DocDsp in DocDsps)
            {
                Discipline? dsp = context.Disciplines.Where(x => x.Id  == DocDsp.DisciplineId).FirstOrDefault();
                bool isContr = StepCounter == 0;
                bool isFirst = StepCounter == 1;
                
                if (DocDsp.Seq > Seq)
                {
                    StepCounter++;
                    Seq = DocDsp.Seq;
                }

                long? AsignedUsrId = null;

                if (DocDsp.Allocation == Enums.AllocationEnum.Project)
                {
                    AsignedUsrId = context.DMSProjectDisciplines.Where(x => x.DMSProjectId == doc.DMSProjectId && x.DisciplineId == DocDsp.DisciplineId).FirstOrDefault().DisciplineUsrId;
                }
                else if (DocDsp.Allocation == Enums.AllocationEnum.DisciplineLeader || DocDsp.Allocation == Enums.AllocationEnum.None)
                {
                    AsignedUsrId = context.Disciplines.Where(x => x.Id == DocDsp.DisciplineId).FirstOrDefault().DisciplineUsrId;
                }
                else if (DocDsp.Allocation == Enums.AllocationEnum.SpecificEmployee)
                {
                    AsignedUsrId = DocDsp.DisciplineUsrId;
                }

                context.DocumentWorkItems.Add(new DocumentWorkItem
                    {
                        TenantId = DocDsp.TenantId,
                        DMSProjectId = doc.DMSProjectId,
                        DocumentId = doc.Id,
                        DisciplineId = DocDsp.DisciplineId,
                        StepNo = StepCounter,
                        Seq = DocDsp.Seq,
                        IsOpen = isFirst,
                        Actions = DocDsp.Actions,
                        Name = dsp.Name,
                        ActionRole = dsp.Name,
                        ActionUsrId = AsignedUsrId,
                        EntityStatusId = isFirst ? PndingStatusId : isContr ? SubmitStatusId : InActvStatusId,
                        StartDate = isFirst ? now : null,
                        CompleteDate = isContr ? now : null,
                        DueDate = now.AddHours(double.Parse((DocDsp.SLA_dec??24).ToString())),
                        CreatedByUsrId = _tenantService.GetUserId(),
                        CreateDate = now
                    });
            }

            await context.SaveChangesAsync();
            await Utilities.DBLog.Action(doc.Id, "Document", "SubmitDocument", "Document submitted for review", context, _tenantService);
            await transaction.CommitAsync();

            doc.Msg = "OK";
            return doc;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    // A document a reviewer returned to the contractor sits in "Rejected" (read-only, with the reviewer's
    // comment) until the contractor explicitly chooses to act on it. Only then does it become editable again -
    // this is that explicit step. The old workflow's work items are left alone (they're the audit trail of
    // what happened last time); SubmitDocument already clears them out when the revised document is resubmitted.
    public static async Task<Document> ReopenAsDraft(long id, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        long? tenantId = _tenantService.GetTenantId();
        Document? doc = await context.Documents.FirstOrDefaultAsync(d => d.Id == id && d.TenantId == tenantId);

        if (doc is null)
        {
            return new Document { Msg = $"Document with Id ({id}) was not found" };
        }

        EntityStatus? currentStatus = doc.EntityStatusId.HasValue
            ? await context.EntityStatuses.FirstOrDefaultAsync(s => s.Id == doc.EntityStatusId.Value)
            : null;

        if (currentStatus?.Name != "Rejected")
        {
            doc.Msg = $"Only documents in 'Rejected' status can be reopened for editing. Current status is '{currentStatus?.Name ?? "unknown"}'.";
            return doc;
        }

        EntityStatus draftStatus = await GetOrAddDocumentStatus("Draft", "secondary", tenantId, context);
        doc.EntityStatusId = draftStatus.Id;
        doc.UpdatedByUsrId = _tenantService.GetUserId();
        doc.UpdateDate = DateTime.UtcNow;

        await context.SaveChangesAsync();
        await Utilities.DBLog.Action(doc.Id, "Document", "ReopenAsDraft", "Document reopened for editing after rejection", context, _tenantService);

        doc.Msg = "OK";
        return doc;
    }

    // Gets the DocumentWorkItem status of the tenant, adding it when the tenant was registered before it existed
    public static async Task<EntityStatus> GetOrAddWorkItemStatus(string name, long? tenantId, ApplicationDbContext context)
    {
        EntityStatus? status = await context.EntityStatuses
            .FirstOrDefaultAsync(e => e.TenantId == tenantId && e.EntityName == "DocumentWorkItem" && e.Name == name);

        if (status is null)
        {
            status = new EntityStatus(tenantId ?? 0, "DocumentWorkItem", name, "عربي", "warn");
            context.EntityStatuses.Add(status);
            await context.SaveChangesAsync();
        }

        return status;
    }

    public static async Task<Document> Update(Document entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        await EnsureDocumentSchemaAsync(context);
        entity.Msg = "OK";

        Document? obj = await context.Documents.FirstOrDefaultAsync(e => e.Id == entity.Id);

        if (obj is null)
        {
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";
            return entity;
        }

        // Check if document is locked (i.e. not Draft)
        if (obj.EntityStatusId.HasValue)
        {
            EntityStatus? status = await context.EntityStatuses.FirstOrDefaultAsync(s => s.Id == obj.EntityStatusId.Value);
            if (status != null && status.Name != "Draft")
            {
                // Non-draft documents are locked and read-only
                entity.Msg = $"This document is in '{status.Name}' status and is locked (read-only).";
                return entity;
            }
        }

        // Populate the Object Will be Saved
        obj.Code = entity.Code is null ? obj.Code : entity.Code;
        obj.CurrentVersion = entity.CurrentVersion is null ? obj.CurrentVersion : entity.CurrentVersion;
        obj.Prefix = entity.Prefix is null ? obj.Prefix : entity.Prefix;
        obj.ContractorId = entity.ContractorId is null ? obj.ContractorId : entity.ContractorId;
        obj.DMSProjectId = entity.DMSProjectId is null ? obj.DMSProjectId : entity.DMSProjectId;
        obj.DocumentTypeId = entity.DocumentTypeId is null ? obj.DocumentTypeId : entity.DocumentTypeId;
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
        Document? obj = context.Documents.FirstOrDefault(e => e.Id == Id);
        if (obj != null && obj.EntityStatusId.HasValue)
        {
            EntityStatus? status = context.EntityStatuses.FirstOrDefault(s => s.Id == obj.EntityStatusId.Value);
            if (status != null && status.Name != "Draft")
            {
                return "This document is locked and cannot be deleted.";
            }
        }
        return "OK";
    }
// Code-Player-End-DTO //
}
