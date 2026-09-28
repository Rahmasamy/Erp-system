using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "ORG", Abbrv = "MEM", SubList = true)]
public class TeamMember : XREFEntity
{

    [Attr(Parent = true)]
    public long? MemberUsrId { get; set; }

    [NotMapped]
    public virtual ApplicationUser? MemberUsr { get; set; }

    public long? TeamId { get; set; }

    [NotMapped]
    public virtual Team? Team { get; set; }
}

// Code-Player-Start-DTO //
public class TeamMemberDTO
{
    public long? MemberUsrId { get; set; }
    public string? MemberUsrName { get; set; }
    public string? MemberUsrPhotoUrl { get; set; }
    public long? TeamId { get; set; }
    public string? TeamName { get; set; }
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