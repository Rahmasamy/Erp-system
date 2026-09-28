using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "CAT", Abbrv = "")]
public class PriceListProduct : XREFEntity
{
    public long? ProductId { get; set; }
    public long? PriceListId { get; set; }
    public decimal FromQuantity_dec { get; set; } = 0;
    public decimal ToQuantity_dec { get; set; } = 0;
    public decimal UnitPrice_val { get; set; } = 0;
    public decimal UnitCost_val { get; set; } = 0;

    public PriceListProduct() { }
    public PriceListProduct(long tnantId, long prdId, long priceListId)
    {
        TenantId = tnantId;
        
        this.ProductId = prdId;
        this.PriceListId = priceListId;
    }
}

// Code-Player-Start-DTO //
// Code-Player-End-DTO //

