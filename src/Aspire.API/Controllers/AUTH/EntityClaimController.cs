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
public class EntityClaimController : ControllerBase
{
    private readonly ILogHandlingService _logger;
    private readonly IEntityClaimService _entityClaimService;
    private readonly IEntityClaimDal _entityClaimDal;
    private readonly ITenantProviderService _tenantProviderService;
    private readonly IUserProviderService _userProviderService;
    private readonly ICurrencyProviderService _currencyProviderService;
    private readonly IMapper _mapper;

    public EntityClaimController(
        ILogHandlingService logger,
        IEntityClaimService entityClaimService,
        IEntityClaimDal entityClaimDal,
        ITenantProviderService tenantProviderService,
        IUserProviderService userProviderService,
        ICurrencyProviderService currencyProviderService,
        IMapper mapper)
    {
        _logger = logger;
        _entityClaimService = entityClaimService;
        _entityClaimDal = entityClaimDal;
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
            PaginationModel<EntityClaimDTO> userEntityClaims = await _entityClaimService.GetAll(filteringModel);
            return Ok(userEntityClaims);
        }
        catch (Exception e)
        {
            _logger.LogException(e, nameof(GetAll));
            return StatusCode(500, new { Message = "Something went wrong!" });
        }
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById([FromQuery] string entityClaimId)
    {
        try
        {
            EntityClaimDTO userEntityClaim = await _entityClaimService.GetById(entityClaimId);
            return Ok(userEntityClaim);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Something went wrong!" });
        }
    }

    [HttpPost("Save")]
    public async Task<IActionResult> Save([FromBody] EntityClaimDTO Model, [FromHeader] string? PreferredCurrency)
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
            ValidatedResult<EntityClaim> result = await _entityClaimService.Save(Model);
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
    public async Task<IActionResult> UpdateRange([FromBody] List<EntityClaimDTO> Models, [FromHeader] string? PreferredCurrency)
    {
        if (!String.IsNullOrEmpty(PreferredCurrency)) _currencyProviderService.SetCurrency(PreferredCurrency);
        string TenantId = _tenantProviderService.GetTenantId();
        if (string.IsNullOrEmpty(TenantId))
        {
            return BadRequest(new { Message = "Tenant Id is empty" });
        }

        bool result = await _entityClaimService.UpdateRange(Models);
        return result ? Ok(true) : BadRequest(new { Message = "EntityClaims update failed." });
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] string entityClaimId)
    {
        string tenantId = _tenantProviderService.GetTenantId();
        if (string.IsNullOrEmpty(tenantId))
        {
            return BadRequest(new { Message = "Tenant Id is empty" });
        }

        bool result = await _entityClaimService.Delete(entityClaimId);
        return result ? Ok(true) : BadRequest(new { Message = "EntityClaim delete failed." });
    }

    [HttpDelete("DeleteRange")]
    public async Task<IActionResult> DeleteRange([FromQuery] string entityClaimsIds)
    {
        var ids = entityClaimsIds.Split(',').Select(e => e.Trim()).ToList();
        string tenantId = _tenantProviderService.GetTenantId();
        if (string.IsNullOrEmpty(tenantId))
        {
            return BadRequest(new { Message = "Tenant Id is empty" });
        }

        bool result = await _entityClaimService.DeleteRange(ids);
        return result ? Ok(true) : BadRequest(new { Message = "EntityClaims delete failed." });
    }

    [HttpGet("GetOptions")]
    public async Task<IActionResult> GetOptions([FromQuery] string Field, [FromQuery] int First, [FromQuery] int PageSize, [FromQuery] string? SearchQuery, [FromQuery] string? EntityClaimType, [FromQuery] string? ProjectId, [FromQuery] string? ProjectItemId)
    {
        string tenantId = _tenantProviderService.GetTenantId();
        if (string.IsNullOrEmpty(tenantId))
        {
            return BadRequest(new { Message = "Tenant Id is empty" });
        }

        //var result = await _entityClaimDal.GetOptions(new EntityForeignOptionsRequest() { Field = Field, First = First, PageSize = PageSize, SearchQuery = SearchQuery });
        return null; // result != null ? Ok(result) : BadRequest(new { Message = "EntityClaim getoptions failed." });
    }
}
