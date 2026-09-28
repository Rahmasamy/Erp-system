namespace Aspire.Data;

[Attr(DomainFolder = "APP", Abbrv = "", WithoutUI = true)]
public class WorkShiftHoliday : XREFEntity
{
    public long? WorkShiftId { get; set; }
    public long? HolidayId { get; set; }
}

// Code-Player-Start-DTO //
// Code-Player-End-DTO //