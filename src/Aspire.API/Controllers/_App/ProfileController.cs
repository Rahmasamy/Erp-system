using Aspire.Context;
using Aspire.Data;
using Aspire.Services;
using Aspire.Utilities;
using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Aspire.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController(IAuthService authService, UserManager<ApplicationUser> userManager,
        IGenericRepository<UserPreference, UserPreferenceDTO, ApplicationDbContext> _genericRepository,
        IGenericRepository<Workflow, WorkflowDTO, ApplicationDbContext> _genericRepositoryWF,
        IMapper _mapper, ApplicationDbContext context, ILogHandlingService logger,        
        ITenantProviderService _tenantService) : ControllerBase
{

    [HttpGet("getlists")]
    public async Task<IActionResult> GetLists([FromQuery] int First, [FromQuery] int PageSize, [FromQuery] string? SearchQuery, [FromQuery] string? Entity, [FromQuery] string? WhrCond, [FromQuery] string? SelectedId)
    {
        var result = _genericRepository.GetLists(new ListsRequest() { First = First, PageSize = PageSize, SearchQuery = SearchQuery, Entity = Entity, SelectedId = SelectedId });
        return result != null ? Ok(result) : BadRequest(new { Message = "Get Lists failed." });
    }

    [HttpGet("getoptions")]
    public async Task<IActionResult> GetOptions([FromQuery] string Field, [FromQuery] string? EntityName, [FromQuery] int First, [FromQuery] int PageSize, [FromQuery] string? WhrCond, [FromQuery] string? SearchQuery = null)
    {
        var result = _genericRepository.GetOptions(new OptionsRequest() { Field = Field, EntityName = EntityName, First = First, PageSize = PageSize, WhrCond = WhrCond, SearchQuery = SearchQuery });
        return result != null ? Ok(result) : BadRequest(new { Message = "Get Options failed." });
    }

    [HttpPost("createVueFlow")]
    public async Task<IActionResult> createVueFlow([FromBody] Workflow Entity)
    {
        Workflow entity = new Workflow();
        entity.TenantId = _tenantService.GetTenantId();
        entity.CreatedByUsrId = _tenantService.GetUserId();
        entity.CreateDate = DateTime.UtcNow;
        entity.Msg = "OK";
        entity.Name = Entity.Name;
        entity.NameAr = Entity.NameAr;
        entity.EntityId = Entity.EntityId;
        entity.IsActive = false;
        entity.Version = "V-1.0.0";
        entity.EntityStatusId = context.EntityStatuses.Where(e => e.EntityName == "Workflow" && e.Name == "Draft").FirstOrDefault().Id;
        context.Workflows.Add(entity);
        await context.SaveChangesAsync();

        return Ok(_mapper.Map<WorkflowDTO>(entity));
    }

    [HttpPost("editVueFlow")]
    public async Task<IActionResult> editVueFlow([FromBody] Workflow Entity)
    {
        Workflow? entity = context.Workflows.Where(e => e.Id == Entity.Id).FirstOrDefault();
        entity.UpdatedByUsrId = _tenantService.GetUserId();
        entity.UpdateDate = DateTime.UtcNow;
        entity.Name = Entity.Name;
        entity.NameAr = Entity.NameAr;
        entity.Description = Entity.Description;
        entity.DescriptionAr = Entity.DescriptionAr;
        entity.IsActive = Entity.IsActive;
        entity.Version = Entity.Version;
        entity.EntityTypeId = Entity.EntityTypeId;
        entity.EntityStatusId = Entity.EntityStatusId;
        entity.Msg = "OK";
        entity.VueFlowDesign = Entity.VueFlowDesign;
        await context.SaveChangesAsync();
        WorkflowDTO dto = null;
        try
        {
            dto = _mapper.Map<WorkflowDTO>(entity);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
        return Ok(dto);
    }

    [HttpPost("toggleActivation")]
    public async Task<IActionResult> toggleActivation([FromQuery] string Id)
    {
        WorkflowDTO? entity = await _genericRepositoryWF.GetByIdEntityDTO(Id);
        entity.UpdatedByUsrId = _tenantService.GetUserId();
        entity.UpdateDate = DateTime.UtcNow;
        entity.IsActive = !entity.IsActive;
        entity.Msg = "OK";
        await context.SaveChangesAsync();

        return Ok(_mapper.Map<WorkflowDTO>(entity));
    }

    [HttpGet("getVueFlow")]
    public async Task<IActionResult> getVueFlow([FromQuery] string Id)
    {
        WorkflowDTO? Obj = await _genericRepositoryWF.GetByIdEntityDTO(Id);
        return Ok(Obj);
    }

    [HttpPost("savepreferences")]
    public async Task<IActionResult> SavePreferences([FromBody] UserPreferenceDTO Model)
    {
        Model.UserId = _tenantService.GetUserId();
        FilteringModel filteringModel = new FilteringModel();
        filteringModel.FilterFields.Add(new FilterField("UserId", FieldType.Text, _tenantService.GetUserId().ToString()));
        filteringModel.FilterFields.Add(new FilterField("TableName", FieldType.Text, Model.TableName ?? ""));

        PaginationModel<UserPreferenceDTO> Objs = await _genericRepository.GetAllJoins(filteringModel);
        if (Objs.Items.Count() == 0)
            Model.Name = Model.TableName + ":" + Model.UserId;
        else
            Model.Id = Objs.Items.FirstOrDefault().Id;

        string result = await _genericRepository.Save(Model, HttpContext);
        return Ok(new { severity = result.Split(":")[0], summary = result.Split(":")[1], message = result.Split(":")[2] });
    }

    [HttpGet("getpreferences")]
    public async Task<IActionResult> GetPreferences([FromQuery] UserPreferenceDTO Model)
    {
        Model.TenantId = _tenantService.GetTenantId();
        Model.UserId = _tenantService.GetUserId();

        FilteringModel filteringModel = new FilteringModel();
        filteringModel.FilterFields.Add(new FilterField("UserId", FieldType.Text, _tenantService.GetUserId().ToString()));
        filteringModel.FilterFields.Add(new FilterField("TableName", FieldType.Text, Model.TableName ?? ""));

        UserPreferenceDTO? result = null;

        try
        {
            PaginationModel<UserPreferenceDTO> Objs = await _genericRepository.GetAllJoins(filteringModel);
            
            if (Objs.Items.Count() > 0)
                result = Objs.Items.FirstOrDefault();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

        return Ok(result);
    }


    [HttpPost("updateprofile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            logger.LogInformation("Start update user profile");
            ApplicationUser? user = await userManager.FindByEmailAsync(dto.Email);
            if (user is not null)
            {
                user.CurrencyId = dto.CurrencyId;
                user.LanguageId = dto.LanguageId;

                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.Email = dto.Email;
                user.PhoneNumber = dto.PhoneNumber;
                user.JobTitle = dto.JobTitle;

                IdentityResult result = await userManager.UpdateAsync(user);
                return Ok(new { Message = "Profile update successful" });
            }
            return BadRequest(new { Message = "User not found" });
        }
        catch (Exception e)
        {
            logger.LogException(e, nameof(UpdateProfile));
            return BadRequest(new { Message = "Something went wrong!" });
        }
    }


    [HttpGet("getuserdata")]
    public async Task<IActionResult> GetUserData()
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ApplicationUser? user = await userManager.FindByIdAsync(_tenantService.GetUserId().ToString());
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }
            bool isExternal = _tenantService.GetIsExternalUser() ?? user.IsExternalUser;
            return Ok(authService.GetLoginResponse(user, isExternal || user.IsExternalUser));

            //return NotFound(new { Message = "User not found" });
        }
        catch (Exception e)
        {
            logger.LogException(e, nameof(UpdateProfile));
            return BadRequest(new { Message = e.InnerException });
        }
    }
}
