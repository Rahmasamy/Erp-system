
using Aspire.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "DMS", Abbrv = "", List = true,
    FilterBy = new string[] { "DMSProjectOwnerId", "ContractorId", "AreaManagerUsrId" },
    Cols = new string[] { "ContractNo", "ProjectCode", "DMSProjectOwnerId", "ContractorId", "AreaManagerUsrId" })]
public class DMSProject : MigratedEntity
{
    public string ContractNo { get; set; } = "";
    public string ProjectCode { get; set; } = "";
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public long? ContractorId { get; set; }
    public long? DMSProjectOwnerId { get; set; }

    public DMSProject() { }

    public DMSProject(long tnantId, string name, string code, string nameAr, long prjOwnrId, long cntrId)
    {
        TenantId = tnantId;

        ProjectCode = code;
        Name = name;
        NameAr = nameAr;
        DMSProjectOwnerId = prjOwnrId;
        ContractorId = cntrId;
    }
}

// Code-Player-Start-DTO //
public class DMSProjectDTO
{
    public string? OldKey { get; set; }

    public string? ContractNo { get; set; }
    public string? ProjectCode { get; set; }
    public long? ContractorId { get; set; }
    public string? ContractorName { get; set; }
    public long? DMSProjectOwnerId { get; set; }
    public string? DMSProjectOwnerName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
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
