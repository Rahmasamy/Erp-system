using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "FIN", Abbrv = "PAY-AR", SubList = true, List = true,
    DisplayValues = new string[] { "NetAmount_val" }
    )]
public class PaymentAR : BaseEntity
{
    [Attr(ColWidth = 150, Filtration = Flr.Multi, Form = Frm.Basic)]
    public long? CustomerId { get; set; }

    [NotMapped]
    public virtual Customer? Customer { get; set; }


    public decimal Amount_val { get; set; } = 0;// Net Amount
    public long? Amount_cur { get; set; } // Currency Id
    public long? Amount_rat { get; set; } // Tax Rate Id
    public decimal? Amount_tax { get; set; } // Tax Amount
    public decimal? Amount_tot { get; set; } // Amount Including Tax


    public DateTime? PaymentDate { get; set; } // Collection Based on Bank Info


    [Attr(ColWidth = 150, Filtration = Flr.Multi, Form = Frm.Basic)]
    public long? BankId { get; set; }

    [NotMapped]
    public virtual Bank? Bank { get; set; }


    [Attr(ColWidth = 150, Filtration = Flr.Multi, Form = Frm.Basic)]
    public long? BankAccountId { get; set; }

    [NotMapped]
    public virtual BankAccount? BankAccount { get; set; }


    [Attr(ColWidth = 150, Filtration = Flr.Multi, Form = Frm.Basic)]
    public long? CurrencyId { get; set; }

    [NotMapped]
    public virtual Currency? Currency { get; set; }
}

// Code-Player-Start-DTO //
public class PaymentARDTO
{
    public long? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public decimal? Amount_val { get; set; }
    public long? Amount_cur { get; set; }
    public long? Amount_rat { get; set; }
    public decimal? Amount_tax { get; set; }
    public decimal? Amount_tot { get; set; }
    public DateTime? PaymentDate { get; set; }
    public long? BankId { get; set; }
    public string? BankName { get; set; }
    public long? BankAccountId { get; set; }
    public string? BankAccountName { get; set; }
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
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