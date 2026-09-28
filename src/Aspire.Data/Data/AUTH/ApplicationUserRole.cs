// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "AUTH", Abbrv = "USR-ROL", SubList = true)]
public class ApplicationUserRole : IdentityUserRole<long>, IAuditableEntity
{
    [NotMapped]
    public virtual ApplicationRole Role { get; set; }


    [NotMapped]
    public virtual ApplicationUser User { get; set; }


    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Attr(Form = Frm.Hidden)]
    public long Id { get; set; }


    [Attr(Form = Frm.Hidden)]
    public long? TenantId { get; set; }
    public bool? IsActive { get; set; }


    [Attr(Form = Frm.Hidden)]
    public string? No { get; set; }

    [Attr(Form = Frm.Hidden)]
    public string? Msg { get; set; }

    [Attr(Form = Frm.Hidden)]
    public string? PhotoUrl { get; set; }

    [Attr(Form = Frm.Hidden)]
    public long? CreatedByUsrId { get; set; }

    [Attr(Form = Frm.Hidden)]
    public long? UpdatedByUsrId { get; set; }
    
    [Attr(Form = Frm.Hidden)]
    public DateTime? CreateDate { get; set; }

    [Attr(Form = Frm.Hidden)]
    public DateTime? UpdateDate { get; set; }
    public ApplicationUserRole() : base() { }
}

// Code-Player-Start-DTO //
public class ApplicationUserRoleTeamDTO : ApplicationUserRoleDTO
{
    public List<TeamMemberAvatarDTO> Stakeholders { get; set; } = new List<TeamMemberAvatarDTO>();
}

public class ApplicationUserRoleDTO
{
    public string? RoleName { get; set; }
    public string? UserName { get; set; }
    public string? UserPhotoUrl { get; set; }
    public long? Id { get; set; }
    public long? TenantId { get; set; }
    public string? TenantName { get; set; }
    public long? OrganizationId { get; set; }
    public string? OrganizationName { get; set; }
    public bool? IsActive { get; set; }
    public string? No { get; set; }
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
    public long? UserId { get; set; }
    public long? RoleId { get; set; }
}
// Code-Player-End-DTO //
