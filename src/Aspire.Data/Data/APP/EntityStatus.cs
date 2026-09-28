namespace Aspire.Data;

[Attr(DomainFolder = "APP", Abbrv = "", WithoutUI = true)]
public class EntityStatus : CoreEntity
{
    public string? Color { get; set; }
    public string? EntityName { get; set; }

    public bool IsStartSLA { get; set; } // For ServiceEntities ONLY
    public bool IsResponseSLA { get; set; } // For ServiceEntities ONLY
    public bool IsResolveSLA { get; set; } // For ServiceEntities ONLY

    public EntityStatus(long tnantId, string entityName, string name, string nameAr, string color)
    {
        TenantId = tnantId;
        
        EntityName = entityName;
        Name = name;
        Color = color;
        NameAr = nameAr;
    }

    public EntityStatus(long? tenantId, string entityName, string name, string nameAr, string color)
    {
        TenantId = tenantId;
        EntityName = entityName;
        Name = name;
        Color = color;
        NameAr = nameAr;
    }

    public EntityStatus(long? tenantId, long? orgId, string entityName, string name, string nameAr, string color)
    {
        TenantId = tenantId;
        EntityName = entityName;
        Name = name;
        Color = color;
        NameAr = nameAr;
    }
}

// Code-Player-Start-DTO //
public class EntityStatusDTO
{
    public string? Color { get; set; }
    public string? EntityName { get; set; }
    public bool? IsStartSLA { get; set; }
    public bool? IsResponseSLA { get; set; }
    public bool? IsResolveSLA { get; set; }
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
