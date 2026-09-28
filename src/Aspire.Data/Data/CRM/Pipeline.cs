using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "CRM", Abbrv = "PL", List = true, Card = true, Tree = true,
    GroupBy = new string[] { "AccountManagerId" },
    FilterBy = new string[] { "AccountManagerId" },
    Aggregate = new string[] { "calc_Amount_val", "calc_PipelineAmount_val", "calc_GP_val" },
    Cols = new string[] { "AccountManagerUsrId", "calc_Amount_val", "calc_PipelineAmount_val", "calc_GP_prc" },
    DisplayValues = new string[] { "AccountManagerUsrId", "calc_Amount_val", "calc_PipelineAmount_val" },
    SummaryValues = new string[] { "calc_Amount_val", "calc_PipelineAmount_val", "calc_GP_val" },
    Menu = new string[] {
        "Stages?CRM_Stage_ListSub"
    })]
public class Pipeline : BaseEntity
{
    public long? AccountManagerUsrId { get; set; } // UserId
    public long? CurrencyId { get; set; }

    public decimal calc_Amount_val { get; set; } = 0; // Sum of Open Deals
    public decimal calc_PipelineAmount_val { get; set; } = 0; // Sum of Open Deals
    public decimal? calc_GP_val { get; set; }
    public decimal? calc_GP_prc { get; set; }

    public Pipeline() { }

    public Pipeline(long tnantId, string name, string nameAr)
    {
        TenantId = tnantId;
        
        this.Name = name;
        NameAr = nameAr;
        calc_Amount_val = 790000000;
        calc_PipelineAmount_val = 350000000;
        calc_GP_val = 120000000;
    }
}

// Code-Player-Start-DTO //
public class PipelineDTO
{
    public long? AccountManagerUsrId { get; set; }
    public string? AccountManagerUsrName { get; set; }
    public string? AccountManagerUsrPhotoUrl { get; set; }
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
    public decimal? calc_Amount_val { get; set; }
    public decimal? calc_PipelineAmount_val { get; set; }
    public decimal? calc_GP_val { get; set; }
    public decimal? calc_GP_prc { get; set; }
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