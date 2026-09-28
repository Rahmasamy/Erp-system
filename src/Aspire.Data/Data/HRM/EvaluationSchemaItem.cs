using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "HRM", Abbrv = "EVI")]
public class EvaluationSchemaItem : BaseEntity
{
    public long? EvaluationSchemaId { get; set; }

    public double? Percentage { get; set; }

    public long? ParentId { get; set; }

    [NotMapped]
    public virtual EvaluationSchemaItem? Parent { get; set; }

    public EvaluationSchemaItem() { }
    public EvaluationSchemaItem(long? tenantId, string name, long? evaluationSchemaId, double percentage)
    {
        this.TenantId = tenantId;
        this.Name = name;
        this.EvaluationSchemaId = evaluationSchemaId;
        this.Percentage = percentage;
    }
}

// Code-Player-Start-DTO //
// Code-Player-End-DTO //