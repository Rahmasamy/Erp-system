
using Aspire.Data;
using Aspire.Services;
using Aspire.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Aspire.APIs;

[ApiController]
[Route("api/[controller]")]
public class TenantsController(
    ITenantService tenantService,
    ILogHandlingService logger,
    IUserProviderService userProviderService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            List<TenantDTO> tenants = await tenantService.GetAllAsync(userProviderService.GetUserId());
            return Ok(tenants);
        }
        catch (Exception ex)
        {
            logger.LogException(ex, nameof(GetAll));
            return BadRequest(ex);
        }
    }

    [HttpGet("getbyid/{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            Aspire.Data.Tenant? tenant = await tenantService.GetByIdAsync(id);
            return tenant is null ? BadRequest(new { Message = "Tenant Not Found" }) : Ok(tenant);
        }
        catch (Exception ex)
        {
            logger.LogException(ex, nameof(GetById));
            return BadRequest(ex);
        }
    }

    [HttpPost("createtenant")]
    public async Task<IActionResult> CreateTenant([FromBody] CreateAndUpdateTenantDTO createTenantDto)
    {
        logger.LogInformation("Request Reached Create Tenant Successfully");
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = userProviderService.GetUserId();
            logger.LogInformation("Creating tenant for user: {userId}", userId);

            if (string.IsNullOrEmpty(userId))
            {
                logger.LogInformation("Unauthorized");
                return Unauthorized(new { Message = "Unauthorized" });
            }

            Aspire.Data.Tenant? tenant = await tenantService.CreateAsync(createTenantDto);
            return tenant is null
                ? BadRequest(new { Message = "Tenant already exists!" })
                : CreatedAtAction(nameof(CreateTenant), new { tenant, Message = "Tenant created successfully" });
        }
        catch (Exception ex)
        {
            logger.LogException(ex, nameof(CreateTenant));
            return BadRequest(ex);
        }
    }

    [HttpPut("updatetenant/{id}")]
    public async Task<IActionResult> UpdateTenant(string id, [FromBody] CreateAndUpdateTenantDTO updateTenantDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool res = await tenantService.UpdateAsync(id, updateTenantDto);
            return !res ? BadRequest(new { Message = "Tenant not found!" }) : Ok(new { Message = "Tenant Updated successfully" });
        }
        catch (Exception ex)
        {
            logger.LogException(ex, nameof(UpdateTenant));
            return BadRequest(ex);
        }
    }

    [HttpDelete("deletetenant/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            bool res = await tenantService.DeleteAsync(id);
            return !res ? BadRequest(new { Message = "Tenant not found!" }) : Ok(new { Message = "Tenant Deleted successfully" });
        }
        catch (Exception ex)
        {
            logger.LogException(ex, nameof(Delete));
            return BadRequest(ex);
        }
    }

    [HttpPost("addtenantidtotoken")]
    public async Task<IActionResult> AddTenantIdToToken(TenantIdJwtDTO model)
    {
        try
        {
            string res = tenantService.AddTenantIdToJwtToken(model.TenantId);
            return string.IsNullOrEmpty(res) ? BadRequest(new { Message = "TenantId not added to JWT token!" }) : Ok(new { Message = res });
        }
        catch (Exception ex)
        {
            logger.LogException(ex, nameof(Delete));
            return BadRequest(ex);
        }
    }
}
