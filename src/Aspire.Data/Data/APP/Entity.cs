namespace Aspire.Data;

[Attr(DomainFolder = "APP", Abbrv = "ET", WithoutUI = true)]
public class Entity : CoreEntity
{
    public string? Abbrv { get; set; } = "ENT";
    public string? Claims { get; set; } // Comma Separated Claim Types
    public string? Type { get; set; }
    public string? CAT { get; set; }

    public Entity() { }
    public Entity(long? tenantId, string claims, string cat, string type, string abbrv, string name, string nameAr, string desc, string descAr) {
        TenantId = tenantId;
        Type = type;
        Abbrv = abbrv;
        Name = name;
        NameAr = nameAr;
        Description = desc;
        DescriptionAr = descAr;
        CAT = cat;
        Claims = claims;
        IsActive = true;
    }
}

// Code-Player-Start-DTO
public class EntityDTO
{
    public long? Id { get; set; }
    public long? TenantId { get; set; }
    public string? No { get; set; }
    public string? Name { get; set; }
    public string? Msg { get; set; }
    public string? PhotoUrl { get; set; }
    public string? EntityType { get; set; }
    public string? Description { get; set; }
    public string? CreatedByUsrId { get; set; }
    public string? CreatedByUsrName { get; set; }
    public DateTime? CreateDate { get; set; }
}
// Code-Player-End-DTO

public class EntityCategory
{
    public string? Category { get; set; }
    public List<EntityDTO>? Entities { get; set; }
}

public class EntityCategorizedDTO
{
    public string? EntityType { get; set; }
    public List<EntityDTO>? Entities { get; set; }
}