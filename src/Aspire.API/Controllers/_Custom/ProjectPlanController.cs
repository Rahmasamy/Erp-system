using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aspire.APIs;

/// <summary>
/// The project schedule: the Gantt chart reads and writes WorkItems through here.
///
/// Why this is hand-written and lives in _Custom:
///  - the generated WorkItemService.Update sits inside the Code-Player markers and
///    drops Progress_prc, PlannedEffort_dec, PlannedCost_val and PlannedDuration_dec,
///    so a save through /WorkItem/edit looks fine and loses the values;
///  - the tree needs one query per expand, not a page of flat rows;
///  - files under Controllers/_Custom carry no generator markers, so a regeneration
///    cannot overwrite this.
///
/// Scope: only the three project work-item types (Stream, Package, Activity) take part
/// in the schedule. Call, Task, Note, Visit and Meeting are excluded.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProjectPlanController(ApplicationDbContext context, ITenantProviderService _tenantService) : ControllerBase
{
    /// <summary>The work-item types that belong on a Gantt chart.</summary>
    private static readonly string[] ScheduleTypeNames =
    [
        "Project Work Stream",
        "Project Work Package",
        "Project Work Activity",
    ];

    /// <summary>
    /// EntityType ids are per-tenant identities, never constants, so the schedule types
    /// are resolved by name for the current tenant.
    /// </summary>
    private async Task<List<long>> ScheduleTypeIds()
    {
        return await context.EntityTypes
            .Where(t => t.EntityName == "WorkItem" && ScheduleTypeNames.Contains(t.Name!))
            .Select(t => t.Id)
            .ToListAsync();
    }

    // ---------------------------------------------------------------- read

    /// <summary>
    /// One level of the tree, or a few. Pass ParentId to expand a node; leave it null
    /// for the project's roots. Depth &gt; 1 pre-loads that many generations in one call,
    /// which the Gantt uses for its first paint.
    /// </summary>
    [HttpPost("GetTree")]
    public async Task<IActionResult> GetTree([FromBody] PlanTreeRequest req)
    {
        try
        {
            if (req is null || req.ProjectId <= 0)
                return Ok(new { severity = "error", summary = "Validation", message = "ProjectId is required" });

            long? tenantId = _tenantService.GetTenantId();
            List<long> typeIds = await ScheduleTypeIds();
            int depth = Math.Clamp(req.Depth ?? 1, 1, 10);

            // Recursive walk from the requested level down. Parameterised, and TenantId is
            // explicit: the global query filter only applies to LINQ, not to raw SQL.
            const string sql = @"
WITH RECURSIVE branch AS (
    SELECT w.*, 1 AS depth, ARRAY[w.""SortOrder""::bigint, w.""Id""] AS ord
    FROM ""WorkItems"" w
    WHERE w.""TenantId"" = {0}
      AND w.""EntityName"" = 'Project'
      AND w.""ObjectId"" = {1}
      AND ({2}::bigint IS NULL AND w.""ParentId"" IS NULL OR w.""ParentId"" = {2}::bigint)
      AND w.""EntityTypeId"" = ANY({3})
    UNION ALL
    SELECT c.*, b.depth + 1, b.ord || ARRAY[c.""SortOrder""::bigint, c.""Id""]
    FROM ""WorkItems"" c
    JOIN branch b ON c.""ParentId"" = b.""Id""
    WHERE c.""TenantId"" = {0}
      AND b.depth < {4}
      AND c.""EntityTypeId"" = ANY({3})
)
-- ordered down the tree, not grouped by parent: ""ORDER BY ParentId"" put every root
-- first and every child afterwards, so a child rendered below the last top-level row
-- instead of under its own parent
SELECT * FROM branch ORDER BY ord";

            List<WorkItem> rows = await context.WorkItems
                .FromSqlRaw(sql, tenantId ?? 0, req.ProjectId, req.ParentId as object ?? DBNull.Value, typeIds.ToArray(), depth)
                .AsNoTracking()
                .ToListAsync();

            List<long> ids = rows.Select(r => r.Id).ToList();

            // which of these have children at all, so the tree knows what to make expandable
            List<long> withChildren = await context.WorkItems
                .Where(w => w.ParentId != null && ids.Contains(w.ParentId.Value) && typeIds.Contains(w.EntityTypeId ?? 0))
                .Select(w => w.ParentId!.Value)
                .Distinct()
                .ToListAsync();

            List<PlanTaskDTO> items = await Decorate(
                rows, withChildren,
                await RolledUpProgress(req.ProjectId),
                await RolledUpEffort(req.ProjectId));

            return Ok(new PaginationModel<PlanTaskDTO>(items, 1, items.Count == 0 ? 1 : items.Count, items.Count, null));
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    /// <summary>
    /// The three work-item types the schedule covers, in hierarchy order, so the client
    /// can offer them when a task is created. EntityType ids are per-tenant identities,
    /// which is why they are looked up by name and never hardcoded anywhere.
    /// </summary>
    [HttpGet("GetTaskTypes")]
    public async Task<IActionResult> GetTaskTypes()
    {
        try
        {
            var types = await context.EntityTypes
                .Where(t => t.EntityName == "WorkItem" && ScheduleTypeNames.Contains(t.Name!))
                .Select(t => new { t.Id, t.Name, t.Color })
                .ToListAsync();

            // the query returns them in whatever order the table holds; the ladder is fixed
            var ordered = ScheduleTypeNames
                .Select(name => types.FirstOrDefault(t => t.Name == name))
                .Where(t => t is not null)
                .Select((t, i) => new { t!.Id, t.Name, t.Color, Level = i + 1 })
                .ToList();

            return Ok(ordered);
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    // ------------------------------------------------------------- checklist
    //
    // A checklist item is a child work item of type "Task". Nothing new is stored:
    // ParentId already allows the nesting, and the schedule queries filter on the
    // three Stream/Package/Activity types, so these never appear on the Gantt or in
    // the timesheet tree. IsTimeSheet is off for the same reason - a tick is not work
    // anyone books hours against.

    /// <summary>The checklist hanging off one task.</summary>
    [HttpGet("GetChecklist")]
    public async Task<IActionResult> GetChecklist([FromQuery] long taskId)
    {
        try
        {
            long? typeId = await ChecklistTypeId();
            if (typeId is null)
                return Ok(Array.Empty<object>());

            var items = await context.WorkItems
                .Where(w => w.ParentId == taskId && w.EntityTypeId == typeId)
                .OrderBy(w => w.SortOrder).ThenBy(w => w.Id)
                .Select(w => new { id = w.Id, text = w.Name, done = w.Progress_prc >= 100 })
                .ToListAsync();

            return Ok(items);
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    /// <summary>Add an item, rename it, tick it or untick it.</summary>
    [HttpPost("SaveChecklistItem")]
    public async Task<IActionResult> SaveChecklistItem([FromBody] ChecklistItemDTO dto)
    {
        try
        {
            if (dto is null || (dto.Id is null or 0 && dto.TaskId is null or 0))
                return Ok(new { severity = "error", summary = "Validation", message = "TaskId is required" });

            WorkItem? item;

            if (dto.Id is > 0)
            {
                item = await context.WorkItems.FirstOrDefaultAsync(w => w.Id == dto.Id);
                if (item is null)
                    return Ok(new { severity = "error", summary = "Validation", message = "Item not found" });
            }
            else
            {
                WorkItem? parent = await context.WorkItems.FirstOrDefaultAsync(w => w.Id == dto.TaskId);
                if (parent is null)
                    return Ok(new { severity = "error", summary = "Validation", message = "Task not found" });

                item = new WorkItem
                {
                    TenantId = _tenantService.GetTenantId(),
                    EntityName = parent.EntityName,
                    ObjectId = parent.ObjectId,
                    ParentId = parent.Id,
                    LineItemId = parent.LineItemId,
                    EntityTypeId = await ChecklistTypeId(),
                    EntityStatusId = parent.EntityStatusId,
                    CreatedByUsrId = _tenantService.GetUserId(),
                    CreateDate = DateTime.UtcNow,
                    IsActive = true,
                    IsLeaf = true,
                    IsTimeSheet = false, // a checklist tick is not bookable work
                    SortOrder = await NextSortOrder(parent.Id, parent.ObjectId),
                };
                context.WorkItems.Add(item);
            }

            if (dto.Text is not null) item.Name = dto.Text.Trim();
            if (dto.Done.HasValue) item.Progress_prc = dto.Done.Value ? 100 : 0;
            item.UpdatedByUsrId = _tenantService.GetUserId();
            item.UpdateDate = DateTime.UtcNow;
            item.Msg = "OK";

            await context.SaveChangesAsync();
            return Ok(new { msg = "OK", id = item.Id });
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    [HttpDelete("DeleteChecklistItem")]
    public async Task<IActionResult> DeleteChecklistItem([FromQuery] long id)
    {
        try
        {
            WorkItem? item = await context.WorkItems.FirstOrDefaultAsync(w => w.Id == id);
            if (item is null) return Ok(new { msg = "OK", deleted = 0 });

            context.WorkItems.Remove(item);
            await context.SaveChangesAsync();
            return Ok(new { msg = "OK", deleted = 1 });
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    /// <summary>The "Task" work-item type, by name - ids are per-tenant identities.</summary>
    private async Task<long?> ChecklistTypeId() =>
        await context.EntityTypes
            .Where(t => t.EntityName == "WorkItem" && t.Name == "Task")
            .Select(t => (long?)t.Id)
            .FirstOrDefaultAsync();

    /// <summary>Every dependency between the work items of one project.</summary>
    [HttpGet("GetDependencies")]
    public async Task<IActionResult> GetDependencies([FromQuery] long projectId)
    {
        try
        {
            List<long> ids = await ProjectItemIds(projectId);

            var links = await context.WorkItemDependencies
                .Where(d => d.PredecessorId != null && d.SuccessorId != null
                            && ids.Contains(d.PredecessorId.Value) && ids.Contains(d.SuccessorId.Value))
                .Select(d => new PlanDependencyDTO
                {
                    Id = d.Id,
                    PredecessorId = d.PredecessorId,
                    SuccessorId = d.SuccessorId,
                    LinkType = d.LinkType,
                    LagDays = d.LagDays,
                })
                .ToListAsync();

            return Ok(links);
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    // --------------------------------------------------------------- write

    /// <summary>
    /// Create or update one task. Only the fields the schedule owns are written, so this
    /// cannot clobber what the entity form manages.
    /// </summary>
    [HttpPost("SaveTask")]
    public async Task<IActionResult> SaveTask([FromBody] PlanTaskSaveDTO dto)
    {
        try
        {
            if (dto is null)
                return Ok(new { severity = "error", summary = "Validation", message = "No task supplied" });

            if (dto.StartDate.HasValue && dto.EndDate.HasValue && dto.EndDate < dto.StartDate)
                return Ok(new { severity = "error", summary = "Validation", message = "End date must be on or after the start date" });

            WorkItem? item;

            if (dto.Id is > 0)
            {
                item = await context.WorkItems.FirstOrDefaultAsync(w => w.Id == dto.Id);
                if (item is null)
                    return Ok(new { severity = "error", summary = "Validation", message = "Task not found" });
            }
            else
            {
                if (dto.ProjectId is not > 0)
                    return Ok(new { severity = "error", summary = "Validation", message = "ProjectId is required for a new task" });

                item = new WorkItem
                {
                    TenantId = _tenantService.GetTenantId(),
                    EntityName = "Project",
                    ObjectId = dto.ProjectId,
                    CreatedByUsrId = _tenantService.GetUserId(),
                    CreateDate = DateTime.UtcNow,
                    IsActive = true,
                    SortOrder = dto.SortOrder ?? await NextSortOrder(dto.ParentId, dto.ProjectId),
                    // a new row has nothing under it yet, so it is a leaf and hours may be
                    // logged against it; RefreshLeafFlags turns both off if it gains children
                    IsLeaf = true,
                    IsTimeSheet = true,
                };
                context.WorkItems.Add(item);
            }

            // The chart creates rows without saying what they are: a row at the root is a
            // stream, a row under a stream is a package, anything deeper is an activity.
            if (item.EntityTypeId is null && dto.EntityTypeId is null)
                item.EntityTypeId = await DefaultTypeIdFor(dto.ParentId ?? item.ParentId);

            if (item.EntityStatusId is null && dto.EntityStatusId is null)
                item.EntityStatusId = await context.EntityStatuses
                    .Where(st => st.EntityName == "WorkItem")
                    .Select(st => (long?)st.Id)
                    .FirstOrDefaultAsync();

            if (dto.Name is not null) item.Name = dto.Name;
            if (dto.Description is not null) item.Description = dto.Description;
            if (dto.ParentId.HasValue) item.ParentId = dto.ParentId == 0 ? null : dto.ParentId;
            if (dto.LineItemId.HasValue) item.LineItemId = dto.LineItemId;
            if (dto.EntityTypeId.HasValue) item.EntityTypeId = dto.EntityTypeId;
            if (dto.EntityStatusId.HasValue) item.EntityStatusId = dto.EntityStatusId;
            // 0 clears the assignment, as with ParentId above: a null cannot be told apart
            // from a field the client simply did not send
            if (dto.AssignedToUsrId.HasValue)
                item.AssignedToUsrId = dto.AssignedToUsrId == 0 ? null : dto.AssignedToUsrId;
            if (dto.TaskPriorityId.HasValue)
                item.TaskPriorityId = dto.TaskPriorityId == 0 ? null : dto.TaskPriorityId;
            if (dto.StartDate.HasValue) item.StartDate = dto.StartDate;
            if (dto.EndDate.HasValue) item.EndDate = dto.EndDate;
            if (dto.BaselineStartDate.HasValue) item.BaselineStartDate = dto.BaselineStartDate;
            if (dto.BaselineEndDate.HasValue) item.BaselineEndDate = dto.BaselineEndDate;
            if (dto.IsMilestone.HasValue) item.IsMilestone = dto.IsMilestone;
            if (dto.SortOrder.HasValue) item.SortOrder = dto.SortOrder.Value;

            // the fields the generated update drops
            if (dto.Progress_prc.HasValue) item.Progress_prc = dto.Progress_prc.Value;
            if (dto.PlannedEffort_dec.HasValue) item.PlannedEffort_dec = dto.PlannedEffort_dec.Value;
            if (dto.PlannedCost_val.HasValue) item.PlannedCost_val = dto.PlannedCost_val.Value;

            if (item.StartDate.HasValue && item.EndDate.HasValue)
                item.PlannedDuration_dec = (decimal)(item.EndDate.Value.Date - item.StartDate.Value.Date).TotalDays + 1;

            if (item.IsMilestone == true) item.EndDate = item.StartDate;

            item.UpdatedByUsrId = _tenantService.GetUserId();
            item.UpdateDate = DateTime.UtcNow;
            item.Msg = "OK";

            await context.SaveChangesAsync();
            await RefreshLeafFlags(item.ParentId);

            // Moving a task moves what waits on it. Without this a link was a drawn
            // arrow and nothing more - you could drag a task three weeks out and every
            // task depending on it would sit where it was, which is the difference
            // between a Gantt and a bar chart.
            int pushed = dto.StartDate.HasValue || dto.EndDate.HasValue
                ? await CascadeSuccessors(item.Id)
                : 0;

            return Ok(new { msg = "OK", id = item.Id, rescheduled = pushed });
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    /// <summary>
    /// Re-derive the work-item type of a row and its descendants from how deep they now
    /// sit: stream, package, then activity for anything below that.
    /// </summary>
    private async Task RetypeSubtree(long rootId, long? newParentId)
    {
        var types = await context.EntityTypes
            .Where(e => e.EntityName == "WorkItem" && ScheduleTypeNames.Contains(e.Name!))
            .Select(e => new { e.Id, e.Name })
            .ToListAsync();

        long? TypeForDepth(int depth) =>
            types.FirstOrDefault(x => x.Name == ScheduleTypeNames[Math.Min(depth, ScheduleTypeNames.Length - 1)])?.Id;

        // how far down the new parent already sits
        int baseDepth = 0;
        long? walk = newParentId;
        while (walk.HasValue && baseDepth < 20)
        {
            baseDepth++;
            walk = await context.WorkItems.Where(w => w.Id == walk).Select(w => w.ParentId).FirstOrDefaultAsync();
        }

        List<long> typeIds = types.Select(x => x.Id).ToList();
        var level = new List<long> { rootId };

        for (int depth = baseDepth; level.Count > 0 && depth < 20; depth++)
        {
            List<WorkItem> items = await context.WorkItems.Where(w => level.Contains(w.Id)).ToListAsync();
            long? typeId = TypeForDepth(depth);
            foreach (WorkItem w in items)
                if (typeId.HasValue) w.EntityTypeId = typeId;

            level = await context.WorkItems
                .Where(w => w.ParentId != null && level.Contains(w.ParentId.Value)
                            && typeIds.Contains(w.EntityTypeId ?? 0))
                .Select(w => w.Id)
                .ToListAsync();
        }

        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Push every task that waits on this one far enough forward to respect its link.
    ///
    /// Finish-to-start with a lag: a successor may not begin until its predecessor has
    /// finished, plus the lag, plus a day. Only late starts are corrected - a successor
    /// already later than it needs to be is left where the plan put it, because pulling
    /// work earlier is a decision, not arithmetic.
    ///
    /// Each task is moved at most once and the walk is depth-capped, so a chain that
    /// somehow closed a loop cannot spin.
    /// </summary>
    private async Task<int> CascadeSuccessors(long fromId)
    {
        var links = await context.WorkItemDependencies
            .Where(d => d.PredecessorId != null && d.SuccessorId != null)
            .Select(d => new { From = d.PredecessorId!.Value, To = d.SuccessorId!.Value, d.LagDays })
            .ToListAsync();

        if (links.Count == 0) return 0;

        var moved = new HashSet<long>();
        var queue = new Queue<(long Id, int Depth)>();
        queue.Enqueue((fromId, 0));
        int changed = 0;

        while (queue.Count > 0)
        {
            var (id, depth) = queue.Dequeue();
            if (depth > 50) continue;

            WorkItem? predecessor = await context.WorkItems.FirstOrDefaultAsync(w => w.Id == id);
            if (predecessor?.EndDate is null) continue;

            foreach (var link in links.Where(l => l.From == id))
            {
                WorkItem? next = await context.WorkItems.FirstOrDefaultAsync(w => w.Id == link.To);
                if (next?.StartDate is null || next.EndDate is null) continue;
                if (!moved.Add(next.Id)) continue;

                DateTime earliest = predecessor.EndDate.Value.Date.AddDays(link.LagDays + 1);
                if (next.StartDate.Value.Date >= earliest) continue;

                // keep the task's length; only slide it
                int shift = (int)(earliest - next.StartDate.Value.Date).TotalDays;
                next.StartDate = next.StartDate.Value.AddDays(shift);
                next.EndDate = next.EndDate.Value.AddDays(shift);
                next.UpdatedByUsrId = _tenantService.GetUserId();
                next.UpdateDate = DateTime.UtcNow;
                changed++;

                queue.Enqueue((next.Id, depth + 1));
            }
        }

        if (changed > 0) await context.SaveChangesAsync();
        return changed;
    }

    /// <summary>
    /// Drag and drop: move a task under a new parent and/or to a new position among its
    /// siblings. Refuses to drop a task inside its own subtree.
    /// </summary>
    [HttpPost("MoveTask")]
    public async Task<IActionResult> MoveTask([FromBody] PlanMoveDTO dto)
    {
        try
        {
            WorkItem? item = await context.WorkItems.FirstOrDefaultAsync(w => w.Id == dto.Id);
            if (item is null)
                return Ok(new { severity = "error", summary = "Validation", message = "Task not found" });

            long? newParent = dto.NewParentId == 0 ? null : dto.NewParentId;

            if (newParent.HasValue && await IsDescendant(newParent.Value, item.Id))
                return Ok(new { severity = "error", summary = "Validation", message = "A task cannot be moved inside itself" });

            item.ParentId = newParent;
            item.UpdatedByUsrId = _tenantService.GetUserId();
            item.UpdateDate = DateTime.UtcNow;

            // The ladder is positional - a row at the root is a stream, one below that a
            // package, anything deeper an activity - so moving a row has to retype it and
            // everything under it. Without this a task created at the root stayed a
            // *stream* after being indented twice: it still counted as a summary, so it
            // could not be given a predecessor and hours could not be booked against it.
            await RetypeSubtree(item.Id, newParent);

            // renumber the destination siblings in the order the client sent
            if (dto.SiblingIdsInOrder is { Count: > 0 })
            {
                List<WorkItem> siblings = await context.WorkItems
                    .Where(w => dto.SiblingIdsInOrder.Contains(w.Id))
                    .ToListAsync();

                for (int i = 0; i < dto.SiblingIdsInOrder.Count; i++)
                {
                    WorkItem? sib = siblings.FirstOrDefault(s => s.Id == dto.SiblingIdsInOrder[i]);
                    if (sib is not null) sib.SortOrder = i;
                }
            }
            else if (dto.SortOrder.HasValue)
            {
                item.SortOrder = dto.SortOrder.Value;
            }

            await context.SaveChangesAsync();
            await RefreshLeafFlags(newParent);

            return Ok(new { msg = "OK" });
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    /// <summary>
    /// Delete a task and everything under it. Refuses while hours are logged anywhere in
    /// that subtree - losing a timesheet to a drag is not recoverable.
    /// </summary>
    [HttpDelete("DeleteTask")]
    public async Task<IActionResult> DeleteTask([FromQuery] long id)
    {
        try
        {
            List<long> subtree = await SubtreeIds(id);
            if (subtree.Count == 0)
                return Ok(new { severity = "error", summary = "Validation", message = "Task not found" });

            bool hasHours = await context.TimeSheets
                .AnyAsync(t => t.WorkItemId != null && subtree.Contains(t.WorkItemId.Value)
                               && (t.RegularHours_dec > 0 || t.OverTimeHours_dec > 0 || t.WeekEndHours_dec > 0));

            if (hasHours)
                return Ok(new { severity = "error", summary = "Validation", message = "Hours are logged against this task or its children" });

            // no database foreign keys here, so the links have to be cleared by hand
            List<WorkItemDependency> links = await context.WorkItemDependencies
                .Where(d => (d.PredecessorId != null && subtree.Contains(d.PredecessorId.Value))
                            || (d.SuccessorId != null && subtree.Contains(d.SuccessorId.Value)))
                .ToListAsync();
            context.WorkItemDependencies.RemoveRange(links);

            List<WorkItem> items = await context.WorkItems.Where(w => subtree.Contains(w.Id)).ToListAsync();
            long? parentId = items.FirstOrDefault(i => i.Id == id)?.ParentId;
            context.WorkItems.RemoveRange(items);

            await context.SaveChangesAsync();
            await RefreshLeafFlags(parentId);

            return Ok(new { msg = "OK", deleted = subtree.Count });
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    /// <summary>
    /// Freeze the plan as it stands: every scheduled item keeps a copy of its dates,
    /// which is what the chart draws behind the bars once Baseline is switched on.
    /// </summary>
    [HttpPost("SetBaseline")]
    public async Task<IActionResult> SetBaseline([FromBody] PlanBaselineRequest req)
    {
        try
        {
            if (req is null || req.ProjectId <= 0)
                return Ok(new { severity = "error", summary = "Validation", message = "ProjectId is required" });

            List<long> ids = await ProjectItemIds(req.ProjectId);
            List<WorkItem> items = await context.WorkItems.Where(w => ids.Contains(w.Id)).ToListAsync();

            int stamped = 0;
            foreach (WorkItem item in items)
            {
                // an existing baseline is the record of what was agreed: only replace it
                // when the caller says so
                if (!req.Overwrite && (item.BaselineStartDate.HasValue || item.BaselineEndDate.HasValue)) continue;

                item.BaselineStartDate = item.StartDate;
                item.BaselineEndDate = item.EndDate;
                item.UpdatedByUsrId = _tenantService.GetUserId();
                item.UpdateDate = DateTime.UtcNow;
                stamped++;
            }

            await context.SaveChangesAsync();
            return Ok(new { msg = "OK", stamped });
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    /// <summary>Link two tasks. Rejects self-links, duplicates and anything that would loop.</summary>
    [HttpPost("AddDependency")]
    public async Task<IActionResult> AddDependency([FromBody] PlanDependencyDTO dto)
    {
        try
        {
            if (dto.PredecessorId is null || dto.SuccessorId is null)
                return Ok(new { severity = "error", summary = "Validation", message = "Both tasks are required" });

            if (dto.PredecessorId == dto.SuccessorId)
                return Ok(new { severity = "error", summary = "Validation", message = "A task cannot depend on itself" });

            bool exists = await context.WorkItemDependencies
                .AnyAsync(d => d.PredecessorId == dto.PredecessorId && d.SuccessorId == dto.SuccessorId);
            if (exists)
                return Ok(new { severity = "error", summary = "Validation", message = "These tasks are already linked" });

            if (await WouldCycle(dto.PredecessorId.Value, dto.SuccessorId.Value))
                return Ok(new { severity = "error", summary = "Validation", message = "That link would create a loop" });

            WorkItemDependency link = new()
            {
                TenantId = _tenantService.GetTenantId(),
                PredecessorId = dto.PredecessorId,
                SuccessorId = dto.SuccessorId,
                LinkType = string.IsNullOrWhiteSpace(dto.LinkType) ? "FS" : dto.LinkType,
                LagDays = dto.LagDays ?? 0,
                IsActive = true,
                CreatedByUsrId = _tenantService.GetUserId(),
                CreateDate = DateTime.UtcNow,
                Msg = "OK",
            };

            context.WorkItemDependencies.Add(link);
            await context.SaveChangesAsync();

            return Ok(new { msg = "OK", id = link.Id });
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    [HttpDelete("DeleteDependency")]
    public async Task<IActionResult> DeleteDependency([FromQuery] long id)
    {
        try
        {
            WorkItemDependency? link = await context.WorkItemDependencies.FirstOrDefaultAsync(d => d.Id == id);
            if (link is null)
                return Ok(new { severity = "error", summary = "Validation", message = "Link not found" });

            context.WorkItemDependencies.Remove(link);
            await context.SaveChangesAsync();

            return Ok(new { msg = "OK" });
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    // -------------------------------------------------------------- helpers

    /// <summary>Joins the labels the chart shows: type, status and the assignee.</summary>
    /// <summary>
    /// A phase's progress is the progress of the work beneath it, weighted by how long
    /// each piece is - a 20-day task at 100% beside a 5-day one at 0% is 80%, not 50%.
    ///
    /// Computed here rather than on the client because the client only holds the levels
    /// it has expanded: a plan opened on its top two levels showed every stream at 0%
    /// until someone opened it, and that is the first number anyone reads.
    /// </summary>
    private async Task<Dictionary<long, decimal>> RolledUpProgress(long projectId)
    {
        List<long> typeIds = await ScheduleTypeIds();

        var all = await context.WorkItems
            .Where(w => w.EntityName == "Project" && w.ObjectId == projectId
                        && typeIds.Contains(w.EntityTypeId ?? 0))
            .Select(w => new { w.Id, w.ParentId, w.Progress_prc, w.StartDate, w.EndDate })
            .ToListAsync();

        var childrenOf = all.Where(w => w.ParentId.HasValue)
            .GroupBy(w => w.ParentId!.Value)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Id).ToList());

        var byId = all.ToDictionary(w => w.Id);
        var result = new Dictionary<long, decimal>();

        decimal Days(long id)
        {
            var w = byId[id];
            if (w.StartDate is null || w.EndDate is null) return 1m;
            return Math.Max(1m, (decimal)(w.EndDate.Value - w.StartDate.Value).TotalDays + 1);
        }

        decimal Resolve(long id, int depth)
        {
            if (result.TryGetValue(id, out decimal done)) return done;
            if (depth > 20 || !childrenOf.TryGetValue(id, out List<long>? kids) || kids.Count == 0)
                return result[id] = byId[id].Progress_prc;

            decimal span = kids.Sum(Days);
            decimal value = span <= 0
                ? byId[id].Progress_prc
                : Math.Round(kids.Sum(k => Resolve(k, depth + 1) * Days(k)) / span, 0);
            return result[id] = value;
        }

        foreach (var w in all) Resolve(w.Id, 0);
        return result;
    }

    /// <summary>
    /// Planned and actual effort summed up the tree, so a phase reports the work
    /// beneath it. Actual effort is written by the timesheet; without this a stream
    /// would always read 0 h against a plan of hundreds.
    /// </summary>
    private async Task<Dictionary<long, (decimal Planned, decimal Actual)>> RolledUpEffort(long projectId)
    {
        List<long> typeIds = await ScheduleTypeIds();

        var all = await context.WorkItems
            .Where(w => w.EntityName == "Project" && w.ObjectId == projectId
                        && typeIds.Contains(w.EntityTypeId ?? 0))
            .Select(w => new { w.Id, w.ParentId, w.PlannedEffort_dec, w.calc_ActualEffort_dec })
            .ToListAsync();

        var childrenOf = all.Where(w => w.ParentId.HasValue)
            .GroupBy(w => w.ParentId!.Value)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Id).ToList());

        var byId = all.ToDictionary(w => w.Id);
        var result = new Dictionary<long, (decimal, decimal)>();

        (decimal, decimal) Resolve(long id, int depth)
        {
            if (result.TryGetValue(id, out var done)) return done;
            if (depth > 20 || !childrenOf.TryGetValue(id, out List<long>? kids) || kids.Count == 0)
                return result[id] = (byId[id].PlannedEffort_dec, byId[id].calc_ActualEffort_dec);

            decimal planned = 0, actual = 0;
            foreach (long k in kids)
            {
                var (p, a) = Resolve(k, depth + 1);
                planned += p;
                actual += a;
            }
            return result[id] = (planned, actual);
        }

        foreach (var w in all) Resolve(w.Id, 0);
        return result;
    }

    private async Task<List<PlanTaskDTO>> Decorate(List<WorkItem> rows, List<long> withChildren, Dictionary<long, decimal>? rolled = null, Dictionary<long, (decimal Planned, decimal Actual)>? effort = null)
    {
        List<long> typeIds = rows.Where(r => r.EntityTypeId.HasValue).Select(r => r.EntityTypeId!.Value).Distinct().ToList();
        List<long> statusIds = rows.Where(r => r.EntityStatusId.HasValue).Select(r => r.EntityStatusId!.Value).Distinct().ToList();
        List<long> userIds = rows.Where(r => r.AssignedToUsrId.HasValue).Select(r => r.AssignedToUsrId!.Value).Distinct().ToList();
        List<long> priorityIds = rows.Where(r => r.TaskPriorityId.HasValue).Select(r => r.TaskPriorityId!.Value).Distinct().ToList();

        var types = await context.EntityTypes.Where(t => typeIds.Contains(t.Id))
            .Select(t => new { t.Id, t.Name, t.Color }).ToListAsync();
        var statuses = await context.EntityStatuses.Where(s => statusIds.Contains(s.Id))
            .Select(s => new { s.Id, s.Name, s.Color }).ToListAsync();
        var users = await context.Users.Where(u => userIds.Contains(u.Id))
            .Select(u => new { u.Id, u.Name, u.PhotoUrl }).ToListAsync();
        var priorities = await context.TaskPriorities.Where(pr => priorityIds.Contains(pr.Id))
            .Select(pr => new { pr.Id, pr.Name }).ToListAsync();

        return rows.Select(w =>
        {
            var type = types.FirstOrDefault(t => t.Id == w.EntityTypeId);
            var status = statuses.FirstOrDefault(s => s.Id == w.EntityStatusId);
            var user = users.FirstOrDefault(u => u.Id == w.AssignedToUsrId);
            var priority = priorities.FirstOrDefault(pr => pr.Id == w.TaskPriorityId);

            return new PlanTaskDTO
            {
                Id = w.Id,
                No = w.No,
                Name = w.Name,
                Description = w.Description,
                ParentId = w.ParentId,
                LineItemId = w.LineItemId,
                SortOrder = w.SortOrder,
                EntityTypeId = w.EntityTypeId,
                EntityTypeName = type?.Name,
                EntityTypeColor = type?.Color,
                EntityStatusId = w.EntityStatusId,
                EntityStatusName = status?.Name,
                EntityStatusColor = status?.Color,
                AssignedToUsrId = w.AssignedToUsrId,
                AssignedToUsrName = user?.Name,
                AssignedToUsrPhotoUrl = user?.PhotoUrl,
                TaskPriorityId = w.TaskPriorityId,
                TaskPriorityName = priority?.Name,
                StartDate = w.StartDate,
                EndDate = w.EndDate,
                BaselineStartDate = w.BaselineStartDate,
                BaselineEndDate = w.BaselineEndDate,
                ActualStartDate = w.ActualStartDate,
                ActualEndDate = w.ActualEndDate,
                PlannedDuration_dec = w.PlannedDuration_dec,
                PlannedEffort_dec = effort is not null && effort.TryGetValue(w.Id, out var e) ? e.Planned : w.PlannedEffort_dec,
                ActualEffort_dec = effort is not null && effort.TryGetValue(w.Id, out var e2) ? e2.Actual : w.calc_ActualEffort_dec,
                Progress_prc = rolled is not null && rolled.TryGetValue(w.Id, out decimal rp) ? rp : w.Progress_prc,
                IsMilestone = w.IsMilestone,
                IsLeaf = w.IsLeaf,
                IsTimeSheet = w.IsTimeSheet,
                HasChildren = withChildren.Contains(w.Id),
            };
        }).ToList();
    }

    /// <summary>Ids of every schedule work item on a project.</summary>
    private async Task<List<long>> ProjectItemIds(long projectId)
    {
        List<long> typeIds = await ScheduleTypeIds();
        return await context.WorkItems
            .Where(w => w.EntityName == "Project" && w.ObjectId == projectId && typeIds.Contains(w.EntityTypeId ?? 0))
            .Select(w => w.Id)
            .ToListAsync();
    }

    /// <summary>A node and everything beneath it.</summary>
    private async Task<List<long>> SubtreeIds(long rootId)
    {
        long? tenantId = _tenantService.GetTenantId();
        const string sql = @"
WITH RECURSIVE sub AS (
    SELECT w.""Id"" FROM ""WorkItems"" w WHERE w.""TenantId"" = {0} AND w.""Id"" = {1}
    UNION ALL
    SELECT c.""Id"" FROM ""WorkItems"" c JOIN sub s ON c.""ParentId"" = s.""Id"" WHERE c.""TenantId"" = {0}
)
SELECT ""Id"" FROM sub";

        return await context.Database.SqlQueryRaw<long>(sql, tenantId ?? 0, rootId).ToListAsync();
    }

    private async Task<bool> IsDescendant(long candidateId, long rootId)
        => (await SubtreeIds(rootId)).Contains(candidateId);

    /// <summary>Would predecessor -> successor close a loop? True when it would.</summary>
    private async Task<bool> WouldCycle(long predecessorId, long successorId)
    {
        var links = await context.WorkItemDependencies
            .Where(d => d.PredecessorId != null && d.SuccessorId != null)
            .Select(d => new { From = d.PredecessorId!.Value, To = d.SuccessorId!.Value })
            .ToListAsync();

        // walk forward from the successor: reaching the predecessor means a loop
        HashSet<long> seen = [];
        Queue<long> queue = new();
        queue.Enqueue(successorId);

        while (queue.Count > 0)
        {
            long current = queue.Dequeue();
            if (current == predecessorId) return true;
            if (!seen.Add(current)) continue;

            foreach (var next in links.Where(l => l.From == current))
                queue.Enqueue(next.To);
        }

        return false;
    }

    /// <summary>Stream at the root, package under a stream, activity below that.</summary>
    private async Task<long?> DefaultTypeIdFor(long? parentId)
    {
        var types = await context.EntityTypes
            .Where(t => t.EntityName == "WorkItem" && ScheduleTypeNames.Contains(t.Name!))
            .Select(t => new { t.Id, t.Name })
            .ToListAsync();

        long? idOf(string name) => types.FirstOrDefault(t => t.Name == name)?.Id;

        if (!parentId.HasValue) return idOf("Project Work Stream");

        long? parentTypeId = await context.WorkItems
            .Where(w => w.Id == parentId)
            .Select(w => w.EntityTypeId)
            .FirstOrDefaultAsync();

        return parentTypeId == idOf("Project Work Stream")
            ? idOf("Project Work Package")
            : idOf("Project Work Activity");
    }

    private async Task<int> NextSortOrder(long? parentId, long? projectId)
    {
        var siblings = context.WorkItems.Where(w => w.ParentId == parentId);
        if (!parentId.HasValue)
            siblings = siblings.Where(w => w.EntityName == "Project" && w.ObjectId == projectId);

        return await siblings.AnyAsync() ? await siblings.MaxAsync(w => w.SortOrder) + 1 : 0;
    }

    /// <summary>
    /// A parent is not a leaf and carries no hours; a task with no children is both.
    /// Kept correct on every structural change so the timesheet can trust the flags.
    /// </summary>
    private async Task RefreshLeafFlags(long? parentId)
    {
        if (!parentId.HasValue) return;

        WorkItem? parent = await context.WorkItems.FirstOrDefaultAsync(w => w.Id == parentId);
        if (parent is null) return;

        // Only real schedule rows count. A checklist item is a child work item of type
        // Task, so counting every child made a task with a checklist read as a parent -
        // and a parent takes no hours, which silently shut its timesheet off.
        List<long> typeIds = await ScheduleTypeIds();
        bool hasChildren = await context.WorkItems
            .AnyAsync(w => w.ParentId == parentId && typeIds.Contains(w.EntityTypeId ?? 0));
        parent.IsLeaf = !hasChildren;
        // hours follow the leaf: a row that gains children stops taking them, and one whose
        // last child is deleted takes them again
        parent.IsTimeSheet = !hasChildren;

        await context.SaveChangesAsync();
    }
}

// ------------------------------------------------------------------ contracts

public class ChecklistItemDTO
{
    /// <summary>Set to update an existing item; omit to add one.</summary>
    public long? Id { get; set; }
    /// <summary>The task the item hangs off. Required when adding.</summary>
    public long? TaskId { get; set; }
    public string? Text { get; set; }
    public bool? Done { get; set; }
}

public class PlanTreeRequest
{
    public long ProjectId { get; set; }
    /// <summary>null = the project's root items.</summary>
    public long? ParentId { get; set; }
    /// <summary>How many generations to return, 1-10. Default 1.</summary>
    public int? Depth { get; set; }
}

public class PlanTaskDTO
{
    public long Id { get; set; }
    public string? No { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public long? ParentId { get; set; }
    public long? LineItemId { get; set; }
    public int SortOrder { get; set; }
    public long? EntityTypeId { get; set; }
    public string? EntityTypeName { get; set; }
    public string? EntityTypeColor { get; set; }
    public long? EntityStatusId { get; set; }
    public string? EntityStatusName { get; set; }
    public string? EntityStatusColor { get; set; }
    public long? AssignedToUsrId { get; set; }
    public string? AssignedToUsrName { get; set; }
    public string? AssignedToUsrPhotoUrl { get; set; }
    public long? TaskPriorityId { get; set; }
    public string? TaskPriorityName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? BaselineStartDate { get; set; }
    public DateTime? BaselineEndDate { get; set; }
    public DateTime? ActualStartDate { get; set; }
    public DateTime? ActualEndDate { get; set; }
    public decimal PlannedDuration_dec { get; set; }
    public decimal PlannedEffort_dec { get; set; }
    /// <summary>Hours booked against this item on the timesheet, summed up the tree.</summary>
    public decimal ActualEffort_dec { get; set; }
    public decimal Progress_prc { get; set; }
    public bool? IsMilestone { get; set; }
    public bool? IsLeaf { get; set; }
    public bool? IsTimeSheet { get; set; }
    public bool HasChildren { get; set; }
}

public class PlanTaskSaveDTO
{
    public long? Id { get; set; }
    public long? ProjectId { get; set; }
    public long? ParentId { get; set; }
    public long? LineItemId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public long? EntityTypeId { get; set; }
    public long? EntityStatusId { get; set; }
    public long? AssignedToUsrId { get; set; }
    /// <summary>A row from TaskPriorities; 0 clears it.</summary>
    public long? TaskPriorityId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? BaselineStartDate { get; set; }
    public DateTime? BaselineEndDate { get; set; }
    public decimal? Progress_prc { get; set; }
    public decimal? PlannedEffort_dec { get; set; }
    public decimal? PlannedCost_val { get; set; }
    public bool? IsMilestone { get; set; }
    public int? SortOrder { get; set; }
}

public class PlanMoveDTO
{
    public long Id { get; set; }
    /// <summary>0 or null moves the task to the project root.</summary>
    public long? NewParentId { get; set; }
    public int? SortOrder { get; set; }
    /// <summary>The destination siblings in their new order; renumbered in one go.</summary>
    public List<long>? SiblingIdsInOrder { get; set; }
}

public class PlanBaselineRequest
{
    public long ProjectId { get; set; }
    /// <summary>Replace a baseline that was already taken.</summary>
    public bool Overwrite { get; set; }
}

public class PlanDependencyDTO
{
    public long? Id { get; set; }
    public long? PredecessorId { get; set; }
    public long? SuccessorId { get; set; }
    public string? LinkType { get; set; }
    public int? LagDays { get; set; }
}
