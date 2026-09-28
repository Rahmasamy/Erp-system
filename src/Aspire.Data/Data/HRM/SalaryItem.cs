
namespace Aspire.Data;

[Attr(DomainFolder = "HRM", Abbrv = "SI", Lookup = true,
    PreventDelete = new string[] { "EmployeeSalaryItem" }
    )]
public class SalaryItem : BaseEntity
{
    public bool IsPermanent { get; set; } = true;

    public decimal SI_val { get; set; } = 0;
    public decimal SI_prc { get; set; } = 0; // % From 

    public SalaryItem() { }
    public SalaryItem(bool IsPr, long? tenantId, string name, string nameAr, decimal value, decimal percent, long? entityTypeId)
    {
        IsPermanent = IsPr;
        TenantId = tenantId;
        Name = name;
        NameAr = nameAr;
        SI_val = value;
        SI_prc = Convert.ToDecimal(percent/100);
        EntityTypeId = entityTypeId;
    }
}

// Code-Player-Start-DTO //
public class SalaryItemDTO
{
    public bool? IsPermanent { get; set; }
    public decimal? SI_val { get; set; }
    public decimal? SI_prc { get; set; }
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