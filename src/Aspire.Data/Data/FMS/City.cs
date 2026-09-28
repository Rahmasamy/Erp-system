
using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "FMS", Abbrv = "CTY", Lookup = true)]
public class City : BaseLookupEntity
{
    public long? CountryId { get; set; }

    public City() { }
    public City(long? tenantId, string name, string nameAr)
    {
        this.TenantId = tenantId;
        this.Name = name;
        NameAr = nameAr;
    }
}

// Code-Player-Start-DTO //
// Code-Player-End-DTO //
