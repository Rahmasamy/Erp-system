using Aspire.Enums;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "DMS", Abbrv = "", WithoutUI = true)]

public class DocumentTypeDiscipline : XREFEntity
{
    public long? DocumentTypeId { get; set; }
    public long? DisciplineId { get; set; }
    public long? DisciplineUsrId { get; set; }
    public string? TaskName { get; set; }
    public int? Seq { get; set; }

    public decimal? SLA_dec { get; set; }

    // Comma Separated Actions & Behaviour as Below
    // --------------------------------------------
    // {DMSActionId}:0      => Stay in Same Step
    // {DMSActionId}:1      => Continue To Next Step
    // {DMSActionId}:2      => Back To Previous Step
    // {DMSActionId}:3      => Return To Contractor
    // {DMSActionId}:4-7    => Go To Step#7 (any Step Number Forward or Backward)
    public string? Actions { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AllocationEnum? Allocation { get; set; }

    public DocumentTypeDiscipline() { }
    public DocumentTypeDiscipline(long? tenantId, long? docId, long? dspId, int? sq, AllocationEnum alct, string? act) 
    {
        this.TenantId = tenantId;
        this.DocumentTypeId = docId;
        this.DisciplineId = dspId;
        this.Seq = sq;
        this.Allocation = alct;
        this.Actions = act;
    }
}

// Code-Player-Start-DTO //
public class DocumentTypeDisciplineDTO
{
    public long? DocumentTypeId { get; set; }
    public string? DocumentTypeName { get; set; }
    public long? DisciplineId { get; set; }
    public string? DisciplineName { get; set; }
    public string? DisciplineNameAr { get; set; }
    public long? DisciplineUsrId { get; set; }
    public string? DisciplineUsrName { get; set; }
    public string? EmployeeName { get; set; }
    public string? TaskName { get; set; }

    public int? Seq { get; set; }
    public decimal? SLA_dec { get; set; }
    public string? Actions { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AllocationEnum? Allocation { get; set; }

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

public class DocumentTypeDisciplineEditDTO
{
    public long? DocumentTypeId { get; set; }
    public List<DocumentTypeDisciplineItemDTO>? Disciplines { get; set; }
}

public class DocumentTypeDisciplineItemDTO
{
    public long? Id { get; set; }
    public long? DisciplineId { get; set; }
    public string? TaskName { get; set; }
    public decimal? SLA_dec { get; set; }
    public bool? IsActive { get; set; }
    public int? Seq { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AllocationEnum? Allocation { get; set; }
    public string? Actions { get; set; }
}
// Code-Player-End-DTO //
