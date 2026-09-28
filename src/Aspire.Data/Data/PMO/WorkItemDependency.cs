using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

/// <summary>
/// A scheduling link between two WorkItems, for the Gantt chart: "B cannot start
/// until A finishes", and the three other variants.
///
/// Kept as its own table rather than a column on WorkItem so a task can have
/// several predecessors, and so "what blocks this task" is a query rather than a
/// string parse.
///
/// Like the rest of the schema, the navigations are [NotMapped] and there are no
/// database foreign keys - deleting a WorkItem must clear its links in code.
/// </summary>
[Attr(DomainFolder = "PMO", Abbrv = "WID")]
public class WorkItemDependency : BaseEntity
{
    /// <summary>The task that must happen first.</summary>
    public long? PredecessorId { get; set; }

    [NotMapped]
    public virtual WorkItem? Predecessor { get; set; }

    /// <summary>The task that waits.</summary>
    public long? SuccessorId { get; set; }

    [NotMapped]
    public virtual WorkItem? Successor { get; set; }

    /// <summary>FS (finish to start, the default), SS, FF or SF.</summary>
    public string? LinkType { get; set; } = "FS";

    /// <summary>Whole days added after the link is satisfied; may be negative.</summary>
    public int LagDays { get; set; } = 0;

    public WorkItemDependency() { }

    public WorkItemDependency(long tnantId, long? predecessorId, long? successorId, string linkType = "FS", int lagDays = 0)
    {
        TenantId = tnantId;
        PredecessorId = predecessorId;
        SuccessorId = successorId;
        LinkType = linkType;
        LagDays = lagDays;
    }
}

// Code-Player-Start-DTO //
public class WorkItemDependencyDTO
{
    public long? PredecessorId { get; set; }
    public string? PredecessorName { get; set; }
    public long? SuccessorId { get; set; }
    public string? SuccessorName { get; set; }
    public string? LinkType { get; set; }
    public int? LagDays { get; set; }
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
