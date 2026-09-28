using Aspire.Context;
using Aspire.Data;
using Aspire.Services;
using Aspire.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Aspire.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(ApplicationDbContext context, IAuthService authService, IPasswordService passwordService, ILogHandlingService logger) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (dto.IsExternalUser)
                {
                    var signInResult = await authService.LoginForExternalUser(new()
                    {
                        Username = dto.Username,
                        Password = dto.Password,
                        IsSelfRegistered = true
                    });

                    if (signInResult.signIn is not null && signInResult.signIn.Succeeded)
                        return Ok(new { Message = "Login successful", signInResult.loginResponse });

                    return Unauthorized(new { Message = "Wrong username or password" });
                }
                else
                {
                    var signInResult = await authService.Login(new()
                    {
                        Username = dto.Username,
                        Password = dto.Password,
                        IsSelfRegistered = true
                    });

                    if (signInResult.signIn is not null && signInResult.signIn.Succeeded)
                        return Ok(new { Message = "Login successful", signInResult.loginResponse });

                    return Unauthorized(new { Message = "Wrong username or password" });
                }
            }
            catch (Exception e)
            {
                logger.LogException(e, nameof(Login));
                var msg = e.InnerException != null ? $"{e.Message} ({e.InnerException.Message})" : e.Message;
                return BadRequest(new { Message = $"Login error: {msg}" });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string logoutId)
        {
            try
            {
                var logoutRequest = await authService.Logout(logoutId);

                return string.IsNullOrEmpty(logoutRequest?.PostLogoutRedirectUri)
                    ? Ok(new { Message = "Logout successful", RedirectUrl = Url.Action("Index", "Home") })
                    : Ok(new { Message = "Logout successful", RedirectUrl = logoutRequest.PostLogoutRedirectUri });
            }
            catch (Exception e)
            {
                logger.LogException(e, nameof(Logout));
                return BadRequest(new { Message = "Something went wrong!" });
            }
        }

        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var token = await passwordService.ForgetPassword(dto);
                if (!string.IsNullOrEmpty(token))
                    return Ok(new { Message = "Password reset token generated", Token = token });

                return BadRequest(new { Message = "User not found" });
            }
            catch (Exception e)
            {
                logger.LogException(e, nameof(ForgotPassword));
                return BadRequest(new { Message = "Something went wrong!" });
            }
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var identityResult = await passwordService.ResetPassword(dto);
                if (identityResult is not null)
                    return identityResult.Succeeded
                    ? Ok(new { Message = "Password reset successful" })
                    : BadRequest(new { Message = identityResult.Errors.Select(e => e.Description) });

                return NotFound(new { Message = "User not found" });
            }
            catch (Exception e)
            {
                logger.LogException(e, nameof(ResetPassword));
                return BadRequest(new { Message = "Something went wrong!" });
            }
        }

        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var identityResult = await passwordService.ChangePassword(dto);

                if (identityResult is not null)
                    return identityResult.Succeeded
                        ? Ok(new { Message = "Password change successful" })
                        : BadRequest(new { Message = identityResult.Errors.Select(e => e.Description) });

                return NotFound(new { Message = "User not found" });
            }
            catch (Exception e)
            {
                logger.LogException(e, nameof(ChangePassword));
                return BadRequest(new { Message = "Something went wrong!" });
            }
        }


        [HttpGet("GetEntityCATs")]
        public async Task<IActionResult> GetEntityCATs()
        {
            try
            {
                return Ok(context.EntityTypes.Where(x => x.EntityName == "Entity").ToList());
            }
            catch (Exception ex)
            {
                return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
            }
        }


        [HttpGet("GetRoleEntityClaims")]
        public async Task<IActionResult> GetRoleEntityClaims([FromQuery] string roleId, [FromQuery] string catId)
        {
            try
            {
                List<RoleEntityClaims> roleEntityClaims = new List<RoleEntityClaims>();
                List<Entity> Ents = context.Entities.Where(x => x.CAT == catId.Substring(0, 3)).ToList();
                foreach (Entity ent in Ents)
                {
                    List<ApplicationRoleClaim> Clms = context.ApplicationRoleClaims.Where(x => x.RoleId == long.Parse(roleId) & x.EntityName == ent.Name).ToList();
                    roleEntityClaims.Add(new RoleEntityClaims { EntityName = ent.Name, Claims = ent.Claims, ClaimsAllowed = Clms.Count > 0 ? Clms[0].ClaimValue : "" });
                }

                return Ok(roleEntityClaims);
            }
            catch (Exception ex)
            {
                return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
            }
        }

        [HttpGet("EditRoleEntityClaims")]
        public async Task<IActionResult> EditRoleEntityClaims([FromQuery] string roleId, [FromBody] RoleEntityClaims Model)
        {
            try
            {
                
                List<ApplicationRoleClaim> Clms = context.ApplicationRoleClaims.Where(x => x.RoleId == long.Parse(roleId)).ToList();

                foreach (ApplicationRoleClaim clm in Clms)
                {
                    clm.EntityName = Model.EntityName;
                    clm.ClaimValue = Model.ClaimsAllowed;
                }

                context.ApplicationRoleClaims.UpdateRange(Clms);
                context.SaveChanges();

                return Ok(Clms);
            }
            catch (Exception ex)
            {
                return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
            }
        }

        //[HttpPost("googlesignin")]
        //public async Task<IActionResult> GoogleSignIn([FromBody] ExternalAuthRequestDTO request)
        //{
        //    try
        //    {
        //        // Validate the Google token
        //        var result = await googleAuthService.GoogleExternalAuthLogIn(request);

        //        if (result.Item1.Count > 0)
        //            return BadRequest(new { Message = result.Item1.Select(e => e.Description) });

        //        return Ok(new { Message = "Login successful", result.loginResponse });
        //    }
        //    catch (Exception e)
        //    {
        //        logger.LogException(e, nameof(GoogleSignIn));
        //        return BadRequest(new { Message = "Something went wrong!" });
        //    }
        //}

        //[HttpPost("googlesignup")]
        //public async Task<IActionResult> GoogleSignUp([FromBody] ExternalAuthRequestDTO request)
        //{
        //    try
        //    {
        //        var result = await googleAuthService.GoogleExternalAuthSignUp(request);

        //        if (result.Item1 is not null)
        //            return Ok(new { Message = "Registration with Google is successful", Id = result.Item2 });

        //        return BadRequest(new { Message = "Something went wrong!" });
        //    }
        //    catch (Exception e)
        //    {
        //        logger.LogException(e, nameof(GoogleSignUp));
        //        return BadRequest(new { Message = "Something went wrong!" });
        //    }
        //}

        //[HttpPost("applesignin")]
        //public async Task<IActionResult> AppleSignIn([FromBody] ExternalAuthRequestDTO request)
        //{
        //    try
        //    {
        //        var result = await appleAuthService.AppleExternalAuth(request);

        //        if (result.Item1.Count > 0)
        //            return BadRequest(new { Message = result.Item1.Select(e => e.Description) });

        //        return Ok(new { Message = "Login successful", result.loginResponse });
        //    }
        //    catch (Exception e)
        //    {
        //        logger.LogException(e, nameof(AppleSignIn));
        //        return BadRequest(new { Message = "Something went wrong!" });
        //    }
        //}
    }
}
