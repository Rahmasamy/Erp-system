using Aspire.Context;
using Aspire.Data;
using Aspire.Enums;
using Aspire.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Aspire.Services;

public class DocumentTypeService()
{

// Code-Player-Start-DTO //
    public static async Task<DocumentType> Create(DocumentType entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        await DocumentService.EnsureDocumentSchemaAsync(context);
        entity.Msg = "OK";

        if (string.IsNullOrWhiteSpace(entity.Name))
        {
            entity.Msg = "Document Type Name is required";
            return entity;
        }

        entity.TenantId = _tenantService.GetTenantId();
        entity.CreatedByUsrId = _tenantService.GetUserId();
        entity.CreateDate = DateTime.UtcNow;
        entity.Msg = "OK";
        context.DocumentTypes.Add(entity);
        await context.SaveChangesAsync();

        // If not upload-only, seed the 6 default workflow disciplines
        if (entity.IsUploadOnly != true)
        {
            await SeedDefaultDisciplinesForDocumentType(entity.Id, entity.TenantId, context, _tenantService);
        }

        return entity;
    }

    private static async Task SeedDefaultDisciplinesForDocumentType(long documentTypeId, long? tenantId, ApplicationDbContext context, ITenantProviderService tenantService)
    {
        var existing = await context.DocumentTypeDisciplines
            .IgnoreQueryFilters()
            .Where(x => x.DocumentTypeId == documentTypeId)
            .AnyAsync();

        if (existing) return;

        var disciplines = await context.Disciplines
            .IgnoreQueryFilters()
            .Where(x => tenantId == null || x.TenantId == tenantId)
            .ToListAsync();

        var cn = disciplines.FirstOrDefault(x => x.Name == "Contractor");
        var dc = disciplines.FirstOrDefault(x => x.Name == "Document Controller");
        var am = disciplines.FirstOrDefault(x => x.Name == "Area Manager");
        var pm = disciplines.FirstOrDefault(x => x.Name == "Project Manager");

        long? userId = tenantService.GetUserId();

        if (cn != null && dc != null && am != null && pm != null)
        {
            context.DocumentTypeDisciplines.Add(new DocumentTypeDiscipline(tenantId, documentTypeId, cn.Id, 0, AllocationEnum.None, "In-Progress:0,Submit:1") { SLA_dec = 24, IsActive = true, CreatedByUsrId = userId, CreateDate = DateTime.UtcNow, Msg = "OK" });
            context.DocumentTypeDisciplines.Add(new DocumentTypeDiscipline(tenantId, documentTypeId, dc.Id, 10, AllocationEnum.DisciplineLeader, "In-Progress:0,Approve:1,Reject:3") { SLA_dec = 24, IsActive = true, CreatedByUsrId = userId, CreateDate = DateTime.UtcNow, Msg = "OK" });
            context.DocumentTypeDisciplines.Add(new DocumentTypeDiscipline(tenantId, documentTypeId, am.Id, 20, AllocationEnum.Project, "In-Progress:0,Approve:1,Reject:3") { SLA_dec = 24, IsActive = true, CreatedByUsrId = userId, CreateDate = DateTime.UtcNow, Msg = "OK" });
            context.DocumentTypeDisciplines.Add(new DocumentTypeDiscipline(tenantId, documentTypeId, pm.Id, 70, AllocationEnum.DisciplineLeader, "In-Progress:0,Approve:1,Reject:3") { SLA_dec = 24, IsActive = true, CreatedByUsrId = userId, CreateDate = DateTime.UtcNow, Msg = "OK" });
            context.DocumentTypeDisciplines.Add(new DocumentTypeDiscipline(tenantId, documentTypeId, dc.Id, 90, AllocationEnum.DisciplineLeader, "In-Progress:0,Submit:1") { SLA_dec = 24, IsActive = true, CreatedByUsrId = userId, CreateDate = DateTime.UtcNow, Msg = "OK" });
            context.DocumentTypeDisciplines.Add(new DocumentTypeDiscipline(tenantId, documentTypeId, cn.Id, 99, AllocationEnum.None, "Complete:0") { SLA_dec = 24, IsActive = true, CreatedByUsrId = userId, CreateDate = DateTime.UtcNow, Msg = "OK" });

            await context.SaveChangesAsync();
        }
    }

    public static async Task<DocumentType> Update(DocumentType entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        await DocumentService.EnsureDocumentSchemaAsync(context);
        entity.Msg = "OK";

        if (entity.Id == null || entity.Id <= 0)
        {
            entity.Msg = "A valid DocumentType Id is required";
            return entity;
        }

        DocumentType? obj = await context.DocumentTypes
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(e => e.Id == entity.Id);

        if (obj is null)
        {
            return new DocumentType { Msg = $"This Id ({entity.Id}) does not exist in the database" };
        }

        // Populate the Object to be Saved
        obj.Code = entity.Code ?? obj.Code;
        obj.Prefixes = entity.Prefixes ?? obj.Prefixes;
        obj.MaxSize_dec = entity.MaxSize_dec ?? obj.MaxSize_dec;
        obj.MaxFilesCount_num = entity.MaxFilesCount_num ?? obj.MaxFilesCount_num;
        obj.FileTypes = entity.FileTypes ?? obj.FileTypes;
        if (entity.IsUploadOnly.HasValue) obj.IsUploadOnly = entity.IsUploadOnly.Value;
        if (entity.IsAllowVersioning.HasValue) obj.IsAllowVersioning = entity.IsAllowVersioning.Value;
        obj.Name = entity.Name ?? obj.Name;
        obj.NameAr = entity.NameAr ?? obj.NameAr;
        obj.Description = entity.Description ?? obj.Description;
        obj.DescriptionAr = entity.DescriptionAr ?? obj.DescriptionAr;
        if (entity.IsActive.HasValue) obj.IsActive = entity.IsActive.Value;
        obj.ActionComment = entity.ActionComment ?? obj.ActionComment;
        obj.PhotoUrl = entity.PhotoUrl ?? obj.PhotoUrl;

        obj.Msg = "OK";
        obj.UpdatedByUsrId = _tenantService.GetUserId();
        obj.UpdateDate = DateTime.UtcNow;
        await context.SaveChangesAsync();
        return obj;
    }

    public static string OnDelete(long Id, ApplicationDbContext context)
    {
        bool hasDocuments = context.Documents.IgnoreQueryFilters().Any(d => d.DocumentTypeId == Id);
        if (hasDocuments)
        {
            return "Cannot delete document type because existing documents reference it";
        }

        // Clean up any associated DocumentTypeDisciplines
        var dtds = context.DocumentTypeDisciplines.IgnoreQueryFilters().Where(x => x.DocumentTypeId == Id).ToList();
        if (dtds.Count > 0)
        {
            context.DocumentTypeDisciplines.RemoveRange(dtds);
            context.SaveChanges();
        }

        return "OK";
    }
// Code-Player-End-DTO //
}