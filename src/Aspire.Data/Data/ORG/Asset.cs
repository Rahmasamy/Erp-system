using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "ORG", Abbrv = "AST", Lookup = true)]
public class Asset : BaseEntity
{
    public long? OwnerUsrId { get; set; }

    public string? Serial { get; set; }

    public DateTime? PurchaseDate { get; set; }
    public DateTime? WarrantyEndDate { get; set; }

    public decimal PurchaseValue_val { get; set; } = 0;

    public decimal CurrentValue_val { get; set; } = 0;

    Asset() { }

    public Asset(long? tenantId, long? usrId, string name, string nameAr)
    {
        TenantId = tenantId;
        Name = name;
        NameAr = nameAr;
        OwnerUsrId = usrId;
    }
}

// Code-Player-Start-DTO //
public class AssetDTO
{
    public long? OwnerUsrId { get; set; }
    public string? OwnerUsrName { get; set; }
    public string? OwnerUsrPhotoUrl { get; set; }
    public string? Serial { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public DateTime? WarrantyEndDate { get; set; }
    public decimal? PurchaseValue_val { get; set; }
    public decimal? CurrentValue_val { get; set; }
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
