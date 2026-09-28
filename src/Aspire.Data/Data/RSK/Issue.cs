using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "RSK", Abbrv = "ISU", SubList = true,
    Cols = new string[] { "OpenDate", "ImpactId", "CriticalityId", "UrgencyId", "SeverityId", "RiskStrategyId" },
    DisplayValues = new string[] { "Score" })]
public class Issue : BaseEntity
{
    public int? Score { get; set; } // Should be Calculated
    public DateTime? OpenDate { get; set; }

    public long? ProjectId { get; set; }

    [NotMapped]
    public virtual Project? Project { get; set; }

    public long? WorkItemId { get; set; }

    [NotMapped]
    public virtual WorkItem? WorkItem { get; set; }


    public long? ITSystemId { get; set; }

    [NotMapped]
    public virtual ITSystem? ITSystem { get; set; }


    public long? ImpactId { get; set; }

    [NotMapped]
    public virtual Impact? Impact { get; set; }


    public long? CriticalityId { get; set; }

    [NotMapped]
    public virtual Criticality? Criticality { get; set; }


    public long? UrgencyId { get; set; }

    [NotMapped]
    public virtual Urgency? Urgency { get; set; }


    public long? SeverityId { get; set; }

    [NotMapped]
    public virtual Severity? Severity { get; set; }


    public long? OwnerId { get; set; }

    [NotMapped]
    public virtual ApplicationUser? Owner { get; set; }

    public decimal? Amount_val { get; set; }
    public string? ActionPlan { get; set; }

    public Issue() { }
    public Issue(long tnantId, List<Impact> Impts, List<Criticality> Crits, List<Severity> Sevrs, List<Urgency> Urgs, List<ApplicationUser> Users, string name, long? projId)
    {
        TenantId = tnantId;
        
        this.Name = name;
        this.ProjectId = projId;

        Random rnd = new Random();

        Score = rnd.Next(0, 100);
        OpenDate = DateTime.Today.AddDays(rnd.Next(0, 100) * -1);
        OwnerId = Users != null && Users.Any() ? Users[rnd.Next(0, Users.Count)].Id : null;
        ImpactId = Impts != null && Impts.Any() ? Impts[rnd.Next(0, Impts.Count)].Id : null;
        CriticalityId = Crits != null && Crits.Any() ? Crits[rnd.Next(0, Crits.Count)].Id : null;
        UrgencyId = Urgs != null && Urgs.Any() ? Urgs[rnd.Next(0, Urgs.Count)].Id : null;
        SeverityId = Sevrs != null && Sevrs.Any() ? Sevrs[rnd.Next(0, Sevrs.Count)].Id : null;
    }
}

// Code-Player-Start-DTO //
public class IssueDTO
{
    public int? Score { get; set; }
    public DateTime? OpenDate { get; set; }
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public long? WorkItemId { get; set; }
    public string? WorkItemName { get; set; }
    public long? ITSystemId { get; set; }
    public string? ITSystemName { get; set; }
    public long? ImpactId { get; set; }
    public string? ImpactName { get; set; }
    public long? CriticalityId { get; set; }
    public string? CriticalityName { get; set; }
    public long? UrgencyId { get; set; }
    public string? UrgencyName { get; set; }
    public long? SeverityId { get; set; }
    public string? SeverityName { get; set; }
    public long? OwnerId { get; set; }
    public string? OwnerName { get; set; }
    public decimal? Amount_val { get; set; }
    public string? ActionPlan { get; set; }
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




