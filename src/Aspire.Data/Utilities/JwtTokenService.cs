using Aspire.Data;
using Aspire.Enums;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Claim = System.Security.Claims.Claim;

namespace Aspire.Utilities;

public static class JwtTokenGenerator
{
    public static string GenerateJwtToken(ApplicationUser user, Contractor? Contrct, List<ActiveUserClaimDTO> userClaims, bool? isExternalUser = null)
    {        
        bool isExt = isExternalUser ?? user.IsExternalUser;
        var claimsList = new List<Claim>
        {
            new Claim("tenantId", (user.TenantId ?? 1).ToString()),
            new Claim("currencyId", (user.CurrencyId ?? 1).ToString()),
            new Claim("LangAbbrv", user.LangAbbrv ?? "English"),
            new Claim("seatType", user.SeatType ?? "User"),
            new Claim("isExternalUser", isExt ? "true" : "false"),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Token Identifier,
            new Claim("userClaims", JsonConvert.SerializeObject(userClaims))
        };

        if (Contrct != null)
        {
            claimsList.Add(new Claim("contractorId", Contrct.Id.ToString()));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfiguration.JWT_KEY));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: AppConfiguration.JWT_ISSUER,
            audience: AppConfiguration.JWT_AUDIENCE,
            claims: claimsList,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static string GenerateNewJwtToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfiguration.JWT_KEY));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: AppConfiguration.JWT_ISSUER,
            audience: AppConfiguration.JWT_AUDIENCE,
            claims: claims,
            expires: DateTime.Now.AddDays(2),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static string GenerateResetPasswordToken(string email, string resetToken)
    {
        var claims = new[]
        {
            new Claim("email", email),
            new Claim("token", resetToken)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfiguration.JWT_KEY));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
           issuer: AppConfiguration.JWT_ISSUER,
            audience: AppConfiguration.JWT_AUDIENCE,
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    public static string GenerateInviteUserToken(string email, string token, string currentTenantId, string role)
    {
        var claims = new[]
        {
            new Claim("email", email),
            new Claim("tenantId", currentTenantId),
            new Claim("role", role),
            new Claim("token", token)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfiguration.JWT_KEY));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
           issuer: AppConfiguration.JWT_ISSUER,
            audience: AppConfiguration.JWT_AUDIENCE,
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}

public interface IJwtTokenService
{
    List<Claim> GetDeserializedJwtTokenClaims();
}

public class JwtTokenService(IHttpContextAccessor context, ILogHandlingService logger) : IJwtTokenService
{
    public List<Claim> GetDeserializedJwtTokenClaims()
    {
        try
        {
            var token = GetJwtToken();
            
            if (string.IsNullOrEmpty(token))
                return [];

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken is not null)
                return jwtToken.Claims.ToList();

            return [];
        }
        catch (Exception ex)
        {
            logger.LogException(ex, nameof(GetDeserializedJwtTokenClaims));
            return [];
        }
    }

    private string GetJwtToken()
    {
        try
        {
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();
                return token;
            }

            return string.Empty;
        }
        catch (Exception ex)
        {
            logger.LogException(ex, nameof(GetJwtToken));
            throw;
        }
    }
}
