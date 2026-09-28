
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
public class ClaimController : ControllerBase
{
    private readonly ILogHandlingService _logger;
    private readonly IClaimService _claimService;
    private readonly IClaimDal _claimDal;
    private readonly ITenantProviderService _tenantProviderService;
    private readonly IUserProviderService _userProviderService;
    private readonly ICurrencyProviderService _currencyProviderService;
    private readonly IMapper _mapper;

    public ClaimController(
        ILogHandlingService logger,
        IClaimService claimService,
        IClaimDal claimDal,
        ITenantProviderService tenantProviderService,
        IUserProviderService userProviderService,
        ICurrencyProviderService currencyProviderService,
        IMapper mapper)
    {
        _logger = logger;
        _claimService = claimService;
        _claimDal = claimDal;
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
            PaginationModel<ClaimDTO> userClaims = await _claimService.GetAll(filteringModel);
            return Ok(userClaims);
        }
        catch (Exception e)
        {
            _logger.LogException(e, nameof(GetAll));
            return StatusCode(500, new { Message = "Something went wrong!" });
        }
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById([FromQuery] string claimId)
    {
        try
        {
            ClaimDTO userClaim = await _claimService.GetById(claimId);
            return Ok(userClaim);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Something went wrong!" });
        }
    }

    [HttpPost("Save")]
    public async Task<IActionResult> Save([FromBody] ClaimDTO Model, [FromHeader] string? PreferredCurrency)
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
            ValidatedResult<Claim> result = await _claimService.Save(Model);
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
    public async Task<IActionResult> UpdateRange([FromBody] List<ClaimDTO> Models, [FromHeader] string? PreferredCurrency)
    {
        if (!String.IsNullOrEmpty(PreferredCurrency)) _currencyProviderService.SetCurrency(PreferredCurrency);
        string TenantId = _tenantProviderService.GetTenantId();
        if (string.IsNullOrEmpty(TenantId))
        {
            return BadRequest(new { Message = "Tenant Id is empty" });
        }

        bool result = await _claimService.UpdateRange(Models);
        return result ? Ok(true) : BadRequest(new { Message = "Claims update failed." });
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] string claimId)
    {
        string tenantId = _tenantProviderService.GetTenantId();
        if (string.IsNullOrEmpty(tenantId))
        {
            return BadRequest(new { Message = "Tenant Id is empty" });
        }

        bool result = await _claimService.Delete(claimId);
        return result ? Ok(true) : BadRequest(new { Message = "Claim delete failed." });
    }

    [HttpDelete("DeleteRange")]
    public async Task<IActionResult> DeleteRange([FromQuery] string claimsIds)
    {
        var ids = claimsIds.Split(',').Select(e => e.Trim()).ToList();
        string tenantId = _tenantProviderService.GetTenantId();
        if (string.IsNullOrEmpty(tenantId))
        {
            return BadRequest(new { Message = "Tenant Id is empty" });
        }

        bool result = await _claimService.DeleteRange(ids);
        return result ? Ok(true) : BadRequest(new { Message = "Claims delete failed." });
    }

    [HttpGet("GetOptions")]
    public async Task<IActionResult> GetOptions([FromQuery] string Field, [FromQuery] int First, [FromQuery] int PageSize, [FromQuery] string? SearchQuery, [FromQuery] string? ClaimType, [FromQuery] string? ProjectId, [FromQuery] string? ProjectItemId)
    {
        string tenantId = _tenantProviderService.GetTenantId();
        if (string.IsNullOrEmpty(tenantId))
        {
            return BadRequest(new { Message = "Tenant Id is empty" });
        }

        //var result = await _claimDal.GetOptions(new EntityForeignOptionsRequest() { Field = Field, First = First, PageSize = PageSize, SearchQuery = SearchQuery });
        return null; // result != null ? Ok(result) : BadRequest(new { Message = "Claim getoptions failed." });
    }
}
