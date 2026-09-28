using Microsoft.AspNetCore.Http;

namespace Aspire.Data;

[Attr(DomainFolder = "APP", Abbrv = "", WithoutUI = true)]
public class EntityAttachment : BaseEntityLinked
{
    public decimal? Version { get; set; }
    public string? FileName { get; set; }
    public double? FileSize { get; set; }
    public string? FileExtension { get; set; }
    public string? ContentType { get; set; }
    public string? ContentBase64 { get; set; }
    public string? DownloadLink { get; set; }
}

// Code-Player-Start-DTO //
public class EntityAttachmentDTO
{
    public string? FileName { get; set; }
    public double? FileSize { get; set; }
    public string? FileExtension { get; set; }
    public string? ContentType { get; set; }
    public string? ContentBase64 { get; set; }
    public string? DownloadLink { get; set; }
    public string? EntityName { get; set; }
    public long? ObjectId { get; set; }
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


public class UpdatePhotoDTO
{
    public long? ObjectId { get; set; } // The Id of specific record in the entity
    public string? EntityName { get; set; } // Entity Name {ex: Project, Company, Deal, etc.}
    public IFormFile? File { get; set; }
}
public class SaveEntityAttachmentDTO
{
    public long? Id { get; set; }
    public long? TenantId { get; set; }
    public long? ObjectId { get; set; } // The Id of specific record in the entity
    public string? EntityName { get; set; } // Entity Name {ex: Project, Company, Deal, etc.}
    public string? FileName { get; set; }
    public string? FileExtension { get; set; }
    public IFormFile? File { get; set; }
}