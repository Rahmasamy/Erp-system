using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using static Duende.IdentityServer.Telemetry.Metrics;
using static System.Formats.Asn1.AsnWriter;

namespace Aspire.Data;

[Attr(DomainFolder = "ORG", Abbrv = "DEP", List = true, SubList = true, Card = true, Tree = true,
    GroupBy = new string[] { "ParentId" },
    FilterBy = new string[] { "ParentId" },
    Cols = new string[] { "ManagerUsrId", "EntityTypeId", "ParentId" },
    DisplayValues = new string[] { "ManagerUsrId", "ParentId" }
    )]
public class Department : BaseEntity
{
    public long? ManagerUsrId { get; set; }

    public long? ParentId { get; set; }

    [NotMapped]
    public virtual Department? Parent { get; set; }

    // Cost Center is a department that only incurs costs, like HR or FIN, and is responsible for minimizing expenses within a budget.
    public long? CostCenterId { get; set; } // Either Cost Center or Profit Center (One of Them)


    // Profit Center is a business unit, such as a specific store or product line, that is accountable for generating both revenue and expenses, with the goal of producing a profit.
    // The primary difference is that profit centers generate their own revenue, while cost centers do not
    public long? ProfitCenterId { get; set; } // Either Cost Center or Profit Center (One of Them)


    public Department() { }
    public Department(long tnantId, long? usrId, long? tyId, long? parentId, string name, string nameAr)
    {
        this.TenantId = tnantId;
        this.ManagerUsrId = usrId;
        this.EntityTypeId = tyId;
        this.Name = name;
        this.ParentId = parentId;
        NameAr = nameAr;
    }
}

// Code-Player-Start-DTO //
public class DepartmentDTO
{
    public long? ManagerUsrId { get; set; }
    public string? ManagerUsrName { get; set; }
    public string? ManagerUsrPhotoUrl { get; set; }
    public long? ParentId { get; set; }
    public string? ParentName { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; }
    public long? ProfitCenterId { get; set; }
    public string? ProfitCenterName { get; set; }
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