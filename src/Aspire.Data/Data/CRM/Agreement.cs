
using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "CRM", Abbrv = "AGR", SubList = true, List = true, Card = true, Tree = true, ShowProgress = true, 
    LIActions = new string[] { "Convert_to_Project:PMO_Project:dollar" },
    GroupBy = new string[] { "AccountManagerUsrId", "CustomerId", "Customer.SectorId", "LineItem.MarketUnitId", "LineItem.ProductId" },
    Cols = new string[] { "CustomerId", "AccountManagerUsrId", "ContractRefNo", "calc_Amount_val", "StartDate", "EndDate" },
    LICols = new string[] { "ProductId", "UnitPrice_val", "Quantity_dec", "calc_TotalPrice_val", "Discount_val", "calc_Amount_val", "calc_Amount_tax", "calc_Amount_tot" },
    Aggregate = new string[] { "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val", "calc_ActualCost_val", "CostToComplete_val", "calc_ActualGP_val", "calc_RecgnzdRevn_val", "calc_UnRecgnzdRevn_val", "calc_Billed_val", "calc_UnBilled_val", "calc_DueInv_val", "calc_Collected_val", "calc_UnCollected_val", "calc_Planned_val", "calc_UnPlanned_val" },
    DisplayValues = new string[] { "CustomerId", "AccountManagerUsrId", "calc_Amount_val", "StartDate", "EndDate" },
    SummaryValues = new string[] { "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val", "calc_RecgnzdRevn_val", "calc_Billed_val", "calc_UnBilled_val", "calc_DueInv_val" },
    Menu = new string[] {
        "LineItems?CRM_Agreement_LineItem",
        "SalesOrders?FIN_SalesOrder_ListSub",
        "Projects?PMO_Project_ListSub"
    })]
public class Agreement : EntityWithLineItems
{
    public long? CustomerId { get; set; }

    public long? AccountManagerUsrId { get; set; }

    public long? CurrencyId { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public bool? IsEtimad { get; set; }
    public string? EtimadNo { get; set; }
    public string? BidRefNo { get; set; }
    public string? AwardLetterRefNo { get; set; }
    public string? ContractRefNo { get; set; }
    public string? PORefNo { get; set; }
    public bool? IsLG { get; set; }
    public string? LGRefNo { get; set; }
    public decimal LGAmount_val { get; set; } = 0;
    public DateTime? LGExpiryDate { get; set; }
    public int PaymentDays_num { get; set; } = 0;

    public Agreement() { }
    public Agreement(long usrId, long tnantId, List<EntityType> EntTyps, List<EntityStatus> EntSts, List<Customer> Custs, string? name, string? nameAr)
    {
        Random rnd = new Random();
        this.AccountManagerUsrId = usrId;
        TenantId = tnantId;
        Name = name;
        NameAr = nameAr;
        EtimadNo = "17770999";
        ContractRefNo = "C4534245";

        CustomerId = Custs != null && Custs.Any() ? Custs[rnd.Next(0, Custs.Count)].Id : null;
        var agrEntTyps = EntTyps?.Where(x => x.EntityName == "Agreement").ToList();
        EntityTypeId = agrEntTyps != null && agrEntTyps.Any() ? agrEntTyps[rnd.Next(0, agrEntTyps.Count)].Id : EntTyps?.FirstOrDefault()?.Id;
        var agrEntSts = EntSts?.Where(x => x.EntityName == "Agreement").ToList();
        EntityStatusId = agrEntSts != null && agrEntSts.Any() ? agrEntSts[rnd.Next(0, agrEntSts.Count)].Id : EntSts?.FirstOrDefault()?.Id;

        StartDate = DateTime.Today.AddDays(rnd.Next(10, 1000) * -1);
        EndDate = ((DateTime)StartDate).AddDays(rnd.Next(100, 1000));
    }
}

// Code-Player-Start-DTO //
public class AgreementDTO
{
    public long? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public long? AccountManagerUsrId { get; set; }
    public string? AccountManagerUsrName { get; set; }
    public string? AccountManagerUsrPhotoUrl { get; set; }
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool? IsEtimad { get; set; }
    public string? EtimadNo { get; set; }
    public string? BidRefNo { get; set; }
    public string? AwardLetterRefNo { get; set; }
    public string? ContractRefNo { get; set; }
    public string? PORefNo { get; set; }
    public bool? IsLG { get; set; }
    public string? LGRefNo { get; set; }
    public decimal? LGAmount_val { get; set; }
    public DateTime? LGExpiryDate { get; set; }
    public int? PaymentDays_num { get; set; }
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

