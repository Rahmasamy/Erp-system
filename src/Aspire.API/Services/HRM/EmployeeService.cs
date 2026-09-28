using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Aspire.Services;

public class EmployeeService()
{
    public static async Task<Employee> Terminate(
    Employee entity,
    ApplicationDbContext context,
    ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        if (entity.Id <= 0)
        {
            entity.Msg = "Employee ID is required to terminate the employee.";
            return entity;
        }

        if (string.IsNullOrWhiteSpace(entity.ActionComment))
        {
            entity.Msg = "Termination reason is required.";
            return entity;
        }

        Employee? obj = await context.Employees.FirstOrDefaultAsync(e => e.Id == entity.Id);
        if (obj is null)
        {
            entity.Msg = $"Employee with ID ({entity.Id}) was not found.";
            return entity;
        }

        long? terminatedStatusId = await context.EntityStatuses
            .Where(e => e.EntityName == "Employee" && e.Name == "Terminated")
            .Select(e => (long?)e.Id)
            .FirstOrDefaultAsync();
        
        if (!terminatedStatusId.HasValue)
        {
            entity.Msg = "The Terminated employee status is not configured.";
            return entity;
        }
        
        // .Value is safe here because HasValue was checked above, ensuring the nullable ID contains a value.
        if (obj.EntityStatusId == terminatedStatusId.Value)
        {
            entity.Msg = "This employee has already been terminated.";
            return entity;
        }
        
        var now = DateTime.UtcNow;
        var currentUserId = _tenantService.GetUserId();

        obj.EntityStatusId = terminatedStatusId.Value;
        obj.IsActive = false;
        obj.ExitDate = now;
        obj.UpdatedByUsrId = currentUserId;
        obj.UpdateDate = now;

        await context.SaveChangesAsync();

        await Utilities.DBLog.Action(obj.Id,
            "Employee",
            "Terminate",
            entity.ActionComment.Trim(),
            context,
            _tenantService);

        // Keep Msg as the controller success flag; the frontend owns the confirmation text.
        obj.Msg = "OK";

        return obj;
    }

    public static async Task<Employee> Change_Department(Employee entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        if (entity.Id <= 0)
        {
            entity.Msg = "Employee ID is required to change the department.";
            return entity;
        }

        if (!entity.DepartmentId.HasValue || entity.DepartmentId.Value <= 0)
        {
            entity.Msg = "A new Department is required.";
            return entity;
        }

        if (string.IsNullOrWhiteSpace(entity.ActionComment))
        {
            entity.Msg = "Change department comment is required.";
            return entity;
        }

        Employee? obj = await context.Employees
            .FirstOrDefaultAsync(e => e.Id == entity.Id);

        if (obj is null)
        {
            entity.Msg = $"Employee with ID ({entity.Id}) was not found.";
            return entity;
        }

        long? tenantId = _tenantService.GetTenantId();
        Department? newDepartment = await context.Departments.FirstOrDefaultAsync(department =>
            department.Id == entity.DepartmentId.Value &&
            department.TenantId == tenantId);

        if (newDepartment is null)
        {
            entity.Msg = "The selected Department was not found.";
            return entity;
        }

        if (newDepartment.IsActive != true)
        {
            entity.Msg = "The selected Department is not active.";
            return entity;
        }

        if (obj.DepartmentId == newDepartment.Id)
        {
            entity.Msg = "The selected Department is already assigned to this employee.";
            return entity;
        }

        long? oldDepartmentId = obj.DepartmentId;
        obj.DepartmentId = newDepartment.Id;
        obj.UpdatedByUsrId = _tenantService.GetUserId();
        obj.UpdateDate = DateTime.UtcNow;

        await context.SaveChangesAsync();

        string oldDepartment = oldDepartmentId?.ToString() ?? "None";
        await Utilities.DBLog.Action(obj.Id,
            "Employee",
            "Change_Department",
            $"Old Department: {oldDepartment}; New Department: {newDepartment.Name}; Reason: {entity.ActionComment.Trim()}",
            context,
            _tenantService);

        obj.Msg = "OK";
        return obj;
    }


    public static async Task<Employee> Change_Job(Employee entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        if (entity.Id <= 0)
        {
            entity.Msg = "Employee ID is required to change the job.";
            return entity;
        }

        if (!entity.JobId.HasValue || entity.JobId.Value <= 0)
        {
            entity.Msg = "A new Job is required.";
            return entity;
        }

        if (string.IsNullOrWhiteSpace(entity.ActionComment))
        {
            entity.Msg = "Change job comment is required.";
            return entity;
        }
        Employee? obj = await context.Employees.FirstOrDefaultAsync(e => e.Id == entity.Id);
        if (obj is null)
        {
            entity.Msg = $"Employee with ID ({entity.Id}) was not found.";
            return entity;
        }

        long? tenantId = _tenantService.GetTenantId();
        Job? newJob = await context.Jobs.FirstOrDefaultAsync(job =>
            job.Id == entity.JobId.Value &&
            job.TenantId == tenantId);

        if (newJob is null)
        {
            entity.Msg = "The selected Job was not found.";
            return entity;
        }
        if (newJob.IsActive != true)
        {
            entity.Msg = "The selected Job is not active.";
            return entity;
        }
        if (obj.JobId == newJob.Id)
        {
            entity.Msg = "The selected Job is already assigned to this employee.";
            return entity;
        }

        long? oldJobId = obj.JobId;
        obj.JobId = newJob.Id;
        obj.UpdatedByUsrId = _tenantService.GetUserId();
        obj.UpdateDate = DateTime.UtcNow;
        obj.Msg = "OK";

        await context.SaveChangesAsync();

        string oldJob = oldJobId?.ToString() ?? "None";
        await Utilities.DBLog.Action(obj.Id,
            "Employee",
            "Change_Job",
            $"Old Job: {oldJob}; New Job: {newJob.Name}; Reason: {entity.ActionComment.Trim()}",
            context,
            _tenantService);

        obj.Msg = "OK";
        return obj;
    }


    public static async Task<Employee> Change_Salary(Employee entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        if (entity.Id <= 0)
        {
            entity.Msg = "Employee ID is required to change the salary.";
            return entity;
        }

        if (entity.Basic_val < 0)
        {
            entity.Msg = "Basic salary cannot be negative.";
            return entity;
        }

        if (string.IsNullOrWhiteSpace(entity.ActionComment))
        {
            entity.Msg = "Salary change comment is required.";
            return entity;
        }

        Employee? obj = await context.Employees
            .FirstOrDefaultAsync(e => e.Id == entity.Id);
        if (obj is null)
        {
            entity.Msg = $"Employee with ID ({entity.Id}) was not found.";
            return entity;
        }
        obj.Basic_val = entity.Basic_val;
        obj.ActionComment = entity.ActionComment.Trim();
        obj.UpdatedByUsrId = _tenantService.GetUserId();
        obj.UpdateDate = DateTime.UtcNow;

        await context.SaveChangesAsync();

        await Utilities.DBLog.Action(obj.Id,
            "Employee",
            "Change_Salary",
            obj.ActionComment,
            context,
            _tenantService);

        obj.Msg = "OK";
        return obj;
    }




// Code-Player-Start-DTO //
    public static async Task<Employee> Create(Employee entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        // Perform the Business Logic BEFOR CREATING the WorkItem Object in the Database
        entity.TenantId = _tenantService.GetTenantId();
        
        entity.CreatedByUsrId = _tenantService.GetUserId();
        entity.CreateDate = DateTime.UtcNow;
        entity.Msg = "OK";
        context.Employees.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public static async Task<Employee> Update(Employee entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        // Update the "Msg" if there is any Validation Error Should Stop the UPDATE Action

        if (entity.Msg != "OK")
            return entity;

        Employee? obj = context.Employees.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";

        // Perform the Business Logic BEFOR UPDATING the ApplicationUser Object in the Database


        // Populate the Object Will be Saved
        obj.FNameEn = entity.FNameEn is null ? obj.FNameEn : entity.FNameEn;
        obj.FNameAr = entity.FNameAr is null ? obj.FNameAr : entity.FNameAr;
        obj.MNameEn = entity.MNameEn is null ? obj.MNameEn : entity.MNameEn;
        obj.MNameAr = entity.MNameAr is null ? obj.MNameAr : entity.MNameAr;
        obj.LNameEn = entity.LNameEn is null ? obj.LNameEn : entity.LNameEn;
        obj.LNameAr = entity.LNameAr is null ? obj.LNameAr : entity.LNameAr;
        obj.BirthDate = entity.BirthDate is null ? obj.BirthDate : entity.BirthDate;
        obj.PersonalEmail = entity.PersonalEmail is null ? obj.PersonalEmail : entity.PersonalEmail;
        obj.Mobile = entity.Mobile is null ? obj.Mobile : entity.Mobile;
        obj.NationalityId = entity.NationalityId is null ? obj.NationalityId : entity.NationalityId;
        obj.MaritalId = entity.MaritalId is null ? obj.MaritalId : entity.MaritalId;
        obj.GenderId = entity.GenderId is null ? obj.GenderId : entity.GenderId;
        obj.BloodGroupId = entity.BloodGroupId is null ? obj.BloodGroupId : entity.BloodGroupId;
        obj.CountryId = entity.CountryId is null ? obj.CountryId : entity.CountryId;
        obj.City = entity.City is null ? obj.City : entity.City;
        obj.State = entity.State is null ? obj.State : entity.State;
        obj.Street = entity.Street is null ? obj.Street : entity.Street;
        obj.ZipCode = entity.ZipCode is null ? obj.ZipCode : entity.ZipCode;
        obj.AppUsrId = entity.AppUsrId is null ? obj.AppUsrId : entity.AppUsrId;
        obj.WorkEmail = entity.WorkEmail is null ? obj.WorkEmail : entity.WorkEmail;
        obj.DirectManagerUsrId = entity.DirectManagerUsrId is null ? obj.DirectManagerUsrId : entity.DirectManagerUsrId;
        obj.StartDate = entity.StartDate is null ? obj.StartDate : entity.StartDate;
        obj.EndDate = entity.EndDate is null ? obj.EndDate : entity.EndDate;
        obj.JoinDate = entity.JoinDate is null ? obj.JoinDate : entity.JoinDate;
        obj.ExitDate = entity.ExitDate is null ? obj.ExitDate : entity.ExitDate;
        obj.WorkShiftId = entity.WorkShiftId is null ? obj.WorkShiftId : entity.WorkShiftId;
        obj.GradeId = entity.GradeId is null ? obj.GradeId : entity.GradeId;
        obj.JobId = entity.JobId is null ? obj.JobId : entity.JobId;
        obj.BranchId = entity.BranchId is null ? obj.BranchId : entity.BranchId;
        obj.LocationId = entity.LocationId is null ? obj.LocationId : entity.LocationId;
        obj.DepartmentId = entity.DepartmentId is null ? obj.DepartmentId : entity.DepartmentId;
        obj.MedicalId = entity.MedicalId is null ? obj.MedicalId : entity.MedicalId;
        obj.ResumeSourceId = entity.ResumeSourceId is null ? obj.ResumeSourceId : entity.ResumeSourceId;
        obj.CurrencyId = entity.CurrencyId is null ? obj.CurrencyId : entity.CurrencyId;
        obj.Basic_val = entity.Basic_val > 0 ? entity.Basic_val : obj.Basic_val;
        obj.GrossPay_val = entity.GrossPay_val > 0 ? entity.GrossPay_val : obj.GrossPay_val;
        obj.NetPay_val = entity.NetPay_val > 0 ? entity.NetPay_val : obj.NetPay_val;
        obj.TotalCost_val = entity.TotalCost_val > 0 ? entity.TotalCost_val : obj.TotalCost_val;
        obj.EntityStatusId = entity.EntityStatusId is null ? obj.EntityStatusId : entity.EntityStatusId;
        obj.EntityTypeId = entity.EntityTypeId is null ? obj.EntityTypeId : entity.EntityTypeId;
        obj.DataConfidentialityId = entity.DataConfidentialityId is null ? obj.DataConfidentialityId : entity.DataConfidentialityId;
        obj.Name = entity.Name is null ? obj.Name : entity.Name;
        obj.NameAr = entity.NameAr is null ? obj.NameAr : entity.NameAr;
        obj.Description = entity.Description is null ? obj.Description : entity.Description;
        obj.DescriptionAr = entity.DescriptionAr is null ? obj.DescriptionAr : entity.DescriptionAr;
        obj.IsActive = entity.IsActive is null ? obj.IsActive : entity.IsActive;
        obj.ActionComment = entity.ActionComment is null ? obj.ActionComment : entity.ActionComment;
        obj.PhotoUrl = entity.PhotoUrl is null ? obj.PhotoUrl : entity.PhotoUrl;

        obj.Msg = "OK";
        obj.UpdatedByUsrId = _tenantService.GetUserId();
        obj.UpdateDate = DateTime.UtcNow;
        await context.SaveChangesAsync();
        return obj;
    }

    public static string OnDelete(long Id, ApplicationDbContext context)
    {
        string msg = "OK";
        // Perform the Check Before DELETE
        // Update the "Msg" if there is any Validation Error Should Stop the DELETE Action


        return msg;
    }
// Code-Player-End-DTO //
}
