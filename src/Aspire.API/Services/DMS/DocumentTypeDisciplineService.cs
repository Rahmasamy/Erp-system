using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Aspire.Services;

public class DocumentTypeDisciplineService()
{


// Code-Player-Start-DTO //
    public static async Task<DocumentTypeDiscipline> Create(DocumentTypeDiscipline entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";

        if (entity.DocumentTypeId == null || entity.DocumentTypeId <= 0)
        {
            entity.Msg = "A valid DocumentTypeId is required";
            return entity;
        }

        if (entity.DisciplineId == null || entity.DisciplineId <= 0)
        {
            entity.Msg = "A valid DisciplineId is required";
            return entity;
        }

        int[] systemSeqs = { 0, 10, 20, 70, 90, 99 };
        if (entity.Seq.HasValue && !systemSeqs.Contains(entity.Seq.Value))
        {
            if (entity.Seq.Value <= 20 || entity.Seq.Value >= 98 || entity.Seq.Value == 70)
            {
                entity.Msg = "Custom Seq must be more than 20, less than 98, and not equal to 70";
                return entity;
            }
        }
        else if (!entity.Seq.HasValue)
        {
            entity.Seq = 21;
        }

        bool duplicate = await context.DocumentTypeDisciplines
            .IgnoreQueryFilters()
            .AnyAsync(x => x.DocumentTypeId == entity.DocumentTypeId && x.DisciplineId == entity.DisciplineId);

        if (duplicate)
        {
            entity.Msg = "This discipline is already added to this document type";
            return entity;
        }

        entity.TenantId = _tenantService.GetTenantId();
        entity.CreatedByUsrId = _tenantService.GetUserId();
        entity.CreateDate = DateTime.UtcNow;
        entity.Msg = "OK";
        context.DocumentTypeDisciplines.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public static async Task<(List<DocumentTypeDiscipline>? Records, string Msg)> Update(DocumentTypeDisciplineEditDTO model, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        if (model is null || model.DocumentTypeId is null)
            return (null, "A valid DocumentTypeId is required");

        List<DocumentTypeDisciplineItemDTO> items = model.Disciplines ?? new List<DocumentTypeDisciplineItemDTO>();

        List<long?> disciplineIds = items.Select(x => x.DisciplineId).Distinct().ToList();

        // Ensure mapping Ids are unique if provided, or discipline Ids are unique for new mappings
        List<long?> itemIds = items.Where(x => x.Id.HasValue).Select(x => x.Id).ToList();
        if (itemIds.Count != itemIds.Distinct().Count())
            return (null, "Duplicate mapping Id is not allowed in the same request");

        List<long?> newDisciplineIds = items.Where(x => !x.Id.HasValue).Select(x => x.DisciplineId).ToList();
        if (newDisciplineIds.Count != newDisciplineIds.Distinct().Count())
            return (null, "Duplicate Discipline is not allowed in the same request");

        DocumentType? documentType = await context.DocumentTypes
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == model.DocumentTypeId);
        if (documentType is null)
            return (null, "This DocumentType Id (" + model.DocumentTypeId + ") is not exist in the database");

        if (items.Count == 0)
            return (new List<DocumentTypeDiscipline>(), "OK");

        List<long?> existingDisciplineIds = await context.Disciplines
            .IgnoreQueryFilters()
            .Where(x => disciplineIds.Contains(x.Id))
            .Select(x => (long?)x.Id)
            .ToListAsync();

        int[] systemSeqs = { 0, 10, 20, 70, 90, 99 };

        foreach (DocumentTypeDisciplineItemDTO item in items)
        {
            if (item.DisciplineId is null || !existingDisciplineIds.Contains(item.DisciplineId))
                return (null, "This Discipline Id (" + item.DisciplineId + ") is not exist in the database");

            if (item.SLA_dec is null || item.SLA_dec < 0 || item.SLA_dec > 48)
                return (null, "SLA must be between 0 and 48 hours");

            // Validate custom sequence values. Repeated custom sequences are allowed for parallel steps.
            if (item.Seq.HasValue)
            {
                if (!systemSeqs.Contains(item.Seq.Value) &&
                    (item.Seq.Value <= 20 || item.Seq.Value >= 98 || item.Seq.Value == 70))
                {
                    return (null, "Custom Seq must be between 21 and 97, and cannot be 70");
                }
            }
        }

        List<DocumentTypeDiscipline> records = new();

        await using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            List<long?> mappingIds = items.Where(x => x.Id.HasValue).Select(x => x.Id).ToList();
            List<DocumentTypeDiscipline> existingRecords = await context.DocumentTypeDisciplines
                .IgnoreQueryFilters()
                .Where(x => x.DocumentTypeId == model.DocumentTypeId &&
                    (disciplineIds.Contains(x.DisciplineId) || mappingIds.Contains(x.Id)))
                .ToListAsync();

            foreach (DocumentTypeDisciplineItemDTO item in items)
            {
                var matches = item.Id.HasValue
                    ? existingRecords.Where(x => x.Id == item.Id.Value).ToList()
                    : existingRecords.Where(x => x.DisciplineId == item.DisciplineId).ToList();
                bool isActive = item.IsActive ?? (item.SLA_dec > 0);
                decimal? sla = item.SLA_dec ?? 0;

                if (matches.Count > 0)
                {
                    var existing = matches[0];
                    existing.SLA_dec = sla;
                    existing.DisciplineId = item.DisciplineId;
                    existing.IsActive = isActive;
                    existing.Seq = item.Seq ?? existing.Seq;
                    existing.TaskName = item.TaskName ?? existing.TaskName;
                    existing.Allocation = item.Allocation ?? existing.Allocation;
                    existing.Actions = item.Actions ?? existing.Actions;
                    existing.UpdatedByUsrId = _tenantService.GetUserId();
                    existing.UpdateDate = DateTime.UtcNow;
                    existing.Msg = "OK";
                    records.Add(existing);

                    // If duplicates exist in DB and no specific mapping Id was targeted, clean them up
                    if (!item.Id.HasValue)
                    {
                        for (int i = 1; i < matches.Count; i++)
                        {
                            context.DocumentTypeDisciplines.Remove(matches[i]);
                        }
                    }
                }
                else
                {
                    // Create new record
                    DocumentTypeDiscipline newRecord = new()
                    {
                        DocumentTypeId = model.DocumentTypeId,
                        DisciplineId = item.DisciplineId,
                        TaskName = item.TaskName,
                        SLA_dec = sla,
                        IsActive = isActive,
                        Seq = item.Seq ?? 21,
                        Allocation = item.Allocation ?? Aspire.Enums.AllocationEnum.Project,
                        Actions = item.Actions ?? "In-Progress:0,Approve:1,Reject:3",
                        TenantId = _tenantService.GetTenantId(),
                        CreatedByUsrId = _tenantService.GetUserId(),
                        CreateDate = DateTime.UtcNow,
                        Msg = "OK"
                    };
                    context.DocumentTypeDisciplines.Add(newRecord);
                    records.Add(newRecord);
                }
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return (records, "OK");
    }

    public static async Task<(bool Success, string Msg)> Delete(long Id, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        // Find the record
        DocumentTypeDiscipline? record = await context.DocumentTypeDisciplines
            .IgnoreQueryFilters()
            .Where(x => x.Id == Id)
            .FirstOrDefaultAsync();
            
        if (record is null)
            return (false, "Record not found");
        
        // Check if it's a system discipline that cannot be deleted
        int[] systemSeqs = { 0, 10, 20, 70, 90, 99 };
        if (record.Seq.HasValue && systemSeqs.Contains(record.Seq.Value))
        {
            return (false, "System disciplines cannot be deleted");
        }
        
        // Perform the delete
        context.DocumentTypeDisciplines.Remove(record);
        await context.SaveChangesAsync();
        
        return (true, "OK");
    }

// Code-Player-End-DTO //
}
