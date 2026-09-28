
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "CRM", Abbrv = "CO", List = true, Card = true, Tree = true, 
    GroupBy = new string[] { "AccountManagerUsrId", "IndustryId", "SectorId", "CountryId" },
    FilterBy = new string[] { "AccountManagerUsrId", "IndustryId", "SectorId", "CountryId" },
    Cols = new string[] { "EntityStatusId", "EntityTypeId", "Domain", "IndustryId", "SectorId" })]
public class Competitor : CompanyEntity
{

    public Competitor() { }
    public Competitor(long tnantId, List<EntityType> EntTyps, List<EntityStatus> EntSts, List<ApplicationUser> Users, List<Sector> Sectors, List<Industry> Indsts, string name, string nameAr, string domain)
    {
        TenantId = tnantId;
        
        Name = name;
        NameAr = nameAr;
        Domain = domain;
        Website = "https://WWW." + domain;
        CRNo = "10099777829";
        VATRegNo = "3445662829";

        Random rnd = new Random();
        AccountManagerUsrId = Users != null && Users.Any() ? Users[rnd.Next(0, Users.Count)].Id : null;
        var compEntTyps = EntTyps?.Where(x => x.EntityName == "Competitor").ToList();
        EntityTypeId = compEntTyps != null && compEntTyps.Any() ? compEntTyps[rnd.Next(0, compEntTyps.Count)].Id : EntTyps?.FirstOrDefault()?.Id;
        var compEntSts = EntSts?.Where(x => x.EntityName == "Competitor").ToList();
        EntityStatusId = compEntSts != null && compEntSts.Any() ? compEntSts[rnd.Next(0, compEntSts.Count)].Id : EntSts?.FirstOrDefault()?.Id;
        SectorId = Sectors != null && Sectors.Any() ? Sectors[rnd.Next(0, Sectors.Count)].Id : null;
        IndustryId = Indsts != null && Indsts.Any() ? Indsts[rnd.Next(0, Indsts.Count)].Id : null;
    }
}

// Code-Player-Start-DTO //
public class CompetitorDTO
{
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
