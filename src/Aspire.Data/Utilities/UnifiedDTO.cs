using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspire.Utilities;
public class UnifiedListDTO
{
    public string? Id { get; set; }
    public string? Value { get; set; } // Will be retrieved with Agreements  
}

public class UnifiedDTO
{
    public string? Id { get; set; }
    public string? Value { get; set; }
    public long? CustomerId { get; set; } // Will be retrieved with Agreements  
    public string? CustomerName { get; set; } // Will be retrieved with Agreements
    public string? Color { get; set; } // Will be retrieved with EntityStatuses
    public string? PhotoUrl { get; set; } // Will be retrieved with AspNetUsers
}
public class UnifiedStatusDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Color { get; set; }
}
public class UnifiedRoleDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public long? UserId { get; set; }
}

public class UnifiedUserDTO
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; } //For Users, Contacts
    public string? JobTitle { get; set; } //For Contacts
    public string? PhoneNumber { get; set; } //For Users
    public string? Mobile { get; set; } //For Contacts
}
public class UnifiedUserPhotoDTO
{
    public string? Id { get; set; }
    public string Type { get; set; }
    public string DownloadLink { get; set; }
}
public class FinalizeContactPhotoDTO
{
    public string? Id { get; set; }
    public string Email { get; set; }
    public long TenantId { get; set; }
}
