using Aspire.Data;
using Aspire.Enums;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Aspire.Utilities;

public interface ITenantProviderService
{
    bool IsAllowed(HttpContext context);
    void SetTenantId(long? Id);
    void SetUserId(long? Id);
    void SetCurrencyId(long? Id);
    void SetLangAbbrv(string? Abbrv);
    void SetIsExternalUser(bool? isExt);

    long? GetTenantId();
    long? GetUserId();
    long? GetCurrencyId();
    string? GetLangAbbrv();
    bool? GetIsExternalUser();
}

public sealed class TenantProviderService(ILogHandlingService logger, IJwtTokenService jwtTokenService) : ITenantProviderService
{
    private List<string> Entities = new() { "auth", "register", "profile", "entityattachment", "entitycomment", "files", "userpreference", "workitem", "workitemassignee", "documentworkitem", "devseed" };
    private List<string> Actions = new() { "" };
    private long? _tntId = 1;
    private long? _usrId = 1;
    private long? _langId = 1;
    private long? _curId = 1;
    private string? _abbrv = "English";
    private bool? _isExt = null;

    public bool IsAllowed(HttpContext context)
    {
        var jwtClaims = jwtTokenService.GetDeserializedJwtTokenClaims();
        if (jwtClaims.Count is not 0)
        {
            var userId = jwtClaims.FirstOrDefault(claim => claim.Type == "sub")?.Value;
            var tenantId = jwtClaims.FirstOrDefault(claim => claim.Type == "tenantId")?.Value;
            var orgId = jwtClaims.FirstOrDefault(claim => claim.Type == "orgId")?.Value;
            var orgIds = jwtClaims.FirstOrDefault(claim => claim.Type == "orgIds")?.Value;
            var currencyId = jwtClaims.FirstOrDefault(claim => claim.Type == "currencyId")?.Value;
            var LangAbbrv = jwtClaims.FirstOrDefault(claim => claim.Type == "LangAbbrv")?.Value;
            var seatType = jwtClaims.FirstOrDefault(claim => claim.Type == "seatType")?.Value;
            var isExternalUser = jwtClaims.FirstOrDefault(claim => claim.Type == "isExternalUser")?.Value;
            var userClaimsJSON = jwtClaims.FirstOrDefault(claim => claim.Type == "userClaims")?.Value;

            if (!string.IsNullOrEmpty(tenantId))
                SetTenantId(long.Parse(tenantId));

            if (!string.IsNullOrEmpty(userId))
                SetUserId(long.Parse(userId));

            if (!string.IsNullOrEmpty(LangAbbrv))
                SetLangAbbrv(LangAbbrv);

            if (!string.IsNullOrEmpty(currencyId))
                SetCurrencyId(long.Parse(currencyId));

            if (!string.IsNullOrEmpty(isExternalUser) && bool.TryParse(isExternalUser, out var parsedExt))
                SetIsExternalUser(parsedExt);

            string[] ar = context.Request.Path.ToString().Split('/');
            var entityName = ar.Length >= 2 ? ar[ar.Length - 2] : "";
            var entityAction = ar.Length >= 1 ? ar[ar.Length - 1] : "";

            if (!string.IsNullOrEmpty(entityName) && !string.IsNullOrEmpty(entityAction))
            {
                if (string.Equals(seatType, SeatTypeEnum.Admin, StringComparison.OrdinalIgnoreCase))
                    return true;

                if (Entities.Any(e => string.Equals(e, entityName, StringComparison.OrdinalIgnoreCase)))
                    return true;

                if (Actions.Any(e => string.Equals(e, entityAction, StringComparison.OrdinalIgnoreCase)))
                    return true;

                bool isContractor = string.Equals(seatType, SeatTypeEnum.Contractor, StringComparison.OrdinalIgnoreCase) ||
                                    string.Equals(isExternalUser, "true", StringComparison.OrdinalIgnoreCase) ||
                                    jwtClaims.Any(claim => claim.Type == "contractorId");

                if (isContractor)
                {
                    if (string.Equals(entityName, "Document", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(entityName, "EntityAttachment", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(entityName, "EntityComment", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(entityName, "Files", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(entityName, "DMSProject", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(entityName, "DocumentType", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(entityName, "DataConfidentiality", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(entityName, "Contractor", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }

                if (!string.IsNullOrEmpty(userClaimsJSON))
                {
                    var userClaims = JsonConvert.DeserializeObject<List<ActiveUserClaimDTO>>(userClaimsJSON);
                    if (userClaims is not null)
                    {
                        return userClaims.Any(claim =>
                            string.Equals(claim.EntityName, entityName, StringComparison.OrdinalIgnoreCase) &&
                            claim.ClaimType?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                .Any(action => string.Equals(action, entityAction, StringComparison.OrdinalIgnoreCase) ||
                                               (string.Equals(entityAction, "Submit", StringComparison.OrdinalIgnoreCase) &&
                                                (string.Equals(action, "Edit", StringComparison.OrdinalIgnoreCase) || string.Equals(action, "Create", StringComparison.OrdinalIgnoreCase)))) == true);
                    }
                }
            }
        }

        return false;

    }

    public void SetTenantId(long? Id) { _tntId = Id; }
    public void SetUserId(long? Id) { _usrId = Id; }
    public void SetLangAbbrv(string? Abbrv) { _abbrv = Abbrv; }
    public void SetCurrencyId(long? Id) { _curId = Id; }
    public void SetIsExternalUser(bool? isExt) { _isExt = isExt; }

    public long? GetTenantId() { return _tntId; }
    public long? GetUserId() { return _usrId; }
    public string? GetLangAbbrv() { return _abbrv; }
    public long? GetCurrencyId() { return _curId; }
    public bool? GetIsExternalUser() { return _isExt; }
}
