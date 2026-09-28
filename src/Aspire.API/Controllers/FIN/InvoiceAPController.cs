using Aspire.Data;
using Aspire.Services;
using Aspire.Utilities;
using Microsoft.AspNetCore.Mvc;
using Aspire.Context;
using AutoMapper;

namespace Aspire.APIs;

[Route("api/[controller]")]
[ApiController]
public class InvoiceAPController(ApplicationDbContext context, IMapper _mapper, ITenantProviderService _tenantService, IGenericRepository<InvoiceAP, InvoiceAPDTO, ApplicationDbContext> _genericRepository) : ControllerBase
{
    [HttpPost("GetAll")]
    public async Task<IActionResult> GetAll([FromBody] FilteringModel? filteringModel)
    {
        try
        {
            PaginationModel<InvoiceAPDTO> Objs = await _genericRepository.GetAllJoins(filteringModel);
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
            InvoiceAPDTO? Obj = await _genericRepository.GetByIdEntityDTO(Id);
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
            InvoiceAPDTO? Obj = await _genericRepository.GetByNameEntityDTO(Name);
            return Ok(Obj);
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] InvoiceAPDTO Model)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            InvoiceAP entity = _mapper.Map<InvoiceAP>(Model);

            InvoiceAP AdjModel = await InvoiceAPService.Create(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<InvoiceAPDTO>(AdjModel)); 
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }

    [HttpPost("Edit")]
    public async Task<IActionResult> Edit([FromBody] InvoiceAPDTO Model)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            InvoiceAP entity = _mapper.Map<InvoiceAP>(Model);

            InvoiceAP AdjModel = await InvoiceAPService.Update(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<InvoiceAPDTO>(AdjModel)); 
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
            InvoiceAPDTO? dto = await _genericRepository.GetByIdEntityDTO(Id);

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
            string Msg = InvoiceAPService.OnDelete(long.Parse(Id), _genericRepository.Context);

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

    [HttpPost("Billed")]
    public async Task<IActionResult> Billed([FromBody] InvoiceAPDTO Model)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            InvoiceAP entity = _mapper.Map<InvoiceAP>(Model);

            InvoiceAP AdjModel = await InvoiceAPService.Billed(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<InvoiceAPDTO>(AdjModel)); 
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }
    [HttpPost("RevokeBilling")]
    public async Task<IActionResult> RevokeBilling([FromBody] InvoiceAPDTO Model)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            InvoiceAP entity = _mapper.Map<InvoiceAP>(Model);

            InvoiceAP AdjModel = await InvoiceAPService.RevokeBilling(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<InvoiceAPDTO>(AdjModel)); 
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }
    [HttpPost("Submitted")]
    public async Task<IActionResult> Submitted([FromBody] InvoiceAPDTO Model)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            InvoiceAP entity = _mapper.Map<InvoiceAP>(Model);

            InvoiceAP AdjModel = await InvoiceAPService.Submitted(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<InvoiceAPDTO>(AdjModel)); 
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }
    [HttpPost("RevokeSubmission")]
    public async Task<IActionResult> RevokeSubmission([FromBody] InvoiceAPDTO Model)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            InvoiceAP entity = _mapper.Map<InvoiceAP>(Model);

            InvoiceAP AdjModel = await InvoiceAPService.RevokeSubmission(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<InvoiceAPDTO>(AdjModel)); 
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }
    [HttpPost("Collected")]
    public async Task<IActionResult> Collected([FromBody] InvoiceAPDTO Model)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            InvoiceAP entity = _mapper.Map<InvoiceAP>(Model);

            InvoiceAP AdjModel = await InvoiceAPService.Collected(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<InvoiceAPDTO>(AdjModel)); 
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }
    [HttpPost("RevokeCollection")]
    public async Task<IActionResult> RevokeCollection([FromBody] InvoiceAPDTO Model)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            InvoiceAP entity = _mapper.Map<InvoiceAP>(Model);

            InvoiceAP AdjModel = await InvoiceAPService.RevokeCollection(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<InvoiceAPDTO>(AdjModel)); 
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }

}