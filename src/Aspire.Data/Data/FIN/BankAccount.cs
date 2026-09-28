
using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "FIN", Abbrv = "IBAN", SubList = true,
       GroupBy = new string[] { "BankId", "CurrencyId" },
       DisplayValues = new string[] { "IBAN", "BankName", "CurrencyName", "Balance_val" }
    )]
public class BankAccount : BaseEntity
{
    [Attr(ColWidth = 150, Filtration = Flr.Multi, Form = Frm.Basic)]
    public long? BankId { get; set; }

    [NotMapped]
    public virtual Bank? Bank { get; set; }


    [Attr(ColWidth = 150, Filtration = Flr.Multi, Form = Frm.Basic)]
    public long? CurrencyId { get; set; }

    [NotMapped]
    public virtual Currency? Currency { get; set; }


    [Attr(ColWidth = 150, Filtration = Flr.Multi, Form = Frm.Basic)]
    public long? CustomerId { get; set; } // NULL => [My Organization]

    [NotMapped]
    public virtual Customer? Customer { get; set; }

    
    [Attr(ColWidth = 150, Filtration = Flr.Multi, Form = Frm.Basic)]
    public long? SupplierId { get; set; } // NULL => [My Organization]

    [NotMapped]
    public virtual Supplier? Supplier { get; set; }

    public long? PurchaseOrderId { get; set; }

    [NotMapped]
    public virtual PurchaseOrder? PurchaseOrder { get; set; }


    [Attr(ColWidth = 100)]
    public string? IBAN { get; set; }

    public decimal? Balance_val { get; set; } // Balance

}

// Code-Player-Start-DTO //
public class BankAccountDTO
{
    public long? BankId { get; set; }
    public string? BankName { get; set; }
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
    public long? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public long? SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public long? PurchaseOrderId { get; set; }
    public string? PurchaseOrderName { get; set; }
    public string? IBAN { get; set; }
    public decimal? Balance_val { get; set; }
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