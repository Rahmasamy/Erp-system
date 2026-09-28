using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;

namespace Aspire.Services;

public class EmployeeDependantService()
{


// Code-Player-Start-DTO //
    public static async Task<EmployeeDependant> Create(EmployeeDependant entity, ApplicationDbContext context, ITenantProviderService _tenantService)
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
        context.EmployeeDependants.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public static async Task<EmployeeDependant> Update(EmployeeDependant entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        EmployeeDependant? obj = context.EmployeeDependants.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Populate the Object Will be Saved
        obj.EmployeeId = entity.EmployeeId is null ? obj.EmployeeId : entity.EmployeeId;
        obj.RelationId = entity.RelationId is null ? obj.RelationId : entity.RelationId;
        obj.FNameEn = entity.FNameEn is null ? obj.FNameEn : entity.FNameEn;
        obj.MNameEn = entity.MNameEn is null ? obj.MNameEn : entity.MNameEn;
        obj.LNameEn = entity.LNameEn is null ? obj.LNameEn : entity.LNameEn;
        obj.FNameAr = entity.FNameAr is null ? obj.FNameAr : entity.FNameAr;
        obj.MNameAr = entity.MNameAr is null ? obj.MNameAr : entity.MNameAr;
        obj.LNameAr = entity.LNameAr is null ? obj.LNameAr : entity.LNameAr;
        obj.BirthDay = entity.BirthDay is null ? obj.BirthDay : entity.BirthDay;
        obj.GenderId = entity.GenderId is null ? obj.GenderId : entity.GenderId;
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