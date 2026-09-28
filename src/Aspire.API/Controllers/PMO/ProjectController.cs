using Aspire.Data;
using Aspire.Services;
using Aspire.Utilities;
using Microsoft.AspNetCore.Mvc;
using Aspire.Context;
using AutoMapper;

namespace Aspire.APIs;

[Route("api/[controller]")]
[ApiController]
public class ProjectController(ApplicationDbContext context, IMapper _mapper, ITenantProviderService _tenantService, IGenericRepository<Project, ProjectDTO, ApplicationDbContext> _genericRepository) : ControllerBase
{
    [HttpPost("GetAll")]
    public async Task<IActionResult> GetAll([FromBody] FilteringModel? filteringModel)
    {
        try
        {
            PaginationModel<ProjectDTO> Objs = await _genericRepository.GetAllJoins(filteringModel);
            List<ProjectTeamDTO> ObjWithTeam_List = new List<ProjectTeamDTO>();
            foreach (ProjectDTO Obj in Objs.Items)
            {
                ProjectTeamDTO ObjWithTeam = _mapper.Map<ProjectTeamDTO>(Obj);

                List<ApplicationUser> StkHldrs = StakeholdersService.Stakeholders(Obj, context);
                foreach (ApplicationUser StkHldr in StkHldrs)
                    ObjWithTeam.Stakeholders.Add(new TeamMemberAvatarDTO() { Name = StkHldr.Name, PhotoUrl = @"/avatars/" + StkHldr.Id + ".png" });

                ObjWithTeam_List.Add(ObjWithTeam);
            }

            PaginationModel<ProjectTeamDTO> Objs2 = new PaginationModel<ProjectTeamDTO>(ObjWithTeam_List, Objs.PageNumber, Objs.PageSize, Objs.TotalItems, Objs.Aggregates);
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
            ProjectDTO? Obj = await _genericRepository.GetByIdEntityDTO(Id);
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
            ProjectDTO? Obj = await _genericRepository.GetByNameEntityDTO(Name);
            return Ok(Obj);
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] ProjectDTO Model)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            Project entity = _mapper.Map<Project>(Model);

            Project AdjModel = await ProjectService.Create(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<ProjectDTO>(AdjModel)); 
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }

    [HttpPost("Edit")]
    public async Task<IActionResult> Edit([FromBody] ProjectDTO Model)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            Project entity = _mapper.Map<Project>(Model);

            Project AdjModel = await ProjectService.Update(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<ProjectDTO>(AdjModel)); 
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
            ProjectDTO? dto = await _genericRepository.GetByIdEntityDTO(Id);

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
            string Msg = ProjectService.OnDelete(long.Parse(Id), _genericRepository.Context);

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

    [HttpPost("Terminate")]
    public async Task<IActionResult> Terminate([FromBody] ProjectDTO Model)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            Project entity = _mapper.Map<Project>(Model);

            Project AdjModel = await ProjectService.Terminate(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<ProjectDTO>(AdjModel)); 
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }
    [HttpPost("On_Hold")]
    public async Task<IActionResult> On_Hold([FromBody] ProjectDTO Model)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            Project entity = _mapper.Map<Project>(Model);

            Project AdjModel = await ProjectService.On_Hold(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<ProjectDTO>(AdjModel)); 
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }
    [HttpPost("Completed")]
    public async Task<IActionResult> Completed([FromBody] ProjectDTO Model)
    {
        try
        {
            if(!_tenantService.IsAllowed(this.HttpContext))
                return Ok(new { severity = "error", summary = "Access", msg = "Access_Denied" });
            
            Project entity = _mapper.Map<Project>(Model);

            Project AdjModel = await ProjectService.Completed(entity, _genericRepository.Context, _tenantService);

            if (AdjModel.Msg != "OK")
                return Ok(new { severity = "error", summary = "Business Validation", msg = AdjModel.Msg });
            else
                return Ok(_mapper.Map<ProjectDTO>(AdjModel)); 
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", msg = ex.Message });
        }
    }

}