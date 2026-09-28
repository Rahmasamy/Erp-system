using Aspire.Services;
using Aspire.Utilities;

namespace Aspire.Middleware;
public class TenantIdMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var scope = scopeFactory.CreateScope();
        try
        {
            var tenantProviderService = scope.ServiceProvider.GetRequiredService<ITenantProviderService>();
            var jwtTokenService = scope.ServiceProvider.GetRequiredService<IJwtTokenService>();

            var jwtClaims = jwtTokenService.GetDeserializedJwtTokenClaims();
            if (jwtClaims.Count is not 0)
            {
                var tenantId = jwtClaims.FirstOrDefault(claim => claim.Type == "tenantId")?.Value;
                Console.WriteLine("Executing TenantIdMiddleware");
                if (!string.IsNullOrEmpty(tenantId))
                {
                    tenantProviderService.SetTenantId(long.Parse(tenantId));
                }
            }
        }
        catch{}
        // Call the next middleware in the pipeline
        await next(context);
    }
}