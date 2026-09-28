
using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "FMS", Abbrv = "FUL", Lookup = true)]
public class FuelType : BaseLookupEntity
{
    public FuelType() { }
    public FuelType(long? tenantId, string name, string nameAr)
    {
        this.TenantId = tenantId;
        this.Name = name;
        NameAr = nameAr;
    }
}

// Code-Player-Start-DTO //
// Code-Player-End-DTO //
