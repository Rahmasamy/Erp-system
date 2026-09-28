using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "SRM", Abbrv = "PO", SubList = true, List = true, Card = true, Tree = true,
    Actions = new string[] { "Create-Invoice:FIN_InvoiceAP:money-bill", "Add-Payment:FIN_PaymentAP:dollar" },
    GroupBy = new string[] { "SupplierId", "PortfolioId" },
    FilterBy = new string[] { "SupplierId", "PortfolioId" },
    DisplayValues = new string[] { "SupplierId", "AccountManagerUsrId", "ProjectId", "calc_Amount_val" },
    Cols = new string[] { "SupplierId", "AccountManagerUsrId", "IssueDate", "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_prc" },
    LICols = new string[] { "ProductId", "Quantity_dec", "calc_Amount_val" },
    Aggregate = new string[] { "calc_Amount_val", "calc_Paid_val", "calc_UnPaid_val" },
    SummaryValues = new string[] { "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val", "calc_RecgnzdRevn_val", "calc_Billed_val", "calc_UnBilled_val", "calc_DueInv_val" },
    Menu = new string[] {
        "LineItems?FIN_PurchaseOrder_LineItem",
        "Invoices?FIN_InvoiceAP_ListSub"
    })]
public class PurchaseOrder : EntityWithLineItems
{
    public long? AccountManagerUsrId { get; set; }
    public DateTime? IssueDate { get; set; } // Creation Date
    public long? SupplierId { get; set; }
    public long? PortfolioId { get; set; }
    public long? ProjectId { get; set; }
    public long? PaymentTermId { get; set; }
    public long? ShippingMethodId { get; set; }
    public DateTime? RequiredDeliveryDate { get; set; } // Creation Date

    public PurchaseOrder() { }
    public PurchaseOrder(long usrId, long tnantId, string name, long? projId, decimal? amount, long? curId, long? ratId, long? supId, long? pfId)
    {
        this.AccountManagerUsrId = usrId;
        TenantId = tnantId;
        
        this.Name = name;
        this.ProjectId = projId;
        PortfolioId = pfId;
        SupplierId = supId;

        Random rnd = new Random();
        IssueDate = DateTime.Today.AddDays(rnd.Next(10, 1000) * -1);
    }
}

// Code-Player-Start-DTO //
public class PurchaseOrderDTO
{
    public long? AccountManagerUsrId { get; set; }
    public string? AccountManagerUsrName { get; set; }
    public string? AccountManagerUsrPhotoUrl { get; set; }
    public DateTime? IssueDate { get; set; }
    public long? SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public long? PortfolioId { get; set; }
    public string? PortfolioName { get; set; }
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public long? PaymentTermId { get; set; }
    public string? PaymentTermName { get; set; }
    public long? ShippingMethodId { get; set; }
    public string? ShippingMethodName { get; set; }
    public DateTime? RequiredDeliveryDate { get; set; }
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