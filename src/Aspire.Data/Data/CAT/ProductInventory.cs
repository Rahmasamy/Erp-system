using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "CAT", Abbrv = "PRD-IVT", SubList = true,
    Description = "List of inventories holding products.")]
public class ProductInventory : XREFEntity
{
    public long? ProductId { get; set; }
    public long? InventoryId { get; set; }

    public decimal Balance_dec { get; set; } = 0;
}

// Code-Player-Start-DTO //
// Code-Player-End-DTO //

