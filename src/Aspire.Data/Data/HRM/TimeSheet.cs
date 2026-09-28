using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "HRM", Abbrv = "TS", Trans = true)]
public class TimeSheet : BaseEntity
{
    public long? AssignedToUsrId { get; set; }

    public bool IsPending { get; set; } = true;

    public long? WorkItemId { get; set; }
    public DateTime? WorkDate { get; set; }
    public decimal Progress_prc { get; set; } = 0;
    public decimal RegularHours_dec { get; set; } = 0;
    public decimal OverTimeHours_dec { get; set; } = 0;
    public decimal WeekEndHours_dec { get; set; } = 0;
    public decimal TotalHours_dec { get; set; } = 0;

    public bool RegularApproved { get; set; } = false;
    public bool OverTimeApproved { get; set; } = false;
    public bool WeekEndApproved { get; set; } = false;

    public decimal calc_RegularCost_val { get; set; } = 0;
    public decimal calc_OverTimeCost_val { get; set; } = 0;
    public decimal calc_WeekEndCost_val { get; set; } = 0;
    public decimal calc_TotalCost_val { get; set; } = 0;

    public TimeSheet() { }

    public TimeSheet(long tnantId, long? workItemId, long? usrId, DateTime dt)
    {
        Random rnd = new Random();

        TenantId = tnantId;
        
        this.WorkItemId = workItemId;
        AssignedToUsrId = usrId;
        WorkDate = dt;

        RegularHours_dec = rnd.Next(1, 8);
        RegularApproved = true;

        if (dt.DayOfWeek == DayOfWeek.Friday || dt.DayOfWeek == DayOfWeek.Saturday)
        {
            WeekEndHours_dec = rnd.Next(1, 8);
            WeekEndApproved = true;
        }
        else
        {
            OverTimeHours_dec = rnd.Next(1, 8);
            OverTimeApproved = true;
        }

    }
}


// Code-Player-Start-DTO //
public class TimeSheetDTO
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public long? WorkItemId { get; set; }
    public DateTime? WorkDate { get; set; }
    public decimal Progress_prc { get; set; } = 0;
    public decimal RegularHours_dec { get; set; } = 0;
    public decimal OverTimeHours_dec { get; set; } = 0;
    public decimal WeekEndHours_dec { get; set; } = 0;
    public decimal TotalHours_dec { get; set; } = 0;
}
// Code-Player-End-DTO //
