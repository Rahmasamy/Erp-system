using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;

namespace Aspire.Services;

public class EntityAttachmentService()
{


// Code-Player-Start-DTO //
    public static async Task<EntityAttachment> Create(EntityAttachment entity, ApplicationDbContext context, ITenantProviderService _tenantService)
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
        context.EntityAttachments.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public static async Task<EntityAttachment> Update(EntityAttachment entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        EntityAttachment? obj = context.EntityAttachments.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
        {
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";
            return entity;
        }

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Populate the Object Will be Saved
        obj.FileName = entity.FileName is null ? obj.FileName : entity.FileName;
        obj.FileSize = entity.FileSize is null ? obj.FileSize : entity.FileSize;
        obj.FileExtension = entity.FileExtension is null ? obj.FileExtension : entity.FileExtension;
        obj.ContentType = entity.ContentType is null ? obj.ContentType : entity.ContentType;
        obj.ContentBase64 = entity.ContentBase64 is null ? obj.ContentBase64 : entity.ContentBase64;
        obj.DownloadLink = entity.DownloadLink is null ? obj.DownloadLink : entity.DownloadLink;
        obj.EntityName = entity.EntityName is null ? obj.EntityName : entity.EntityName;
        obj.ObjectId = entity.ObjectId is null ? obj.ObjectId : entity.ObjectId;
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