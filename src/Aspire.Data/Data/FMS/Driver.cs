
using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "FMS", Abbrv = "DRV", Lookup = true)]
public class Driver : BaseLookupEntity
{
    public Driver() { }
    public Driver(long? tenantId, string name, string nameAr)
    {
        this.TenantId = tenantId;
        this.Name = name;
        NameAr = nameAr;
    }
}

// Code-Player-Start-DTO //
// Code-Player-End-DTO //
