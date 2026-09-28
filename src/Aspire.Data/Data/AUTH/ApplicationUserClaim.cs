// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "AUTH", Abbrv = "USR-CLM", SubList = true)]
public class ApplicationUserClaim : IdentityUserClaim<long>, IAuditableEntity
{
    // ClaimType => [EntityName:Claim] & ClaimValue [Based on Claim Type]

    [NotMapped]
    public virtual ApplicationUser? User { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string? EntityName { get; set; }
    public string? No { get; set; }
    public string? Msg { get; set; }
    public long? TenantId { get; set; }
    public long? CreatedByUsrId { get; set; }
    public long? UpdatedByUsrId { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }

    public ApplicationUserClaim() : base()
    {
    }
}

// Code-Player-Start-DTO //
public class ApplicationUserClaimDTO
{
    public string? UserName { get; set; }
    public string? UserPhotoUrl { get; set; }
    public long? Id { get; set; }
    public string? EntityName { get; set; }
    public string? No { get; set; }
    public string? Msg { get; set; }
    public string? PhotoUrl { get; set; }
    public long? TenantId { get; set; }
    public string? TenantName { get; set; }
    public long? OrganizationId { get; set; }
    public string? OrganizationName { get; set; }
    public long? CreatedByUsrId { get; set; }
    public string? CreatedByUsrName { get; set; }
    public string? CreatedByPhotoUrl { get; set; }
    public long? UpdatedByUsrId { get; set; }
    public string? UpdatedByUsrName { get; set; }
    public string? UpdatedByPhotoUrl { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public long? UserId { get; set; }
    public string? ClaimType { get; set; }
    public string? ClaimValue { get; set; }
}
// Code-Player-End-DTO //

public class ActiveUserClaimDTO
{
    public ActiveUserClaimDTO() { }
    public ActiveUserClaimDTO(string entityName, string claimType, string claimValue)
    {
        this.EntityName = entityName;
        this.ClaimType = claimType;
        this.ClaimValue = claimValue;
    }

    public string? EntityName { get; set; }
    public string? ClaimType { get; set; }
    public string? ClaimValue { get; set; }
}