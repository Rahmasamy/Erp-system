
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "CRM", Abbrv = "LD", List = true, Card = true, Tree = true,
    Actions = new string[] { "Qualify:Dlg:dollar", "Disqualify:Dlg:dollar" },
    Cols = new string[] { "Domain", "AccountManagerUsrId", "AnnualRevenue_val", "Mobile", "Employees_num" },
    GroupBy = new string[] { "IndustryId", "SectorId" },
    FilterBy = new string[] { "IndustryId", "SectorId" },
    DisplayValues = new string[] { "Domain", "Mobile", "AnnualRevenue_val" },
    SummaryValues = new string[] { "Domain", "Website", "Mobile", "AnnualRevenue_val" })]
public class Lead : BaseEntity
{
    public DateTime? IssueDate { get; set; }
    public DateTime? FollowUpDate { get; set; }
    public long? IndustryId { get; set; }
    public long? SectorId { get; set; }
    public long? AccountManagerUsrId { get; set; }
    public long? CurrencyId { get; set; }
    public long? CountryId { get; set; }
    public string? City { get; set; }


    [Attr(Section = "Individual Information (B2C Lead)")]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? JobTitle { get; set; }
    public string? Mobile { get; set; }
    public long? GenderId { get; set; }


    [Attr(Section = "Company Information (B2B Lead)")]
    public string? Domain { get; set; }
    public string? Website { get; set; }
    public int Employees_num { get; set; } = 1;
    public decimal AnnualRevenue_val { get; set; } = 0;
    public string? Phone { get; set; }
    public string? CompanyEmail { get; set; }

    [System.ComponentModel.DataAnnotations.ConcurrencyCheck] //prevents two requests from successfully qualifying the same previously unlinked lead simultaneously.
    public long? CustomerId { get; set; }

    public long? ContactId { get; set; }

    public Lead() { }

    public Lead(long usrId, long tnantId, List<Sector> Sectors, List<Industry> Indsts, List<EntityType> EntTyps, List<EntityStatus> EntSts, string name, string nameAr, string domain)
    {
        Random rnd = new Random();

        this.AccountManagerUsrId = usrId;
        TenantId = tnantId;
        
        Name = name;
        NameAr = nameAr;
        Domain = domain;
        Website = "https://WWW." + domain;

        SectorId = Sectors != null && Sectors.Any() ? Sectors[rnd.Next(0, Sectors.Count)].Id : null;
        IndustryId = Indsts != null && Indsts.Any() ? Indsts[rnd.Next(0, Indsts.Count)].Id : null;
        var leadEntTyps = EntTyps?.Where(x => x.EntityName == "Lead").ToList();
        EntityTypeId = leadEntTyps != null && leadEntTyps.Any() ? leadEntTyps[rnd.Next(0, leadEntTyps.Count)].Id : EntTyps?.FirstOrDefault()?.Id;
        var leadEntSts = EntSts?.Where(x => x.EntityName == "Lead").ToList();
        EntityStatusId = leadEntSts != null && leadEntSts.Any() ? leadEntSts[rnd.Next(0, leadEntSts.Count)].Id : EntSts?.FirstOrDefault()?.Id;

        AnnualRevenue_val = 30790000;
    }
}

// Code-Player-Start-DTO //
public class LeadDTO
{
    public DateTime? IssueDate { get; set; }
    public DateTime? FollowUpDate { get; set; }
    public long? IndustryId { get; set; }
    public string? IndustryName { get; set; }
    public long? SectorId { get; set; }
    public string? SectorName { get; set; }
    public long? AccountManagerUsrId { get; set; }
    public string? AccountManagerUsrName { get; set; }
    public string? AccountManagerUsrPhotoUrl { get; set; }
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
    public long? CountryId { get; set; }
    public string? CountryName { get; set; }
    public string? City { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? JobTitle { get; set; }
    public string? Mobile { get; set; }
    public long? GenderId { get; set; }
    public string? GenderName { get; set; }
    public string? Domain { get; set; }
    public string? Website { get; set; }
    public int? Employees_num { get; set; }
    public decimal? AnnualRevenue_val { get; set; }
    public string? Phone { get; set; }
    public string? CompanyEmail { get; set; }
    public long? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public long? ContactId { get; set; }
    public string? ContactName { get; set; }
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

public class QualifyLeadRequest
{
    public long Id { get; set; }
    public string CustomerType { get; set; } = string.Empty;
    public string? ActionComment { get; set; }
}

public class DisqualifyLeadRequest
{
    public long Id { get; set; }
    public string? ActionComment { get; set; }
}



