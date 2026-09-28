
using Aspire.Utilities;

namespace Aspire.Data;

[Attr(DomainFolder = "APP", Abbrv = "NT", Lookup = true)]
public class Nationality : CoreEntity
{
    public string Abbrv { get; set; }
    public Nationality(long? tenantId, string name, string nameAr, string abbrv)
    {
        TenantId = tenantId;
        Name = name;
        NameAr = nameAr;
        Abbrv = abbrv;
    }
}

// Code-Player-Start-DTO //
public class NationalityDTO
{
    public string? Abbrv { get; set; }
    public string? Name { get; set; }
    public string? NameAr { get; set; }
    public string? Description { get; set; }
    public string? DescriptionAr { get; set; }
    public long? OrganizationId { get; set; }
    public string? OrganizationName { get; set; }
    public long? Id { get; set; }
    public string? No { get; set; }
    public long? TenantId { get; set; }
    public string? TenantName { get; set; }
    public bool? IsActive { get; set; }
    public string? Msg { get; set; }
    public string? PhotoUrl { get; set; }
    public long? CreatedByUsrId { get; set; }
    public string? CreatedByUsrName { get; set; }
    public string? CreatedByPhotoUrl { get; set; }
    public long? UpdatedByUsrId { get; set; }
    public string? UpdatedByUsrName { get; set; }
    public string? UpdatedByPhotoUrl { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
// Code-Player-End-DTO //