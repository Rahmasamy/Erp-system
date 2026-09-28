using Aspire.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aspire.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class DevSeedController(
        IDatabaseSeederService seederService,
        IRegisterService registerService,
        IWebHostEnvironment env
    ) : ControllerBase
    {
        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var status = await seederService.GetSeedStatusAsync();
            return Ok(new { success = true, counts = status });
        }

        [HttpPost("seed-all")]
        public async Task<IActionResult> SeedAll()
        {
            var result = await seederService.SeedAllAsync();
            return Ok(result);
        }

        [HttpPost("seed-local-tenant")]
        public IActionResult SeedLocalTenant()
        {
            long tenantId = registerService.CreateTenant();
            registerService.Register(tenantId);
            return Ok(new { success = true, tenantId });
        }

        [HttpPost("seed-activities")]
        public async Task<IActionResult> SeedActivities([FromQuery] long? tenantId = null)
        {
            var result = await seederService.SeedMissingActivitiesAndAttachmentsAsync(tenantId);
            return Ok(result);
        }

        [HttpPost("seed-admin")]
        public async Task<IActionResult> SeedAdmin([FromQuery] string username = "admin", [FromQuery] string email = "admin@aspire.com", [FromQuery] string password = "Sehmawy&*190876")
        {
            var result = await seederService.EnsureAdminUserAsync(username, email, password);
            return Ok(result);
        }

        [HttpPost("seed-all-modules")]
        public IActionResult SeedAllModules([FromQuery] long tenantId = 1)
        {
            registerService.Register(tenantId);
            return Ok(new { success = true, message = "All modules registered successfully for tenant." });
        }
    }
}

