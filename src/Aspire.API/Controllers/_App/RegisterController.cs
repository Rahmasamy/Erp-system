using Aspire.Data;
using Aspire.Services;
using Aspire.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Aspire.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterController(IRegisterService registerService, ITenantProviderService tenantService, ILogHandlingService logger) : ControllerBase
{
    [HttpPost("sendemail")]
    [HttpPost("send-otp")]
    public async Task<IActionResult> SendEmail([FromBody] JsonElement payload)
    {
        try
        {
            string email = string.Empty;
            bool isExternal = false;

            if (payload.ValueKind == JsonValueKind.String)
            {
                email = payload.GetString() ?? string.Empty;
            }
            else if (payload.ValueKind == JsonValueKind.Object)
            {
                if (payload.TryGetProperty("email", out var emailProp)) email = emailProp.GetString() ?? string.Empty;
                else if (payload.TryGetProperty("Email", out var emailPropP)) email = emailPropP.GetString() ?? string.Empty;

                if (payload.TryGetProperty("isExternal", out var extProp)) isExternal = extProp.GetBoolean();
                else if (payload.TryGetProperty("IsExternal", out var extPropP)) isExternal = extPropP.GetBoolean();
                else if (payload.TryGetProperty("isExternalUser", out var extUserProp)) isExternal = extUserProp.GetBoolean();
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new { IsValid = false, Message = "Email address is required." });
            }

            var result = await registerService.SendEmail(email, isExternal);
            if (!result.IsValid)
            {
                return BadRequest(new { IsValid = false, Message = result.Message });
            }

            return Ok(new { IsValid = true, Message = result.Message });
        }
        catch (Exception ex)
        {
            logger.LogException(ex, nameof(SendEmail));
            return BadRequest(new { IsValid = false, Message = "An unexpected error occurred while sending OTP." });
        }
    }

    [HttpPost("send-employee-email")]
    public async Task<IActionResult> SendEmployeeEmail([FromBody] SendEmailDTO dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                return BadRequest(new { IsValid = false, Message = "Email address is required." });
            }

            var result = await registerService.SendEmail(dto.Email, isExternal: false);
            if (!result.IsValid)
            {
                return BadRequest(new { IsValid = false, Message = result.Message });
            }

            return Ok(new { IsValid = true, Message = result.Message });
        }
        catch (Exception ex)
        {
            logger.LogException(ex, nameof(SendEmployeeEmail));
            return BadRequest(new { IsValid = false, Message = "An unexpected error occurred while sending employee OTP." });
        }
    }

    [HttpPost("send-external-email")]
    public async Task<IActionResult> SendExternalEmail([FromBody] SendEmailDTO dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                return BadRequest(new { IsValid = false, Message = "Email address is required." });
            }

            var result = await registerService.SendEmail(dto.Email, isExternal: true);
            if (!result.IsValid)
            {
                return BadRequest(new { IsValid = false, Message = result.Message });
            }

            return Ok(new { IsValid = true, Message = result.Message });
        }
        catch (Exception ex)
        {
            logger.LogException(ex, nameof(SendExternalEmail));
            return BadRequest(new { IsValid = false, Message = "An unexpected error occurred while sending external user OTP." });
        }
    }

    [HttpPost("confirmotp")]
    public IActionResult ConfirmOtp([FromBody] ConfirmOTPDTO dto)
    {
        try
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Otp))
            {
                return BadRequest(new { IsValid = false, Message = "Email and OTP code are required." });
            }

            var result = registerService.ConfrimOtp(dto);
            if (!result.IsValid)
            {
                return BadRequest(new { IsValid = false, Message = result.Message });
            }

            return Ok(new { IsValid = true, Message = result.Message });
        }
        catch (Exception ex)
        {
            logger.LogException(ex, nameof(ConfirmOtp));
            return BadRequest(new { IsValid = false, Message = "An unexpected error occurred during OTP verification." });
        }
    }

    [HttpPost("register")]
    [HttpPost("register-employee")]
    public async Task<IActionResult> RegisterEmployee([FromBody] EmployeeRegisterDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? e.Exception?.Message : e.ErrorMessage));
                return BadRequest(new { IsValid = false, Message = string.IsNullOrWhiteSpace(errors) ? "Invalid registration payload." : errors });
            }

            var result = await registerService.RegisterEmployee(dto);
            if (!result.IsValid)
            {
                return BadRequest(new { IsValid = false, Message = result.Message });
            }

            return Ok(new { IsValid = true, Message = result.Message, loginResponse = result.User });
        }
        catch (Exception ex)
        {
            logger.LogException(ex, nameof(RegisterEmployee));
            return BadRequest(new { IsValid = false, Message = "An unexpected error occurred during employee registration." });
        }
    }

    [HttpPost("register-external")]
    [HttpPost("register-external-user")]
    public async Task<IActionResult> RegisterExternalUser([FromBody] ExternalUserRegisterDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? e.Exception?.Message : e.ErrorMessage));
                return BadRequest(new { IsValid = false, Message = string.IsNullOrWhiteSpace(errors) ? "Invalid external registration payload." : errors });
            }

            var result = await registerService.RegisterExternalUser(dto);
            if (!result.IsValid)
            {
                return BadRequest(new { IsValid = false, Message = result.Message });
            }

            return Ok(new { IsValid = true, Message = result.Message, loginResponse = result.User });
        }
        catch (Exception ex)
        {
            logger.LogException(ex, nameof(RegisterExternalUser));
            return BadRequest(new { IsValid = false, Message = "An unexpected error occurred during external registration." });
        }
    }

    [HttpPost("complete-profile")]
    [HttpPost("completeprofile")]
    public async Task<IActionResult> CompleteProfile([FromBody] CompleteEmployeeProfileDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? e.Exception?.Message : e.ErrorMessage));
                return BadRequest(new { IsValid = false, Message = string.IsNullOrWhiteSpace(errors) ? "Invalid profile payload." : errors });
            }

            long? currentUserId = tenantService.GetUserId();
            var result = await registerService.CompleteProfile(dto, currentUserId);
            if (!result.IsValid)
            {
                return BadRequest(new { IsValid = false, Message = result.Message });
            }

            return Ok(new { IsValid = true, Message = result.Message, loginResponse = result.User });
        }
        catch (Exception ex)
        {
            logger.LogException(ex, nameof(CompleteProfile));
            return BadRequest(new { IsValid = false, Message = "An unexpected error occurred while completing profile." });
        }
    }
}
