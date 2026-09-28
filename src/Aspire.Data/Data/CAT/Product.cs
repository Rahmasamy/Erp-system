using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace Aspire.Data;

[Attr(DomainFolder = "CAT", Abbrv = "PRD", SubList = true, List = true, Card = true, Tree = true,
    Cols = new string[] { "ProductCategoryId", "MarketUnitId", "TaxRateId", "UnitPrice_val", "UnitCost_val" },
    GroupBy = new string[] { "ProductCategoryId", "MarketUnitId", "TaxRateId" },
    DisplayValues = new string[] { "SKUNo", "ProductCategoryName", "UnitPrice_val" },
    SummaryValues = new string[] { "UnitPrice_val", "UnitCost_val" },
    Menu = new string[] {
        "Activities?CAT_ProductActivity_ListSub",
        "Deliverables?CAT_ProductDeliverable_ListSub",
        "Inventories?CAT_ProductInventory_ListSub",
        "SubProducts?CAT_ProductBundle_ListSub"
    })]
public class Product : BaseEntity
{
    [Attr(Key = true)]
    public string? SKUNo { get; set; } // Internal Identifier
    public string? PN { get; set; } // External Identifier used by Manufacturers to identify specific product or components.

    public bool CanBeSold { get; set; }
    public bool CanBePurchased { get; set; }
    public bool CanBeExpensed { get; set; }

    public long? UOMId { get; set; } // Only Base Units Should Be Availible

    public string? DescriptionSales { get; set; }
    public string? DescriptionPurchase { get; set; }

    public string? WarningSales { get; set; } // Warning When Selling this Product
    public string? WarningPurchase { get; set; } // Warning When Purchasing this Product

    public decimal UnitPrice_val { get; set; } = 0;
    public decimal UnitCost_val { get; set; } = 0;

    public long? DeliveryProofId { get; set; }

    public long? ProductCategoryId { get; set; }

    public long? MarketUnitId { get; set; }

    public long? TradingTypeId { get; set; }

    public decimal ReOrderLimit_dec { get; set; } = 0;

    public long? BillingFrequencyId { get; set; }

    public long? TaxRateId { get; set; }

    public Product() { }
    public Product(long tnantId, string name, string nameAr, long? productCategoryId, long? marketUnitId, decimal amount, long? curId, long? ratId)
    {
        TenantId = tnantId;
        
        MarketUnitId = marketUnitId;
        this.Name = name;
        this.NameAr = nameAr;
        this.ProductCategoryId = productCategoryId;
        TaxRateId = ratId;

        UnitPrice_val = amount;
        UnitCost_val = UnitPrice_val * 90 / 100;
    }
}

// Code-Player-Start-DTO //
public class ProductDTO
{
    public string? SKUNo { get; set; }
    public string? PN { get; set; }
    public bool? CanBeSold { get; set; }
    public bool? CanBePurchased { get; set; }
    public bool? CanBeExpensed { get; set; }
    public long? UOMId { get; set; }
    public string? UOMName { get; set; }
    public string? DescriptionSales { get; set; }
    public string? DescriptionPurchase { get; set; }
    public string? WarningSales { get; set; }
    public string? WarningPurchase { get; set; }
    public decimal? UnitPrice_val { get; set; }
    public decimal? UnitCost_val { get; set; }
    public long? DeliveryProofId { get; set; }
    public string? DeliveryProofName { get; set; }
    public long? ProductCategoryId { get; set; }
    public string? ProductCategoryName { get; set; }
    public long? MarketUnitId { get; set; }
    public string? MarketUnitName { get; set; }
    public long? TradingTypeId { get; set; }
    public string? TradingTypeName { get; set; }
    public decimal? ReOrderLimit_dec { get; set; }
    public long? BillingFrequencyId { get; set; }
    public string? BillingFrequencyName { get; set; }
    public long? TaxRateId { get; set; }
    public string? TaxRateName { get; set; }
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
