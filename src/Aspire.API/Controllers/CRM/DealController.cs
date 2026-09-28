using Aspire.Data;
using Aspire.Services;
using Aspire.Utilities;
using Microsoft.AspNetCore.Mvc;
using Aspire.Context;
using AutoMapper;

namespace Aspire.APIs;

public class Won
{
    public long Id { get; set; }
    public string? ActionComment { get; set; }
    public string? Option { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class DealController(ApplicationDbContext context, IMapper _mapper, ITenantProviderService _tenantService, IGenericRepository<Deal, DealDTO, ApplicationDbContext> _genericRepository) : ControllerBase
{
    // Code-Player-Start-DTO //
    [HttpPost("GetAll")]
    public async Task<IActionResult> GetAll([FromBody] FilteringModel? filteringModel)
    {
        try
        {
            PaginationModel<DealDTO> Objs = await _genericRepository.GetAllJoins(filteringModel);
            List<DealTeamDTO> ObjWithTeam_List = new List<DealTeamDTO>();
            foreach (DealDTO Obj in Objs.Items)
            {
                DealTeamDTO ObjWithTeam = _mapper.Map<DealTeamDTO>(Obj);

                List<ApplicationUser> StkHldrs = StakeholdersService.Stakeholders(Obj, context);
                foreach (ApplicationUser StkHldr in StkHldrs)
                    ObjWithTeam.Stakeholders.Add(new TeamMemberAvatarDTO() { Name = StkHldr.Name, PhotoUrl = @"/avatars/" + StkHldr.Id + ".png" });

                ObjWithTeam_List.Add(ObjWithTeam);
            }

            PaginationModel<DealTeamDTO> Objs2 = new PaginationModel<DealTeamDTO>(ObjWithTeam_List, Objs.PageNumber, Objs.PageSize, Objs.TotalItems, Objs.Aggregates);
            return Ok(Objs2);

        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById([FromQuery] string Id)
    {
        try
        {
            DealDTO? Obj = await _genericRepository.GetByIdEntityDTO(Id);
            return Ok(Obj);
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    [HttpGet("GetByName")]
    public async Task<IActionResult> GetByName([FromQuery] string Name)
    {
        try
        {
            DealDTO? Obj = await _genericRepository.GetByNameEntityDTO(Name);
            return Ok(Obj);
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] DealDTO Model)
    {
        try
        {
            if (!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });

            Deal entity = _mapper.Map<Deal>(Model);

            Deal AdjModel = await DealService.Create(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<DealDTO>(AdjModel));
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }

    [HttpPost("Edit")]
    public async Task<IActionResult> Edit([FromBody] DealDTO Model)
    {
        try
        {
            if (!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });

            Deal entity = _mapper.Map<Deal>(Model);

            Deal AdjModel = await DealService.Update(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<DealDTO>(AdjModel));
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }

    [HttpPost("SavePhoto")]
    public async Task<IActionResult> SavePhoto([FromQuery] string Id, [FromQuery] string FileName)
    {
        try
        {
            DealDTO? dto = await _genericRepository.GetByIdEntityDTO(Id);

            if (dto != null)
                dto.PhotoUrl = "/Aspire-Attachments/" + FileName;

            string result = await _genericRepository.Save(dto, this.HttpContext);
            return Ok(new { severity = result.Split(":")[0], summary = result.Split(":")[1], message = result.Split(":")[2] });
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] string Id)
    {
        try
        {
            string result = "";
            string Msg = DealService.OnDelete(long.Parse(Id), _genericRepository.Context);

            if (Msg == "OK")
                result = await _genericRepository.Delete(Id, this.HttpContext);
            else
                result = "error:OnDelete:" + Msg;

            return Ok(new { severity = result.Split(":")[0], summary = result.Split(":")[1], message = result.Split(":")[2] });
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    [HttpPost("MovetoNextStage")]
    public async Task<IActionResult> MovetoNextStage([FromBody] DealDTO Model)
    {
        try
        {
            if (!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });

            Deal entity = _mapper.Map<Deal>(Model);

            Deal AdjModel = await DealService.MovetoNextStage(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<DealDTO>(AdjModel));
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }
    [HttpPost("CreatePnl")]
    public async Task<IActionResult> CreatePnl([FromBody] DealDTO Model)
    {
        try
        {
            if (!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });

            Deal entity = _mapper.Map<Deal>(Model);

            (Deal AdjModel, Pnl? pnl) = await DealService.CreatePnl(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(new
                {
                    deal = _mapper.Map<DealDTO>(AdjModel),
                    pnl = pnl is null ? null : _mapper.Map<PnlDTO>(pnl)
                });
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }
    [HttpPost("Won")]
    public async Task<IActionResult> Won([FromBody] Won Model)
    {
        try
        {
            if (!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });

            Deal entity = new Deal { Id = Model.Id, ActionComment = Model.ActionComment };

            Deal AdjModel = await DealService.Won(entity, Model.Option, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
            {
                CustomerPO? customerPO = context.CustomerPOs.Where(c => c.DealId == AdjModel.Id).FirstOrDefault();
                SalesOrder? salesOrder = context.SalesOrders.Where(s => s.DealId == AdjModel.Id).FirstOrDefault();

                return Ok(new
                {
                    deal = _mapper.Map<DealDTO>(AdjModel),
                    customerPO = customerPO is null ? null : _mapper.Map<CustomerPODTO>(customerPO),
                    salesOrder = salesOrder is null ? null : _mapper.Map<SalesOrderDTO>(salesOrder)
                });
            }
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }
    [HttpPost("Lost")]
    public async Task<IActionResult> Lost([FromBody] DealDTO Model)
    {
        try
        {
            if (!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });

            Deal entity = _mapper.Map<Deal>(Model);

            Deal AdjModel = await DealService.Lost(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<DealDTO>(AdjModel));
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }

    // Code-Player-End-DTO //
}