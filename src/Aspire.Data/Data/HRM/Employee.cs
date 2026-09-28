
using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace Aspire.Data;

[Attr(DomainFolder = "HRM", Abbrv = "E", ShowAvatar = true, ShowProgress = true, ShowStakeholders = true, List = true, Card = true, Tree = true,
    Actions = new string[] { "Terminate:Dlg:lock", "Change_Department:Dlg:dollar", "Change_Job:Dlg:dollar", "Change_Salary:Dlg:dollar" },
    Cols = new string[] { "JobId", "DepartmentId", "CountryId", "Mobile", "NetPay_val", "StartDate", "EndDate" },
    GroupBy = new string[] { "GradeId", "BranchId", "JobId", "DepartmentId", "CountryId" },
    FilterBy = new string[] { "GradeId", "BranchId", "JobId", "DepartmentId", "CountryId" },
    DisplayValues = new string[] { "JobId", "DepartmentId", "NetPay_val", "StartDate", "EndDate" },
    SummaryValues = new string[] { "Basic_val", "NetPay_val", "TotalCost_val" },
    Menu = new string[] {
        "CostItems?HRM_EmployeeCostItem_ListSub",
        "SalaryItems?HRM_EmployeeSalaryItem_ListSub",
        "Payroll?HRM_Payroll_ListSub",
        "Dependants?HRM_EmployeeDependant_ListSub",
        "Qualifications?HRM_EmployeeQualification_ListSub"
    })]
public class Employee : BaseEntity
{
    [Attr(Section = "Personal Information")]
    public string? Code { get; set; }
    public bool? IsController { get; set; }
    public bool? IsValidProfile { get; set; }
    public string? FNameEn { get; set; }
    public string? FNameAr { get; set; }
    public string? MNameEn { get; set; }
    public string? MNameAr { get; set; }
    public string? LNameEn { get; set; }
    public string? LNameAr { get; set; }
    public DateTime? BirthDate { get; set; } = null;
    public string? PersonalEmail { get; set; }
    public string? Mobile { get; set; }
    public long? NationalityId { get; set; }
    public long? MaritalId { get; set; }
    public long? GenderId { get; set; }
    public long? BloodGroupId { get; set; }

    
    [Attr(Section = "Address")]
    public long? CountryId { get; set; } // Address
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Street { get; set; }
    public string? ZipCode { get; set; }


    [Attr(Section = "Business Information")]
    public long? AppUsrId { get; set; }
    public string? WorkEmail { get; set; } // This will be used to link the EMP to USER
    public long? DirectManagerUsrId { get; set; }
    public DateTime? StartDate { get; set; } // Contract
    public DateTime? EndDate { get; set; } // Contract
    public DateTime? JoinDate { get; set; } = null;
    public DateTime? ExitDate { get; set; } = null;
    public long? WorkShiftId { get; set; } 
    public long? GradeId { get; set; }
    public long? JobId { get; set; }
    public long? BranchId { get; set; }
    public long? LocationId { get; set; }
    public long? DepartmentId { get; set; }
    public long? MedicalId { get; set; }
    public long? ResumeSourceId { get; set; }


    [Attr(Section = "Financials")]
    public long? CurrencyId { get; set; }
    public decimal Basic_val { get; set; } = 0;
    public decimal GrossPay_val { get; set; } = 0; // Gross Salary
    public decimal TotalCost_val { get; set; } = 0; // Net Cost
    public decimal NetPay_val { get; set; } = 0;// Net Salary After Taxs
    public decimal WorkingHours_dec { get; set; } = 8;
    public decimal WorkingHoursRamadan_dec { get; set; } = 6;

    public Employee() { }
    public Employee(long tnantId, ApplicationUser usr, string name, long? jobId, string code, string eml, string mbl, bool isController)
    {
        this.TenantId = tnantId;
        this.AppUsrId = usr.Id;
        this.Name = name;
        this.JobId = jobId;
        this.Code = code;
        this.WorkEmail = eml;
        this.Mobile = mbl;
        this.IsController = isController;
    }

}

// Code-Player-Start-DTO //
public class EmployeeTeamDTO : EmployeeDTO
{
    public List<TeamMemberAvatarDTO> Stakeholders { get; set; } = new List<TeamMemberAvatarDTO>();
}

public class EmployeeDTO
{
    public string? FNameEn { get; set; }
    public string? FNameAr { get; set; }
    public string? MNameEn { get; set; }
    public string? MNameAr { get; set; }
    public string? LNameEn { get; set; }
    public string? LNameAr { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? PersonalEmail { get; set; }
    public string? Mobile { get; set; }
    public long? NationalityId { get; set; }
    public string? NationalityName { get; set; }
    public long? MaritalId { get; set; }
    public string? MaritalName { get; set; }
    public long? GenderId { get; set; }
    public string? GenderName { get; set; }
    public long? BloodGroupId { get; set; }
    public string? BloodGroupName { get; set; }
    public long? CountryId { get; set; }
    public string? CountryName { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Street { get; set; }
    public string? ZipCode { get; set; }
    public long? AppUsrId { get; set; }
    public string? AppUsrName { get; set; }
    public string? AppUsrPhotoUrl { get; set; }
    public string? WorkEmail { get; set; }
    public long? DirectManagerUsrId { get; set; }
    public string? DirectManagerUsrName { get; set; }
    public string? DirectManagerUsrPhotoUrl { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? JoinDate { get; set; }
    public DateTime? ExitDate { get; set; }
    public long? WorkShiftId { get; set; }
    public string? WorkShiftName { get; set; }
    public long? GradeId { get; set; }
    public string? GradeName { get; set; }
    public long? JobId { get; set; }
    public string? JobName { get; set; }
    public long? BranchId { get; set; }
    public string? BranchName { get; set; }
    public long? LocationId { get; set; }
    public string? LocationName { get; set; }
    public long? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public long? MedicalId { get; set; }
    public string? MedicalName { get; set; }
    public long? ResumeSourceId { get; set; }
    public string? ResumeSourceName { get; set; }
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
    public decimal? Basic_val { get; set; }
    public decimal? GrossPay_val { get; set; }
    public decimal? TotalCost_val { get; set; }
    public decimal? NetPay_val { get; set; }
    public decimal? WorkingHours_dec { get; set; }
    public decimal? WorkingHoursRamadan_dec { get; set; }
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