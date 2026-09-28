using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;

namespace Aspire.Services;

public class ProjectService()
{
    public static async Task<Project> Terminate(Project entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        Project? obj = context.Projects.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Log The Action
        await Utilities.DBLog.Action(obj.Id, "{Entity Name}", "{Action Name}", obj.ActionComment, context, _tenantService);

        return entity;
    }


    public static async Task<Project> On_Hold(Project entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        Project? obj = context.Projects.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Log The Action
        await Utilities.DBLog.Action(obj.Id, "{Entity Name}", "{Action Name}", obj.ActionComment, context, _tenantService);

        return entity;
    }


    public static async Task<Project> Completed(Project entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        Project? obj = context.Projects.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Log The Action
        await Utilities.DBLog.Action(obj.Id, "{Entity Name}", "{Action Name}", obj.ActionComment, context, _tenantService);

        return entity;
    }




// Code-Player-Start-DTO //
    public static async Task<Project> Create(Project entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.StartDate is not null && entity.EndDate is not null && entity.EndDate.Value.Date < entity.StartDate.Value.Date)
        {
            entity.Msg = "End date must be greater than or equal to the start date.";
            return entity;
        }

        if (entity.Msg != "OK")
            return entity;

        // Perform the Business Logic BEFOR CREATING the WorkItem Object in the Database
        entity.TenantId = _tenantService.GetTenantId();
        entity.CreatedByUsrId = _tenantService.GetUserId();
        entity.CreateDate = DateTime.UtcNow;
        entity.Msg = "OK";
        context.Projects.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public static async Task<Project> Update(Project entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        Project? obj = context.Projects.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
        {
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";
            return entity;
        }

        DateTime? effectiveStartDate = entity.StartDate ?? obj.StartDate;
        DateTime? effectiveEndDate = entity.EndDate ?? obj.EndDate;
        if (effectiveStartDate is not null && effectiveEndDate is not null && effectiveEndDate.Value.Date < effectiveStartDate.Value.Date)
        {
            entity.Msg = "End date must be greater than or equal to the start date.";
            return entity;
        }

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Populate the Object Will be Saved
        obj.PMOAuditorUsrId = entity.PMOAuditorUsrId is null ? obj.PMOAuditorUsrId : entity.PMOAuditorUsrId;
        obj.TeamCount_num = entity.TeamCount_num == 0 ? obj.TeamCount_num : entity.TeamCount_num;
        obj.calc_Amount_val = entity.calc_Amount_val == 0 ? obj.calc_Amount_val : entity.calc_Amount_val;
        obj.Amount_cur = entity.Amount_cur is null ? obj.Amount_cur : entity.Amount_cur;
        obj.calc_Amount_tax = entity.calc_Amount_tax == 0 ? obj.calc_Amount_tax : entity.calc_Amount_tax;
        obj.calc_Amount_tot = entity.calc_Amount_tot == 0 ? obj.calc_Amount_tot : entity.calc_Amount_tot;

        obj.AccountManagerUsrId = entity.AccountManagerUsrId is null ? obj.AccountManagerUsrId : entity.AccountManagerUsrId;
        obj.ProjectManagerUsrId = entity.ProjectManagerUsrId is null ? obj.ProjectManagerUsrId : entity.ProjectManagerUsrId;
        obj.CustomerId = entity.CustomerId is null ? obj.CustomerId : entity.CustomerId;
        obj.StartDate = entity.StartDate is null ? obj.StartDate : entity.StartDate;
        obj.EndDate = entity.EndDate is null ? obj.EndDate : entity.EndDate;
        obj.AgreementId = entity.AgreementId is null ? obj.AgreementId : entity.AgreementId;
        obj.ProgrammeId = entity.ProgrammeId is null ? obj.ProgrammeId : entity.ProgrammeId;
        obj.PortfolioId = entity.PortfolioId is null ? obj.PortfolioId : entity.PortfolioId;
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
