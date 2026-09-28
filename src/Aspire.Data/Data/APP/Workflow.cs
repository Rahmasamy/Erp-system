using Aspire.Utilities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Aspire.Data;

[Attr(DomainFolder = "APP", Abbrv = "WF", WithoutUI = true, List = true)]
public class Workflow : BaseEntity
{
    // Workflow (Design) => Init Workflow (Trigger) => EntityWorkflow (Worflow Instance)

    public long? EntityId { get; set; }

    [NotMapped]
    public virtual Entity? Entity { get; set; }

    public string? Version { get; set; }

    public JsonDocument? VueFlowDesign { get; set; }

    public Workflow() { }
    public Workflow(long? tenantId, long? entityId, string name, string nameAr, long? typeId, long? statusId)
    {
        TenantId = tenantId;
        EntityId = entityId;
        EntityTypeId = typeId;
        EntityStatusId = statusId;
        Name = name;
        NameAr = nameAr;
        IsActive = false;
        Version = "V-1.0.0";
    }
}


// Code-Player-Start-DTO //
public class WorkflowDTO
{
    public long? EntityId { get; set; }
    public string? EntityName { get; set; }
    public string? Version { get; set; }
    public string? VueFlowDesign { get; set; }
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