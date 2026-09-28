using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "CRM", Abbrv = "SO", SubList = true, List = true, Card = true, Tree = true,
    Actions = new string[] { "Create-Invoice:FIN_InvoiceAR:money-bill", "Add-Payment:FIN_PaymentAR:dollar" },
    GroupBy = new string[] { "CustomerId", "AgreementId" },
    FilterBy = new string[] { "CustomerId", "AgreementId" },
    Cols = new string[] { "CustomerId", "AccountManagerUsrId", "IssueDate", "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_prc" },
    LICols = new string[] { "ProductId", "Quantity_dec", "calc_Amount_val" },
    Aggregate = new string[] { "calc_Amount_val", "calc_Collected_val", "calc_UnCollected_val" },
    DisplayValues = new string[] { "CustomerId", "AgreementId", "calc_Amount_val" },
    SummaryValues = new string[] { "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val", "calc_RecgnzdRevn_val", "calc_Billed_val", "calc_UnBilled_val", "calc_DueInv_val" },
    LIActions = new string[] { "Issue_Invoice:FIN_InvoiceAR:dollar" },
    Menu = new string[] {
        "LineItems?FIN_SalesOrder_LineItem",
        "Invoices?FIN_InvoiceAR_ListSub"
    })]

/*
An internal document created by the seller after the customer officially accepts a quotation or issues a Purchase Order (PO).
Operational, but not yet recognized revenue. 
It tells the warehouse or operations team to allocate inventory and prepare the goods.
When the deal is confirmed but the goods have not yet been delivered or invoiced. 
It is a legally binding commitment to deliver the items.
 */

public class SalesOrder : EntityWithLineItems
{
    public long? AccountManagerUsrId { get; set; }
    public DateTime? IssueDate { get; set; }
    public long? CustomerId { get; set; }
    public long? PnlId { get; set; }
    public long? AgreementId { get; set; }
    public long? PaymentTermId { get; set; }
    public long? ShippingMethodId { get; set; }

    public long? DealId { get; set; } // Traces this Sales Order back to the deal it was created from
    public long? CustomerPOId { get; set; } // Set when created together with a Customer PO via Deal.Won

    public SalesOrder() { }
    public SalesOrder(long usrId, long tnantId, string name, long? projId, decimal? amount, long? curId, long? ratId, long? supId, long? pfId)
    {
        this.AccountManagerUsrId = usrId;
        TenantId = tnantId;
        
        this.Name = name;
        this.
        CustomerId = supId;

        Random rnd = new Random();
        IssueDate = DateTime.Today.AddDays(rnd.Next(10, 1000) * -1);

        calc_Amount_val = 1700000;
        calc_Budget_val = 1500000;
        calc_PlannedGP_val = 200000;
        calc_RecgnzdRevn_val = 120000;
        calc_Billed_val = 80000;
        calc_UnBilled_val = 20000;
        calc_DueInv_val = 30000;

    }
}

// Code-Player-Start-DTO //
public class SalesOrderDTO
{
    public long? AccountManagerUsrId { get; set; }
    public string? AccountManagerUsrName { get; set; }
    public string? AccountManagerUsrPhotoUrl { get; set; }
    public DateTime? IssueDate { get; set; }
    public long? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public long? PnlId { get; set; }
    public string? PnlName { get; set; }
    public long? AgreementId { get; set; }
    public string? AgreementName { get; set; }
    public long? PaymentTermId { get; set; }
    public string? PaymentTermName { get; set; }
    public long? ShippingMethodId { get; set; }
    public string? ShippingMethodName { get; set; }
    public long? DealId { get; set; }
    public string? DealName { get; set; }
    public long? CustomerPOId { get; set; }
    public string? CustomerPOName { get; set; }
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