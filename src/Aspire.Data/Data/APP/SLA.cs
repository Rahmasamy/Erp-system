
using Aspire.Utilities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "APP", Abbrv = "SLA", WithoutUI = true)]
public class SLA : BaseEntity
{
    public long? WorkShiftId { get; set; }

    public SLA() { }

    public SLA(long? tenantId, long? entityTypeId, string servName, string name, string nameAr, string approvals, string availibleFor, string timeFrame)
    {
        this.TenantId = tenantId;
        this.EntityTypeId = entityTypeId;
        this.Name = name;
        this.NameAr = nameAr;
    }
}

// Code-Player-Start-DTO //
public class SLADTO
{
    public long? WorkShiftId { get; set; }
    public string? WorkShiftName { get; set; }
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