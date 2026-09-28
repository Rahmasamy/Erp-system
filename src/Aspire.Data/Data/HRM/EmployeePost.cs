using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "HRM", Abbrv = "E-PST", SubList = true)]
public class EmployeePost : XREFEntity
{
    [Attr(Parent = true)]
    public long? EmployeeId { get; set; }

    [NotMapped]
    public virtual Employee? Employee { get; set; }

    public long? PostId { get; set; }

    [NotMapped]
    public virtual Post? Post { get; set; }
    public DateTime? SubmissionDate { get; set; }
}

// Code-Player-Start-DTO //
public class EmployeePostDTO
{
    public long? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public long? PostId { get; set; }
    public string? PostName { get; set; }
    public DateTime? SubmissionDate { get; set; }
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