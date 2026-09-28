using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "GRC", Abbrv = "ADT", List = true, Id = true, SubList = true,
    Cols = new string[] { "FrameworkId", "StartDate", "EndDate", "Score" },
    DisplayValues = new string[] { "FrameworkName", "StartDate", "EndDate", "Score" })]
public class Audit : BaseEntity
{
    public decimal Score_dec { get; set; } = 0;
    public decimal MinScoreLimit_dec { get; set; } = 0;
    public decimal MaxScoreLimit_dec { get; set; } = 0;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public long? AssessmentId { get; set; }

    [NotMapped]
    public virtual Assessment? Assessment { get; set; }


    public long? FrameworkId { get; set; }

    [NotMapped]
    public virtual Framework? Framework { get; set; }

    public long? AuditorId { get; set; } // UserId

    [NotMapped]
    public virtual ApplicationUser? Auditor { get; set; }


    public long? ApproverId { get; set; } // UserId

    [NotMapped]
    public virtual ApplicationUser? Approver { get; set; }

    public Audit(long? tenantId, string name, long? frameworkId)
    {
        this.TenantId = tenantId;
        this.Name = name;
        this.FrameworkId = frameworkId;
    }

    public Audit(long? tenantId, long? entityTypeId, long? entityStatusId, string name, long? assessmentId, long? frameworkId)
    {
        this.TenantId = tenantId;
        this.Name = name;
        this.EntityTypeId = entityTypeId;
        this.EntityStatusId = entityStatusId;
        this.AssessmentId = assessmentId;
        this.FrameworkId = frameworkId;
    }
}

// Code-Player-Start-DTO //
public class AuditDTO
{
    public decimal? Score_dec { get; set; }
    public decimal? MinScoreLimit_dec { get; set; }
    public decimal? MaxScoreLimit_dec { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public long? AssessmentId { get; set; }
    public string? AssessmentName { get; set; }
    public long? FrameworkId { get; set; }
    public string? FrameworkName { get; set; }
    public long? AuditorId { get; set; }
    public string? AuditorName { get; set; }
    public long? ApproverId { get; set; }
    public string? ApproverName { get; set; }
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