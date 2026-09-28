using Aspire.Utilities;
using Microsoft.AspNetCore.Http;

namespace Aspire.Migration;

public class MigrationTenantProvider : ITenantProviderService
{
    private long? _tenantId = 1;
    private long? _userId = 1;
    private long? _currencyId = 1;
    private string? _langAbbrv = "English";
    private bool? _isExternalUser = false;

    public bool IsAllowed(HttpContext context)
    {
        return true;
    }

    public void SetTenantId(long? id)
    {
        _tenantId = id;
    }

    public void SetUserId(long? id)
    {
        _userId = id;
    }

    public void SetCurrencyId(long? id)
    {
        _currencyId = id;
    }

    public void SetLangAbbrv(string? abbrv)
    {
        _langAbbrv = abbrv;
    }

    public void SetIsExternalUser(bool? isExt)
    {
        _isExternalUser = isExt;
    }

    public long? GetTenantId()
    {
        return _tenantId;
    }

    public long? GetUserId()
    {
        return _userId;
    }

    public long? GetCurrencyId()
    {
        return _currencyId;
    }

    public string? GetLangAbbrv()
    {
        return _langAbbrv;
    }

    public bool? GetIsExternalUser()
    {
        return _isExternalUser;
    }
}