namespace Aspire.Data;

[Attr(DomainFolder = "APP", Abbrv = "", Lookup = true)]
public class ServicePriority : CoreEntity
{
    public string? Color { get; set; }

    public ServicePriority(long? tenantId, string name, string nameAr, string color)
    {
        TenantId = tenantId;
        Name = name;
        Color = color;
        NameAr = nameAr;
        this.Color = "success";
    }
}

// Code-Player-Start-DTO //
public class ServicePriorityDTO
{
    public string? Color { get; set; }
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

