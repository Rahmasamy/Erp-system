using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "HRM", Abbrv = "SCM", Lookup = true)]
public class EvaluationSchema : BaseEntity
{
    public long? OwnerUsrId { get; set; }

    public double? MinScoreLimit { get; set; }
    public double? MaxScoreLimit { get; set; }

    EvaluationSchema() { }
    public EvaluationSchema(long? tenantId, string name, long? ownerId)
    {
        this.TenantId = tenantId;
        this.Name = name;
        this.OwnerUsrId = ownerId;
    }
}

// Code-Player-Start-DTO //
// Code-Player-End-DTO //