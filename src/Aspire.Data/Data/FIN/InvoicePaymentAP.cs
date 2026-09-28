using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "FIN", Abbrv = "INV-PAY-AP", SubList = true, List = true)]
public class InvoicePaymentAP : XREFEntity
{
    [Attr(ColWidth = 150, Filtration = Flr.Multi, Form = Frm.Basic)]
    public long? PaymentAPId { get; set; }

    [NotMapped]
    public virtual PaymentAP? PaymentAP { get; set; }


    [Attr(ColWidth = 150, Filtration = Flr.Multi, Form = Frm.Basic)]
    public long? InvoiceAPId { get; set; }

    [NotMapped]
    public virtual InvoiceAP? InvoiceAP { get; set; }


    [Attr(ColWidth = 150, Filtration = Flr.Multi, Form = Frm.Basic)]
    public long? LineItemId { get; set; }

    [NotMapped]
    public virtual LineItem? LineItem { get; set; }


    [Attr(ColWidth = 150, Aggregation = Agg.Sum)]
    public decimal? Amount_ap { get; set; } // Net Amount
    public long? Amount_cur { get; set; } // Currency Id
    public long? Amount_rat { get; set; } // Tax Rate Id
    public decimal? Amount_tax { get; set; } // Tax Amount
    public decimal? Amount_tot { get; set; } // Amount Including Tax


}

// Code-Player-Start-DTO //
public class InvoicePaymentAPDTO
{
    public long? PaymentAPId { get; set; }
    public string? PaymentAPName { get; set; }
    public long? InvoiceAPId { get; set; }
    public string? InvoiceAPName { get; set; }
    public long? LineItemId { get; set; }
    public string? LineItemName { get; set; }
    public decimal? Amount_ap { get; set; }
    public long? Amount_cur { get; set; }
    public long? Amount_rat { get; set; }
    public decimal? Amount_tax { get; set; }
    public decimal? Amount_tot { get; set; }
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