using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "PMO", Abbrv = "WI", SubList = true)]
public class WorkItem : BaseEntityLinked
{
    // This Work Line Item Could be Linked to One of the Following Parents
    // [Project, Programme, Portofolio, Lead, Deal, Risk, Issue]
    public bool? IsMilestone { get; set; } = false;
    public bool? IsLeaf { get; set; } = true;
    public bool? IsTimeSheet { get; set; } = true;

    public string? WBSCode { get; set; }
    public long? ProjectId { get; set; }
    public long? LineItemId { get; set; }

    [NotMapped]
    public virtual LineItem? LineItem { get; set; }

    /// <summary>Position among siblings: the Gantt reorders by drag, and the WBS
    /// numbering (1.1, 1.2) has to survive new rows being added.</summary>
    public int SortOrder { get; set; } = 0;

    public long? ParentId { get; set; } // WorkItem
    public long? PredecessorId { get; set; } // WorkItem
    public long? SuccessorId { get; set; } // WorkItem

    [NotMapped]
    public virtual WorkItem? Parent { get; set; }


    public long? AssignedToUsrId { get; set; }

    public decimal PlannedEffort_dec { get; set; } = 0;
    public decimal calc_ActualEffort_dec { get; set; } = 0;
    public decimal PlannedCost_val { get; set; } = 0;
    public decimal calc_ActualCost_val { get; set; } = 0;
    public decimal PlannedDuration_dec { get; set; } = 0;
    public decimal calc_ActualDuration_dec { get; set; } = 0;

    public decimal Analysis_prc { get; set; } = 0;
    public decimal Progress_prc { get; set; } = 0;

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    /// <summary>The plan as first agreed, for the Gantt baseline overlay.</summary>
    public DateTime? BaselineStartDate { get; set; }
    public DateTime? BaselineEndDate { get; set; }

    public DateTime? ActualStartDate { get; set; }
    public DateTime? ActualEndDate { get; set; }

    public long? TaskSizeId { get; set; }

    public long? TaskPriorityId { get; set; }
    public long? TaskComplexityId { get; set; }

    public int calc_Comments_num { get; set; } = 0;

    public WorkItem() { }
    public WorkItem(long usrId, long tnantId, List<TaskSize> TskSz, List<TaskComplexity> TskCoxs, List<TaskPriority> TskPrs, string name, long? entityTypeId, long? entityStatusId, long? parentId, long? projectId, long? lineItemId, bool isTimeSheet = true, bool isLeaf = true)
    {
        Random rnd = new Random();
        AssignedToUsrId = usrId;
        TenantId = tnantId;

        TaskPriorityId = TskPrs != null && TskPrs.Any() ? TskPrs[rnd.Next(0, TskPrs.Count)].Id : null;
        TaskSizeId = TskSz != null && TskSz.Any() ? TskSz[rnd.Next(0, TskSz.Count)].Id : null;
        TaskComplexityId = TskCoxs != null && TskCoxs.Any() ? TskCoxs[rnd.Next(0, TskCoxs.Count)].Id : null;

        this.Name = name;
        this.ParentId = parentId;
        this.ProjectId = projectId;
        this.LineItemId = lineItemId;
        this.EntityTypeId = entityTypeId;
        this.EntityStatusId = entityStatusId;
        this.IsTimeSheet = isTimeSheet;
        this.IsLeaf = isLeaf;
        this.StartDate = new DateTime(2026, 9, 3);
        this.EndDate = new DateTime(2026, 10, 5);
        this.ActualStartDate = new DateTime(2026, 9, 3);
        this.ActualEndDate = new DateTime(2026, 10, 7);
        this.PlannedEffort_dec = 72;
        this.calc_ActualEffort_dec = 96;
        this.PlannedDuration_dec = PlannedEffort_dec / 8;
        this.calc_ActualEffort_dec = calc_ActualEffort_dec / 8;
        this.IsTimeSheet = true;
        this.Analysis_prc = 100;
        this.Progress_prc = 75;
    }

    public WorkItem(long usrId, long tnantId, List<TaskSize> TskSz, List<TaskComplexity> TskCoxs, List<TaskPriority> TskPrs, string name, long? entityTypeId, long? entityStatusId, long? parentId, string entityName, long? objectId, long? lineItemId, bool isTimeSheet = true, bool isLeaf = true)
    {
        Random rnd = new Random();
        AssignedToUsrId = usrId;
        TenantId = tnantId;

        TaskPriorityId = TskPrs != null && TskPrs.Any() ? TskPrs[rnd.Next(0, TskPrs.Count)].Id : null;
        TaskSizeId = TskSz != null && TskSz.Any() ? TskSz[rnd.Next(0, TskSz.Count)].Id : null;
        TaskComplexityId = TskCoxs != null && TskCoxs.Any() ? TskCoxs[rnd.Next(0, TskCoxs.Count)].Id : null;

        this.Name = name;
        this.ParentId = parentId;
        this.EntityName = entityName;
        this.ObjectId = objectId;
        this.LineItemId = lineItemId;
        this.EntityTypeId = entityTypeId;
        this.EntityStatusId = entityStatusId;
        this.IsTimeSheet = isTimeSheet;
        this.IsLeaf = isLeaf;
        this.StartDate = new DateTime(2026, 9, 3);
        this.EndDate = new DateTime(2026, 10, 5);
        this.ActualStartDate = new DateTime(2026, 9, 3);
        this.ActualEndDate = new DateTime(2026, 10, 7);
        this.PlannedEffort_dec = 72;
        this.calc_ActualEffort_dec = 96;
        this.PlannedDuration_dec = PlannedEffort_dec / 8;
        this.calc_ActualEffort_dec = calc_ActualEffort_dec / 8;
        this.IsTimeSheet = true;
        this.Analysis_prc = 100;
        this.Progress_prc = 75;
        this.CreatedByUsrId = usrId;
        this.CreateDate = DateTime.UtcNow;
    }
}


// Code-Player-Start-DTO //
public class WorkItemDTO
{
    public string? WBSCode { get; set; }
    public long? ProjectId { get; set; }
    public bool? IsMilestone { get; set; }
    public bool? IsLeaf { get; set; }
    public bool? IsTimeSheet { get; set; }
    public long? LineItemId { get; set; }
    public string? LineItemName { get; set; }
    public long? ParentId { get; set; } // WorkItem
    public long? PredecessorId { get; set; } // WorkItem
    public long? SuccessorId { get; set; } // WorkItem
    public long? PredecessorName { get; set; }
    public long? SuccessorName { get; set; }
    public string? ParentName { get; set; }
    public long? AssignedToUsrId { get; set; }
    public string? AssignedToUsrName { get; set; }
    public string? AssignedToUsrPhotoUrl { get; set; }
    public decimal? PlannedEffort_dec { get; set; }
    public decimal? calc_ActualEffort_dec { get; set; }
    public decimal? PlannedCost_val { get; set; }
    public decimal? calc_ActualCost_val { get; set; }
    public decimal? PlannedDuration_dec { get; set; }
    public decimal? calc_ActualDuration_dec { get; set; }
    public decimal? Analysis_prc { get; set; }
    public decimal? Progress_prc { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? ActualStartDate { get; set; }
    public DateTime? ActualEndDate { get; set; }
    public long? TaskSizeId { get; set; }
    public string? TaskSizeName { get; set; }
    public long? TaskPriorityId { get; set; }
    public string? TaskPriorityName { get; set; }
    public long? TaskComplexityId { get; set; }
    public string? TaskComplexityName { get; set; }
    public int? calc_Comments_num { get; set; }
    public string? EntityName { get; set; }
    public long? ObjectId { get; set; }
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









