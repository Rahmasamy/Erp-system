using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "HRM", Abbrv = "SI", SubList = true)]
public class EmployeeSalaryItem : XREFEntity
{
    [Attr(Parent = true)]
    public long? EmployeeId { get; set; }
    public long? SalaryItemId { get; set; }
    public decimal Earning_val { get; set; } = 0;
    public decimal Deduction_val { get; set; } = 0;
}

// Code-Player-Start-DTO //
public class EmployeeSalaryItemDTO
{
    public long? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public long? SalaryItemId { get; set; }
    public string? SalaryItemName { get; set; }
    public decimal? Earning_val { get; set; }
    public decimal? Deduction_val { get; set; }
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