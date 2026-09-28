using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "GRC", Abbrv = "FRM-ITM", WithoutUI = true)]
public class FrameworkItem : BaseEntity
{
    public long? FrameworkId { get; set; }

    [NotMapped]
    public virtual Framework? Framework { get; set; }


    public long? ParentId { get; set; }

    [NotMapped]
    public virtual FrameworkItem? Parent { get; set; }

    public string? Type { get; set; }
    public string? Key { get; set; }
    public string? Objective { get; set; }
    public string? Principal { get; set; }
    public string? Tools { get; set; }
    public string? Guidelines { get; set; }
    public string? Deliverables { get; set; }    

    public FrameworkItem()
    {

    }

    public FrameworkItem(long? tenantId, long? frameworkId, string key, long? parentId, string name, string type)
    {
        this.TenantId = tenantId;
        this.FrameworkId = frameworkId;
        this.Key = key;
        this.ParentId = parentId;
        this.Name = name;
        this.Type = type;
    }

    public FrameworkItem(string name, string desc, long? frameworkId, long? parentId)
    {
        this.FrameworkId = frameworkId;
        this.ParentId = parentId;
        this.Name = name;
        this.Description = desc;
    }
}

// Code-Player-Start-DTO //
public class FrameworkItemDTO
{
    public long? FrameworkId { get; set; }
    public string? FrameworkName { get; set; }
    public long? ParentId { get; set; }
    public string? ParentName { get; set; }
    public string? Type { get; set; }
    public string? Key { get; set; }
    public string? Objective { get; set; }
    public string? Principal { get; set; }
    public string? Tools { get; set; }
    public string? Guidelines { get; set; }
    public string? Deliverables { get; set; }
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