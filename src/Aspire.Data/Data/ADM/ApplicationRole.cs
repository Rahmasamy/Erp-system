// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using Aspire.Utilities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "ADM", Abbrv = "ROL", List = true, Id = true,
    Menu = new string[] { "Users?ADM_ApplicationUserRole_ListSub" })]
public class ApplicationRole : IdentityRole<long>, IAuditableEntity
{

    //public long Id { get; set; }

    public long? TenantId { get; set; }
    public string? Description { get; set; }
    public string? NameAr { get; set; }
    public string? DescriptionAr { get; set; }
    public bool? IsActive { get; set; }

    public string? Type { get; set; }

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
    public ApplicationRole() : base()
    {

    }

    public ApplicationRole(long Id, string roleName) : base(roleName)
    {
        TenantId = Id;
    }
}

// Code-Player-Start-DTO //
public class ApplicationRoleDTO
{
    public long? TenantId { get; set; }
    public string? TenantName { get; set; }
    public string? Description { get; set; }
    public string? NameAr { get; set; }
    public string? DescriptionAr { get; set; }
    public bool? IsActive { get; set; }
    public string? Type { get; set; }
    public string? No { get; set; }
    public string? Msg { get; set; }
    public string? PhotoUrl { get; set; }
    public long? CreatedByUsrId { get; set; }
    public string? CreatedByUsrName { get; set; }
    public string? CreatedByUsrPhotoUrl { get; set; }
    public long? UpdatedByUsrId { get; set; }
    public string? UpdatedByUsrName { get; set; }
    public string? UpdatedByUsrPhotoUrl { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? NormalizedName { get; set; }
    public string? ConcurrencyStamp { get; set; }
}
// Code-Player-End-DTO //