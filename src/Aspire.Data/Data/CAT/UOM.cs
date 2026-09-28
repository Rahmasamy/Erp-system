using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "CAT", Abbrv = "UOM", WithoutUI = true)]
public class UOM : CoreEntity
{
    public long? ParentId { get; set; } // If NULL => Base Unit

    [NotMapped]
    public virtual UOM? Parent { get; set; }

    public decimal RoundingPrecision_dec { get; set; } = 2;
    public decimal Quantity_dec { get; set; } = 1; // If BaseUnit (ParentId Is NULL) => "1", If Not BaseUnit => "> 1" How many from the BaseUnit?

    public UOM() { }

    public UOM(long? tenantId, string name, string nameAr, long? parentId, decimal quantity)
    {
        this.TenantId = tenantId;
        this.Name = name;
        this.NameAr = nameAr;
        Quantity_dec = quantity;
        ParentId = parentId;
    }
}

// Code-Player-Start-DTO //
public class UOMDTO
{
    public long? ParentId { get; set; }
    public string? ParentName { get; set; }
    public decimal? RoundingPrecision_dec { get; set; }
    public decimal? Quantity_dec { get; set; }
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

