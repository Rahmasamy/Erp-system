//using Aspire.Context;
//
//using Aspire.Data;
//using Aspire.Enums;
//using Aspire.Services;
//using Aspire.Utilities;
//using Microsoft.AspNetCore.Identity;
//using Tradify.Identity.Domain.Helper;

//using Tradify.Shared.Enums;
//using Tradify.Shared.Utilities;

//namespace Aspire.Services;

//public interface IInviteUserService
//{
//    Task<ValidatedResult<string>> InviteUserToTenant(UserInvitationDTO inviteUserDto);
//    Task<ValidatedResult<string>> AcceptInvitation(AcceptInviteDTO acceptInvite);
//    Task<ValidatedResult<CheckInviteResponseDTO>> CheckInvitation(CheckInviteDTO checkInvite);
//}

//public class InviteUserService(ApplicationDbContext context,
//    IMailgunEmailService emailService,
//    //IUserRoleService userRoleService,
//    //IRoleService roleService,
//    ITenantService tenantService,
//    ITenantProviderService tenantProviderService,
//    IUserInvitationDal userInvitationDal,
//    UserManager<ApplicationUser> userManager,
//    //IUserRoleDal userRoleDal, 
//    ILogHandlingService logger) : IInviteUserService
//{
//    public async Task<ValidatedResult<string>> InviteUserToTenant(UserInvitationDTO inviteUserDto)
//    {
//        try
//        {
//            ValidatedResult<string> result = new() { IsValid = false, Message = "Server error" };
//            var user = await userManager.FindByIdAsync(inviteUserDto.UserId);
//            if (user is null)
//            {
//                result.Errors = ["Sending user not found."];
//                return result;
//            }
//            var existingUser = await userManager.FindByEmailAsync(inviteUserDto.Email);
//            if(existingUser is not null)
//            {
//                var userTenants = await tenantService.GetAllAsync(existingUser.Id);
//                if(userTenants.Any(e => e.Id == inviteUserDto.TenantId))
//                {
//                    result.Errors = ["Invited user is already registered to this tenant."];
//                    return result;
//                }
//            }
//            logger.LogInformation("Generate token to invite user to tenant");

//            ApplicationUser applicationUser = new()
//            {
//                Id = Guid.NewGuid().ToString(),
//                Email = inviteUserDto.Email,
//                UserName = inviteUserDto.Email,
//                EmailConfirmed = true,
//                FirstName = inviteUserDto.FirstName,
//                LastName = inviteUserDto.LastName,
//                IsActive = true,
//                IsVerified = true,
//                OTP = string.Empty,
//                LastAccessedTenant = inviteUserDto.TenantId,
//                PreferredCurrency = String.IsNullOrEmpty(inviteUserDto.PreferredCurrency) ? CurrencyEnum.SAR : inviteUserDto.PreferredCurrency,
//                PreferredLanguage = String.IsNullOrEmpty(inviteUserDto.PreferredLanguage) ? LanguageEnum.English : inviteUserDto.PreferredLanguage,
//                JobTitle = inviteUserDto.JobTitle,
//                PhoneNumber = inviteUserDto.PhoneNumber,
//                CreatedByUsrId = inviteUserDto.UserId,
//                CreateDate = DateTime.UtcNow
//            };
//            var token = JwtTokenGenerator.GenerateJwtToken(applicationUser, [], inviteUserDto.RoleId, String.Empty);

//            var invitation = await userInvitationDal.GetByInvitedEmail(inviteUserDto.Email, inviteUserDto.TenantId);
//            if (invitation != null)
//            {
//                if(invitation.TokenExpirationDate > DateTime.UtcNow)
//                {
//                    result.Errors = ["Invitation is already sent for this email"];
//                    return result;
//                }
//                else
//                {
//                    invitation.Token = token;
//                    invitation.TokenExpirationDate = DateTime.UtcNow.AddDays(int.Parse(AppConfiguration.INVITEUSEREXPIRATION));
//                    invitation.RoleId = inviteUserDto.RoleId;
//                    invitation.UserId = inviteUserDto.UserId;
//                    invitation.TenantId = inviteUserDto.TenantId;
//                    invitation.FirstName = inviteUserDto.FirstName;
//                    invitation.LastName = inviteUserDto.LastName;
//                    invitation.JobTitle = inviteUserDto.JobTitle;
//                    invitation.PhoneNumber = inviteUserDto.PhoneNumber;
//                    invitation.PreferredCurrency = inviteUserDto.PreferredCurrency;
//                    invitation.PreferredLanguage = inviteUserDto.PreferredLanguage;
//                    invitation.UpdatedByUsrId = inviteUserDto.UserId;
//                    invitation.UpdateDate = DateTime.UtcNow;
//                    await userInvitationDal.Update(invitation);
//                }
//            }
//            else
//            {
//                await userInvitationDal.Add(new()
//                {
//                    Token = token,
//                    TokenExpirationDate = DateTime.UtcNow.AddDays(int.Parse(AppConfiguration.INVITEUSEREXPIRATION)),
//                    RoleId = inviteUserDto.RoleId,
//                    UserId = inviteUserDto.UserId,
//                    TenantId = inviteUserDto.TenantId,
//                    Email = inviteUserDto.Email,
//                    FirstName = inviteUserDto.FirstName,
//                    LastName = inviteUserDto.LastName,
//                    JobTitle = inviteUserDto.JobTitle,
//                    PhoneNumber = inviteUserDto.PhoneNumber,
//                    PreferredCurrency = inviteUserDto.PreferredCurrency,
//                    PreferredLanguage = inviteUserDto.PreferredLanguage,
//                    CreatedByUsrId = inviteUserDto.UserId,
//                    CreateDate = DateTime.UtcNow
//                });

//                var exists = await userManager.FindByEmailAsync(inviteUserDto.Email);
//                if(exists is null)
//                {
//                    await userManager.CreateAsync(applicationUser, AppConfiguration.DefaultPassword);
//                    logger.LogInformation("Update user: '{0}' data successfully", user.UserName);

//                    context.UserRoles.Add(new()
//                    {
//                        UserId = applicationUser.Id,
//                        RoleId = inviteUserDto.RoleId,
//                        TenantId = inviteUserDto.TenantId,
//                        IsActive = false
//                    });
//                }
//            }

//            #region Send Email
//            logger.LogInformation("Start sending email for inviting user: '{0}'", inviteUserDto.Email);

//            var inviteUserUrl = $"{AppConfiguration.FRONTENDURL}/auth/accept-invitation/{token}";
//            //var inviteUserUrl = $"{"http://localhost:4200"}/auth/accept-invitation/{token}";

//            var emailResult = await emailService.SendInviteUserEmail(inviteUserDto.Email, inviteUserUrl, user.Email);

//            if (emailResult)
//            {
//                logger.LogInformation("Email sent successfully for user: '{0}'", inviteUserDto.Email);
//                logger.LogInformation("Finish transaction for invite user '{0}' to tenant: '{1}'", inviteUserDto.UserId, inviteUserDto.TenantId);
//                result.Message = String.Empty;
//                result.IsValid = true;
//                result.Result = "Invitation sent successfully";
//                return result;
//            }

//            #endregion

//            result.Errors = ["Invitation not sent"];
//            return result;
//        }
//        catch (Exception ex)
//        {
//            logger.LogException(ex, nameof(InviteUserToTenant));
//            throw;
//        }
//    }

//    public async Task<ValidatedResult<string>> AcceptInvitation(AcceptInviteDTO acceptInvite)
//    {
//        try
//        {
//            ValidatedResult<string> result = new() { IsValid = false, Message = "Server error" };
//            var userInvitation = await userInvitationDal.GetByToken(acceptInvite.Token);
//            if (userInvitation is null)
//            {
//                result.Errors = ["Invalid token."];
//                return result;
//            }

//            tenantProviderService.SetTenantId(userInvitation.TenantId);

//            var user = await userManager.FindByEmailAsync(userInvitation.Email);
//            await userManager.ChangePasswordAsync(user, AppConfiguration.DefaultPassword, acceptInvite.Password);
//            //await userRoleDal.UpdateUserRole(user.Id, userInvitation.RoleId, userInvitation.TenantId, true);

//            await userInvitationDal.Delete(userInvitation.Id);
//            result.Message = String.Empty;
//            result.IsValid = true;
//            result.Result = "Invitation accepted";
//            return result;
//        }
//        catch (Exception ex)
//        {
//            logger.LogException(ex, nameof(AcceptInvitation));
//            throw;
//        }
//    }

//    public async Task<ValidatedResult<CheckInviteResponseDTO>> CheckInvitation(CheckInviteDTO checkInvite)
//    {
//        try
//        {
//            ValidatedResult<CheckInviteResponseDTO> result = new() { IsValid = false, Message = "Server error" };

//            var userInvitation = await userInvitationDal.GetByToken(checkInvite.Token);
//            if(userInvitation is null)
//            {
//                result.Errors = ["Invalid or expired token."];
//                return result;
//            }
//            if(userInvitation.TokenExpirationDate < DateTime.UtcNow)
//            {
//                await userInvitationDal.Delete(userInvitation.Id);
//                result.Errors = ["Invalid or expired token."];
//                return result;
//            }

//            tenantProviderService.SetTenantId(userInvitation.TenantId);
//            var existingUser = await userManager.FindByEmailAsync(userInvitation.Email);
//            var role = context.Roles.Select(e => e.Id == userInvitation.RoleId);
//            var tenant = context.Roles.Select(e => e.Id == userInvitation.RoleId);
//            result.Message = String.Empty;
//            result.IsValid = true;
//            if (existingUser is not null)
//            {
//                //await userRoleDal.UpdateUserRole(existingUser.Id, userInvitation.RoleId, role.TenantId, true);
//                await userInvitationDal.Delete(userInvitation.Id);
//                result.Result = new() 
//                { 
//                    IsRegisterd = true, 
//                    Tenant = tenant.CompanyName ?? "New tenant" 
//                };
//                return result;
//            }
//            result.Result = new() 
//            { 
//                IsRegisterd = false,
//                Email = userInvitation.Email, 
//                RoleId = userInvitation.RoleId, 
//                RoleName = role.Name, 
//                Tenant = tenant.CompanyName 
//            };
//            return result;
//        }
//        catch (Exception ex)
//        {
//            logger.LogException(ex, nameof(CheckInvitation));
//            throw;
//        }
//    }
//}
