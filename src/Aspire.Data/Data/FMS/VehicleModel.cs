
using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "FMS", Abbrv = "VML", Lookup = true)]
public class VehicleModel : BaseLookupEntity
{
    public VehicleModel() { }
    public VehicleModel(long? tenantId, string name, string nameAr)
    {
        this.TenantId = tenantId;
        this.Name = name;
        NameAr = nameAr;
    }
}

// Code-Player-Start-DTO //
// Code-Player-End-DTO //
