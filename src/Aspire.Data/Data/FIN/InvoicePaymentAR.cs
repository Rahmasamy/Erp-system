using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "FIN", Abbrv = "INV-PAY-AR", SubList = true)]
public class InvoicePaymentAR : XREFEntity
{
    [Attr(ColWidth = 150, Filtration = Flr.Multi, Form = Frm.Basic)]
    public long? PaymentARId { get; set; }

    [NotMapped]
    public virtual PaymentAR? PaymentAR { get; set; }


    [Attr(ColWidth = 150, Filtration = Flr.Multi, Form = Frm.Basic)]
    public long? InvoiceARId { get; set; }

    [NotMapped]
    public virtual InvoiceAR? InvoiceAR { get; set; }


    [Attr(ColWidth = 150, Filtration = Flr.Multi, Form = Frm.Basic)]
    public long? LineItemId { get; set; }

    [NotMapped]
    public virtual LineItem? LineItem { get; set; }


    [Attr(ColWidth = 150, Aggregation = Agg.Sum)]
    public decimal? Amount_ar { get; set; } // Net Amount
    public long? Amount_cur { get; set; } // Currency Id
    public long? Amount_rat { get; set; } // Tax Rate Id
    public decimal? Amount_tax { get; set; } // Tax Amount
    public decimal? Amount_tot { get; set; } // Amount Including Tax


}

// Code-Player-Start-DTO //
public class InvoicePaymentARDTO
{
    public long? PaymentARId { get; set; }
    public string? PaymentARName { get; set; }
    public long? InvoiceARId { get; set; }
    public string? InvoiceARName { get; set; }
    public long? LineItemId { get; set; }
    public string? LineItemName { get; set; }
    public decimal? Amount_ar { get; set; }
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