
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "DMS", Abbrv = "", List = true,
    FilterBy = new string[] { "ContractorId", "DMSProjectId", "DocumentTypeId" },
    Cols = new string[] { "ContractorId", "DMSProjectId", "DocumentTypeId" } )]
public class Document : MigratedEntity
{
    public string? Code { get; set; } // Code {Prefix-Code}
    public string? Prefix { get; set; }
    public long? ContractorId { get; set; }
    public long? DMSProjectId { get; set; }
    public long? DocumentTypeId { get; set; }
    public decimal? Version { get; set; }
    public long? OldWorkflowId { get; set; }
    public string? StatusDataMining { get; set; }
    public string? WorkflowDataMining { get; set; }
    public string? CurrentStatus { get; set; }
    public decimal? CurrentVersion { get; set; }
    public string? CurrentVersionFile { get; set; }
    public string? CurrentOldWorkflow { get; set; }
    public string? Metadata { get; set; }
    public bool? IsArchived { get; set; }
    public bool? IsUploadOnly { get; set; }
    public bool? IsDeleted { get; set; }
    public string? Dates { get; set; }
    public DateTime? DeleteDate { get; set; }
    public long? DeletedByUsrId { get; set; }
    public DateTime? PermanentDeleteEligibleDate { get; set; }


    public Document() { }

    public Document(long tnantId, string name, string nameAr)
    {
        TenantId = tnantId;
        
        Name = name;
        NameAr = nameAr;
    }
}

// Code-Player-Start-DTO //
public class DocumentDTO
{
    public string? OldKey { get; set; }

    public string? Code { get; set; } // Code {Prefix-Code}
    public string? Prefix { get; set; }
    public decimal? Version { get; set; }
    public long? ContractorId { get; set; }
    public string? ContractorName { get; set; }
    public long? DMSProjectId { get; set; }
    public string? DMSProjectName { get; set; }
    public long? DocumentTypeId { get; set; }
    public string? DocumentTypeName { get; set; }
    public long? EntityStatusId { get; set; }
    public string? EntityStatusName { get; set; }
    public string? EntityStatusColor { get; set; }
    public long? EntityTypeId { get; set; }
    public string? EntityTypeName { get; set; }
    public string? EntityTypeColor { get; set; }
    public long? DataConfidentialityId { get; set; }
    public string? DataConfidentialityName { get; set; }
    public long? OldWorkflowId { get; set; }
    public string? StatusDataMining { get; set; }
    public string? WorkflowDataMining { get; set; }
    public string? CurrentStatus { get; set; }
    public decimal? CurrentVersion { get; set; }
    public string? CurrentVersionFile { get; set; }
    public string? CurrentOldWorkflow { get; set; }
    public string? Metadata { get; set; }
    public bool? IsArchived { get; set; }
    public bool? IsUploadOnly { get; set; }
    public bool? IsDeleted { get; set; }
    public string? Dates { get; set; }
    public DateTime? DeleteDate { get; set; }
    public long? DeletedByUsrId { get; set; }
    public DateTime? PermanentDeleteEligibleDate { get; set; }
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
