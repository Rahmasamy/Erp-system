using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "HRM", Abbrv = "CI", SubList = true)]
public class EmployeeCostItem : XREFEntity
{
    [Attr(Parent = true)]
    public long? EmployeeId { get; set; }

    public long? CostItemId { get; set; }
    public decimal? CostValue_val { get; set; }
}

// Code-Player-Start-DTO //
public class EmployeeCostItemDTO
{
    public long? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public long? CostItemId { get; set; }
    public string? CostItemName { get; set; }
    public decimal? CostValue_val { get; set; }
    public long? OrganizationId { get; set; }
    public string? OrganizationName { get; set; }
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