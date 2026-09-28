using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "GRC", Abbrv = "REG", List = true,
    Cols = new string[] { "EntityStatusId", "EntityTypeId" },
    DisplayValues = new string[] { "EntityStatusName", "EntityTypeName" },
       Menu = new string[] {
        "Frameworks?GRC_Framework_ListSub"
    })]
public class Regulator : BaseEntity
{
    public Regulator(long? tenantId, string name)
    {
        this.TenantId = tenantId;
        this.Name = name;
    }
}
// Code-Player-Start-DTO //
public class RegulatorDTO
{
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
    public long? OrganizationId { get; set; }
    public string? OrganizationName { get; set; }
    public long? Id { get; set; }
    public string? No { get; set; }
    public long? TenantId { get; set; }
    public string? TenantName { get; set; }
    public bool? IsActive { get; set; }
    public string? Msg { get; set; }
    public string? PhotoUrl { get; set; }
    public long? CreatedByUsrId { get; set; }
    public string? CreatedByUsrName { get; set; }
    public string? CreatedByPhotoUrl { get; set; }
    public long? UpdatedByUsrId { get; set; }
    public string? UpdatedByUsrName { get; set; }
    public string? UpdatedByPhotoUrl { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
// Code-Player-End-DTO //









