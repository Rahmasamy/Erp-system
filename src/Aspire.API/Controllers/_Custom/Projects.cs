using Aspire.Data;
using Aspire.Services;
using Aspire.Utilities;
using Microsoft.AspNetCore.Mvc;
using Aspire.Context;
using AutoMapper;

namespace Aspire.APIs;

[Route("api/[controller]")]
[ApiController]
public class ProjectCustomController(ApplicationDbContext context, IMapper _mapper, ITenantProviderService _tenantService, IGenericRepository<Project, ProjectDTO, ApplicationDbContext> _genericRepository)
    : ControllerBase
{
    [HttpPost("GetAll")]
    public async Task<IActionResult> GetAll([FromBody] FilteringModel? filteringModel)
    {
        try
        {
            PaginationModel<ProjectDTO> Objs = await _genericRepository.GetAllJoins(filteringModel);

            List<ProjectTeamDTO> PrjWithTeam_List = new List<ProjectTeamDTO>();
            foreach (ProjectDTO Obj in Objs.Items)
            {
                ProjectTeamDTO PrjWithTeam = _mapper.Map<ProjectTeamDTO>(Obj);

                List<ApplicationUser> StkHldrs = context.ApplicationUsers.ToList();
                foreach (ApplicationUser StkHldr in StkHldrs)
                    PrjWithTeam.Stakeholders.Add(new TeamMemberAvatarDTO() { Name = StkHldr.Name, PhotoUrl = @"/avatars/" + StkHldr.Id + ".png" });

                PrjWithTeam_List.Add(PrjWithTeam);
            }

            PaginationModel<ProjectTeamDTO> Objs2 = new PaginationModel<ProjectTeamDTO>(PrjWithTeam_List, Objs.PageNumber, Objs.PageSize, Objs.TotalItems, Objs.Aggregates);
            return Ok(Objs2);
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }
}
