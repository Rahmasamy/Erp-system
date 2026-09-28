using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;

namespace Aspire.Services;

public class StakeholderService()
{


// Code-Player-Start-DTO //
    public static async Task<Stakeholder> Create(Stakeholder entity, ApplicationDbContext context, ITenantProviderService _tenantService)
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
        context.Stakeholders.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public static async Task<Stakeholder> Update(Stakeholder entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        Stakeholder? obj = context.Stakeholders.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Populate the Object Will be Saved
        obj.Category = entity.Category is null ? obj.Category : entity.Category;
        obj.DealId = entity.DealId is null ? obj.DealId : entity.DealId;
        obj.ProjectId = entity.ProjectId is null ? obj.ProjectId : entity.ProjectId;
        obj.ContactId = entity.ContactId is null ? obj.ContactId : entity.ContactId;
        obj.TeamMemberUsrId = entity.TeamMemberUsrId is null ? obj.TeamMemberUsrId : entity.TeamMemberUsrId;
        obj.ResponsibleFor = entity.ResponsibleFor is null ? obj.ResponsibleFor : entity.ResponsibleFor;
        obj.AccountableFor = entity.AccountableFor is null ? obj.AccountableFor : entity.AccountableFor;
        obj.ConsultantOn = entity.ConsultantOn is null ? obj.ConsultantOn : entity.ConsultantOn;
        obj.InformedAbout = entity.InformedAbout is null ? obj.InformedAbout : entity.InformedAbout;
        obj.Power = entity.Power is null ? obj.Power : entity.Power;
        obj.Interest = entity.Interest is null ? obj.Interest : entity.Interest;
        obj.Support = entity.Support is null ? obj.Support : entity.Support;
        obj.Influence = entity.Influence is null ? obj.Influence : entity.Influence;
        obj.Knowledge = entity.Knowledge is null ? obj.Knowledge : entity.Knowledge;
        obj.JobTitle = entity.JobTitle is null ? obj.JobTitle : entity.JobTitle;
        obj.Mobile = entity.Mobile is null ? obj.Mobile : entity.Mobile;
        obj.Email = entity.Email is null ? obj.Email : entity.Email;
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