
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using static Aspire.Data.BaseEntity;

namespace Aspire.Data;

public class TeamMemberAvatarDTO
{
    public string? Name { get; set; }
    public string? PhotoUrl { get; set; }
}


[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
public class Attr : Attribute
{
    // See the attribute guidelines at http://go.microsoft.com/fwlink/?LinkId=85236
    public string NameAr { get; set; } = "";
    public string Description { get; set; } = "";
    public string DescriptionAr { get; set; } = "";
    public string DomainFolder { get; set; } = "APP";
    public string Abbrv { get; set; } = "";
    public bool Id { get; set; } = false;
    public bool Service { get; set; } = false;
    public bool ShowProgress { get; set; } = false;
    public bool ShowAvatar { get; set; } = false;
    public bool ShowStakeholders { get; set; } = false;
    public bool WithoutUI { get; set; } = false;
    public bool List { get; set; } = false;
    public bool Card { get; set; } = false;
    public bool Tree { get; set; } = false;
    public bool Group { get; set; } = false;
    public bool SubList { get; set; } = false;
    public bool SubCard { get; set; } = false;
    public bool SubTree { get; set; } = false;
    public bool SubGroup { get; set; } = false;
    public bool Lookup { get; set; } = false;
    public bool Trans { get; set; } = false;

    public string[]? DisplayValues { get; set; } = null;
    public string[]? SummaryValues { get; set; } = null;
    public string[]? GroupBy { get; set; } = null;
    public string[]? FilterBy { get; set; } = null;
    public string[]? Aggregate { get; set; } = null;
    public string[]? Cols { get; set; } = null;
    public string[]? Actions { get; set; } = null;
    public string[]? LICols { get; set; } = null;
    public string[]? LIActions { get; set; } = null;
    public string[]? PreventDelete { get; set; } = null;
    public string[]? Menu { get; set; } = null;

    public string DisplayKey = "Name";
    public bool Parent { get; set; } = false;
    public string Section { get; set; } = "";
    public int ColWidth { get; set; } = 0;
    public string ColTitle { get; set; } = "";
    public Flr Filtration { get; set; } = Flr.None;
    public Frm Form { get; set; } = Frm.Basic;
    public string Aggregation { get; set; } = Agg.None;
    public bool Key { get; set; } = false;
    public bool Required { get; set; } = false;
    public bool Unique { get; set; } = false;
    public string Text { get; set; } = null;
}

public enum Flr
{
    None, Multi, Single, Range
}

public enum Frm
{
    Basic, Detailed, Calc, ReadOnly, Hidden
}

public static class Agg
{
    public const string None = "";
    public const string Count = "Count";
    public const string Sum = "Sum";
    public const string Avg = "Avg";
    public const string Min = "Min";
    public const string Max = "Max";
}

public interface IAuditableEntity
{
    public long Id { get; set; }
    public long? TenantId { get; set; }
    long? CreatedByUsrId { get; set; }
    DateTime? CreateDate { get; set; }
    long? UpdatedByUsrId { get; set; }
    DateTime? UpdateDate { get; set; }
}

public abstract class DatabaseEntity : IAuditableEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Attr(Form = Frm.Hidden)]
    public long Id { get; set; }

    [Attr(Form = Frm.Hidden)]
    public string? No { get; set; }

    [Attr(Form = Frm.Hidden)]
    public long? TenantId { get; set; }

    [NotMapped]
    public virtual Tenant? Tenant { get; set; }

    [Attr(Form = Frm.Hidden)]
    public bool? IsActive { get; set; }

    [Attr(Form = Frm.Hidden)]
    public string? Msg { get; set; }

    [Attr(Form = Frm.Hidden)]
    public string? ActionComment { get; set; }

    [Attr(Form = Frm.Hidden)]
    public string? PhotoUrl { get; set; }


    [Attr(Form = Frm.Hidden)]
    public long? CreatedByUsrId { get; set; }

    [Attr(Form = Frm.Hidden)]
    public long? UpdatedByUsrId { get; set; }

    [Attr(Form = Frm.Hidden)]
    public DateTime? CreateDate { get; set; }

    [Attr(Form = Frm.Hidden)]
    public DateTime? UpdateDate { get; set; }
}

public abstract class CoreEntity : DatabaseEntity
{ // Organization - Currency - Language - Customer/Supplier - EntityStatus - EntityType - Country - Marital - Relation - Gender
    public string? Name { get; set; }
    public string? NameAr { get; set; }
    public string? Description { get; set; }
    public string? DescriptionAr { get; set; }

    public CoreEntity () { }
}

[Attr(Lookup = true)]
public abstract class BaseLookupEntity : CoreEntity
{

}


public abstract class BaseEntity : CoreEntity
{
    public long? EntityStatusId { get; set; }
    public long? EntityTypeId { get; set; }
    public long? DataConfidentialityId { get; set; }

    public BaseEntity() { }
}

public abstract class MigratedEntity : BaseEntity
{
    public string? OldKey { get; set; }

}

public abstract class MigratedCompanyEntity : CompanyEntity
{
    public string? OldKey { get; set; }

}

public abstract class XREFEntity : DatabaseEntity
{ // All Cross-Reference Entities
    public XREFEntity() { }
}

public abstract class BaseEntityLinked : BaseEntity
{ // WorkItem - EntityAttachments - EntityComments - EntityWorkflow - LineItems
    public string? EntityName { get; set; } // Entity Name {ex: Project, Company, Deal, etc.}
    public long? ObjectId { get; set; } // The Id of specific record in the entity
}

public abstract class EntityWithLineItems : BaseEntity
{ // Deal -> Pnl -> Quote -> Agreement -> Project -> Program -> Protofolio, CR, PO, SO, BillingPlan, InvoiceAR, InvoiceAP, CreditNote, DebitNote
    public decimal calc_TotalCost_val { get; set; } = 0; // Sum of Line Items
    public decimal calc_TotalPrice_val { get; set; } = 0; // Sum of Line Items
    public decimal calc_Discount_val { get; set; } = 0; // Sum of Line Items
    public decimal calc_ManagementBuffer_val { get; set; } = 0; // Sum of Line Items
    public decimal calc_Amount_val { get; set; } = 0; // Sum of Line Items
    public long? Amount_cur { get; set; } // Currency Id
    public decimal calc_Amount_tax { get; set; } = 0; // Sum of Line Items
    public decimal calc_Amount_tot { get; set; } = 0; // (Final Amount + Tax Amount) OR Sum of Line Items
    public decimal calc_Budget_val { get; set; } = 0; // (Total Cost - Management Buffer) OR Sum of Line Items
    public decimal calc_PlannedGP_val { get; set; } = 0; // (Amount_val - Budget) OR Sum of Line Items
    public decimal calc_ActualCost_val { get; set; } = 0; // Sum of Line Items
    public decimal calc_ActualGP_val { get; set; } = 0; // Sum of Line Items

    public decimal CostToComplete_val { get; set; } // Budget_val - ActualCost_val
    public decimal calc_RecgnzdRevn_val { get; set; } // The revenue the company has fulfilled its obligations on, and there is reasonable expectation of payment (Should be Billed)
    public decimal calc_UnRecgnzdRevn_val { get; set; } // Amount_val - RecognizedRevenue_val
    public decimal calc_Billed_val { get; set; } // Sum of Invoices (Billed)
    public decimal calc_UnBilled_val { get; set; } // RecgnzdRevn_val - Billed_val    
    public decimal calc_DueInv_val { get; set; } // Sum of Invoices (Due)
    public decimal calc_Collected_val { get; set; } // Sum of Invoices (Collected)    
    public decimal calc_UnCollected_val { get; set; } // Billed_val - Collected_val    
    public decimal calc_Paid_val { get; set; } = 0; // Sum of Line Items
    public decimal calc_UnPaid_val { get; set; } = 0; // Sum of Line Items
    public decimal calc_Planned_val { get; set; } // Sum of Line Items
    public decimal calc_UnPlanned_val { get; set; }  // Sum of Line Items


    // Will be Calc'd
    public decimal calc_Discount_prc { get; set; } = 0; // Discount_val / TotalPrice_val
    public decimal calc_ManagementBuffer_prc { get; set; } = 0; // ManagementBuffer_val / TotalPrice_val
    public decimal calc_PlannedGP_prc { get; set; } // PlannedGP_val / Budget_val
    public decimal calc_ActualGP_prc { get; set; } = 0; // ActualGP_val / Budget_val
}

public interface IEntityWithLineItems
{
    public decimal calc_TotalPrice_val { get; set; }
    public decimal calc_TotalCost_val { get; set; }
    public decimal calc_Discount_val { get; set; }
    public decimal calc_ManagementBuffer_val { get; set; }
    public long? Amount_cur { get; set; } // Currency Id
    public decimal calc_Amount_val { get; set; }
    public long? Amount_rat { get; set; } // Tax Rate Id
    public decimal calc_Amount_tax { get; set; }
    public decimal calc_Amount_tot { get; set; }
    public decimal calc_Budget_val { get; set; }
    public decimal calc_PlannedGP_val { get; set; }
    public decimal calc_ActualCost_val { get; set; }
    public decimal calc_ActualGP_val { get; set; }
    public decimal CostToComplete_val { get; set; }
    public decimal calc_RecgnzdRevn_val { get; set; }
    public decimal calc_UnRecgnzdRevn_val { get; set; }
    public decimal calc_Billed_val { get; set; }
    public decimal calc_UnBilled_val { get; set; }
    public decimal calc_DueInv_val { get; set; }
    public decimal calc_Collected_val { get; set; }
    public decimal calc_UnCollected_val { get; set; }
    public decimal calc_Planned_val { get; set; }
    public decimal calc_UnPlanned_val { get; set; }


    // Will be Calc'd
    public decimal calc_Discount_prc { get; set; }
    public decimal calc_PlannedGP_prc { get; set; }
    public decimal calc_ActualGP_prc { get; set; }
    public decimal calc_ManagementBuffer_prc { get; set; }

}

public class CalcSales
{
    public long? Amount_cur { get; set; } // Currency Id
    public decimal? TotalPrice_val { get; set; }
    public decimal? TotalCost_val { get; set; }
    public decimal? Discount_val { get; set; }
    public decimal? ManagementBuffer_val { get; set; }
    public decimal? Amount_val { get; set; }
    public long? Amount_rat { get; set; } // Tax Rate Id
    public decimal? Amount_tax { get; set; }
    public decimal? Amount_tot { get; set; }
    public decimal? Budget_val { get; set; }
    public decimal? GP_val { get; set; }
    public decimal? ActualCost_val { get; set; }
    public decimal? ActualGP_val { get; set; }
    public decimal? CostToComplete_val { get; set; }
    public decimal? RecgnzdRevn_val { get; set; }
    public decimal? UnRecgnzdRevn_val { get; set; }
    public decimal? Billed_val { get; set; }
    public decimal? UnBilled_val { get; set; }
    public decimal? DueInv_val { get; set; }
    public decimal? Collected_val { get; set; }
    public decimal? UnCollected_val { get; set; }
    public decimal? Planned_val { get; set; }
    public decimal? UnPlanned_val { get; set; }


    // Will be Calc'd
    public double? calc_Discount_prc { get; set; }
    public decimal? calc_PlannedGP_prc { get; set; }
    public decimal? calc_ActualGP_prc { get; set; }
    public double? calc_ManagementBuffer_prc { get; set; }

}

public class CompanyEntity : BaseEntity
{
    public long? AccountManagerUsrId { get; set; } // UserId
    public long? CurrencyId { get; set; }
    public string? Domain { get; set; }
    public string? Website { get; set; }
    public string? CRNo { get; set; }
    public string? VATRegNo { get; set; }
    public string? RegistrationNo { get; set; }
    public long? IndustryId { get; set; }    
    public long? SectorId { get; set; }    
    public int? Employees_num { get; set; }
    public decimal? AnnualRevenue_val { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }


    [Attr(Section = "Address")]
    public long? CountryId { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Address { get; set; }
    public string? Street { get; set; }
    public string? ZipCode { get; set; }
}

public abstract class ServiceEntity : BaseEntity
{
    public JsonDocument? VueFlowExec { get; set; }

    public long? UrgencyId { get; set; }

    [NotMapped]
    public virtual Urgency? Urgency { get; set; }

    public long? RequestForUsrId { get; set; }

    [NotMapped]
    public virtual ApplicationUser? RequestForUsr { get; set; }

    public long? ActingForUsrId { get; set; }

    [NotMapped]
    public virtual ApplicationUser? ActingForUsr { get; set; }
}


public abstract class IncidentEntity : ServiceEntity
{
    public long? ImpactId { get; set; }

    [NotMapped]
    public virtual Impact? Impact { get; set; }


    public long? SeverityId { get; set; }

    [NotMapped]
    public virtual Severity? Severity { get; set; }
}

public class EntityAbbrvDTO
{
    public string? Abbrv { get; set; }
}