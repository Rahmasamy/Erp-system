using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "ORG", Abbrv = "BRN", Lookup = true,
    GroupBy = new string[] { "OwnerUsrId" },
    FilterBy = new string[] { "OwnerUsrId" },
    Cols = new string[] { "OwnerUsrId" },
    DisplayValues = new string[] { "OwnerUsrId" }
)]
public class Branch : BaseLookupEntity
{
    public long? OwnerUsrId { get; set; }

    [NotMapped]
    public virtual ApplicationUser? OwnerUsr { get; set; }

    Branch() { }
    public Branch(long? tenantId, long usrId, string name, string nameAr)
    {
        TenantId = tenantId;
        Name = name;
        NameAr = nameAr;
        this.OwnerUsrId = usrId;
    }

}

// Code-Player-Start-DTO //
public class BranchDTO
{
    public long? OwnerUsrId { get; set; }
    public string? OwnerUsrName { get; set; }
    public string? OwnerUsrPhotoUrl { get; set; }
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
