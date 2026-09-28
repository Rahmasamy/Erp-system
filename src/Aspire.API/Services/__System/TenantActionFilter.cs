using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Aspire.Services;
public class TenantActionFilter(ITenantProviderService _tenantService, ApplicationDbContext dbContext,
    IJwtTokenService jwtTokenService) : IActionFilter
{
    private List<string> Entities = new() { "auth", "register", "profile", "entityattachment", "entitycomment", "files", "userpreference" };
    private List<string> Actions = new() { "" };
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var jwtClaims = jwtTokenService.GetDeserializedJwtTokenClaims();
        if (jwtClaims.Count is not 0)
        {
            var userId = jwtClaims.FirstOrDefault(claim => claim.Type == "sub")?.Value;
            var tenantId = jwtClaims.FirstOrDefault(claim => claim.Type == "tenantId")?.Value;

            if (!string.IsNullOrEmpty(tenantId))
                _tenantService.SetTenantId(long.Parse(tenantId));

            if (!string.IsNullOrEmpty(userId))
                _tenantService.SetUserId(long.Parse(userId));

            ApplicationUser? user = dbContext.ApplicationUsers.Where(e => e.Id == long.Parse(userId)).FirstOrDefault();
            _tenantService.SetCurrencyId(user.CurrencyId);
            _tenantService.SetLangAbbrv(user.LangAbbrv);

            var userClaimsJson = jwtClaims.FirstOrDefault(claim => claim.Type == "userClaims")?.Value;
            if (!string.IsNullOrEmpty(userClaimsJson))
            {
                var userClaims = JsonConvert.DeserializeObject<List<ActiveUserClaimDTO>>(userClaimsJson);
                if (userClaims is not null)
                {
                    string[] ar = context.HttpContext.Request.Path.ToString().Split('/');
                    var entityName = ar[ar.Length-2];
                    var entityAction = ar[ar.Length - 1];

                    if (!string.IsNullOrEmpty(entityName) && !string.IsNullOrEmpty(entityAction))
                    {
                        bool claimFound = Entities.Any(e => e == entityName.ToLower())
                                        || Actions.Any(e => e == entityAction.ToLower())
                                        || userClaims.Any(e => e.ClaimType == entityName.ToLower() + ":" + entityAction.ToLower());
                        if (!claimFound)
                        {
                            //context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                            //context.HttpContext.Response.WriteAsync("Access denied.");
                            return;
                        }
                    }
                }
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}

