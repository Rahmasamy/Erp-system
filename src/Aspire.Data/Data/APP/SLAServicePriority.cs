namespace Aspire.Data;

[Attr(DomainFolder = "APP", Abbrv = "", WithoutUI = true)]
public class SLAServicePriority : XREFEntity
{
    public long? ServicePriorityId { get; set; }
    public long? HolidayId { get; set; }

    public int ResponseDys_num { get; set; } = 0;
    public int ResponseHrs_num { get; set; } = 0;
    public int ResponseMns_num { get; set; } = 0;

    public int ResolutionDys_num { get; set; } = 0;
    public int ResolutionHrs_num { get; set; } = 0;
    public int ResolutionMns_num { get; set; } = 0;
}

// Code-Player-Start-DTO //
public class SLAServicePriorityDTO
{
    public long? ServicePriorityId { get; set; }
    public string? ServicePriorityName { get; set; }
    public long? HolidayId { get; set; }
    public string? HolidayName { get; set; }
    public int? ResponseDys_num { get; set; }
    public int? ResponseHrs_num { get; set; }
    public int? ResponseMns_num { get; set; }
    public int? ResolutionDys_num { get; set; }
    public int? ResolutionHrs_num { get; set; }
    public int? ResolutionMns_num { get; set; }
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