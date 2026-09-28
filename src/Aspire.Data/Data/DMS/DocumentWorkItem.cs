
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "DMS", Abbrv = "SP", WithoutUI = true)]
public class DocumentWorkItem : BaseEntity
{
    public string? Actions { get; set; }
    public decimal SLA_dec { get; set; } = 24;

    public long? DMSProjectId { get; set; }
    public long? DocumentId { get; set; }
    public long? DisciplineId { get; set; }
    public long? ActionUsrId { get; set; }
    public string? ActionRole { get; set; }
    public string? Action { get; set; }
    public int? Seq { get; set; }
    public int? StepNo { get; set; } // stage number 1..5, one work item per stage
    public bool IsOpen { get; set; } = false;
    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public bool IsSLABreach { get; set; }
    public decimal SLABreach_dec { get; set; } = 0;

    public DocumentWorkItem() { }

}

// Code-Player-Start-DTO //
public class DocumentWorkItemDTO
{
    public long? DMSProjectId { get; set; }
    public int? StepNo { get; set; }
    public bool? IsOpen { get; set; } // nullable like the column: work items created before the workflow have NULL
    public string? DMSProjectName { get; set; }
    public long? DocumentId { get; set; }
    public string? DocumentName { get; set; }
    public long? DisciplineId { get; set; }
    public string? DisciplineName { get; set; }
    public long? ActionUsrId { get; set; }
    public string? ActionUsrName { get; set; }
    public string? ActionUsrPhotoUrl { get; set; }
    public string? ActionUsrEmail { get; set; }
    public string? ActionRole { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CompleteDate { get; set; }
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
