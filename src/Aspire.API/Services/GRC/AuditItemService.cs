using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;

namespace Aspire.Services;

public class AuditItemService()
{


// Code-Player-Start-DTO //
    public static async Task<AuditItem> Create(AuditItem entity, ApplicationDbContext context, ITenantProviderService _tenantService)
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
        context.AuditItems.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public static async Task<AuditItem> Update(AuditItem entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        AuditItem? obj = context.AuditItems.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Populate the Object Will be Saved
        obj.AuditId = entity.AuditId is null ? obj.AuditId : entity.AuditId;
        obj.ParentId = entity.ParentId is null ? obj.ParentId : entity.ParentId;
        obj.Type = entity.Type is null ? obj.Type : entity.Type;
        obj.Key = entity.Key is null ? obj.Key : entity.Key;
        obj.Objective = entity.Objective is null ? obj.Objective : entity.Objective;
        obj.Principal = entity.Principal is null ? obj.Principal : entity.Principal;
        obj.Tools = entity.Tools is null ? obj.Tools : entity.Tools;
        obj.Guidelines = entity.Guidelines is null ? obj.Guidelines : entity.Guidelines;
        obj.Deliverables = entity.Deliverables is null ? obj.Deliverables : entity.Deliverables;
        obj.Observation = entity.Observation is null ? obj.Observation : entity.Observation;
        obj.Ctrls = entity.Ctrls is null ? obj.Ctrls : entity.Ctrls;
        obj.Evids = entity.Evids is null ? obj.Evids : entity.Evids;
        obj.Excps = entity.Excps is null ? obj.Excps : entity.Excps;
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