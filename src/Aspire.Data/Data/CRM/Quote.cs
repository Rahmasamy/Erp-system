using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "CRM", Abbrv = "QT", List = true, SubList = true, Card = true, Tree = true, 
    Actions = new string[] { "Send-to-Customer:Dlg:dollar", "Accepted-By-Customer:Dlg:dollar" },
    GroupBy = new string[] { "CustomerId", "DealId" },
    FilterBy = new string[] { "CustomerId", "DealId", "QuoteDate" },
    Cols = new string[] { "CustomerId", "AccountManagerUsrId", "IssueDate", "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_prc" },
    LICols = new string[] { "ProductId", "UnitPrice_val", "Quantity_dec", "calc_TotalPrice_val", "Discount_val", "calc_Amount_val", "calc_Amount_tax", "calc_Amount_tot" },
    LIActions = new string[] { "Create_Sales_Order:CRM_SalesOrder:dollar" },
    Aggregate = new string[] { "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val" },
    DisplayValues = new string[] { "CustomerId", "DealId", "QuoteDate", "calc_Amount_val", "calc_PlannedGP_prc" },
    SummaryValues = new string[] { "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val", "calc_RecgnzdRevn_val", "calc_Billed_val", "calc_UnBilled_val", "calc_DueInv_val" },
    Menu = new string[] {
        "LineItems?CRM_Quote_LineItem"
    })]
public class Quote : EntityWithLineItems
{
    public long? AccountManagerUsrId { get; set; } // Should Follw Deal AM
    public DateTime? IssueDate { get; set; }

    public long? CurrencyId { get; set; } // Should Follw Deal Currency
    public long? CustomerId { get; set; }
    public long? DealId { get; set; }
    public long? PnlId { get; set; }
    public virtual Pnl? Pnl { get; set; }


    public Quote() { }
    public Quote(long usrId, long tnantId, List<EntityType> EntTyps, List<EntityStatus> EntSts, List<Customer> Custs, List<ApplicationUser> Users, string name, string? nameAr, long? dealId, long? pnlId)
    {
        Random rnd = new Random();

        this.AccountManagerUsrId = usrId;
        TenantId = tnantId;
        
        Name = name;
        NameAr = nameAr;
        PnlId = pnlId;
        DealId = dealId;

        CustomerId = Custs != null && Custs.Any() ? Custs[rnd.Next(0, Custs.Count)].Id : null;
        var quoteEntTyps = EntTyps?.Where(x => x.EntityName == "Quote").ToList();
        EntityTypeId = quoteEntTyps != null && quoteEntTyps.Any() ? quoteEntTyps[rnd.Next(0, quoteEntTyps.Count)].Id : EntTyps?.FirstOrDefault()?.Id;
        var quoteEntSts = EntSts?.Where(x => x.EntityName == "Quote").ToList();
        EntityStatusId = quoteEntSts != null && quoteEntSts.Any() ? quoteEntSts[rnd.Next(0, quoteEntSts.Count)].Id : EntSts?.FirstOrDefault()?.Id;

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
public class QuoteDTO
{
    public long? AccountManagerUsrId { get; set; }
    public string? AccountManagerUsrName { get; set; }
    public string? AccountManagerUsrPhotoUrl { get; set; }
    public DateTime? IssueDate { get; set; }
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
    public long? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public long? DealId { get; set; }
    public string? DealName { get; set; }
    public long? PnlId { get; set; }
    public string? PnlName { get; set; }
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