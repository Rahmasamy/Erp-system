
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using static Duende.IdentityServer.Models.IdentityResources;

namespace Aspire.Data;

[Attr(DomainFolder = "DMS", Abbrv = "SP", List = true,
    FilterBy = new string[] { "EntityStatusId", "AccountManagerUsrId" },
    Cols = new string[] { "EntityStatusId", "RegistrationNo", "Email", "Phone" })]
public class DMSProjectOwner : MigratedCompanyEntity
{
    public string? OldId { get; set; }

    public DMSProjectOwner() { }

    public DMSProjectOwner(long tnantId, string name, string nameAr, string regNo, string email, string phone)
    {
        TenantId = tnantId;

        Name = name;
        NameAr = nameAr;
        RegistrationNo = regNo;
        Email = email;
        Phone = phone;

    }
}

// Code-Player-Start-DTO //
public class DMSProjectOwnerDTO
{
    public string? OldKey { get; set; }

    public long? AccountManagerUsrId { get; set; }
    public string? AccountManagerUsrName { get; set; }
    public string? AccountManagerUsrPhotoUrl { get; set; }
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
    public string? Domain { get; set; }
    public string? Website { get; set; }
    public string? CRNo { get; set; }
    public string? VATRegNo { get; set; }
    public string? RegistrationNo { get; set; }
    public long? IndustryId { get; set; }
    public string? IndustryName { get; set; }
    public long? SectorId { get; set; }
    public string? SectorName { get; set; }
    public int? Employees_num { get; set; }
    public decimal? AnnualRevenue_val { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }
    public long? CountryId { get; set; }
    public string? CountryName { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Address { get; set; }
    public string? Street { get; set; }
    public string? ZipCode { get; set; }
    public long? EntityStatusId { get; set; }
    public string? EntityStatusName { get; set; }
    public string? EntityStatusColor { get; set; }
    public long? EntityTypeId { get; set; }
    public string? EntityTypeName { get; set; }
    public string? EntityTypeColor { get; set; }
    public long? DataConfidentialityId { get; set; }
    public string? DataConfidentialityName { get; set; }
    public string? Name { get; set; }
    public string? NameAr { get; set; }
    public string? Description { get; set; }
    public string? DescriptionAr { get; set; }
    public long? Id { get; set; }
    public string? No { get; set; }
    public long? TenantId { get; set; }
    public string? TenantName { get; set; }
    public bool? IsActive { get; set; }
    public string? Msg { get; set; }
    public string? ActionComment { get; set; }
    public string? PhotoUrl { get; set; }
    public long? CreatedByUsrId { get; set; }
    public string? CreatedByUsrName { get; set; }
    public string? CreatedByUsrPhotoUrl { get; set; }
    public long? UpdatedByUsrId { get; set; }
    public string? UpdatedByUsrName { get; set; }
    public string? UpdatedByUsrPhotoUrl { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
// Code-Player-End-DTO //
