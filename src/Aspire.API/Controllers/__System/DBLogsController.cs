using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Aspire.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DBLogsController(ApplicationDbContext context, ILogHandlingService logger) : ControllerBase
{
    [HttpGet("GetLogs")]
    public async Task<IActionResult> GetLogs([FromQuery] long objectId, [FromQuery] string? entityName = null)
    {
        try
        {
            var logs = await DBLog.GetLogs(objectId, context, entityName);
            return Ok(logs);
        }
        catch (Exception ex)
        {
            logger.LogException(ex, nameof(GetLogs));
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("GetByObjectId")]
    public async Task<IActionResult> GetByObjectId([FromQuery] long objectId, [FromQuery] string? entityName = null)
    {
        try
        {
            var logs = await DBLog.GetLogs(objectId, context, entityName);
            return Ok(logs);
        }
        catch (Exception ex)
        {
            logger.LogException(ex, nameof(GetLogs));
            return BadRequest(new { Message = ex.Message });
        }
    }
}
