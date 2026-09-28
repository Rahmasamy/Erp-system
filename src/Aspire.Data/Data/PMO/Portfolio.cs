using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace Aspire.Data;

[Attr(DomainFolder = "PMO", Abbrv = "PRF", List = true, Card = true, Tree = true, 
    Actions = new string[] { "Add-Project:PMO_Project:dollar", "Add-Program:PMO_Project:dollar" },
    GroupBy = new string[] { "PortfolioManagerUsrId" },
    FilterBy = new string[] { "PortfolioManagerUsrId" },
    Cols = new string[] { "PortfolioManagerUsrId", "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val", "Projects_num", "Programs_num" },
    Aggregate = new string[] { "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val", "calc_ActualCost_val", "CostToComplete_val", "calc_ActualGP_val", "calc_RecgnzdRevn_val", "calc_UnRecgnzdRevn_val", "calc_Billed_val", "calc_UnBilled_val", "calc_DueInv_val", "calc_Collected_val", "calc_UnCollected_val", "calc_Planned_val", "calc_UnPlanned_val" },
    DisplayValues = new string[] { "PortfolioManagerUsrId", "NumberOfProjects", "calc_Amount_val" },
    SummaryValues = new string[] { "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val", "calc_RecgnzdRevn_val", "calc_Billed_val", "calc_UnBilled_val", "calc_DueInv_val" },
    Menu = new string[] {
        "Programs?PMO_Programme_ListSub",
        "Projects?PMO_Project_ListSub"
    })]
public class Portfolio : EntityWithLineItems
{
    public long? PortfolioManagerUsrId { get; set; } // UserId

    [NotMapped]
    public virtual ApplicationUser? PortfolioManagerUsr { get; set; }

    public int calc_Programs_num { get; set; }

    public int calc_Projects_num { get; set; } = 0;


    public Portfolio() { }

    public Portfolio(long usrId, long tnantId, string name, string? nameAr, decimal amount, long? curId, long? ratId)
    {
        PortfolioManagerUsrId = usrId;
        TenantId = tnantId;
        
        Name = name;
        NameAr = nameAr;
        calc_Amount_val = amount;
        Amount_cur = curId;

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
public class PortfolioDTO
{
    public long? PortfolioManagerUsrId { get; set; }
    public string? PortfolioManagerUsrName { get; set; }
    public string? PortfolioManagerUsrPhotoUrl { get; set; }
    public int? calc_Programs_num { get; set; }
    public int? calc_Projects_num { get; set; }
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