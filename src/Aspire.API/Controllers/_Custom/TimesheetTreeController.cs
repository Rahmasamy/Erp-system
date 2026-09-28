using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aspire.APIs;

/// <summary>
/// The timesheet: a person's assigned work items as a tree, with the hours they logged
/// against each day of a week.
///
/// Rules this enforces, rather than trusting the client:
///  - hours belong to leaves only (IsLeaf / IsTimeSheet). A stream or a package shows
///    the sum of everything beneath it and cannot be typed into.
///  - an approved day cannot be edited.
///  - TotalHours_dec is computed here, never sent.
///
/// It replaces _Reports/WorkItemTree.cs, whose SQL selects columns that do not exist
/// (WorkItems.AssignedToId, EntityTypes.EntityTypeId, EntityStatuses.EntityStatusId),
/// has its hours loop commented out, is built by string concatenation and never filters
/// by tenant. Those routes stay for now so the current page keeps working.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TimesheetTreeController(ApplicationDbContext context, ITenantProviderService _tenantService) : ControllerBase
{
    /// <summary>
    /// One level of the person's tree. Omit ParentId for the roots; the response says
    /// which rows have children, so the table can expand on demand.
    /// </summary>
    [HttpPost("GetTree")]
    public async Task<IActionResult> GetTree([FromBody] TimesheetTreeRequest req)
    {
        try
        {
            long? tenantId = _tenantService.GetTenantId();
            long? userId = req?.UserId ?? _tenantService.GetUserId();

            DateTime from = (req?.From ?? DateTime.UtcNow.Date).Date;
            DateTime to = (req?.To ?? from.AddDays(6)).Date;
            if (to < from) (from, to) = (to, from);

            // Everything the person is assigned that belongs to this week, plus every
            // ancestor, so the tree has a spine to hang the leaves on. Walking up from the
            // assignments handles any depth - the old query hardcoded three levels.
            //
            // "This week" is the task's own dates overlapping it, or hours already booked
            // in it. Without the second half a correction to a task that has since finished
            // would be impossible; without the first, every task a person has ever been
            // given appears in every week of the year, all of them reading zero.
            // A task with no dates at all is never hidden.
            const string sql = @"
WITH RECURSIVE mine AS (
    SELECT w.*
    FROM ""WorkItems"" w
    WHERE w.""TenantId"" = {0}
      AND (w.""AssignedToUsrId"" = {1}
           OR w.""Id"" IN (SELECT a.""WorkItemId"" FROM ""WorkItemAssignees"" a
                           WHERE a.""TenantId"" = {0} AND a.""AssignedToUsrId"" = {1}))
      AND (
            (COALESCE(w.""StartDate"", {2}) <= {3} AND COALESCE(w.""EndDate"", {3}) >= {2})
            OR EXISTS (SELECT 1 FROM ""TimeSheets"" ts
                       WHERE ts.""WorkItemId"" = w.""Id""
                         AND ts.""AssignedToUsrId"" = {1}
                         AND ts.""WorkDate"" >= {2} AND ts.""WorkDate"" <= {3})
          )
    UNION
    SELECT p.*
    FROM ""WorkItems"" p
    JOIN mine m ON m.""ParentId"" = p.""Id""
    WHERE p.""TenantId"" = {0}
)
SELECT * FROM mine ORDER BY ""ParentId"" NULLS FIRST, ""SortOrder"", ""Id""";

            List<WorkItem> all = await context.WorkItems
                .FromSqlRaw(sql, tenantId ?? 0, userId ?? 0, from, to)
                .AsNoTracking()
                .ToListAsync();

            if (all.Count == 0)
                return Ok(new PaginationModel<TimesheetNodeDTO>([], 1, 1, 0, null));

            List<long> ids = all.Select(w => w.Id).ToList();

            // hours for the week, for this person, against any of those items
            List<TimeSheet> entries = await context.TimeSheets
                .Where(t => t.AssignedToUsrId == userId
                            && t.WorkItemId != null && ids.Contains(t.WorkItemId.Value)
                            && t.WorkDate != null && t.WorkDate.Value.Date >= from && t.WorkDate.Value.Date <= to)
                .AsNoTracking()
                .ToListAsync();

            // labels
            List<long> typeIds = all.Where(w => w.EntityTypeId.HasValue).Select(w => w.EntityTypeId!.Value).Distinct().ToList();
            var types = await context.EntityTypes.Where(t => typeIds.Contains(t.Id))
                .Select(t => new { t.Id, t.Name }).ToListAsync();

            int days = (int)(to - from).TotalDays + 1;

            // one node per work item, own hours filled in
            Dictionary<long, TimesheetNodeDTO> nodes = all.ToDictionary(
                w => w.Id,
                w => new TimesheetNodeDTO
                {
                    Id = w.Id,
                    Name = w.Name,
                    ParentId = w.ParentId,
                    Type = types.FirstOrDefault(t => t.Id == w.EntityTypeId)?.Name,
                    Leaf = w.IsLeaf == true && w.IsTimeSheet == true,
                    Progress = w.Progress_prc,
                    Hours = Enumerable.Range(0, days).Select(i => new TimesheetDayDTO
                    {
                        Date = from.AddDays(i),
                        RegularHours = 0,
                        OverTimeHours = 0,
                        Approved = false,
                    }).ToList(),
                });

            foreach (TimeSheet e in entries)
            {
                if (!nodes.TryGetValue(e.WorkItemId!.Value, out TimesheetNodeDTO? node)) continue;
                int i = (int)(e.WorkDate!.Value.Date - from).TotalDays;
                if (i < 0 || i >= days) continue;

                node.Hours[i].RegularHours += e.RegularHours_dec;
                node.Hours[i].OverTimeHours += e.OverTimeHours_dec + e.WeekEndHours_dec;
                node.Hours[i].Approved = e.RegularApproved || e.OverTimeApproved || e.WeekEndApproved;
            }

            // roll the leaves up: a parent shows what its whole subtree logged
            foreach (TimesheetNodeDTO node in nodes.Values.Where(n => n.Leaf))
            {
                long? parentId = node.ParentId;
                while (parentId.HasValue && nodes.TryGetValue(parentId.Value, out TimesheetNodeDTO? parent))
                {
                    for (int i = 0; i < days; i++)
                    {
                        parent.Hours[i].RegularHours += node.Hours[i].RegularHours;
                        parent.Hours[i].OverTimeHours += node.Hours[i].OverTimeHours;
                    }
                    parentId = parent.ParentId;
                }
            }

            foreach (TimesheetNodeDTO node in nodes.Values)
            {
                node.RegularHours = node.Hours.Sum(h => h.RegularHours);
                node.TotalHours = node.Hours.Sum(h => h.RegularHours + h.OverTimeHours);
                node.HasChildren = nodes.Values.Any(n => n.ParentId == node.Id);
            }

            // the level the caller asked for
            List<TimesheetNodeDTO> level = nodes.Values
                .Where(n => req?.ParentId is null
                    ? !n.ParentId.HasValue || !nodes.ContainsKey(n.ParentId.Value)
                    : n.ParentId == req.ParentId)
                .OrderBy(n => n.Name)
                .ToList();

            return Ok(new PaginationModel<TimesheetNodeDTO>(level, 1, level.Count == 0 ? 1 : level.Count, level.Count, null));
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    /// <summary>
    /// Total the hours booked against each task into its actual effort.
    /// </summary>
    private async Task RefreshActualEffort(List<long> itemIds)
    {
        if (itemIds.Count == 0) return;

        var totals = await context.TimeSheets
            .Where(ts => ts.WorkItemId != null && itemIds.Contains(ts.WorkItemId.Value))
            .GroupBy(ts => ts.WorkItemId!.Value)
            .Select(g => new { Id = g.Key, Hours = g.Sum(x => x.TotalHours_dec) })
            .ToListAsync();

        List<WorkItem> items = await context.WorkItems.Where(w => itemIds.Contains(w.Id)).ToListAsync();
        foreach (WorkItem item in items)
            item.calc_ActualEffort_dec = totals.FirstOrDefault(x => x.Id == item.Id)?.Hours ?? 0;

        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Save a week in one call. Each entry is one task on one day; the unique index on
    /// (TenantId, WorkItemId, WorkDate, AssignedToUsrId) keeps it to one row.
    /// </summary>
    [HttpPut("SaveHours")]
    public async Task<IActionResult> SaveHours([FromBody] List<TimesheetEntryDTO> entries)
    {
        try
        {
            if (entries is null || entries.Count == 0)
                return Ok(new { msg = "OK", saved = 0 });

            long? userId = _tenantService.GetUserId();
            long? tenantId = _tenantService.GetTenantId();
            List<long> itemIds = entries.Select(e => e.WorkItemId).Distinct().ToList();

            List<WorkItem> items = await context.WorkItems.Where(w => itemIds.Contains(w.Id)).ToListAsync();

            // hours belong to leaves: a parent's number is the sum of its children
            WorkItem? notALeaf = items.FirstOrDefault(w => w.IsLeaf != true || w.IsTimeSheet != true);
            if (notALeaf is not null)
                return Ok(new { severity = "error", summary = "Validation", message = $"Hours can only be logged on \"{notALeaf.Name}\"'s lowest level" });

            int saved = 0;

            foreach (TimesheetEntryDTO entry in entries)
            {
                if (entry.RegularHours is < 0 or > 24 || entry.OverTimeHours is < 0 or > 24)
                    return Ok(new { severity = "error", summary = "Validation", message = "Hours must be between 0 and 24" });

                DateTime day = entry.WorkDate.Date;

                TimeSheet? row = await context.TimeSheets.FirstOrDefaultAsync(t =>
                    t.WorkItemId == entry.WorkItemId && t.AssignedToUsrId == userId && t.WorkDate == day);

                if (row is not null && (row.RegularApproved || row.OverTimeApproved || row.WeekEndApproved))
                    return Ok(new { severity = "error", summary = "Validation", message = "That day has been approved and cannot be changed" });

                bool weekend = day.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday;

                if (row is null)
                {
                    row = new TimeSheet
                    {
                        TenantId = tenantId,
                        AssignedToUsrId = userId,
                        WorkItemId = entry.WorkItemId,
                        WorkDate = day,
                        Description = string.Empty,
                        CreatedByUsrId = userId,
                        CreateDate = DateTime.UtcNow,
                        IsActive = true,
                    };
                    context.TimeSheets.Add(row);
                }

                row.RegularHours_dec = weekend ? 0 : entry.RegularHours;
                row.WeekEndHours_dec = weekend ? entry.RegularHours : 0;
                row.OverTimeHours_dec = entry.OverTimeHours;
                row.TotalHours_dec = row.RegularHours_dec + row.WeekEndHours_dec + row.OverTimeHours_dec;
                if (entry.Progress_prc.HasValue) row.Progress_prc = entry.Progress_prc.Value;
                row.UpdatedByUsrId = userId;
                row.UpdateDate = DateTime.UtcNow;
                row.Msg = "OK";

                // progress typed on the timesheet belongs to the task as well
                if (entry.Progress_prc.HasValue)
                {
                    WorkItem? item = items.FirstOrDefault(w => w.Id == entry.WorkItemId);
                    if (item is not null) item.Progress_prc = entry.Progress_prc.Value;
                }

                saved++;
            }

            await context.SaveChangesAsync();

            // Hours logged are the task's actual effort. calc_ActualEffort_dec exists on
            // every work item and has always been 0, while the hours sat in a table
            // nothing read back - so a plan could never be compared against what it
            // really cost. Recomputed from the timesheet rather than accumulated, so a
            // correction to an earlier week lands correctly.
            await RefreshActualEffort(itemIds);

            return Ok(new { msg = "OK", saved });
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }
}

// ------------------------------------------------------------------ contracts

public class TimesheetTreeRequest
{
    /// <summary>Defaults to the signed-in user.</summary>
    public long? UserId { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    /// <summary>null = the top of the person's tree; otherwise expand this node.</summary>
    public long? ParentId { get; set; }
}

public class TimesheetNodeDTO
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public long? ParentId { get; set; }
    public string? Type { get; set; }
    /// <summary>True when hours may be typed against this row.</summary>
    public bool Leaf { get; set; }
    public bool HasChildren { get; set; }
    public decimal Progress { get; set; }
    public decimal RegularHours { get; set; }
    public decimal TotalHours { get; set; }
    public List<TimesheetDayDTO> Hours { get; set; } = [];
}

public class TimesheetDayDTO
{
    public DateTime Date { get; set; }
    public decimal RegularHours { get; set; }
    public decimal OverTimeHours { get; set; }
    public bool Approved { get; set; }
}

public class TimesheetEntryDTO
{
    public long WorkItemId { get; set; }
    public DateTime WorkDate { get; set; }
    public decimal RegularHours { get; set; }
    public decimal OverTimeHours { get; set; }
    public decimal? Progress_prc { get; set; }
}
