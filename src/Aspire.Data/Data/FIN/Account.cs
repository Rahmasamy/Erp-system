using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "FIN", Abbrv = "ACC", List = true, Tree = true,
    Cols = new string[] { "Credit_Balance_dec", "Debit_Balance_dec" },
    SummaryValues = new string[] { "Credit_Balance_dec", "Debit_Balance_dec" }
    )]
public class Account : BaseEntity
{
    public long? ParentId { get; set; }

    public decimal CreditBalance_dec { get; set; } = 0;
    public decimal DebitBalance_dec { get; set; } = 0;
    public decimal NetBalance_dec { get; set; } = 0;
    public bool? IsNature { get; set; } // Debit = False & Credit = True (what side the account normally carries a balance on)
    public bool? IsNetBalanceNature { get; set; } // Debit = False & Credit = True
    public bool? IsParentAccount { get; set; }
    public bool? IsReconciliationRequired { get; set; }
    public bool? IsPostingAccount { get; set; } // IsPostingAccount = true → Can be selected in Journal Lines

    Account() { }
    public Account(long? tenantId, decimal CR, decimal DB, string name, string nameAr)
    {
        TenantId = tenantId;
        Name = name;
        NameAr = nameAr;
        CreditBalance_dec = CR;
        DebitBalance_dec = DB;
    }
}

// Code-Player-Start-DTO //
public class AccountDTO
{
    public long? ParentId { get; set; }
    public string? ParentName { get; set; }
    public decimal? CreditBalance_dec { get; set; }
    public decimal? DebitBalance_dec { get; set; }
    public decimal? NetBalance_dec { get; set; }
    public bool? IsNature { get; set; }
    public bool? IsNetBalanceNature { get; set; }
    public bool? IsParentAccount { get; set; }
    public bool? IsReconciliationRequired { get; set; }
    public bool? IsPostingAccount { get; set; }
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