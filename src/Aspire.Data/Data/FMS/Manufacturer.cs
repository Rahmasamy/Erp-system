
using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "FMS", Abbrv = "MNF", Lookup = true)]
public class Manufacturer : BaseLookupEntity
{
    public Manufacturer() { }
    public Manufacturer(long? tenantId, string name, string nameAr)
    {
        this.TenantId = tenantId;
        this.Name = name;
        NameAr = nameAr;
    }
}

// Code-Player-Start-DTO //
// Code-Player-End-DTO //
