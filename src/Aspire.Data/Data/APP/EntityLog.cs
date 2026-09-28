using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "APP", Abbrv = "", WithoutUI = true)]
public class EntityLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long? TenantId { get; set; }
    public long Id { get; set; }
    public long ObjectId { get; set; }
    public string? EntityName { get; set; }
    public string? Action { get; set; }
    public string? ActionComment { get; set; }
    public long? ActionUsrId { get; set; }
    public DateTime? ActionDate { get; set; }

    public EntityLog() {}
}

public class EntityLogDTO
{
    public long Id { get; set; }
    public long? TenantId { get; set; }
    public long ObjectId { get; set; }
    public string? EntityName { get; set; }
    public string? Action { get; set; }
    public string? ActionComment { get; set; }
    public long? ActionUsrId { get; set; }
    public string? ActionUsrName { get; set; }
    public string? ActionUsrPhotoUrl { get; set; }
    public string? ActionUsrEmail { get; set; }
    public DateTime? ActionDate { get; set; }
}