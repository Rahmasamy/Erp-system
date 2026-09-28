using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "CAT", Abbrv = "PRD-BND", SubList = true,
    Description = "List of sub-products related to the bundle product.")]
public class ProductBundle : XREFEntity
{
    [Attr(Parent = true)]
    public string? BundleId { get; set; }

    [NotMapped]
    public virtual Product? Bundle { get; set; }


    public string? ProductId { get; set; }

    [NotMapped]
    public virtual Product? Product { get; set; }


    public decimal? Quantity { get; set; }
}

// Code-Player-Start-DTO //
public class ProductBundleDTO
{
    public string? BundleId { get; set; }
    public string? BundleName { get; set; }
    public string? ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal? Quantity { get; set; }
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
