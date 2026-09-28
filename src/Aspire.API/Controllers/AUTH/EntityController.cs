using Aspire.DAL;
using Aspire.Data;
using Aspire.Services;
using Aspire.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


using static Aspire.Utilities.GlobalErrorHandling;
/// <summary>

/// Version : 1.0.0
/// </summary>
namespace Aspire.APIs;

[Route("api/[controller]")]
[ApiController]
public class EntityController : ControllerBase
{
    private readonly ILogHandlingService _logger;
    private readonly IEntityService _entityService;
    private readonly IEntityDal _entityDal;
    private readonly ITenantProviderService _tenantProviderService;
    private readonly IUserProviderService _userProviderService;
    private readonly ICurrencyProviderService _currencyProviderService;
    private readonly IMapper _mapper;

    public EntityController(
        ILogHandlingService logger,
        IEntityService entityService,
        IEntityDal entityDal,
        ITenantProviderService tenantProviderService,
        IUserProviderService userProviderService,
        ICurrencyProviderService currencyProviderService,
        IMapper mapper)
    {
        _logger = logger;
        _entityService = entityService;
        _entityDal = entityDal;
        _tenantProviderService = tenantProviderService;
        _userProviderService = userProviderService;
        _currencyProviderService = currencyProviderService;
        _mapper = mapper;
    }


    [HttpPost("GetAll")]
    public async Task<IActionResult> GetAll([FromBody] FilteringModel? filteringModel, [FromHeader] string? PreferredCurrency)
    {
        try
        {
            if (!String.IsNullOrEmpty(PreferredCurrency)) _currencyProviderService.SetCurrency(PreferredCurrency);
            PaginationModel<EntityDTO> userEntities = await _entityService.GetAll(filteringModel);
            if(filteringModel is not null && !String.IsNullOrEmpty(filteringModel.GroupBy))
            {
                var categorizedEntities = userEntities.Items.GroupBy(e => e.EntityType)
                .Select(g => new EntityCategory
                {
                    Category = g.Key,
                    Entities = g.ToList()
                })
                .ToList();
                return Ok(new PaginationModel<EntityCategory>(categorizedEntities, userEntities.TotalItems, userEntities.PageNumber, userEntities.PageSize, userEntities.Aggregates));
            }
            return Ok(userEntities);
        }
        catch (Exception e)
        {
            _logger.LogException(e, nameof(GetAll));
            return StatusCode(500, new { Message = "Something went wrong!" });
        }
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById([FromQuery] string entityId)
    {
        try
        {
            EntityDTO userEntity = await _entityService.GetById(entityId);
            return Ok(userEntity);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Something went wrong!" });
        }
    }

    [HttpPost("Save")]
    public async Task<IActionResult> Save([FromBody] EntityDTO Model, [FromHeader] string? PreferredCurrency)
    {
        if (!String.IsNullOrEmpty(PreferredCurrency)) _currencyProviderService.SetCurrency(PreferredCurrency);
        ProblemDetails problemDetails;
        try
        {
            string TenantId = _tenantProviderService.GetTenantId();
            if (string.IsNullOrEmpty(TenantId))
            {
                problemDetails = SetProblemDetails("Error", "Validation errors", ["Tenant Id is empty"]);
                return BadRequest(problemDetails);
            }

            //Model.TenantId = TenantId;
            ValidatedResult<Entity> result = await _entityService.Save(Model);
            if (result is not null) return Ok(result);

            problemDetails = SetProblemDetails("Error", result.Message, result.Errors);
            return BadRequest(problemDetails);
        }
        catch (Exception e)
        {
            _logger.LogException(e, nameof(Save));
            problemDetails = SetProblemDetails("Error", e.Message, []);
            return BadRequest(problemDetails);
        }
    }

    [HttpPut("UpdateRange")]
    public async Task<IActionResult> UpdateRange([FromBody] List<EntityDTO> Models, [FromHeader] string? PreferredCurrency)
    {
        if (!String.IsNullOrEmpty(PreferredCurrency)) _currencyProviderService.SetCurrency(PreferredCurrency);
        string TenantId = _tenantProviderService.GetTenantId();
        if (string.IsNullOrEmpty(TenantId))
        {
            return BadRequest(new { Message = "Tenant Id is empty" });
        }

        bool result = await _entityService.UpdateRange(Models);
        return result ? Ok(true) : BadRequest(new { Message = "Entities update failed." });
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] string entityId)
    {
        string tenantId = _tenantProviderService.GetTenantId();
        if (string.IsNullOrEmpty(tenantId))
        {
            return BadRequest(new { Message = "Tenant Id is empty" });
        }

        bool result = await _entityService.Delete(entityId);
        return result ? Ok(true) : BadRequest(new { Message = "Entity delete failed." });
    }

    [HttpDelete("DeleteRange")]
    public async Task<IActionResult> DeleteRange([FromQuery] string entitiesIds)
    {
        var ids = entitiesIds.Split(',').Select(e => e.Trim()).ToList();
        string tenantId = _tenantProviderService.GetTenantId();
        if (string.IsNullOrEmpty(tenantId))
        {
            return BadRequest(new { Message = "Tenant Id is empty" });
        }

        bool result = await _entityService.DeleteRange(ids);
        return result ? Ok(true) : BadRequest(new { Message = "Entities delete failed." });
    }

    [HttpGet("GetOptions")]
    public async Task<IActionResult> GetOptions([FromQuery] string Field, [FromQuery] int First, [FromQuery] int PageSize, [FromQuery] string? SearchQuery, [FromQuery] string? EntityType, [FromQuery] string? ProjectId, [FromQuery] string? ProjectItemId)
    {
        string tenantId = _tenantProviderService.GetTenantId();
        if (string.IsNullOrEmpty(tenantId))
        {
            return BadRequest(new { Message = "Tenant Id is empty" });
        }

        //var result = await _entityDal.GetOptions(new EntityForeignOptionsRequest() { Field = Field, First = First, PageSize = PageSize, SearchQuery = SearchQuery });
        return null; //result != null ? Ok(result) : BadRequest(new { Message = "Entity getoptions failed." });
    }
}
