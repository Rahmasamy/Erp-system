
using Aspire.Utilities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "APP", Abbrv = "", WithoutUI = true)]
public class ServiceCatalogue : BaseEntity
{
    public long? WorkflowId { get; set; }
    public long? SLAId { get; set; }
    public string? ServName { get; set; }
    public string? TermsConditions { get; set; }
    public string? TermsConditionsAr { get; set; }


    public ServiceCatalogue() { }
    public ServiceCatalogue(long? tenantId, long? entityTypeId, string servName, string name, string nameAr, string approvals, string availibleFor, string timeFrame)
    {
        this.ServName = servName;
        this.TenantId = tenantId;
        this.EntityTypeId = entityTypeId;
        this.Name = name;
        this.NameAr = nameAr;
    }
}

// Code-Player-Start-DTO //
public class ServiceCatalogueDTO
{
    public long? WorkflowId { get; set; }
    public string? WorkflowName { get; set; }
    public long? SLAId { get; set; }
    public string? SLAName { get; set; }
    public string? ServName { get; set; }
    public string? TermsConditions { get; set; }
    public string? TermsConditionsAr { get; set; }
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