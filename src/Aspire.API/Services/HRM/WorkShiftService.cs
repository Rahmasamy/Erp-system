using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;

namespace Aspire.Services;

public class WorkShiftService()
{


// Code-Player-Start-DTO //
    public static async Task<WorkShift> Create(WorkShift entity, ApplicationDbContext context, ITenantProviderService _tenantService)
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
        context.WorkShifts.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public static async Task<WorkShift> Update(WorkShift entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        WorkShift? obj = context.WorkShifts.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Populate the Object Will be Saved
        obj.TimeZoneId = entity.TimeZoneId is null ? obj.TimeZoneId : entity.TimeZoneId;
        obj.Sun_From = entity.Sun_From is null ? obj.Sun_From : entity.Sun_From;
        obj.Sun_To = entity.Sun_To is null ? obj.Sun_To : entity.Sun_To;
        obj.Mon_From = entity.Mon_From is null ? obj.Mon_From : entity.Mon_From;
        obj.Mon_To = entity.Mon_To is null ? obj.Mon_To : entity.Mon_To;
        obj.Tue_From = entity.Tue_From is null ? obj.Tue_From : entity.Tue_From;
        obj.Tue_To = entity.Tue_To is null ? obj.Tue_To : entity.Tue_To;
        obj.Wed_From = entity.Wed_From is null ? obj.Wed_From : entity.Wed_From;
        obj.Wed_To = entity.Wed_To is null ? obj.Wed_To : entity.Wed_To;
        obj.Thr_From = entity.Thr_From is null ? obj.Thr_From : entity.Thr_From;
        obj.Thr_To = entity.Thr_To is null ? obj.Thr_To : entity.Thr_To;
        obj.Fri_From = entity.Fri_From is null ? obj.Fri_From : entity.Fri_From;
        obj.Fri_To = entity.Fri_To is null ? obj.Fri_To : entity.Fri_To;
        obj.Sat_From = entity.Sat_From is null ? obj.Sat_From : entity.Sat_From;
        obj.Sat_To = entity.Sat_To is null ? obj.Sat_To : entity.Sat_To;
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