using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;

namespace Aspire.Services;

public class WorkItemService()
{


// Code-Player-Start-DTO //
    public static async Task<WorkItem> Create(WorkItem entity, ApplicationDbContext context, ITenantProviderService _tenantService)
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
        context.WorkItems.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public static async Task<WorkItem> Update(WorkItem entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        WorkItem? obj = context.WorkItems.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Populate the Object Will be Saved
        obj.IsMilestone = entity.IsMilestone is null ? obj.IsMilestone : entity.IsMilestone;
        obj.IsLeaf = entity.IsLeaf is null ? obj.IsLeaf : entity.IsLeaf;
        obj.IsTimeSheet = entity.IsTimeSheet is null ? obj.IsTimeSheet : entity.IsTimeSheet;
        obj.LineItemId = entity.LineItemId is null ? obj.LineItemId : entity.LineItemId;
        obj.ParentId = entity.ParentId is null ? obj.ParentId : entity.ParentId;
        obj.AssignedToUsrId = entity.AssignedToUsrId is null ? obj.AssignedToUsrId : entity.AssignedToUsrId;
        obj.StartDate = entity.StartDate is null ? obj.StartDate : entity.StartDate;
        obj.EndDate = entity.EndDate is null ? obj.EndDate : entity.EndDate;
        obj.ActualStartDate = entity.ActualStartDate is null ? obj.ActualStartDate : entity.ActualStartDate;
        obj.ActualEndDate = entity.ActualEndDate is null ? obj.ActualEndDate : entity.ActualEndDate;
        obj.TaskSizeId = entity.TaskSizeId is null ? obj.TaskSizeId : entity.TaskSizeId;
        obj.TaskPriorityId = entity.TaskPriorityId is null ? obj.TaskPriorityId : entity.TaskPriorityId;
        obj.TaskComplexityId = entity.TaskComplexityId is null ? obj.TaskComplexityId : entity.TaskComplexityId;
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