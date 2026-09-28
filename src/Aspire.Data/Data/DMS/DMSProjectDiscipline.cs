using Aspire.Enums;
using System;

namespace Aspire.Data;

[Attr(DomainFolder = "DMS", Abbrv = "", WithoutUI = true)]

public class DMSProjectDiscipline : XREFEntity
{
    public long? DMSProjectId { get; set; }
    public long? DisciplineId { get; set; }
    public long? DisciplineUsrId { get; set; }

    public DMSProjectDiscipline() { }
    public DMSProjectDiscipline(long? tenantId, long? prjId, long? dspId, long? usrId) 
    {
        this.TenantId = tenantId;
        this.DMSProjectId = prjId;
        this.DisciplineId = dspId;
        this.DisciplineUsrId = usrId;
    }
}

// Code-Player-Start-DTO //
public class DMSProjectDisciplineDTO
{
    public long? DMSProjectId { get; set; }
    public string? DMSProjectName { get; set; }
    public long? DisciplineId { get; set; }
    public string? DisciplineName { get; set; }
    public long? DisciplineUsrId { get; set; }
    public string? DisciplineUsrName { get; set; }
    public string? DisciplineUsrPhotoUrl { get; set; }
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
