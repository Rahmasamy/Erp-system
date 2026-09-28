using Aspire.Data;
using Aspire.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Aspire.Services;

public interface IPasswordService
{
    Task<string> ForgetPassword(ForgotPasswordDTO dto);
    Task<IdentityResult> ResetPassword(ResetPasswordDTO dto);
    Task<IdentityResult> ChangePassword(ChangePasswordDTO dto);
}
public class PasswordService(UserManager<ApplicationUser> userManager, ILogHandlingService logger) : IPasswordService
{
    public async Task<string> ForgetPassword(ForgotPasswordDTO dto)
    {
        try
        {
            logger.LogInformation("Start getting user data for user:'{0}'", dto.Username);
            ApplicationUser? user = await userManager.FindByNameAsync(dto.Username);
            if (user is not null)
            {
                logger.LogInformation("Start generating reset password token for user:'{0}'", dto.Username);
                string resetPasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);

                string token = JwtTokenGenerator.GenerateResetPasswordToken(user.Email, resetPasswordToken);
                // Generate the reset URL
                var resetPasswordUrl = $"{AppConfiguration.FRONTENDURL}/auth/reset";

                logger.LogInformation("Start sending the email for user:'{0}'", user.Email);
                var emailResult = true; // await emailService.SendResetPasswordEmail(user.Email, resetPasswordUrl);
                if (emailResult)
                {
                    logger.LogInformation("Email sent successfully for this email:'{0}'", user.Email);
                    return token;
                }
                else
                {
                    logger.LogInformation("Failed to sent email for this email:'{0}'", user.Email);
                }
            }

            logger.LogInformation("User:'{0}' not found", dto.Username);
            return null;
        }
        catch (Exception e)
        {
            logger.LogException(e, nameof(ForgetPassword));
            throw;
        }
    }

    public async Task<IdentityResult> ResetPassword(ResetPasswordDTO dto)
    {
        try
        {
            if (dto.Token == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "ResetToken",
                    Description = "Reset token is missing or expired."
                });
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(dto.Token) as JwtSecurityToken;

            if (jwtToken == null)
            {
                throw new SecurityTokenException("Invalid token.");
            }

            var email = jwtToken.Claims.First(claim => claim.Type == "email").Value;
            var resetToken = jwtToken.Claims.First(claim => claim.Type == "token").Value;

            ApplicationUser? user = await userManager.FindByNameAsync(email);
            if (user is not null)
            {
                logger.LogInformation("Start reset password for user: '{0}'", email);
                IdentityResult result = await userManager.ResetPasswordAsync(user, resetToken, dto.Password);
                if (result.Succeeded)
                {
                    var updateResult = await userManager.UpdateAsync(user);
                    logger.LogInformation("Finish reset password for user: '{0}'", email);
                    return updateResult;
                }
                return result;
            }

            logger.LogInformation("User: '{0}' not found", email);
            return null;
        }
        catch (Exception e)
        {
            logger.LogException(e, nameof(ResetPassword));
            throw;
        }
    }

    public async Task<IdentityResult> ChangePassword(ChangePasswordDTO dto)
    {
        try
        {
            ApplicationUser? user = await userManager.FindByNameAsync(dto.Username);
            if (user is not null)
            {
                logger.LogInformation("Start changing password for User: '{0}'", dto.Username);
                if (dto.NewPassword != dto.ConfirmNewPassword)
                {
                    logger.LogInformation("Password and Confirm Password not match for User: '{0}'", dto.Username);

                    var identityResult = IdentityResult.Failed(new IdentityError
                    {
                        Code = "PasswordMismatch",
                        Description = "New Password and Confirm Password do not match"
                    });
                    return identityResult;
                }

                IdentityResult result = await userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
                logger.LogInformation("Finish changing password for User: '{0}'", dto.Username);
                return result;
            }
            logger.LogInformation("User: '{0}' not found", dto.Username);
            return null;
        }
        catch (Exception e)
        {
            logger.LogException(e, nameof(ChangePassword));
            throw;
        }
    }
}
