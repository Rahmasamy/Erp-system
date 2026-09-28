using Aspire.Context;
using Aspire.Data;
using Aspire.Services;
using Aspire.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aspire.APIs;

[Route("api/[controller]")]
[ApiController]
public class Reports(ApplicationDbContext context, IGenericRepository<AuditItem, AuditItemDTO, ApplicationDbContext> _genericRepository) : ControllerBase
{
    Dictionary<string, List<AuditItemsTreeDTO>> lookups = [];
    string filteringOperation = string.Empty;
    List<string> allWorkActivityIds = new();

    [HttpGet("GetValueByPipeline")]
    public async Task<IActionResult> GetValueByPipeline()
    {
        try
        {
            List<PipelineValueDTO> dTOs = new List<PipelineValueDTO>();

            PipelineValueDTO dTO1 = new PipelineValueDTO();
            dTO1.Id = 1;
            dTO1.PipelineName = "Gov";
            dTO1.Value = 100000;
            dTO1.Percentage = 10;
            dTOs.Add(dTO1);

            PipelineValueDTO dTO2 = new PipelineValueDTO();
            dTO2.Id = 2;
            dTO2.PipelineName = "PIF";
            dTO2.Value = 200000;
            dTO2.Percentage = 20;
            dTOs.Add(dTO2);

            PipelineValueDTO dTO3 = new PipelineValueDTO();
            dTO3.Id = 3;
            dTO3.PipelineName = "B2C";
            dTO3.Value = 300000;
            dTO3.Percentage = 30;
            dTOs.Add(dTO3);

            PipelineValueDTO dTO4 = new PipelineValueDTO();
            dTO4.Id = 4;
            dTO4.PipelineName = "B2B";
            dTO4.Value = 700000;
            dTO4.Percentage = 70;
            dTOs.Add(dTO4);

            return Ok(dTOs);
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    [HttpGet("GetStagesByPipelineId")]
    public async Task<IActionResult> GetStagesByPipelineId([FromQuery] string Id)
    {
        try
        {
            List<StageValueDTO> dTOs = new List<StageValueDTO>();
            Dictionary<string, int[]> value = new Dictionary<string, int[]>();
            Dictionary<string, int[]> prcnt = new Dictionary<string, int[]>();
            
            value.Add("1", new int[] { 600000, 480000, 360000, 240000, 120000 });
            value.Add("2", new int[] { 600000, 380000, 260000, 180000, 100000 });
            value.Add("3", new int[] { 600000, 280000, 210000, 150000, 90000 });
            value.Add("4", new int[] { 600000, 280000, 210000, 150000, 90000 });

            prcnt.Add("1", new int[] { 100, 80, 60, 40, 20 });
            prcnt.Add("2", new int[] { 100, 90, 70, 50, 30 });
            prcnt.Add("3", new int[] { 100, 70, 60, 30, 10 });
            prcnt.Add("4", new int[] { 100, 70, 60, 30, 10 });

            StageValueDTO dTO1 = new StageValueDTO();
            dTO1.Id = 1;
            dTO1.StageName = "Lead";
            dTO1.Value = value[Id][0];
            dTO1.Percentage = prcnt[Id][0];
            dTOs.Add(dTO1);

            StageValueDTO dTO2 = new StageValueDTO();
            dTO2.Id = 2;
            dTO2.StageName = "Opportunity";
            dTO2.Value = value[Id][1];
            dTO2.Percentage = prcnt[Id][1];
            dTOs.Add(dTO2);

            StageValueDTO dTO3 = new StageValueDTO();
            dTO3.Id = 3;
            dTO3.StageName = "Qualified";
            dTO3.Value = value[Id][2];
            dTO3.Percentage = prcnt[Id][2];
            dTOs.Add(dTO3);

            StageValueDTO dTO4 = new StageValueDTO();
            dTO4.Id = 4;
            dTO4.StageName = "Solid";
            dTO4.Value = value[Id][3];
            dTO4.Percentage = prcnt[Id][3];
            dTOs.Add(dTO4);

            StageValueDTO dTO5 = new StageValueDTO();
            dTO5.Id = 5;
            dTO5.StageName = "Retained";
            dTO5.Value = value[Id][4];
            dTO5.Percentage = prcnt[Id][4];
            dTOs.Add(dTO5);
            
            return Ok(dTOs);
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    [HttpGet("GetStatusesByAuditId")]
    public async Task<IActionResult> GetStatusesByAuditId([FromQuery] string Id)
    {
        try
        {
            List<AuditItemStatusDTO> dTOs = new List<AuditItemStatusDTO>();

            AuditItemStatusDTO dTO1 = new AuditItemStatusDTO();
            dTO1.EntityStatusName = "To-do";
            dTO1.EntityStatusColor = "yellow";
            dTO1.Count = 20;
            dTO1.Percentage = 20;
            dTOs.Add(dTO1);

            AuditItemStatusDTO dTO2 = new AuditItemStatusDTO();
            dTO2.EntityStatusName = "In-Progress";
            dTO2.EntityStatusColor = "green";
            dTO2.Count = 40;
            dTO2.Percentage = 40;
            dTOs.Add(dTO2);

            AuditItemStatusDTO dTO3 = new AuditItemStatusDTO();
            dTO3.EntityStatusName = "In-Review";
            dTO3.EntityStatusColor = "blue";
            dTO3.Count = 20;
            dTO3.Percentage = 20;
            dTOs.Add(dTO3);

            AuditItemStatusDTO dTO4 = new AuditItemStatusDTO();
            dTO4.EntityStatusName = "Done";
            dTO4.EntityStatusColor = "gray";
            dTO4.Count = 20;
            dTO4.Percentage = 20;
            dTOs.Add(dTO4);

            return Ok(dTOs);

            //return Ok(context.AuditItems.Where(e => e.AuditId == long.Parse(Id))
            //                    .GroupBy(g => g.EntityStatusId)
            //                    .Select(s => new AuditItemStatusDTO() { Count = s.Count(), 
            //                        EntityStatusName = context.EntityStatuses.Where(ent => ent.Id == s.Key).ToList()[0].Name,
            //                        EntityStatusColor = context.EntityStatuses.Where(ent => ent.Id == s.Key).ToList()[0].Color
            //                    }).ToList());
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    [HttpGet("getTypesByAuditId")]
    public async Task<IActionResult> GetTypesByAuditId([FromQuery] string Id)
    {
        try
        {
            List<AuditItemTypeDTO> dTOs = new List<AuditItemTypeDTO>();

            AuditItemTypeDTO dTO1 = new AuditItemTypeDTO();
            dTO1.EntityTypeName = "Not Assessed";
            dTO1.EntityTypeColor = "yellow";
            dTO1.Count = 20;
            dTO1.Percentage = 20;
            dTOs.Add(dTO1);

            AuditItemTypeDTO dTO2 = new AuditItemTypeDTO();
            dTO2.EntityTypeName = "Non Compliant";
            dTO2.EntityTypeColor = "green";
            dTO2.Count = 30;
            dTO2.Percentage = 30;
            dTOs.Add(dTO2);

            AuditItemTypeDTO dTO3 = new AuditItemTypeDTO();
            dTO3.EntityTypeName = "Partially Compliant";
            dTO3.EntityTypeColor = "blue";
            dTO3.Count = 20;
            dTO3.Percentage = 20;
            dTOs.Add(dTO3);

            AuditItemTypeDTO dTO4 = new AuditItemTypeDTO();
            dTO4.EntityTypeName = "Compliant";
            dTO4.EntityTypeColor = "gray";
            dTO4.Count = 20;
            dTO4.Percentage = 20;
            dTOs.Add(dTO4);

            AuditItemTypeDTO dTO5 = new AuditItemTypeDTO();
            dTO5.EntityTypeName = "Not Applicable";
            dTO5.EntityTypeColor = "gray";
            dTO5.Count = 10;
            dTO5.Percentage = 10;
            dTOs.Add(dTO5);
            
            return Ok(dTOs);

            //return Ok(context.AuditItems.Where(e => e.AuditId == long.Parse(Id))
            //                    .GroupBy(g => g.EntityTypeId)
            //                    .Select(s => new AuditItemTypeDTO()
            //                    {
            //                        Count = s.Count(),
            //                        EntityTypeName = context.EntityTypes.Where(ent => ent.Id == s.Key).ToList()[0].Name,
            //                        EntityTypeColor = context.EntityTypes.Where(ent => ent.Id == s.Key).ToList()[0].Color
            //                    }).ToList());
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    [HttpGet("GetCostByProjectId")]
    public async Task<IActionResult> GetCostByProjectId([FromQuery] string Id)
    {
        try
        {
            List<AuditItemTypeDTO> dTOs = new List<AuditItemTypeDTO>();

            AuditItemTypeDTO dTO1 = new AuditItemTypeDTO();
            dTO1.EntityTypeName = "MISC";
            dTO1.EntityTypeColor = "yellow";
            dTO1.Count = 20;
            dTO1.Percentage = 20;
            dTOs.Add(dTO1);

            AuditItemTypeDTO dTO2 = new AuditItemTypeDTO();
            dTO2.EntityTypeName = "Business Analysis";
            dTO2.EntityTypeColor = "green";
            dTO2.Count = 30;
            dTO2.Percentage = 30;
            dTOs.Add(dTO2);

            AuditItemTypeDTO dTO3 = new AuditItemTypeDTO();
            dTO3.EntityTypeName = "Development";
            dTO3.EntityTypeColor = "blue";
            dTO3.Count = 20;
            dTO3.Percentage = 20;
            dTOs.Add(dTO3);

            AuditItemTypeDTO dTO4 = new AuditItemTypeDTO();
            dTO4.EntityTypeName = "Testing";
            dTO4.EntityTypeColor = "gray";
            dTO4.Count = 20;
            dTO4.Percentage = 20;
            dTOs.Add(dTO4);

            AuditItemTypeDTO dTO5 = new AuditItemTypeDTO();
            dTO5.EntityTypeName = "Project Management";
            dTO5.EntityTypeColor = "gray";
            dTO5.Count = 10;
            dTO5.Percentage = 10;
            dTOs.Add(dTO5);

            return Ok(dTOs);

            //return Ok(context.AuditItems.Where(e => e.AuditId == long.Parse(Id))
            //                    .GroupBy(g => g.EntityTypeId)
            //                    .Select(s => new AuditItemTypeDTO()
            //                    {
            //                        Count = s.Count(),
            //                        EntityTypeName = context.EntityTypes.Where(ent => ent.Id == s.Key).ToList()[0].Name,
            //                        EntityTypeColor = context.EntityTypes.Where(ent => ent.Id == s.Key).ToList()[0].Color
            //                    }).ToList());
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    [HttpGet("getScoreByAuditId")]
    public async Task<IActionResult> GetScoreByAuditId([FromQuery] string Id)
    {
        try
        {
            List<RangeDTO> rangeDTOs = new List<RangeDTO>();
            rangeDTOs.Add(new RangeDTO() { Min = 0, Max = 5, Title = "", Color = "" });
            return Ok(new ScoreDTO() { Score_dec = decimal.Parse("3.52"), Ranges = rangeDTOs });
        }
        catch (Exception ex)
        {
            return Ok();
        }
    }

    [HttpPost("GetProjectCost")]
    public async Task<IActionResult> GetProjectCost([FromBody] FilteringModel? filteringModel)
    {
        try
        {
            List<RangeDTO> rangeDTOs = new List<RangeDTO>();
            rangeDTOs.Add(new RangeDTO() { Min = 0, Max = 30, Title = "Low", Color = "Gold" });
            rangeDTOs.Add(new RangeDTO() { Min = 30, Max = 60, Title = "Meduim", Color = "Yellow" });
            rangeDTOs.Add(new RangeDTO() { Min = 60, Max = 100, Title = "High", Color = "RED" });
            return Ok(new ScoreDTO() { Score_dec = 20, Ranges = rangeDTOs });
        }
        catch (Exception ex)
        {
            return Ok(new ScoreDTO() { Score_dec = 0, Msg = ex.Message });
        }
    }

    [HttpPost("GetProjectProgress")]
    public async Task<IActionResult> GetProjectProgress([FromBody] FilteringModel? filteringModel)
    {
        try
        {
            List<RangeDTO> rangeDTOs = new List<RangeDTO>();
            rangeDTOs.Add(new RangeDTO() { Min = 0, Max = 40, Title = "Low", Color = "RED" });
            rangeDTOs.Add(new RangeDTO() { Min = 40, Max = 70, Title = "Meduim", Color = "Yellow" });
            rangeDTOs.Add(new RangeDTO() { Min = 70, Max = 100, Title = "High", Color = "Gold" });
            return Ok(new ScoreDTO() { Score_dec = 50, Ranges = rangeDTOs });
        }
        catch (Exception ex)
        {
            return Ok(new ScoreDTO() { Score_dec = 0, Msg = ex.Message });
        }
    }

    [HttpPost("GetProjectSPI")]
    public async Task<IActionResult> GetProjectSPI([FromBody] FilteringModel? filteringModel)
    {
        try
        {
            List<RangeDTO> rangeDTOs = new List<RangeDTO>();
            rangeDTOs.Add(new RangeDTO() { Min = 0, Max = 0.6, Title = "Critical" , Color = "RED" });
            rangeDTOs.Add(new RangeDTO() { Min = 0.6, Max = 0.9, Title = "Behind", Color = "Gray" });
            rangeDTOs.Add(new RangeDTO() { Min = 0.95, Max = 1.05, Title = "On", Color = "Green" });
            rangeDTOs.Add(new RangeDTO() { Min = 1.05, Max = 1.3, Title = "Ahead", Color = "lightblue" });
            rangeDTOs.Add(new RangeDTO() { Min = 1.3, Max = 1.7, Title = "Excellent", Color = "blue" });
            return Ok(new ScoreDTO() { Score_dec = decimal.Parse("0.52"), Ranges = rangeDTOs });
        }
        catch (Exception ex)
        {
            return Ok(new ScoreDTO() { Score_dec = 0, Msg = ex.Message });
        }
    }

    [HttpPost("GetProjectCPI")]
    public async Task<IActionResult> GetProjectCPI([FromBody] FilteringModel? filteringModel)
    {
        try
        {
            List<RangeDTO> rangeDTOs = new List<RangeDTO>();
            rangeDTOs.Add(new RangeDTO() { Min = 0, Max = 0.6, Title = "Critical", Color = "RED" });
            rangeDTOs.Add(new RangeDTO() { Min = 0.6, Max = 0.9, Title = "Overrun", Color = "Gray" });
            rangeDTOs.Add(new RangeDTO() { Min = 0.95, Max = 1.05, Title = "On Budget", Color = "Green" });
            rangeDTOs.Add(new RangeDTO() { Min = 1.05, Max = 1.3, Title = "Savings", Color = "lightblue" });
            rangeDTOs.Add(new RangeDTO() { Min = 1.3, Max = 1.7, Title = "Excellent", Color = "Blue" });
            return Ok(new ScoreDTO() { Score_dec = decimal.Parse("3.52"), Ranges = rangeDTOs });
        }
        catch (Exception ex)
        {
            return Ok(new ScoreDTO() { Score_dec = 0, Msg = ex.Message });
        }
    }

    [HttpPost("GetProjectInfo")]
    public async Task<IActionResult> GetProjectInfo([FromBody] FilteringModel? filteringModel)
    {
        try
        {
            ProjectInfoDTO dTO = new ProjectInfoDTO();
            dTO.StartDate = new DateTime(2025, 1,1);
            dTO.EndDate = new DateTime(2026, 12, 31);
            dTO.Budget_val = 800000;
            dTO.ActualCost_val = 300000;
            dTO.RemainingBudget_val = 500000;
            dTO.NetAmount_val = 1000000;
            dTO.RecognizedRevenue_val = 400000;
            dTO.Billed_val = 300000;
            dTO.UnBilled_val = 100000;

            dTO.NonRecognizedRevenue_val = 600000;
            dTO.Planned_val = 300000;
            dTO.UnPlanned_val = 300000;
            return Ok(dTO);
        }
        catch (Exception ex)
        {
            return Ok();
        }
    }

    [HttpPost("GetProjectTrend")]
    public async Task<IActionResult> GetProjectTrend([FromBody] FilteringModel? filteringModel)
    {
        try
        {
            List<ProjectOverTimeDTO> dTOs = new List<ProjectOverTimeDTO>();
            dTOs.Add(new ProjectOverTimeDTO(1, 1, 200000, 0, "Jan-2025"));
            dTOs.Add(new ProjectOverTimeDTO(0.9, 0.9, 250000, 10000, "Feb-2025"));
            dTOs.Add(new ProjectOverTimeDTO(0.7, 0.8, 300000, 20000, "Mar-2025"));
            dTOs.Add(new ProjectOverTimeDTO(0.6, 0.75, 400000, 30000, "Apr-2025"));
            dTOs.Add(new ProjectOverTimeDTO(0.5, 0.73, 400000, 70000, "May-2025"));
            dTOs.Add(new ProjectOverTimeDTO(0.7, 0.5, 400000, 90000,"Jun-2025"));
            dTOs.Add(new ProjectOverTimeDTO(0.3, 0.3, 320000, 200000, "Jul-2025"));
            dTOs.Add(new ProjectOverTimeDTO(0.8, 0.6, 350000, 210000, "Aug-2025"));
            dTOs.Add(new ProjectOverTimeDTO(0.9, 0.8, 310000, 250000, "Sep-2025"));
            dTOs.Add(new ProjectOverTimeDTO(1.3, 1.2, 300000, 270000, "Oct-2025"));

            return Ok(dTOs);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    [HttpGet("GetAuditItemsTree")]
    public async Task<IActionResult> GetAuditItemsTree([FromQuery] string AuditId)
    {
        try
        {
            FilteringModel filteringModel = new FilteringModel();
            filteringModel.FilterFields.Add(new FilterField("AuditId", "", AuditId));
            filteringModel.PageSize = 1000;
            PaginationModel<AuditItemDTO> Objs = await _genericRepository.GetAllJoins(filteringModel);

            List<AuditItemsTreeDTO> AudItms = BuildTree(Objs, context.AuditItems
                .Where(w => w.AuditId == long.Parse(AuditId))
                .Select(p => new AuditItemsTreeDTO
                {
                    Key = p.Id,
                    Label = p.Name,
                    ParentId = p.ParentId,
                    Children = new List<AuditItemsTreeDTO>()
                }).ToList());

            return Ok(AudItms);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message });
        }
    }

    
    [HttpGet("GetValueByMonth")]
    public async Task<IActionResult> GetValueByMonth()
    {
        try
        {
            List<PipelineValueDTO> dTOs = new List<PipelineValueDTO>();

            PipelineValueDTO dTO1 = new PipelineValueDTO();
            dTO1.Id = 1;
            dTO1.PipelineName = "Jan-2025";
            dTO1.Value = 100000;
            dTO1.Percentage = 10;
            dTOs.Add(dTO1);

            PipelineValueDTO dTO2 = new PipelineValueDTO();
            dTO2.Id = 2;
            dTO2.PipelineName = "Feb-2025";
            dTO2.Value = 200000;
            dTO2.Percentage = 20;
            dTOs.Add(dTO2);

            PipelineValueDTO dTO3 = new PipelineValueDTO();
            dTO3.Id = 3;
            dTO3.PipelineName = "Mar-2025";
            dTO3.Value = 300000;
            dTO3.Percentage = 30;
            dTOs.Add(dTO3);

            PipelineValueDTO dTO4 = new PipelineValueDTO();
            dTO4.Id = 4;
            dTO4.PipelineName = "April-2025";
            dTO4.Value = 700000;
            dTO4.Percentage = 70;
            dTOs.Add(dTO4);

            PipelineValueDTO dTO5 = new PipelineValueDTO();
            dTO5.Id = 5;
            dTO5.PipelineName = "May-2025";
            dTO5.Value = 700000;
            dTO5.Percentage = 70;
            dTOs.Add(dTO5);

            PipelineValueDTO dTO6 = new PipelineValueDTO();
            dTO6.Id = 6;
            dTO6.PipelineName = "Jun-2025";
            dTO6.Value = 700000;
            dTO6.Percentage = 70;
            dTOs.Add(dTO6);

            PipelineValueDTO dTO7 = new PipelineValueDTO();
            dTO7.Id = 7;
            dTO7.PipelineName = "Jul-2025";
            dTO7.Value = 700000;
            dTO7.Percentage = 70;
            dTOs.Add(dTO7);
            
            return Ok(dTOs);
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    private List<AuditItemsTreeDTO> BuildTree(PaginationModel<AuditItemDTO> Objs, List<AuditItemsTreeDTO> flatItems)
    {
        var lookup = flatItems.ToLookup(item => item.ParentId);

        foreach (var item in flatItems)
        {

            object data = Objs.Items.Where(w => w.Id == item.Key).ToList();

            item.Description = (((List<AuditItemDTO>)data)[0]).Description;
            item.EntityStatusName = (((List<AuditItemDTO>)data)[0]).EntityStatusName;
            item.EntityTypeName = (((List<AuditItemDTO>)data)[0]).EntityTypeName;
            item.EntityStatusColor = (((List<AuditItemDTO>)data)[0]).EntityStatusColor;
            item.EntityTypeColor = (((List<AuditItemDTO>)data)[0]).EntityTypeColor;
            item.Score_dec = (((List<AuditItemDTO>)data)[0]).Score_dec;
            item.Ctrls = (((List<AuditItemDTO>)data)[0]).Ctrls;
            item.Evids = (((List<AuditItemDTO>)data)[0]).Evids;
            item.Excps = (((List<AuditItemDTO>)data)[0]).Excps;
            item.Data = (((List<AuditItemDTO>)data)[0]);
            item.Children.AddRange(lookup[item.Key]);
            item.Root = item.ParentId == null;
            item.Leaf = item.Children.Count == 0;
        }

        return lookup[null].ToList(); // Return root items
    }
}

public class AuditItemsTreeDTO
{
    public long? Key { get; set; }
    public string? Label { get; set; }
    public object? Data { get; set; }
    public long? ParentId { get; set; }
    public int? Ctrls { get; set; }
    public int? Evids { get; set; }
    public int? Excps { get; set; }
    public string? EntityStatusId { get; set; }
    public string? EntityStatusName { get; set; }
    public string? EntityStatusColor { get; set; }
    public string? EntityTypeId { get; set; }
    public string? EntityTypeName { get; set; }
    public string? EntityTypeColor { get; set; }
    public string? Description { get; set; }
    public decimal? Score_dec { get; set; }
    public List<AuditItemsTreeDTO> Children { get; set; } = [];
    public bool Root { get; set; }
    public bool Leaf { get; set; }
}
