
namespace Aspire.Data;

[Attr(DomainFolder = "HRM", Abbrv = "CI", Lookup = true,
    PreventDelete = new string[] { "EmployeeCostItem" } )]
public class CostItem : BaseEntity
{
    public decimal CI_val { get; set; } = 0;
    public decimal CI_prc { get; set; } = 0;

    public CostItem() { }
    public CostItem(long? tenantId, string name, string nameAr, decimal value, decimal percent, long? entityTypeId)
    {
        TenantId = tenantId;
        Name = name;
        NameAr = nameAr;
        CI_val = value;
        CI_prc = Convert.ToDecimal(percent/100);
        EntityTypeId = entityTypeId;
    }
}

// Code-Player-Start-DTO //
public class CostItemDTO
{
    public decimal? CI_val { get; set; }
    public decimal? CI_prc { get; set; }
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