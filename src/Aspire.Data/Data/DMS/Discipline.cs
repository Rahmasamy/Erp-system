namespace Aspire.Data;

[Attr(DomainFolder = "DMS", Abbrv = "", List = true,
    FilterBy = new string[] { "DisciplineUsrId" },
    Cols = new string[] { "DisciplineUsrId" })]
public class Discipline : BaseLookupEntity
{
    public long? DisciplineUsrId { get; set; }
    public Discipline() { }
    public Discipline(long? tenantId, string name, long? usrId)
    {
        this.TenantId = tenantId;
        this.Name = name;
        this.DisciplineUsrId = usrId;
    }
}

// Code-Player-Start-DTO //
public class DisciplineDTO
{
    public long? DisciplineUsrId { get; set; }
    public string? DisciplineUsrName { get; set; }
    public string? DisciplineUsrPhotoUrl { get; set; }
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
