using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Aspire.Data;
public class Tenant : DatabaseEntity
{
    public string? Name { get; set; }
    public string? NameAr { get; set; }
    public string? Domain { get; set; }
    public string? Indusrty { get; set; }
    public string? Role { get; set; }
    public string? EmployeesCount { get; set; }
    public DateTime? SubscriptionStartDate { get; set; }
    public DateTime? SubscriptionEndDate { get; set; }

}

public class TenantDTO
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? NameAr { get; set; }
    public string? Domain { get; set; }
    public string? ContactEmail { get; set; }
    public string? Indusrty { get; set; }
    public string? Role { get; set; }
    public string? EmployeesCount { get; set; }
    public DateTime? SubscriptionStartDate { get; set; }
    public DateTime? SubscriptionEndDate { get; set; }
    public bool? IsActive { get; set; }
    public List<ApplicationRoleDTO> Roles { get; set; }
    public bool IsSuperAdmin { get; set; }

    public TenantDTO()
    {
    }

    public TenantDTO(Tenant tenant)
    {
        Id = tenant.Id;
        Domain = tenant.Domain;
        Indusrty = tenant.Indusrty;
        Role = tenant.Role;
        EmployeesCount = tenant.EmployeesCount;
        SubscriptionStartDate = tenant.SubscriptionStartDate;
        SubscriptionEndDate = tenant.SubscriptionEndDate;
        IsActive = tenant.IsActive;
    }
}

public class CreateAndUpdateTenantDTO
{
    [Required(ErrorMessage = "Company name is required")]
    [MaxLength(100, ErrorMessage = "Company name cannot exceed 100 characters")]
    public required string CompanyName { get; set; }
    public required string Indusrty { get; set; }
    public required string Role { get; set; }
    public required string EmployeesCount { get; set; }
    public required string Domain { get; set; }
    public bool IsActive { get; set; }
}

public class SwitchTenantDTO
{
    public string TenantId { get; set; }
}

public class SwitchTenantResponseDTO
{
    public string Token { get; set; }
    public ApplicationRoleDTO Role { get; set; }
}

public class TenantIdJwtDTO
{
    public string TenantId { get; set; }
}