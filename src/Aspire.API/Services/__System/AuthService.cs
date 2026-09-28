using Aspire.Context;
using Aspire.Data;
using Aspire.Enums;
using Aspire.Utilities;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Aspire.Services;

public interface IAuthService
{
    LoginResponseDTO GetLoginResponse(ApplicationUser? user, bool isExternalUser = false);
    LoginResponseForExternalUserDTO GetLoginResponseForExternalUser(ApplicationUser? user);
    Task<(SignInResult signIn, LoginResponseDTO loginResponse)> Login(LoginDTO dto);
    Task<(SignInResult signIn, LoginResponseForExternalUserDTO loginResponse)> LoginForExternalUser(LoginDTO dto);
    Task<LogoutRequest> Logout(string logoutId);
}

public class AuthService(SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager, IIdentityServerInteractionService interactionService,
    ITenantProviderService _tenantService,
    ILogHandlingService logger, ApplicationDbContext context) : IAuthService
{
    public LoginResponseDTO GetLoginResponse(ApplicationUser? user, bool isExternalUser = false)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user), "User cannot be null");

        var tenantId = user.TenantId ?? 1;
        var currencyId = user.CurrencyId ?? 1;
        var langAbbrv = user.LangAbbrv ?? "English";

        _tenantService.SetTenantId(tenantId);
        _tenantService.SetUserId(user.Id);
        _tenantService.SetCurrencyId(currencyId);
        _tenantService.SetLangAbbrv(langAbbrv);

        var tenants = context.Tenants.ToList();
        var dtCls = context.DataConfidentialities.ToList();
        var currs = context.Currencies.ToList();
        var taxes = context.TaxRates.ToList();

        // Claims for Retrieving Data  
        List<ActiveUserClaimDTO> userClaims = new List<ActiveUserClaimDTO>();

        List<ApplicationUserRole> userRolesView = context.ApplicationUserRoles.Where(e => e.UserId == user.Id).ToList();
        foreach (var userRole in userRolesView)
        {
            List<ApplicationRoleClaim> roleClaims = context.ApplicationRoleClaims.Where(e => e.RoleId == userRole.RoleId).ToList();                        
            userClaims.AddRange(roleClaims.Select(e => new ActiveUserClaimDTO(e.EntityName, e.ClaimType, e.ClaimValue)));
        }

        List<ActiveUserClaimDTO> userClaimsToken = userClaims.GroupBy(i => i.EntityName)
                                                                .Select(group  => new ActiveUserClaimDTO() { 
                                                                        EntityName = group.Key, 
                                                                        ClaimType = string.Join(",", group.Select(item => item.ClaimType)) 
                                                                    }).ToList();

        ExternalUser? extUsr = context.ExternalUsers.FirstOrDefault(x => x.AppUsrId == user.Id || (x.Email != null && user.Email != null && (x.Email == user.Email || user.Email.StartsWith(x.Email))));
        Contractor? Contrct = (extUsr != null && extUsr.ContractorId.HasValue) 
            ? context.Contractors.FirstOrDefault(x => x.Id == extUsr.ContractorId.Value) 
            : null;

        bool effectiveIsExternal = isExternalUser || user.IsExternalUser || extUsr != null || user.SeatType == SeatTypeEnum.Contractor;

        string fullName = (!string.IsNullOrWhiteSpace(user.FirstName) || !string.IsNullOrWhiteSpace(user.LastName))
            ? $"{user.FirstName} {user.LastName}".Trim()
            : (!string.IsNullOrWhiteSpace(user.Name) ? user.Name : (user.Email ?? user.UserName ?? "User"));

        string firstName = !string.IsNullOrWhiteSpace(user.FirstName) 
            ? user.FirstName 
            : (!string.IsNullOrWhiteSpace(user.Name) ? user.Name.Split(' ').FirstOrDefault() ?? "" : "");

        string lastName = !string.IsNullOrWhiteSpace(user.LastName)
            ? user.LastName
            : (!string.IsNullOrWhiteSpace(user.Name) && user.Name.Contains(' ') ? user.Name.Substring(user.Name.IndexOf(' ') + 1) : "");

        LoginResponseDTO response = new()
        {
            Token = "",
            Id = user.Id,
            TenantId = tenantId,
            CurrencyId = currencyId,
            ContractorId = Contrct?.Id,
            LangAbbrv = langAbbrv,
            SeatType = user.SeatType ?? (effectiveIsExternal ? "Contractor" : "User"),
            IsExternalUser = effectiveIsExternal,
            DataClasses = dtCls.Select(e => new ActiveDataConfidentialityDTO(e)).ToList(),
            Currencies = currs.Select(e => new ActiveCurrencyDTO(e)).ToList(),
            TaxRates = taxes.Select(e => new ActiveTaxRateDTO(e)).ToList(),
            Email = user.Email,
            FullName = fullName,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = user.PhoneNumber,
            Mobile = user.Mobile,
            JobTitle = user.JobTitle,
            IsValidProfile = user.IsValidProfile ?? false,
        };

        var token = JwtTokenGenerator.GenerateJwtToken(user, Contrct, userClaimsToken.Distinct().ToList(), effectiveIsExternal);
        response.Token = token;

        return response;
    }

    public LoginResponseForExternalUserDTO GetLoginResponseForExternalUser(ApplicationUser? user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user), "User cannot be null");

        var tenantId = user.TenantId ?? 1;
        var currencyId = user.CurrencyId ?? 1;
        var langAbbrv = user.LangAbbrv ?? "English";

        _tenantService.SetTenantId(tenantId);
        _tenantService.SetUserId(user.Id);
        _tenantService.SetCurrencyId(currencyId);
        _tenantService.SetLangAbbrv(langAbbrv);

        var tenants = context.Tenants.ToList();
        var dtCls = context.DataConfidentialities.ToList();
        var currs = context.Currencies.ToList();
        var taxes = context.TaxRates.ToList();

        // Claims for Retrieving Data  
        List<ActiveUserClaimDTO> userClaims = new List<ActiveUserClaimDTO>();

        List<ApplicationUserRole> userRolesView = context.ApplicationUserRoles.Where(e => e.UserId == user.Id).ToList();
        foreach (var userRole in userRolesView)
        {
            List<ApplicationRoleClaim> roleClaims = context.ApplicationRoleClaims.Where(e => e.RoleId == userRole.RoleId).ToList();
            userClaims.AddRange(roleClaims.Select(e => new ActiveUserClaimDTO(e.EntityName, e.ClaimType, e.ClaimValue)));
        }

        List<ActiveUserClaimDTO> userClaimsToken = userClaims.GroupBy(i => i.EntityName)
                                                                .Select(group => new ActiveUserClaimDTO()
                                                                {
                                                                    EntityName = group.Key,
                                                                    ClaimType = string.Join(",", group.Select(item => item.ClaimType))
                                                                }).ToList();

        ExternalUser? extUsr = context.ExternalUsers.FirstOrDefault(x => x.AppUsrId == user.Id || (x.Email != null && user.Email != null && (x.Email == user.Email || user.Email.StartsWith(x.Email))));
        Contractor? Contrct = (extUsr != null && extUsr.ContractorId.HasValue) 
            ? context.Contractors.FirstOrDefault(x => x.Id == extUsr.ContractorId.Value) 
            : null;

        string fullName = (!string.IsNullOrWhiteSpace(user.FirstName) || !string.IsNullOrWhiteSpace(user.LastName))
            ? $"{user.FirstName} {user.LastName}".Trim()
            : (!string.IsNullOrWhiteSpace(extUsr?.FullName) ? extUsr.FullName : (!string.IsNullOrWhiteSpace(user.Name) ? user.Name : (user.Email ?? user.UserName ?? "External User")));

        string firstName = !string.IsNullOrWhiteSpace(user.FirstName)
            ? user.FirstName
            : (extUsr?.FullName != null ? extUsr.FullName.Split(' ').FirstOrDefault() ?? "" : (!string.IsNullOrWhiteSpace(user.Name) ? user.Name.Split(' ').FirstOrDefault() ?? "" : ""));

        string lastName = !string.IsNullOrWhiteSpace(user.LastName)
            ? user.LastName
            : (extUsr?.FullName != null && extUsr.FullName.Contains(' ') ? extUsr.FullName.Substring(extUsr.FullName.IndexOf(' ') + 1) : (!string.IsNullOrWhiteSpace(user.Name) && user.Name.Contains(' ') ? user.Name.Substring(user.Name.IndexOf(' ') + 1) : ""));

        LoginResponseForExternalUserDTO response = new()
        {
            Token = "",
            Id = user.Id,
            TenantId = tenantId,
            CurrencyId = currencyId,
            LangAbbrv = langAbbrv,
            SeatType = user.SeatType ?? "Contractor",
            ContractorId = Contrct?.Id,
            IsExternalUser = true,
            DataClasses = dtCls.Select(e => new ActiveDataConfidentialityDTO(e)).ToList(),
            Currencies = currs.Select(e => new ActiveCurrencyDTO(e)).ToList(),
            TaxRates = taxes.Select(e => new ActiveTaxRateDTO(e)).ToList(),
            Email = user.Email,
            FullName = fullName,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = user.PhoneNumber ?? extUsr?.Phone,
            Mobile = user.Mobile ?? extUsr?.Phone,
            JobTitle = user.JobTitle,
            IsValidProfile = user.IsValidProfile ?? true,
        };

        var token = JwtTokenGenerator.GenerateJwtToken(user, Contrct, userClaimsToken.Distinct().ToList(), true);
        response.Token = token;

        return response;
    }

    private async Task<ApplicationUser?> FindUserAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return null;

        var clean = username.Trim();
        var cleanLower = clean.ToLowerInvariant();
        var normalized = clean.ToUpperInvariant();

        // 1. Exact match by UserName
        var user = await userManager.FindByNameAsync(clean);
        if (user != null) return user;

        // 2. Exact match by Email
        user = await userManager.FindByEmailAsync(clean);
        if (user != null) return user;

        // 3. Match directly in Users table (e.g., prefix match for tenant usernames like Sehmawy@Aspire.com_1)
        user = await context.Users.FirstOrDefaultAsync(u =>
            u.NormalizedUserName == normalized ||
            u.NormalizedEmail == normalized ||
            u.UserName == clean ||
            u.Email == clean ||
            (u.Email != null && u.Email.ToLower() == cleanLower) ||
            (u.UserName != null && u.UserName.ToLower() == cleanLower) ||
            (u.UserName != null && (u.UserName.StartsWith(clean) || u.UserName.ToLower().StartsWith(cleanLower + "_"))) ||
            (u.Email != null && (u.Email.StartsWith(clean) || u.Email.ToLower().StartsWith(cleanLower + "_"))) ||
            (u.NormalizedUserName != null && u.NormalizedUserName.StartsWith(normalized)) ||
            (u.NormalizedEmail != null && u.NormalizedEmail.StartsWith(normalized)));
        if (user != null) return user;

        // 4. Match via ExternalUsers table
        var extUsr = await context.ExternalUsers.FirstOrDefaultAsync(x =>
            (x.Email != null && x.Email.ToLower() == cleanLower) ||
            (x.Name != null && x.Name.ToLower() == cleanLower));
        if (extUsr != null)
        {
            if (extUsr.AppUsrId.HasValue)
            {
                user = await userManager.FindByIdAsync(extUsr.AppUsrId.Value.ToString());
                if (user != null) return user;
            }
            if (!string.IsNullOrWhiteSpace(extUsr.Email))
            {
                user = await userManager.FindByEmailAsync(extUsr.Email)
                    ?? await userManager.FindByNameAsync(extUsr.Email);
                if (user != null) return user;
            }
        }

        // 5. Match via Employees table
        var emp = await context.Employees.FirstOrDefaultAsync(x =>
            (x.WorkEmail != null && x.WorkEmail.ToLower() == cleanLower) ||
            (x.PersonalEmail != null && x.PersonalEmail.ToLower() == cleanLower) ||
            (x.Code != null && x.Code.ToLower() == cleanLower) ||
            (x.Name != null && x.Name.ToLower() == cleanLower));
        if (emp != null)
        {
            if (emp.AppUsrId.HasValue)
            {
                user = await userManager.FindByIdAsync(emp.AppUsrId.Value.ToString());
                if (user != null) return user;
            }
            if (!string.IsNullOrWhiteSpace(emp.WorkEmail))
            {
                user = await userManager.FindByEmailAsync(emp.WorkEmail)
                    ?? await userManager.FindByNameAsync(emp.WorkEmail);
                if (user != null) return user;
            }
        }

        return null;
    }

    public async Task<(SignInResult signIn, LoginResponseForExternalUserDTO loginResponse)> LoginForExternalUser(LoginDTO dto)
    {
        try
        {
            logger.LogInformation("Start Login External User: '{0}'", dto.Username);

            ApplicationUser? user = await FindUserAsync(dto.Username);
            
            if (user is not null)
            {
                if (user.IsActive == false)
                {
                    logger.LogInformation("Not active User: '{0}'", dto.Username);
                    return (null, null);
                }

                logger.LogInformation("Start sign in password for User: '{0}'", dto.Username);

                SignInResult signInResult = await signInManager.PasswordSignInAsync(user, dto.Password, false, false);

                if (signInResult.Succeeded)
                {
                    logger.LogInformation("Successful sign in for User: '{0}'", dto.Username);
                    if (!user.IsExternalUser)
                    {
                        user.IsExternalUser = true;
                        await userManager.UpdateAsync(user);
                    }
                    return (signInResult, GetLoginResponseForExternalUser(user));
                }
            }

            logger.LogInformation("User: '{0}' not found or credentials invalid", dto.Username);
            return (null, null);
        }
        catch (Exception e)
        {
            logger.LogException(e, nameof(LoginForExternalUser));
            throw;
        }
    }

    public async Task<(SignInResult signIn, LoginResponseDTO loginResponse)> Login(LoginDTO dto)
    {
        try
        {
            logger.LogInformation("Start Login User: '{0}'", dto.Username);

            ApplicationUser? user = await FindUserAsync(dto.Username);

            if (user is not null)
            {
                if (user.IsActive == false)
                {
                    logger.LogInformation("Not active User: '{0}'", dto.Username);
                    return (null, null);
                }

                logger.LogInformation("Start sign in password for User: '{0}'", dto.Username);

                SignInResult signInResult = await signInManager.PasswordSignInAsync(user, dto.Password, false, false);

                if (signInResult.Succeeded)
                {
                    logger.LogInformation("Successful sign in for User: '{0}'", dto.Username);
                    if (dto.IsExternalUser && !user.IsExternalUser)
                    {
                        user.IsExternalUser = true;
                        await userManager.UpdateAsync(user);
                    }
                    return (signInResult, GetLoginResponse(user, dto.IsExternalUser || user.IsExternalUser));
                }
            }

            logger.LogInformation("User: '{0}' not found or credentials invalid", dto.Username);
            return (null, null);
        }
        catch (Exception e)
        {
            logger.LogException(e, nameof(Login));
            throw;
        }
    }

    public async Task<LogoutRequest> Logout(string logoutId)
    {
        try
        {
            logger.LogInformation("Logout for User: '{0}'", logoutId);
            await signInManager.SignOutAsync();
            LogoutRequest logoutRequest = await interactionService.GetLogoutContextAsync(logoutId);
            return logoutRequest;
        }
        catch (Exception e)
        {
            logger.LogException(e, nameof(Logout));
            throw;
        }
    }
}
