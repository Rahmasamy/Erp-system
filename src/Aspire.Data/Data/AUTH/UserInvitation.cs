using System.Text.Json.Serialization;


namespace Aspire.Data;
public class UserInvitation : DatabaseEntity
{
    public long UserId { get; set; }

    [JsonIgnore]
    public virtual ApplicationUser User { get; set; }

    [JsonIgnore]
    public virtual Tenant Tenant { get; set; }
    public long RoleId { get; set; }

    [JsonIgnore]
    public virtual ApplicationRole Role { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public DateTime? TokenExpirationDate { get; set; }
    public string? PhoneNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PreferredLanguage { get; set; }
    public string? PreferredCurrency { get; set; }
    public string? JobTitle { get; set; }

    public UserInvitation()
    {
    }
}

public class UserInvitationDTO
{
    public string? UserId { get; set; }
    public string? TenantId { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PreferredLanguage { get; set; }
    public string? PreferredCurrency { get; set; }
    public string? JobTitle { get; set; }
    public string? RoleId { get; set; }
    public string? CreatedByUsrId { get; set; }
}
