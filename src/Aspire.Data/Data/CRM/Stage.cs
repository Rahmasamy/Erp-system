using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "CRM", Abbrv = "STG", SubList = true,
    Cols = new string[] { "Pipeline_prc", "calc_Amount_val", "calc_PipelineAmount_val" }
    )]
public class Stage : BaseEntity
{
    public long? PipelineId { get; set; }

    [NotMapped]
    public virtual Pipeline? Pipeline { get; set; }

    public decimal Pipeline_prc { get; set; } = 0; // %
    public decimal calc_Amount_val { get; set; } = 0; // Sum of Open Deals
    public decimal calc_PipelineAmount_val { get; set; } = 0; // Sum of Open Deals

    public Stage() { }

    public Stage(long tnantId, long? pipelineId, decimal percent, string name, string nameAr)
    {
        TenantId = tnantId;
        
        this.PipelineId = pipelineId;
        this.Name = name;
        NameAr = nameAr;
        Pipeline_prc = percent;
        calc_Amount_val = 100000000 * percent/100;
        calc_PipelineAmount_val = 100000000;
    }
}

// Code-Player-Start-DTO //
public class StageDTO
{
    public long? PipelineId { get; set; }
    public string? PipelineName { get; set; }
    public decimal? Pipeline_prc { get; set; }
    public decimal? calc_Amount_val { get; set; }
    public decimal? calc_PipelineAmount_val { get; set; }
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