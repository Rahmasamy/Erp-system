using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "HRM", Abbrv = "E-DP", SubList = true)]
public class EmployeeDependant : BaseEntity
{
    [Attr(Parent = true)]
    public long? EmployeeId { get; set; }

    [NotMapped]
    public virtual Employee? Employee { get; set; }

    public long? RelationId { get; set; }

    [NotMapped]
    public virtual Relation? Relation { get; set; }

    public string? FNameEn { get; set; }
    public string? MNameEn { get; set; }
    public string? LNameEn { get; set; }
    public string? FNameAr { get; set; }
    public string? MNameAr { get; set; }
    public string? LNameAr { get; set; }
    public DateTime? BirthDay { get; set; }
    public long? GenderId { get; set; }

    [NotMapped]
    public virtual Gender? Gender { get; set; }
}

// Code-Player-Start-DTO //
public class EmployeeDependantDTO
{
    public long? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public long? RelationId { get; set; }
    public string? RelationName { get; set; }
    public string? FNameEn { get; set; }
    public string? MNameEn { get; set; }
    public string? LNameEn { get; set; }
    public string? FNameAr { get; set; }
    public string? MNameAr { get; set; }
    public string? LNameAr { get; set; }
    public DateTime? BirthDay { get; set; }
    public long? GenderId { get; set; }
    public string? GenderName { get; set; }
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