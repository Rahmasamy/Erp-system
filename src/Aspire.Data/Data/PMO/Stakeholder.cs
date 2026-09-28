using Aspire.Utilities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Reflection;
using static Duende.IdentityServer.Models.IdentityResources;
namespace Aspire.Data;

[Attr(DomainFolder = "PMO", Abbrv = "STK" )]
public class Stakeholder : BaseEntity
{
    public string? Category { get; set; }
    public long? DealId { get; set; }

    [NotMapped]
    public virtual Deal? Deal { get; set; }

    public long? ProjectId { get; set; }

    [NotMapped]
    public virtual Project? Project { get; set; }

    public long? ContactId { get; set; }
    
    [NotMapped]
    public virtual Contact? Contact { get; set; }

    public long? TeamMemberUsrId { get; set; }

    public string? ResponsibleFor { get; set; }
    public string? AccountableFor { get; set; }
    public string? ConsultantOn { get; set; }
    public string? InformedAbout { get; set; }
    public double? Power { get; set; }
    public double? Interest { get; set; }
    public double? Support { get; set; }
    public double? Influence { get; set; }
    public double? Knowledge { get; set; }

    public string? JobTitle { get; set; } = string.Empty;
    public string? Mobile { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;

    public Stakeholder() { }
    public Stakeholder(long tnantId, string category, long? projectId, long? dealId, Contact? contact, ApplicationUser? teamMember, double? power, double? interest, double? influence, double? support, double? knowledge)
    {
        TenantId = tnantId;
        
        Name = contact is null ? teamMember.Name : contact.Name;
        ProjectId = projectId;
        DealId = dealId;
        ContactId = contact is null ? null : contact.Id;
        TeamMemberUsrId = teamMember is null ? null : teamMember.Id;
        JobTitle = teamMember is null ? null : teamMember.JobTitle;
        Email = teamMember is null ? null : teamMember.Email;
        Mobile = teamMember is null ? null : teamMember.Mobile;
        Category = category;

        Power = power;
        Interest = interest;
        Influence = influence;
        Support = support;
        Knowledge = knowledge;

        ResponsibleFor = "Manage the technical team";
        AccountableFor = "Complete system design";
        ConsultantOn = "Timeline of the project";
        InformedAbout = "Project budget and managemnet agreements";
    }
}

// Code-Player-Start-DTO //
public class StakeholderDTO
{
    public string? Category { get; set; }
    public long? DealId { get; set; }
    public string? DealName { get; set; }
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public long? ContactId { get; set; }
    public string? ContactName { get; set; }
    public long? TeamMemberUsrId { get; set; }
    public string? TeamMemberUsrName { get; set; }
    public string? TeamMemberUsrPhotoUrl { get; set; }
    public string? ResponsibleFor { get; set; }
    public string? AccountableFor { get; set; }
    public string? ConsultantOn { get; set; }
    public string? InformedAbout { get; set; }
    public double? Power { get; set; }
    public double? Interest { get; set; }
    public double? Support { get; set; }
    public double? Influence { get; set; }
    public double? Knowledge { get; set; }
    public string? JobTitle { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }
    public long? EntityStatusId { get; set; }
    public string? EntityStatusName { get; set; }
    public string? EntityStatusColor { get; set; }
    public long? EntityTypeId { get; set; }
    public string? EntityTypeName { get; set; }
    public string? EntityTypeColor { get; set; }
    public long? DataConfidentialityId { get; set; }
    public string? DataConfidentialityName { get; set; }
    public string? Name { get; set; }
    public string? NameAr { get; set; }
    public string? Description { get; set; }
    public string? DescriptionAr { get; set; }
    public long? Id { get; set; }
    public string? No { get; set; }
    public long? TenantId { get; set; }
    public string? TenantName { get; set; }
    public bool? IsActive { get; set; }
    public string? Msg { get; set; }
    public string? ActionComment { get; set; }
    public string? PhotoUrl { get; set; }
    public long? CreatedByUsrId { get; set; }
    public string? CreatedByUsrName { get; set; }
    public string? CreatedByUsrPhotoUrl { get; set; }
    public long? UpdatedByUsrId { get; set; }
    public string? UpdatedByUsrName { get; set; }
    public string? UpdatedByUsrPhotoUrl { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
// Code-Player-End-DTO //