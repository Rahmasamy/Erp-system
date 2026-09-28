using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "CRM", Abbrv = "DL", SubList = true, List = true, Tree = true, Card = true, ShowStakeholders = true, ShowProgress = true,
    Actions = new string[] { "Move-to-Next-Stage:Dlg:dollar", "Create-Pnl:Dlg:dollar", "Won:Dlg:dollar", "Lost:Dlg:dollar", "Convert_to_Agreement:CRM_Agreement:dollar" },
    FilterBy = new string[] { "CustomerId", "PipelineId" },
    Cols = new string[] { "CustomerId", "AccountManagerUsrId", "PipelineId", "StageId", "calc_Amount_val", "calc_PipelineAmount_val" },
    Aggregate = new string[] { "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val", "calc_PipelineAmount_val" },
    DisplayValues = new string[] { "CustomerId", "PipelineId", "calc_Amount_val", "StartDate", "EndDate" },
    SummaryValues = new string[] { "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val", "calc_RecgnzdRevn_val", "calc_Billed_val", "calc_UnBilled_val", "calc_DueInv_val" })]
public class Route : BaseEntity
{
    public long? StartCityId { get; set; }
    public long? EndCityId { get; set; }
    public decimal? Distance_dec { get; set; }
    public decimal? EstimatedDuration_dec { get; set; }
    public long? RoadConditionId { get; set; }


    public Route() { }
    public Route(long usrId, long tnantId, List<EntityType> EntTyps, List<EntityStatus> EntSts, List<Customer> Custs, List<ApplicationUser> Users, Pipeline PL, Stage ST,  string name, string? nameAr)
    {

    }
}

// Code-Player-Start-DTO //
// Code-Player-End-DTO //









