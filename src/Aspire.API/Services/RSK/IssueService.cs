using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;

namespace Aspire.Services;

public class IssueService()
{


// Code-Player-Start-DTO //
    public static async Task<Issue> Create(Issue entity, ApplicationDbContext context, ITenantProviderService _tenantService)
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
        context.Issues.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public static async Task<Issue> Update(Issue entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        Issue? obj = context.Issues.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Populate the Object Will be Saved
        obj.Score = entity.Score is null ? obj.Score : entity.Score;
        obj.OpenDate = entity.OpenDate is null ? obj.OpenDate : entity.OpenDate;
        obj.ProjectId = entity.ProjectId is null ? obj.ProjectId : entity.ProjectId;
        obj.WorkItemId = entity.WorkItemId is null ? obj.WorkItemId : entity.WorkItemId;
        obj.ITSystemId = entity.ITSystemId is null ? obj.ITSystemId : entity.ITSystemId;
        obj.ImpactId = entity.ImpactId is null ? obj.ImpactId : entity.ImpactId;
        obj.CriticalityId = entity.CriticalityId is null ? obj.CriticalityId : entity.CriticalityId;
        obj.UrgencyId = entity.UrgencyId is null ? obj.UrgencyId : entity.UrgencyId;
        obj.SeverityId = entity.SeverityId is null ? obj.SeverityId : entity.SeverityId;
        obj.OwnerId = entity.OwnerId is null ? obj.OwnerId : entity.OwnerId;
        obj.Amount_val = entity.Amount_val is null ? obj.Amount_val : entity.Amount_val;
        obj.ActionPlan = entity.ActionPlan is null ? obj.ActionPlan : entity.ActionPlan;
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