using Aspire.Data;
using Newtonsoft.Json;
using Aspire.Utilities;

namespace Aspire.Middleware;

public class UserClaimMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            var scope = scopeFactory.CreateScope();
            var jwtTokenService = scope.ServiceProvider.GetRequiredService<IJwtTokenService>();

            var jwtClaims = jwtTokenService.GetDeserializedJwtTokenClaims();
            if (jwtClaims.Count is not 0)
            {
                var userClaimsJson = jwtClaims.FirstOrDefault(claim => claim.Type == "userClaims")?.Value;

                if (!string.IsNullOrEmpty(userClaimsJson))
                {
                    var userClaims = JsonConvert.DeserializeObject<List<ActiveUserClaimDTO>>(userClaimsJson);
                    if(userClaims is not null)
                    {
                        var tenantId = jwtClaims.FirstOrDefault(claim => claim.Type == "tenantId")?.Value;
                        //var tenantClaims = userClaims.FirstOrDefault(e => e.TenantId == tenantId);
                        var entityName = context.Request.Headers["EntityName"].ToString();
                        var entityOperation = context.Request.Headers["EntityClaim"].ToString();
                    }
                }
            }
        }
        catch { }
        // Call the next middleware in the pipeline
        await next(context);
    }
}

