using Aspire.Data;
using Aspire.Services;
using Aspire.Utilities;
using Microsoft.AspNetCore.Mvc;
using Aspire.Context;
using AutoMapper;

namespace Aspire.APIs;

[Route("api/[controller]")]
[ApiController]
public class RiskController(ApplicationDbContext context, IMapper _mapper, ITenantProviderService _tenantService, IGenericRepository<Risk, RiskDTO, ApplicationDbContext> _genericRepository) : ControllerBase
{
    [HttpPost("GetAll")]
    public async Task<IActionResult> GetAll([FromBody] FilteringModel? filteringModel)
    {
        try
        {
            PaginationModel<RiskDTO> Objs = await _genericRepository.GetAllJoins(filteringModel);
            return Ok(Objs);
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
            RiskDTO? Obj = await _genericRepository.GetByIdEntityDTO(Id);
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
            RiskDTO? Obj = await _genericRepository.GetByNameEntityDTO(Name);
            return Ok(Obj);
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] RiskDTO Model)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            Risk entity = _mapper.Map<Risk>(Model);

            Risk AdjModel = await RiskService.Create(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<RiskDTO>(AdjModel)); 
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }

    [HttpPost("Edit")]
    public async Task<IActionResult> Edit([FromBody] RiskDTO Model)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            Risk entity = _mapper.Map<Risk>(Model);

            Risk AdjModel = await RiskService.Update(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<RiskDTO>(AdjModel)); 
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
            RiskDTO? dto = await _genericRepository.GetByIdEntityDTO(Id);

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
            string Msg = RiskService.OnDelete(long.Parse(Id), _genericRepository.Context);

            if(Msg == "OK")
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


}