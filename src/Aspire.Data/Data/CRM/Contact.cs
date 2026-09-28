using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "CRM", Abbrv = "CON", SubList = true, List = true, 
    Actions = new string[] { "Promot-to-VIP:Dlg:dollar", "De-Promote:Dlg:dollar" },
    GroupBy = new string[] { "CustomerId", "SupplierId", "AccountManagerUsrId", "CountryId" },
    FilterBy = new string[] { "CustomerId", "SupplierId", "AccountManagerUsrId", "CountryId" },
    Cols = new string[] { "FirstName", "LastName", "Email", "Mobile", "JobTitle" },
    DisplayValues = new string[] { "Email", "Mobile", "JobTitle" })]
public class Contact : BaseEntityLinked
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public long? GenderId { get; set; }
    public string? JobTitle { get; set; }

    public long? AccountManagerId { get; set; } // UserId

    public long? CountryId { get; set; }
    public string? City { get; set; }
    public string? Email { get; set; }
    public string? Mobile { get; set; }

    public Contact() { }
    public Contact(long tnantId, string name, string nameAr, string email, long? cusId, long? suppId)
    {
        TenantId = tnantId;
        
        Name = name;
        NameAr = nameAr;
        Email = email;
        ObjectId = cusId is null ? suppId : cusId;
    }
}

// Code-Player-Start-DTO //
public class ContactDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public long? GenderId { get; set; }
    public string? GenderName { get; set; }
    public string? JobTitle { get; set; }
    public long? AccountManagerId { get; set; }
    public string? AccountManagerName { get; set; }
    public long? CountryId { get; set; }
    public string? CountryName { get; set; }
    public string? City { get; set; }
    public string? Email { get; set; }
    public string? Mobile { get; set; }
    public string? EntityName { get; set; }
    public long? ObjectId { get; set; }
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