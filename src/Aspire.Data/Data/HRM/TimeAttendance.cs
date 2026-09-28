using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "HRM", Abbrv = "TA", Trans = true)]
public class TimeAttendance : BaseEntity
{
    public DateTime? AttendanceDate { get; set; }
    public string? CheckInOut { get; set; }
    public double? CheckInOutMinutes { get; set; }

    public long? AssignedToId { get; set; }

    [NotMapped]
    public virtual ApplicationUser? AssignedTo { get; set; }


    public TimeAttendance() { }
    public TimeAttendance(long? tenantId, long? userId)
    {
        this.TenantId = tenantId;
        this.AssignedToId = userId;
        this.AttendanceDate = DateTime.Now;

        string[] archeckInOut = DateTime.Now.TimeOfDay.ToString().Split(':');
        this.CheckInOut = archeckInOut[0] + ":" + archeckInOut[1];        
        this.CheckInOutMinutes = DateTime.Now.TimeOfDay.TotalMinutes;
    }
}

public class TimeAttendanceDTO
{
    public DateTime? AttendanceDate { get; set; }
    public string? CheckIn { get; set; }
    public string? CheckOut { get; set; }
    public string? Hours { get; set; }
}


// Code-Player-Start-DTO //
// Code-Player-End-DTO //
