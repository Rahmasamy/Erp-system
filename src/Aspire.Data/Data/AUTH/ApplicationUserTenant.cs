// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

namespace Aspire.Data;

public class ApplicationUserTenant : DatabaseEntity
{
    public virtual Tenant? Tenant { get; set; }
    public long UserId { get; set; }
    public virtual ApplicationUser? User { get; set; }
}

public class ApplicationUserTenantDTO
{
    public long? Id { get; set; }
    public long? TenantId { get; set; }
    public string? TenantName { get; set; }
    public long? UserId { get; set; }
    public string? UserName { get; set; }
    public string? UserPhotoUrl { get; set; }
    public bool? IsActive { get; set; }
    public long? CreatedByUsrId { get; set; }
    public string? CreatedByUsrName { get; set; }
    public DateTime? CreateDate { get; set; }
    public long? UpdatedByUsrId { get; set; }
    public string? UpdatedByUsrName { get; set; }
    public DateTime? UpdateDate { get; set; }
}