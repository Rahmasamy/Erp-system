using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Aspire.Data;

[Attr(DomainFolder = "GRC", Abbrv = "ADT-ITM", WithoutUI = true)]
public class AuditItem : BaseEntity
{
    public decimal Score_dec { get; set; } = 0;

    public long? AuditId { get; set; }

    [NotMapped]
    public virtual Audit? Audit { get; set; }

    public long? ParentId { get; set; }

    [NotMapped]
    public virtual AuditItem? Parent { get; set; }

    public string? Type { get; set; }
    public string? Key { get; set; }
    public string? Objective { get; set; }
    public string? Principal { get; set; }
    public string? Tools { get; set; }
    public string? Guidelines { get; set; }
    public string? Deliverables { get; set; }

    public string? Observation { get; set; }

    public int? Ctrls { get; set; } = 0;
    public int? Evids { get; set; } = 0;
    public int? Excps { get; set; } = 0;

    public AuditItem()
    {

    }

    public AuditItem(long? tenantId, FrameworkItem itm, long? auditId, long? parentId)
    {
        this.TenantId = tenantId;
        this.AuditId = auditId;
        this.ParentId = parentId;
        this.Name = itm.Name;
        this.Description = itm.Description;
        this.Key = itm.Key;
        this.Objective = itm.Objective;
        this.Deliverables = itm.Deliverables;
        this.Principal = itm.Principal;
        this.Guidelines = itm.Guidelines;
        this.Tools = itm.Tools;
        this.Ctrls = 0;
        this.Evids = 0;
        this.Excps = 0;
    }
}

public class AuditItemStatusDTO
{
    public string? EntityStatusName { get; set; }
    public string? EntityStatusColor { get; set; }
    public int? Count { get; set; }
    public decimal? Percentage { get; set; }
}

public class PipelineValueDTO
{
    public long? Id { get; set; }
    public string? PipelineName { get; set; }
    public decimal? Value { get; set; }
    public decimal? Percentage { get; set; }
}


public class StageValueDTO
{
    public long? Id { get; set; }
    public string? StageName { get; set; }
    public decimal? Value { get; set; }
    public decimal? Percentage { get; set; }
}


public class AuditItemTypeDTO
{
    public string? EntityTypeName { get; set; }
    public string? EntityTypeColor { get; set; }
    public int? Count { get; set; }
    public decimal? Percentage { get; set; }
}


public class ProjectInfoDTO
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public decimal? NetAmount_val { get; set; } // Net Amount
    public decimal? TaxAmount_val { get; set; } // Tax Amount
    public decimal? TotalAmount_val { get; set; } // Amount Including Tax

    public decimal? Budget_val { get; set; }
    public decimal? ActualCost_val { get; set; }
    public decimal? RemainingBudget_val { get; set; }

    public decimal? RecognizedRevenue_val { get; set; } // The revenue the the company has fulfilled its obligations on, and there is reasonable expectation of payment (Should be Billed/Paied)
    public decimal? Billed_val { get; set; }
    public decimal? UnBilled_val { get; set; }

    public decimal? NonRecognizedRevenue_val { get; set; } // The revenue the the company has fulfilled its obligations on, and there is reasonable expectation of payment (Should be Billed/Paied)
    public decimal? Planned_val { get; set; }
    public decimal? UnPlanned_val { get; set; }
}
public class ProjectOverTimeDTO
{
    public ProjectOverTimeDTO(double? cPI, double? sPI, double? bTC, double? currentCost, string? timeInterval)
    {
        CPI = cPI;
        SPI = sPI;
        BTC = bTC;
        CurrentCost = currentCost;
        Profit = 450000 - bTC;
        Progress = currentCost * 1.3; //RecognizedRevenue
        TimeInterval = timeInterval;
    }

    public double? Profit { get; set; }
    public double? CurrentCost { get; set; }
    public double? PlannedCost { get; set; }

    public double? CPI { get; set; }
    public double? SPI { get; set; }
    public double? BTC { get; set; }
    public double? Progress { get; set; }
    public string? TimeInterval { get; set; }
}


public class ScoreDTO
{
    public decimal Score_dec { get; set; } = 0;
    public string? Msg { get; set; }
    public List<RangeDTO>? Ranges { get; set; }
}

public class RangeDTO
{
    public double? Min { get; set; }
    public double? Max { get; set; }

    public string? Title { get; set; }
    public string? Color { get; set; }

}

// Code-Player-Start-DTO //
public class AuditItemDTO
{
    public decimal? Score_dec { get; set; }
    public long? AuditId { get; set; }
    public string? AuditName { get; set; }
    public long? ParentId { get; set; }
    public string? ParentName { get; set; }
    public string? Type { get; set; }
    public string? Key { get; set; }
    public string? Objective { get; set; }
    public string? Principal { get; set; }
    public string? Tools { get; set; }
    public string? Guidelines { get; set; }
    public string? Deliverables { get; set; }
    public string? Observation { get; set; }
    public int? Ctrls { get; set; }
    public int? Evids { get; set; }
    public int? Excps { get; set; }
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

// Code-Player-Start-DTO-Post //
public class AuditItemDTO_Post
{
    public string? Msg { get; set; }
    public double? Score { get; set; }
    public string? Observation { get; set; }
    public long? Id { get; set; }
    public long? EntityStatusId { get; set; }
    public long? EntityTypeId { get; set; }
    public long? UpdatedByUsrId { get; set; }
}
// Code-Player-End-DTO-Post //