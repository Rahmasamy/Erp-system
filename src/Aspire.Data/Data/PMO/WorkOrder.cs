using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "PMO", Abbrv = "WO", SubList = true, List = true, Tree = true, Card = true, ShowStakeholders = true,
    Actions = new string[] { "Terminate:dollar,On_Hold:dollar,Completed:Dlg:dollar" },
    GroupBy = new string[] { "CustomerId", "AgreementId", "PortfolioId", "ProgrammeId", "ProjectManagerId", "AccountManagerId" },
    FilterBy = new string[] { "CustomerId", "AgreementId", "PortfolioId", "ProgrammeId", "ProjectManagerId", "AccountManagerId" },
    Cols = new string[] { "ProjectManagerId", "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val", "StartDate", "EndDate" },
    LICols = new string[] { "ProductId", "Quantity_dec", "calc_Amount_val", "Progress_prc", "calc_RecgnzdRevn_prc" },
    Aggregate = new string[] { "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val", "calc_ActualCost_val", "CostToComplete_val", "calc_ActualGP_val", "calc_RecgnzdRevn_val", "calc_UnRecgnzdRevn_val", "calc_Billed_val", "calc_UnBilled_val", "calc_DueInv_val", "calc_Collected_val", "calc_UnCollected_val", "calc_Planned_val", "calc_UnPlanned_val" },
    DisplayValues = new string[] { "CustomerName", "ProjectManagerName", "MarketUnitName", "AgreementName", "calc_Amount_val", "StartDate", "EndDate" },
    Menu = new string[] {
        "LineItems?PMO_WorkOrder_LineItem",
        "Scope?FIN_SalesOrder_ListSub:PMO_WorkItem_ListSub:PMO_Deliverable_ListSub",
    })]
public class WorkOrder : EntityWithLineItems
{
    public DateTime? IssueDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public long? AgreementId { get; set; }

    [NotMapped]
    public virtual Agreement? Agreement { get; set; }

    [Attr(Form = Frm.Basic, Filtration = Flr.Multi, ColWidth = 150)]
    public long? CustomerId { get; set; }

    [NotMapped]
    public virtual Customer? Customer { get; set; }

    public long? ProgrammeId { get; set; }
    [NotMapped]
    public virtual Programme? Programme { get; set; }

    public long? PortfolioId { get; set; }

    [NotMapped]
    public virtual Portfolio? Portfolio { get; set; }

    public long? ProjectManagerId { get; set; } // UserId

    [NotMapped]
    public virtual ApplicationUser? ProjectManager { get; set; }


    public long? AccountManagerId { get; set; } // UserId

    [NotMapped]
    public virtual ApplicationUser? AccountManager { get; set; }

    public long? PMOAuditorId { get; set; }

    [NotMapped]
    public virtual ApplicationUser? PMOAuditor { get; set; }

    public WorkOrder() { }
    public WorkOrder(long tnantId, string name, string? nameAr, long? cusId, long? agreementId, decimal amount, long? curId, DateTime startDate, DateTime endDate)
    {
        TenantId = tnantId;
        
        Name = name;
        NameAr = nameAr;
        CustomerId = cusId;
        AgreementId = agreementId;
        StartDate = startDate;
        EndDate = endDate;
    }
}

// Code-Player-Start-DTO //
public class WorkOrderTeamDTO : WorkOrderDTO
{
    public List<TeamMemberAvatarDTO> Stakeholders { get; set; } = new List<TeamMemberAvatarDTO>();
}

public class WorkOrderDTO
{
    public DateTime? IssueDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public long? AgreementId { get; set; }
    public string? AgreementName { get; set; }
    public long? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public long? ProgrammeId { get; set; }
    public string? ProgrammeName { get; set; }
    public long? PortfolioId { get; set; }
    public string? PortfolioName { get; set; }
    public long? ProjectManagerId { get; set; }
    public string? ProjectManagerName { get; set; }
    public long? AccountManagerId { get; set; }
    public string? AccountManagerName { get; set; }
    public long? PMOAuditorId { get; set; }
    public string? PMOAuditorName { get; set; }
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