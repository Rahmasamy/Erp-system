using Aspire.Utilities;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "PMO", Abbrv = "PRJ", SubList = true, List = true, Tree = true, Card = true, ShowStakeholders = true,
    Actions = new string[] { "Terminate:Dlg:dollar", "On_Hold:Dlg:dollar", "Completed:Dlg:dollar" },
    GroupBy = new string[] { "CustomerId", "AgreementId", "PortfolioId", "ProgrammeId", "ProjectManagerUsrId", "AccountManagerUsrId" },
    FilterBy = new string[] { "CustomerId", "AgreementId", "PortfolioId", "ProgrammeId", "ProjectManagerUsrd", "AccountManagerUsrId" },
    Cols = new string[] { "ProjectManagerUsrId", "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val", "StartDate", "EndDate" },
    LICols = new string[] { "ProductId", "Quantity_dec", "calc_Amount_val", "Progress_prc", "calc_RecgnzdRevn_prc" },
    Aggregate = new string[] { "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val", "calc_ActualCost_val", "CostToComplete_val", "calc_ActualGP_val", "calc_RecgnzdRevn_val", "calc_UnRecgnzdRevn_val", "calc_Billed_val", "calc_UnBilled_val", "calc_DueInv_val", "calc_Collected_val", "calc_UnCollected_val", "calc_Planned_val", "calc_UnPlanned_val" },
    DisplayValues = new string[] { "CustomerId", "ProjectManagerUsrId", "calc_Amount_val", "StartDate", "EndDate" },
    SummaryValues = new string[] { "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val", "calc_RecgnzdRevn_val", "calc_Billed_val", "calc_UnBilled_val", "calc_DueInv_val" },
    Menu = new string[] {
        "LineItems?PMO_Project_LineItem",
        "Scope?FIN_SalesOrder_ListSub:PMO_ChangeRequest_ListSub:PMO_WorkItem_ListSub:PMO_Deliverable_ListSub",
        "Stakeholders?SHRD_Stakeholder_ListSub",
        "Time?PMO_Phase_ListSub:PMO_Milestone_ListSub",
        "Financials?PMO_BillingPlan_ListSub:FIN_InvoiceAR_ListSub:FIN_InvoiceAP_ListSub:FIN_PurchaseOrder_ListSub",
        "RisksIssues?RSK_Risk_ListSub:RSK_Issue_ListSub",
        "DeliveryProofs?PMO_DeliveryNote_ListSub:PMO_COC_ListSub:PMO_TeamRoster_ListSub",
        "MngmtCenter?PMO_Assumption_ListSub:PMO_Constrain_ListSub:PMO_Plan_ListSub:PMO_MOM_ListSub:PMO_Lesson_ListSub"
    })]
public class Project : EntityWithLineItems
{
    public long? AccountManagerUsrId { get; set; } // UserId
    public long? ProjectManagerUsrId { get; set; } // UserId
    public long? CustomerId { get; set; }
    public int TeamCount_num { get; set; } = 0;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }


    [Attr(Section = "Project Governanace")]
    public long? AgreementId { get; set; }
    public long? ProgrammeId { get; set; }
    public long? PortfolioId { get; set; }
    public long? PMOAuditorUsrId { get; set; }


    [Attr(Section = "Project Settings")]
    public decimal IncrementHours_dec { get; set; } = 1;
    public decimal MaxRegularHours_dec { get; set; } = 8;
    public decimal MaxOverTimeHours_dec { get; set; } = 4;
    public decimal Progress_prc { get; set; } = 0;


    [JsonIgnore]
    [NotMapped]
    public virtual ICollection<LineItem>? LineItems { get; set; }

    public Project() { }
    public Project(long usrId, long tnantId, List<EntityType> EntTyps, List<EntityStatus> EntSts, List<Customer> Custs, List<ApplicationUser> Users, string name, string? nameAr, long? agrtId, long? progId, long? prtfId)
    {
        Random rnd = new Random();

        ProjectManagerUsrId = usrId;
        TenantId = tnantId;
        
        TeamCount_num = rnd.Next(3, 12);
        Progress_prc = rnd.Next(10, 97);

        CustomerId = Custs != null && Custs.Any() ? Custs[rnd.Next(0, Custs.Count)].Id : null;
        if (Users != null && Users.Any())
        {
            ProjectManagerUsrId = Users[rnd.Next(0, Users.Count)].Id;
            PMOAuditorUsrId = Users[rnd.Next(0, Users.Count)].Id;
            AccountManagerUsrId = Users[rnd.Next(0, Users.Count)].Id;
        }
        var projEntTyps = EntTyps?.Where(x => x.EntityName == "Project").ToList();
        EntityTypeId = projEntTyps != null && projEntTyps.Any() ? projEntTyps[rnd.Next(0, projEntTyps.Count)].Id : EntTyps?.FirstOrDefault()?.Id;
        var projEntSts = EntSts?.Where(x => x.EntityName == "Project").ToList();
        EntityStatusId = projEntSts != null && projEntSts.Any() ? projEntSts[rnd.Next(0, projEntSts.Count)].Id : EntSts?.FirstOrDefault()?.Id;

        Name = name;
        NameAr = nameAr;
        AgreementId = agrtId;
        ProgrammeId = progId;
        PortfolioId = prtfId;

        StartDate = DateTime.Today.AddDays(rnd.Next(10, 1000) * -1);
        EndDate = ((DateTime)StartDate).AddDays(rnd.Next(100, 1000));

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
public class ProjectTeamDTO : ProjectDTO
{
    public List<TeamMemberAvatarDTO> Stakeholders { get; set; } = new List<TeamMemberAvatarDTO>();
}

public class ProjectDTO
{
    public long? AccountManagerUsrId { get; set; }
    public string? AccountManagerUsrName { get; set; }
    public string? AccountManagerUsrPhotoUrl { get; set; }
    public long? ProjectManagerUsrId { get; set; }
    public string? ProjectManagerUsrName { get; set; }
    public string? ProjectManagerUsrPhotoUrl { get; set; }
    public long? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public int? TeamCount_num { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public long? AgreementId { get; set; }
    public string? AgreementName { get; set; }
    public long? ProgrammeId { get; set; }
    public string? ProgrammeName { get; set; }
    public long? PortfolioId { get; set; }
    public string? PortfolioName { get; set; }
    public long? PMOAuditorUsrId { get; set; }
    public string? PMOAuditorUsrName { get; set; }
    public string? PMOAuditorUsrPhotoUrl { get; set; }
    public decimal? IncrementHours_dec { get; set; }
    public decimal? MaxRegularHours_dec { get; set; }
    public decimal? MaxOverTimeHours_dec { get; set; }
    public decimal? Progress_prc { get; set; }
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