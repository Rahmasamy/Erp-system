using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "APP", Abbrv = "", WithoutUI = true)]
public class EntityAttribute : CoreEntity
{
    public long? EntityId { get; set; }

    [NotMapped]
    public virtual Entity? Entity { get; set; }
    public string? DBFieldName { get; set; }
    public string? DataType { get; set; }

    public EntityAttribute() { }

    public EntityAttribute(long? tenantId, long? entityId, string? dBFieldName, string? dataType) {
        TenantId = tenantId;
        EntityId = entityId;
        Name = dBFieldName;
        DBFieldName = dBFieldName;
        DataType = dataType;
    }
}

// Code-Player-Start-DTO //
public class EntityAttributeDTO
{
    public long? EntityId { get; set; }
    public string? EntityName { get; set; }
    public string? DBFieldName { get; set; }
    public string? DataType { get; set; }
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