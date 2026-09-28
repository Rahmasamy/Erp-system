using Aspire.Data;
using Aspire.Services;
using Aspire.Utilities;
using Microsoft.AspNetCore.Mvc;
using Aspire.Context;
using AutoMapper;

namespace Aspire.APIs;

[Route("api/[controller]")]
[ApiController]
public class DocumentController(ApplicationDbContext context, IMapper _mapper, ITenantProviderService _tenantService, IGenericRepository<Document, DocumentDTO, ApplicationDbContext> _genericRepository) : ControllerBase
{

// Code-Player-Start-DTO //
    [HttpPost("GetAll")]
    public async Task<IActionResult> GetAll([FromBody] FilteringModel? filteringModel)
    {
        try
        {
            filteringModel ??= new FilteringModel();
            filteringModel.FilterValues ??= new Dictionary<string, object>();

            // If the logged in user is associated with a contractor, enforce ContractorId filtering
            long? userId = _tenantService.GetUserId();
            if (userId.HasValue)
            {
                var extUser = context.ExternalUsers.FirstOrDefault(u => u.AppUsrId == userId.Value);
                if (extUser != null && extUser.ContractorId.HasValue)
                {
                    filteringModel.FilterValues["ContractorId"] = extUser.ContractorId.Value.ToString();
                }
            }

            PaginationModel<DocumentDTO> Objs = await _genericRepository.GetAllJoins(filteringModel);
            return Ok(Objs);
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    [HttpPost("Submit")]
    public async Task<IActionResult> Submit([FromQuery] string Id)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });

            if (!long.TryParse(Id, out long docId))
                return Ok(new { severity = "error", summary = "Validation", msg = "Invalid Document Id" });

            Document AdjModel = await DocumentService.SubmitDocument(docId, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
            {
                DocumentDTO? result = await _genericRepository.GetByIdEntityDTO(AdjModel.Id.ToString());
                return Ok(result ?? _mapper.Map<DocumentDTO>(AdjModel));
            }
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }

    // The contractor's explicit "I've read the rejection comments, let me edit this" step - flips a
    // Rejected document back to Draft so its details/attachments become editable again.
    [HttpPost("ReopenAsDraft")]
    public async Task<IActionResult> ReopenAsDraft([FromQuery] string Id)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });

            if (!long.TryParse(Id, out long docId))
                return Ok(new { severity = "error", summary = "Validation", msg = "Invalid Document Id" });

            Document AdjModel = await DocumentService.ReopenAsDraft(docId, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
            {
                DocumentDTO? result = await _genericRepository.GetByIdEntityDTO(AdjModel.Id.ToString());
                return Ok(result ?? _mapper.Map<DocumentDTO>(AdjModel));
            }
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById([FromQuery] string Id)
    {
        try
        {
            DocumentDTO? Obj = await _genericRepository.GetByIdEntityDTO(Id);
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
            DocumentDTO? Obj = await _genericRepository.GetByNameEntityDTO(Name);
            return Ok(Obj);
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] DocumentDTO Model)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            Document entity = _mapper.Map<Document>(Model);

            Document AdjModel = await DocumentService.Create(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
            {
                DocumentDTO? result = await _genericRepository.GetByIdEntityDTO(AdjModel.Id.ToString());
                return Ok(result ?? _mapper.Map<DocumentDTO>(AdjModel)); 
            }
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }

    [HttpPost("Edit")]
    public async Task<IActionResult> Edit([FromBody] DocumentDTO Model)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            Document entity = _mapper.Map<Document>(Model);

            Document AdjModel = await DocumentService.Update(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
            {
                DocumentDTO? result = await _genericRepository.GetByIdEntityDTO(AdjModel.Id.ToString());
                return Ok(result ?? _mapper.Map<DocumentDTO>(AdjModel)); 
            }
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
            DocumentDTO? dto = await _genericRepository.GetByIdEntityDTO(Id);

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
            string Msg = DocumentService.OnDelete(long.Parse(Id), _genericRepository.Context);

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
// Code-Player-End-DTO //
}