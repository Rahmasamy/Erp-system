
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Aspire.Data;

[Attr(DomainFolder = "FIN", Abbrv = "DNP", SubList = true, List = true, Id = true,
    Menu = new string[] {
        "LineItems?FIN_DebitNoteAP_LineItem"
    })]
public class DebitNoteAP : EntityWithLineItems
{
    // Debit Note increases the amount a customer [My Organization] owes for Vendor Invoice (INV-AP),
    // Typically issued by a vendor to correct an undercharged invoice or to request payment for goods previously on credit.The primary difference is that a credit note decreases a liability, and a debit note increases it
    public long? SupplierId { get; set; }
    [NotMapped]
    public virtual Supplier? Supplier { get; set; }

    public long? InvoiceAPId { get; set; }

    [NotMapped]
    public virtual InvoiceAP? InvoiceAP { get; set; }


    public long? PaymentTermId { get; set; } // If NULL => Due Date to be manually identified, otherwise it will be calculated based on the selected Payment Term

    [NotMapped]
    public virtual PaymentTerm? PaymentTerm { get; set; }
    public DateTime? IssueDate { get; set; } // Creation Date
    public DateTime? DueDate { get; set; } // Due Date Based on the Creation Date + Terms & Conditions
    public DateTime? SubmissionDate { get; set; } // Submission
    public DateTime? PaymentDate { get; set; } // Collection Based on Bank Info
}

// Code-Player-Start-DTO //
public class DebitNoteAPDTO
{
    public long? SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public long? InvoiceAPId { get; set; }
    public string? InvoiceAPName { get; set; }
    public long? PaymentTermId { get; set; }
    public string? PaymentTermName { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? SubmissionDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public decimal? calc_TotalCost_val { get; set; }
    public decimal? calc_TotalPrice_val { get; set; }
    public decimal? calc_Discount_val { get; set; }
    public decimal? calc_ManagementBuffer_val { get; set; }
    public decimal? calc_Amount_val { get; set; }
    public long? Amount_cur { get; set; }
    public decimal? calc_Amount_tax { get; set; }
    public decimal? calc_Amount_tot { get; set; }
    public decimal? calc_Budget_val { get; set; }
    public decimal? calc_PlannedGP_val { get; set; }
    public decimal? calc_ActualCost_val { get; set; }
    public decimal? calc_ActualGP_val { get; set; }
    public decimal? CostToComplete_val { get; set; }
    public decimal? calc_RecgnzdRevn_val { get; set; }
    public decimal? calc_UnRecgnzdRevn_val { get; set; }
    public decimal? calc_Billed_val { get; set; }
    public decimal? calc_UnBilled_val { get; set; }
    public decimal? calc_DueInv_val { get; set; }
    public decimal? calc_Collected_val { get; set; }
    public decimal? calc_UnCollected_val { get; set; }
    public decimal? calc_Paid_val { get; set; }
    public decimal? calc_UnPaid_val { get; set; }
    public decimal? calc_Planned_val { get; set; }
    public decimal? calc_UnPlanned_val { get; set; }
    public decimal? calc_Discount_prc { get; set; }
    public decimal? calc_ManagementBuffer_prc { get; set; }
    public decimal? calc_PlannedGP_prc { get; set; }
    public decimal? calc_ActualGP_prc { get; set; }
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