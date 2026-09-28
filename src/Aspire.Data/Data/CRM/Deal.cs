using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "CRM", Abbrv = "DL", SubList = true, List = true, Tree = true, Card = true, ShowStakeholders = true, ShowProgress = true,
    Actions = new string[] { "Move-to-Next-Stage:Dlg:dollar", "Create-Pnl:Dlg:dollar", "Won:Dlg:dollar", "Lost:Dlg:dollar", "Convert_to_Agreement:CRM_Agreement:dollar" },
    GroupBy = new string[] { "CustomerId", "PipelineId", "AccountManagerUsrId" },
    FilterBy = new string[] { "CustomerId", "PipelineId" },
    Cols = new string[] { "CustomerId", "AccountManagerUsrId", "PipelineId", "StageId", "calc_Amount_val", "calc_PipelineAmount_val" },
    LICols = new string[] { "ProductId", "UnitPrice_val", "Quantity_dec", "calc_TotalPrice_val", "Discount_val", "calc_Amount_val"},
    LIActions = new string[] { "Create_Pnl:CRM_Pnl:dollar" },
    Aggregate = new string[] { "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val", "calc_PipelineAmount_val" },
    DisplayValues = new string[] { "CustomerId", "PipelineId", "calc_Amount_val", "StartDate", "EndDate" },
    SummaryValues = new string[] { "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val", "calc_RecgnzdRevn_val", "calc_Billed_val", "calc_UnBilled_val", "calc_DueInv_val" },
    Menu = new string[] {
        "LineItems?CRM_Deal_LineItem",
        "Pnls?CRM_Pnl_ListSub",
        "Quotations?CRM_Quote_ListSub"
    })]
public class Deal : EntityWithLineItems
{
    public long? AccountManagerUsrId { get; set; } // UserId
    public DateTime? IssueDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public long? CustomerId { get; set; }
    public long? CurrencyId { get; set; }
    public long? PipelineId { get; set; }
    public long? StageId { get; set; }
    public long? ConfidenceId { get; set; }


    [Attr(Section = "Etimad Information")]
    public bool? IsEtimad { get; set; }
    public string? EtimadNo { get; set; }
    public string? BidRefNo { get; set; }
    public string? AwardLetterRefNo { get; set; }
    public string? ContractRefNo { get; set; }
    public string? PORefNo { get; set; }
    public bool? IsLG { get; set; }
    public string? LGRefNo { get; set; }
    public decimal? LGValue_dec { get; set; }
    public DateTime? LGExpiryDate { get; set; }
    public int? PaymentDays_num { get; set; }

    public decimal calc_PipelineAmount_val { get; set; } = 0; // Calculated Amount Based on Stage & Confidence



    [Attr(Section = "Notes")]
    public string? TechnicalNotes { get; set; }
    public string? FinantialNotes { get; set; }
    public string? AccountNotes { get; set; }
    public string? LostNotes { get; set; }


    [Attr(Section = "Closure")]
    public DateTime? CloseDate { get; set; }
    public DateTime? AwardDate { get; set; }
    public decimal CompetitorWonAmount_val { get; set; } = 0;
    public long? CompetitorId { get; set; }
    public long? DealLostReasonId { get; set; }

    public Deal() { }
    public Deal(long usrId, long tnantId, List<EntityType> EntTyps, List<EntityStatus> EntSts, List<Customer> Custs, List<ApplicationUser> Users, Pipeline PL, Stage ST,  string name, string? nameAr)
    {
        Random rnd = new Random();

        this.AccountManagerUsrId = usrId;
        TenantId = tnantId;
        
        Name = name;
        NameAr = nameAr;
        PipelineId = PL?.Id;
        StageId = ST?.Id;

        CustomerId = Custs != null && Custs.Any() ? Custs[rnd.Next(0, Custs.Count)].Id : null;
        if (Users != null && Users.Any())
        {
            AccountManagerUsrId = Users[rnd.Next(0, Users.Count)].Id;
        }
        var dealEntTyps = EntTyps?.Where(x => x.EntityName == "Deal").ToList();
        EntityTypeId = dealEntTyps != null && dealEntTyps.Any() ? dealEntTyps[rnd.Next(0, dealEntTyps.Count)].Id : EntTyps?.FirstOrDefault()?.Id;
        var dealEntSts = EntSts?.Where(x => x.EntityName == "Deal").ToList();
        EntityStatusId = dealEntSts != null && dealEntSts.Any() ? dealEntSts[rnd.Next(0, dealEntSts.Count)].Id : EntSts?.FirstOrDefault()?.Id;

        StartDate = DateTime.Today.AddDays(-100);
        EndDate = DateTime.Today.AddDays(50);

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
public class DealTeamDTO : DealDTO
{
    public List<TeamMemberAvatarDTO> Stakeholders { get; set; } = new List<TeamMemberAvatarDTO>();
}

public class DealDTO
{
    public long? AccountManagerUsrId { get; set; }
    public string? AccountManagerUsrName { get; set; }
    public string? AccountManagerUsrPhotoUrl { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public long? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
    public long? PipelineId { get; set; }
    public string? PipelineName { get; set; }
    public long? StageId { get; set; }
    public string? StageName { get; set; }
    public long? ConfidenceId { get; set; }
    public string? ConfidenceName { get; set; }
    public bool? IsEtimad { get; set; }
    public string? EtimadNo { get; set; }
    public string? BidRefNo { get; set; }
    public string? AwardLetterRefNo { get; set; }
    public string? ContractRefNo { get; set; }
    public string? PORefNo { get; set; }
    public bool? IsLG { get; set; }
    public string? LGRefNo { get; set; }
    public decimal? LGValue_dec { get; set; }
    public DateTime? LGExpiryDate { get; set; }
    public int? PaymentDays_num { get; set; }
    public decimal? calc_PipelineAmount_val { get; set; }
    public string? TechnicalNotes { get; set; }
    public string? FinantialNotes { get; set; }
    public string? AccountNotes { get; set; }
    public string? LostNotes { get; set; }
    public DateTime? CloseDate { get; set; }
    public DateTime? AwardDate { get; set; }
    public decimal? CompetitorWonAmount_val { get; set; }
    public long? CompetitorId { get; set; }
    public string? CompetitorName { get; set; }
    public long? DealLostReasonId { get; set; }
    public string? DealLostReasonName { get; set; }
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









