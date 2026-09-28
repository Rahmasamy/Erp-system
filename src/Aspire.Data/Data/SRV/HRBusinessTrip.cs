
using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "SRV", Abbrv = "REQ-HR", Service = true)]
public class HRBusinessTrip : ServiceEntity
{
    public long? ProjectId { get; set; }

    [NotMapped]
    public virtual Project? Project { get; set; }

    public long? CountryId { get; set; }

    [NotMapped]
    public virtual Country? Country { get; set; }
    
    public string? CityName { get; set; }
    
    public DateTime? StartDate { get; set; } 
    public DateTime? EndDate { get; set; }

    public decimal Flight_val { get; set; } = 0;
    public long? Flight_cur { get; set; }


    public decimal Hotel_val { get; set; } = 0;
    public long? Hotel_cur { get; set; }


    public decimal Taxi_val { get; set; } = 0;
    public long? Taxi_cur { get; set; }


    public decimal Perdiem_val { get; set; } = 0;
    public long? Perdiem_cur { get; set; }


    public decimal TotalAmount_val { get; set; } = 0;
    public long? TotalAmount_cur { get; set; }


    public DateTime? SettlementDate { get; set; }
}

// Code-Player-Start-DTO //
public class HRBusinessTripDTO
{
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public long? CountryId { get; set; }
    public string? CountryName { get; set; }
    public string? CityName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? Flight_val { get; set; }
    public long? Flight_cur { get; set; }
    public decimal? Hotel_val { get; set; }
    public long? Hotel_cur { get; set; }
    public decimal? Taxi_val { get; set; }
    public long? Taxi_cur { get; set; }
    public decimal? Perdiem_val { get; set; }
    public long? Perdiem_cur { get; set; }
    public decimal? TotalAmount_val { get; set; }
    public long? TotalAmount_cur { get; set; }
    public DateTime? SettlementDate { get; set; }
    public string? VueFlowExec { get; set; }
    public long? UrgencyId { get; set; }
    public string? UrgencyName { get; set; }
    public long? RequestForUsrId { get; set; }
    public string? RequestForUsrName { get; set; }
    public string? RequestForUsrPhotoUrl { get; set; }
    public long? ActingForUsrId { get; set; }
    public string? ActingForUsrName { get; set; }
    public string? ActingForUsrPhotoUrl { get; set; }
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
