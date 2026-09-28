using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "HRM", Abbrv = "PST", List = true)]
public class Post : BaseEntity
{
    public long? JobId { get; set; }

    [NotMapped]
    public virtual Job? Job { get; set; }


    public long? AccountManagerUsrId { get; set; } // HR UserId

    [NotMapped]
    public virtual ApplicationUser? AccountManagerUsr { get; set; }


    public long? HiringManagerUsrId { get; set; } // Business Manager UserId

    [NotMapped]
    public virtual ApplicationUser? HiringManagerUsr { get; set; }

}

// Code-Player-Start-DTO //
public class PostDTO
{
    public long? JobId { get; set; }
    public string? JobName { get; set; }
    public long? AccountManagerUsrId { get; set; }
    public string? AccountManagerUsrName { get; set; }
    public string? AccountManagerUsrPhotoUrl { get; set; }
    public long? HiringManagerUsrId { get; set; }
    public string? HiringManagerUsrName { get; set; }
    public string? HiringManagerUsrPhotoUrl { get; set; }
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