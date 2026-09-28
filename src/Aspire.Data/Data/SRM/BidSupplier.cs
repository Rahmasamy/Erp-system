using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "SRM", Abbrv = "BD", SubList = true, List = true, Card = true, Tree = true,
    Actions = new string[] { "Publish:Dlg:money-bill", "Close-and-Evaluate:Dlg:money-bill", "Completed:Dlg:dollar", "Cancel:Dlg:dollar" },
    GroupBy = new string[] { "SupplierId", "PortfolioId" },
    FilterBy = new string[] { "SupplierId", "PortfolioId" },
    DisplayValues = new string[] { "SupplierId", "ProjectId", "IssueDate", "RequiredDeliveryDate", "calc_Amount_val" },
    Cols = new string[] { "SupplierId", "AgreementId", "IssueDate", "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_prc" },
    LICols = new string[] { "ProductId", "Quantity_dec", "calc_Amount_val" },
    Aggregate = new string[] { "calc_Amount_val", "calc_Paid_val", "calc_UnPaid_val" },
    Menu = new string[] {
        "LineItems?FIN_PurchaseOrder_LineItem",
        "Invoices?FIN_InvoiceAP_ListSub"
    })]
public class BidSupplier : XREFEntity
{
    public long? BidId { get; set; }
    public long? SupplierId { get; set; }
    public decimal SupplyAmount_val { get; set; }

    public BidSupplier() { }
    public BidSupplier(long BId, long sId, long tnantId, decimal amount)
    {
        BidId = BId;
        SupplierId = sId;
        TenantId = tnantId;
        
        SupplyAmount_val = amount;
    }
}

// Code-Player-Start-DTO //
// Code-Player-End-DTO //