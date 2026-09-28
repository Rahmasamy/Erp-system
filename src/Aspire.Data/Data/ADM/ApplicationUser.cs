// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Aspire.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "ADM", Abbrv = "USR", List = true, Id = true,
    Description = "System Users", DescriptionAr = "مستخدمين النظام", NameAr = "المستخدمين",
    Actions = new string[] { "Activate:Dlg:dollar", "Deactivate:Dlg:dollar", "ChangeSeatType:Dlg:dollar" },
    Menu = new string[] { "Roles?ADM_ApplicationUserRole_ListSub" })]
public class ApplicationUser : IdentityUser<long>, IAuditableEntity
{
    public AppUserType AppUserType { get; set; }
    public string? Name { get; set; }
    public string? NameAr { get; set; }
    public long? TenantId { get; set; }
    public string? ActionComment { get; set; }
    public long? CurrencyId { get; set; }
    public long? LanguageId { get; set; }
    public string? LastAccessedTenant { get; set; } // Only the User Can Join Muliple Tenants
    public string? SeatType { get; set; } = SeatTypeEnum.User;
    public string? LangAbbrv { get; set; } = "English";
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? JobTitle { get; set; } = string.Empty;
    public string? Mobile { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
    public bool IsExternalUser { get; set; } = false;
    public string? OTP { get; set; } = string.Empty;
    public bool? IsValidProfile { get; set; } = false;

    public string? No { get; set; }
    public string? Msg { get; set; }
    public string? PhotoUrl { get; set; }

    public bool? IsVerified { get; set; } = true;
    public long? CreatedByUsrId { get; set; }

    public long? UpdatedByUsrId { get; set; }

    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }

    public ApplicationUser() : base()
    {
    }

}

// Code-Player-Start-DTO //
public class ApplicationUserDTO
{
    public AppUserType AppUserType { get; set; }

    public string? Name { get; set; }
    public string? NameAr { get; set; }
    public long? TenantId { get; set; }
    public string? TenantName { get; set; }
    public string? ActionComment { get; set; }
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
    public long? LanguageId { get; set; }
    public string? LanguageName { get; set; }
    public string? LastAccessedTenant { get; set; }
    public string? SeatType { get; set; }
    public string? LangAbbrv { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? JobTitle { get; set; }
    public string? Mobile { get; set; }
    public bool? IsActive { get; set; }
    public string? OTP { get; set; }
    public bool? IsValidProfile { get; set; }
    public string? No { get; set; }
    public string? Msg { get; set; }
    public string? PhotoUrl { get; set; }
    public bool? IsVerified { get; set; }
    public long? CreatedByUsrId { get; set; }
    public string? CreatedByUsrName { get; set; }
    public string? CreatedByUsrPhotoUrl { get; set; }
    public long? UpdatedByUsrId { get; set; }
    public string? UpdatedByUsrName { get; set; }
    public string? UpdatedByUsrPhotoUrl { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public long? Id { get; set; }
    public string? UserName { get; set; }
    public string? NormalizedUserName { get; set; }
    public string? Email { get; set; }
    public string? NormalizedEmail { get; set; }
    public bool? EmailConfirmed { get; set; }
    public string? PasswordHash { get; set; }
    public string? SecurityStamp { get; set; }
    public string? ConcurrencyStamp { get; set; }
    public string? PhoneNumber { get; set; }
    public bool? PhoneNumberConfirmed { get; set; }
    public bool? TwoFactorEnabled { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public bool? LockoutEnabled { get; set; }
    public int? AccessFailedCount { get; set; }
}
// Code-Player-End-DTO //

public class UpdateProfileDTO
{
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [Required]
    public string? Email { get; set; }
    public string? JobTitle { get; set; }
    public string? PhoneNumber { get; set; }
    public long CurrencyId { get; set; }
    public long LanguageId { get; set; }
    public long OrganizationId { get; set; }
    public long TenantId { get; set; }
}

public class CheckInviteDTO
{
    public string Token { get; set; }
}

public class AcceptInviteDTO
{
    public string Token { get; set; }
    public string Password { get; set; }
}

public class CheckInviteResponseDTO
{
    public bool IsRegisterd { get; set; }
    public string? Tenant { get; set; }
    public string? Email { get; set; }
    public string? RoleId { get; set; }
    public string? RoleName { get; set; }
}

public class RegisterDTO
{
    [Required]
    public string Name { get; set; }

    public string? JobTitle { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    [DataType("Password")]
    public string Password { get; set; }
    
    [Required]
    [Compare("Password")]
    [DataType("Password")]
    public string ConfirmPassword { get; set; }
    public bool IsSelfRegisterd { get; set; } = true;
}

public class SendEmailDTO
{
    [Required]
    public string Email { get; set; } = string.Empty;
    public bool IsExternal { get; set; } = false;
}

public class EmployeeRegisterDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType("Password")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Compare("Password")]
    [DataType("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Mobile { get; set; }
    public string? JobTitle { get; set; }
}

public class ExternalUserRegisterDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType("Password")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Compare("Password")]
    [DataType("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool IsExternalUser { get; set; } = true;
}

public class CompleteEmployeeProfileDTO
{
    public long? UserId { get; set; }
    public string? Email { get; set; }

    [Required]
    public string Mobile { get; set; } = string.Empty;

    [Required]
    public string JobTitle { get; set; } = string.Empty;

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

public class ConfirmOTPDTO
{
    public string Email { get; set; } = string.Empty;
    public string Otp { get; set; } = string.Empty;
}

public class LoginDTO
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public bool IsExternalUser { get; set; } = false;
    public bool IsSelfRegistered { get; set; } = false;
}

public class LoginResponseDTO
{
    public long? Id { get; set; }
    public long? TenantId { get; set; }
    public string? Token { get; set; }
    public string? LangAbbrv { get; set; }
    public long? CurrencyId { get; set; }
    public long? ContractorId { get; set; }
    public string? SeatType { get; set; }
    public bool IsExternalUser { get; set; } = false;
    public List<ActiveDataConfidentialityDTO>? DataClasses { get; set; }
    public List<ActiveCurrencyDTO>? Currencies { get; set; }
    public List<ActiveTaxRateDTO>? TaxRates { get; set; }
    
    public string? FullName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }
    public string? JobTitle { get; set; }
    public bool? IsValidProfile { get; set; } = false;
}

public class LoginResponseForExternalUserDTO
{
    public long? Id { get; set; }
    public long? TenantId { get; set; }
    public string? Token { get; set; }
    public string? LangAbbrv { get; set; }
    public long? CurrencyId { get; set; }
    public long? ContractorId { get; set; }
    public string? SeatType { get; set; }
    public bool IsExternalUser { get; set; } = true;
    public List<ActiveDataConfidentialityDTO>? DataClasses { get; set; }
    public List<ActiveCurrencyDTO>? Currencies { get; set; }
    public List<ActiveTaxRateDTO>? TaxRates { get; set; }

    public string? FullName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }
    public string? JobTitle { get; set; }
    public bool? IsValidProfile { get; set; } = true;
}

public class ChangePasswordDTO
{
    [Required]
    public required string Username { get; set; }
    [Required]
    public required string OldPassword { get; set; }
    [Required]
    public required string NewPassword { get; set; }
    [Required]
    public required string ConfirmNewPassword { get; set; }
}

public class ResetPasswordDTO
{
    [Required]
    public required string Username { get; set; }
    [Required]
    public required string Token { get; set; }
    [Required]
    public required string Password { get; set; }
}

public class ForgotPasswordDTO
{
    [Required]
    public required string Username { get; set; }
}

public class ExternalAuthRequestDTO
{
    public string IdToken { get; set; }
}

public class ExternalLoginConfirmationDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

public class AppleKeyDTO
{
    public string Kty { get; set; }
    public string Kid { get; set; }
    public string Use { get; set; }
    public string Alg { get; set; }
    public string N { get; set; }
    public string E { get; set; }
}

public class AppleKeySetDTO
{
    public AppleKeyDTO[] Keys { get; set; }
}