
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Aspire.Data;

[Attr(DomainFolder = "FIN", Abbrv = "DNR", SubList = true, List = true, Id = true,
    Actions = new string[] { "Billed:Dlg:dollar", "Revoke-Billing:Dlg:dollar", "Submitted:Dlg:dollar", "Revoke-Submission:Dlg:dollar", "Collected:Dlg:dollar", "Revoke-Collection:Dlg:dollar" },
    GroupBy = new string[] { "CustomerId", "AccountManagerUsrId", "SalesOrderId" },
    FilterBy = new string[] { "CustomerId", "AccountManagerUsrId", "SalesOrderId", "IssueDate", "DueDate" },
    Cols = new string[] { "CustomerId", "AccountManagerUsrId", "SalesOrderId", "calc_Amount_val", "calc_Billed_val", "calc_Collected_val", "InvoiceDate", "DueDate" },
    LICols = new string[] { "ProductId", "UnitPrice_val", "Quantity_dec", "calc_TotalPrice_val", "Discount_val", "calc_Amount_val", "calc_Amount_tax", "calc_Amount_tot" },
    Aggregate = new string[] { "calc_Amount_val", "calc_Collected_val", "calc_UnCollected_val" },
    DisplayValues = new string[] { "CustomerId", "AccountManagerUsrId", "calc_Amount_val" },
    SummaryValues = new string[] { "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val", "calc_RecgnzdRevn_val", "calc_Billed_val", "calc_UnBilled_val", "calc_DueInv_val" },
    Menu = new string[] {
        "LineItems?FIN_DebitNoteAR_LineItem"
    })]
public class DebitNoteAR : EntityWithLineItems
{
    // Debit Note increases the amount a buyer [My Customer] owes for Customer Invoice (INV-AR),
    // Typically, issued by a buyer [My Customer] for receiving additional goods or services than what was on the invoice, though a supplier may issue a debit note to rectify an undercharge. 

    public long? AccountManagerUsrId { get; set; } // Should Follw Deal AM
    public DateTime? IssueDate { get; set; }
    public long? CustomerId { get; set; }
    public long? SalesOrderId { get; set; }
    public long? InvoiceARId { get; set; }
    public long? PaymentTermId { get; set; } // If NULL => Due Date to be manually identified, otherwise it will be calculated based on the selected Payment Term
    public DateTime? DueDate { get; set; } // Due Date Based on the Creation Date + Terms & Conditions
    public DateTime? SubmissionDate { get; set; } // Submission
    public DateTime? PaymentDate { get; set; } // Collection Based on Bank Info


    public DebitNoteAR() { }
    public DebitNoteAR(long usrId, long tnantId, string name, long? projId, decimal? amount, long? curId, long? ratId, long? supId, long? POId)
    {
        this.AccountManagerUsrId = usrId;
        Random rnd = new Random();

        TenantId = tnantId;
        
        this.Name = name;
        CustomerId = supId;
        calc_Amount_val = rnd.Next(1, 99999);
        calc_Amount_tax = calc_Amount_val * 15 / 100;
        calc_Amount_tot = calc_Amount_val * 115 / 100;
        Amount_cur = curId;
        IssueDate = new DateTime(2026, 1, 17);
        DueDate = new DateTime(2026, 4, 17);
    }
}

// Code-Player-Start-DTO //
public class DebitNoteARDTO
{
    public long? AccountManagerUsrId { get; set; }
    public string? AccountManagerUsrName { get; set; }
    public string? AccountManagerUsrPhotoUrl { get; set; }
    public DateTime? IssueDate { get; set; }
    public long? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public long? SalesOrderId { get; set; }
    public string? SalesOrderName { get; set; }
    public long? InvoiceARId { get; set; }
    public string? InvoiceARName { get; set; }
    public long? PaymentTermId { get; set; }
    public string? PaymentTermName { get; set; }
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