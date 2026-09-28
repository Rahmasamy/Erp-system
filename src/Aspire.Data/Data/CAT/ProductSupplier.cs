using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "CAT", Abbrv = "PRD-IVT", SubList = true,
    Description = "List of inventories holding products.")]
public class ProductSupplier : XREFEntity
{
    public long? ProductId { get; set; }
    public long? SupplierId { get; set; }
}

// Code-Player-Start-DTO //
// Code-Player-End-DTO //

