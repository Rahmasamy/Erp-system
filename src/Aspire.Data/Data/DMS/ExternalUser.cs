
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "DMS", Abbrv = "", List = true,
    FilterBy = new string[] { "ContractorId" },
    Cols = new string[] { "ContractorId", "FullName", "Email", "Phone" })]
public class ExternalUser : MigratedEntity
{
    public long? AppUsrId { get; set; }
    public long? ContractorId { get; set; }
    public string AssignedProjects { get; set; } = "";
    public string PermissionLevel { get; set; } = ""; // View Only / Add New Document

    public string EntityType { get; set; } = ""; // Contractor / Project Owner
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }

    public ExternalUser() { }

    public ExternalUser(long tnantId, ApplicationUser usr, string name, string usrname, long contrId, string? email, string? phone)
    {
        this.TenantId = tnantId;
        this.AppUsrId = usr.Id;
        this.Name = name;
        this.ContractorId = contrId;
        this.Email = email;
        this.Phone = phone;
    }
}

// Code-Player-Start-DTO //
public class ExternalUserDTO
{
    public string? OldKey { get; set; }

    public string? EntityType { get; set; }
    public long? ContractorId { get; set; }
    public string? ContractorName { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? AssignedProjects { get; set; }
    public string? PermissionLevel { get; set; }
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
