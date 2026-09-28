namespace Aspire.Data;

[Attr(DomainFolder = "DMS", Abbrv = "SP", List = true,
    Cols = new string[] { "Code", "Prefix", "FileTypes" })]

public class DocumentType : MigratedEntity
{
    public string? Code { get; set; }
    public decimal? MaxSize_dec { get; set; }
    public int? MaxFilesCount_num { get; set; }
    public bool? IsUploadOnly { get; set; } = false; // If true => There is no workflow (just upload)
    public bool? IsAllowVersioning { get; set; } = false;
    public string? Prefixes { get; set; } // Comma Separated
    public string? FileTypes { get; set; } // Comma Separated

    public DocumentType() { }
    public DocumentType(long? tenantId, string name, string code, decimal maxSize, string fileTypes)
    {
        this.TenantId = tenantId;
        this.Name = name;
        this.Code = code;
        this.MaxSize_dec = maxSize;
        this.FileTypes = fileTypes;
    }
}

// Code-Player-Start-DTO //
public class DocumentTypeDTO
{
    public string? OldKey { get; set; }

    public string? Code { get; set; }
    public decimal? MaxSize_dec { get; set; }
    public int? MaxFilesCount_num { get; set; }
    public bool? IsUploadOnly { get; set; } = false; // If true => There is no workflow (just upload)
    public bool? IsAllowVersioning { get; set; } = false;
    public string? Prefixes { get; set; } // Comma Separated
    public string? FileTypes { get; set; } // Comma Separated
    
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
