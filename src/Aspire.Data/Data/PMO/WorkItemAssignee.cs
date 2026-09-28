using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "PMO", Abbrv = "TSA")]
public class WorkItemAssignee : BaseEntity
{
    public long? WorkItemId { get; set; }

    public long? AssignedToUsrId { get; set; }

    public WorkItemAssignee() { }

    public WorkItemAssignee(long tnantId, long? workItemId, List<ApplicationUser> Users)
    {
        Random rnd = new Random();
        TenantId = tnantId;
        
        this.WorkItemId = workItemId;
        AssignedToUsrId = Users != null && Users.Any() ? Users[rnd.Next(0, Users.Count)].Id : null;
    }
}

// Code-Player-Start-DTO //
public class WorkItemAssigneeDTO
{
    public long? WorkItemId { get; set; }
    public string? WorkItemName { get; set; }
    public long? AssignedToUsrId { get; set; }
    public string? AssignedToUsrName { get; set; }
    public string? AssignedToUsrPhotoUrl { get; set; }
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









