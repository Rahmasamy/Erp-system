namespace Aspire.Data;

[Attr(DomainFolder = "APP", Abbrv = "", WithoutUI = true)]
public class EntityType : CoreEntity
{
    public string? Color { get; set; }
    public string? EntityName { get; set; }
    public long? SLAId { get; set; } // For ServiceEntities ONLY

    public EntityType(long? tenantId, string entityName, string name, string nameAr, string color)
    {
        TenantId = tenantId;
        EntityName = entityName;
        Name = name;
        Color = color;
        NameAr = nameAr;
    }
}

// Code-Player-Start-DTO //
public class EntityTypeDTO
{
    public string? Color { get; set; }
    public string? EntityName { get; set; }
    public long? SLAId { get; set; }
    public string? SLAName { get; set; }
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
