using Aspire.Data;
using Aspire.Services;
using Aspire.Utilities;
using Microsoft.AspNetCore.Mvc;
using Aspire.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Aspire.APIs;

[Route("api/[controller]")]
[ApiController]
public class DocumentTypeDisciplineController(ApplicationDbContext context, IMapper _mapper, ITenantProviderService _tenantService, IGenericRepository<DocumentTypeDiscipline, DocumentTypeDisciplineDTO, ApplicationDbContext> _genericRepository) : ControllerBase
{

// Code-Player-Start-DTO //
    [HttpPost("GetAll")]
    public async Task<IActionResult> GetAll([FromBody] FilteringModel? filteringModel)
    {
        try
        {
            var query = context.DocumentTypeDisciplines
                .IgnoreQueryFilters()
                .AsNoTracking();

            long? docTypeId = null;
            long? disciplineId = null;
            if (filteringModel?.FilterValues != null)
            {
                foreach (var filter in filteringModel.FilterValues)
                {
                    if (string.Equals(filter.Key, "DocumentTypeId", StringComparison.OrdinalIgnoreCase) &&
                        long.TryParse(filter.Value?.ToString(), out var parsedDocTypeId))
                    {
                        docTypeId = parsedDocTypeId;
                    }

                    if (string.Equals(filter.Key, "DisciplineId", StringComparison.OrdinalIgnoreCase) &&
                        long.TryParse(filter.Value?.ToString(), out var parsedDisciplineId))
                    {
                        disciplineId = parsedDisciplineId;
                    }
                }
            }

            if (filteringModel?.FilterFields != null)
            {
                foreach (var field in filteringModel.FilterFields)
                {
                    if (string.Equals(field.Name, "DocumentTypeId", StringComparison.OrdinalIgnoreCase) &&
                        field.Values?.Count > 0 && long.TryParse(field.Values[0], out var parsedDocTypeId))
                    {
                        docTypeId = parsedDocTypeId;
                    }

                    if (string.Equals(field.Name, "DisciplineId", StringComparison.OrdinalIgnoreCase) &&
                        field.Values?.Count > 0 && long.TryParse(field.Values[0], out var parsedDisciplineId))
                    {
                        disciplineId = parsedDisciplineId;
                    }
                }
            }

            if (docTypeId.HasValue)
                query = query.Where(x => x.DocumentTypeId == docTypeId.Value);

            if (disciplineId.HasValue)
                query = query.Where(x => x.DisciplineId == disciplineId.Value);

            var dtdQuery = from dtd in query
                           join dt in context.DocumentTypes.IgnoreQueryFilters().AsNoTracking()
                               on dtd.DocumentTypeId equals dt.Id into dtJoin
                           from dt in dtJoin.DefaultIfEmpty()
                           join d in context.Disciplines.IgnoreQueryFilters().AsNoTracking()
                               on dtd.DisciplineId equals d.Id into dJoin
                           from d in dJoin.DefaultIfEmpty()
                           join u in context.ApplicationUsers.IgnoreQueryFilters().AsNoTracking()
                               on d.DisciplineUsrId equals u.Id into uJoin
                           from u in uJoin.DefaultIfEmpty()
                           orderby dtd.Seq, dtd.Id
                           select new DocumentTypeDisciplineDTO
                           {
                               Id = dtd.Id,
                               DocumentTypeId = dtd.DocumentTypeId,
                               DocumentTypeName = dt != null ? dt.Name : null,
                               DisciplineId = dtd.DisciplineId,
                               DisciplineName = d != null ? d.Name : null,
                               DisciplineNameAr = d != null ? d.NameAr : null,
                               DisciplineUsrId = d != null ? d.DisciplineUsrId : null,
                               DisciplineUsrName = u != null ? u.Name : null,
                               TaskName = dtd.TaskName,
                               Seq = dtd.Seq,
                               SLA_dec = dtd.SLA_dec,
                               Actions = dtd.Actions,
                               Allocation = dtd.Allocation,
                               IsActive = dtd.IsActive,
                               TenantId = dtd.TenantId,
                               No = dtd.No,
                               Msg = dtd.Msg,
                               ActionComment = dtd.ActionComment,
                               PhotoUrl = dtd.PhotoUrl,
                               CreatedByUsrId = dtd.CreatedByUsrId,
                               UpdatedByUsrId = dtd.UpdatedByUsrId,
                               CreateDate = dtd.CreateDate,
                               UpdateDate = dtd.UpdateDate
                           };

            var list = await dtdQuery.ToListAsync();

            var result = new PaginationModel<DocumentTypeDisciplineDTO>(list, filteringModel?.PageNumber ?? 1, filteringModel?.PageSize ?? 500, list.Count, null);
            return Ok(result);
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
            if (long.TryParse(Id, out long id))
            {
                var dtdQuery = from dtd in context.DocumentTypeDisciplines.IgnoreQueryFilters().AsNoTracking().Where(x => x.Id == id)
                               join dt in context.DocumentTypes.IgnoreQueryFilters().AsNoTracking()
                                   on dtd.DocumentTypeId equals dt.Id into dtJoin
                               from dt in dtJoin.DefaultIfEmpty()
                               join d in context.Disciplines.IgnoreQueryFilters().AsNoTracking()
                                   on dtd.DisciplineId equals d.Id into dJoin
                               from d in dJoin.DefaultIfEmpty()
                               join u in context.ApplicationUsers.IgnoreQueryFilters().AsNoTracking()
                                   on d.DisciplineUsrId equals u.Id into uJoin
                               from u in uJoin.DefaultIfEmpty()
                               select new DocumentTypeDisciplineDTO
                               {
                                   Id = dtd.Id,
                                   DocumentTypeId = dtd.DocumentTypeId,
                                   DocumentTypeName = dt != null ? dt.Name : null,
                                   DisciplineId = dtd.DisciplineId,
                                   DisciplineName = d != null ? d.Name : null,
                                   DisciplineNameAr = d != null ? d.NameAr : null,
                                   DisciplineUsrId = d != null ? d.DisciplineUsrId : null,
                                   DisciplineUsrName = u != null ? u.Name : null,
                                   TaskName = dtd.TaskName,
                                   Seq = dtd.Seq,
                                   SLA_dec = dtd.SLA_dec,
                                   Actions = dtd.Actions,
                                   Allocation = dtd.Allocation,
                                   IsActive = dtd.IsActive,
                                   TenantId = dtd.TenantId,
                                   No = dtd.No,
                                   Msg = dtd.Msg,
                                   ActionComment = dtd.ActionComment,
                                   PhotoUrl = dtd.PhotoUrl,
                                   CreatedByUsrId = dtd.CreatedByUsrId,
                                   UpdatedByUsrId = dtd.UpdatedByUsrId,
                                   CreateDate = dtd.CreateDate,
                                   UpdateDate = dtd.UpdateDate
                               };

                var item = await dtdQuery.FirstOrDefaultAsync();
                return Ok(item);
            }
            return Ok(null);
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
            DocumentTypeDisciplineDTO? Obj = await _genericRepository.GetByNameEntityDTO(Name);
            return Ok(Obj);
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] DocumentTypeDisciplineDTO Model)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            DocumentTypeDiscipline entity = _mapper.Map<DocumentTypeDiscipline>(Model);

            DocumentTypeDiscipline AdjModel = await DocumentTypeDisciplineService.Create(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<DocumentTypeDisciplineDTO>(AdjModel)); 
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }

    [HttpPost("Edit")]
    public async Task<IActionResult> Edit([FromBody] DocumentTypeDisciplineEditDTO Model)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            (List<DocumentTypeDiscipline>? Records, string Msg) = await DocumentTypeDisciplineService.Update(Model, _genericRepository.Context, _tenantService);

            if (Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = Msg });
            else
                return Ok(_mapper.Map<List<DocumentTypeDisciplineDTO>>(Records)); 
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
            DocumentTypeDisciplineDTO? dto = await _genericRepository.GetByIdEntityDTO(Id);

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
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            if(!long.TryParse(Id, out long id))
                return Ok(new { severity = "error", summary = "Validation", msg = "Invalid Id" });
            
            (bool Success, string Msg) = await DocumentTypeDisciplineService.Delete(id, _genericRepository.Context, _tenantService);

            if(Success)
                return Ok(new { severity = "success", summary = "Success", msg = "Record deleted successfully" });
            else
                return Ok(new { severity = "error", summary = "Business Validation", msg = Msg });
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }
// Code-Player-End-DTO //
}
