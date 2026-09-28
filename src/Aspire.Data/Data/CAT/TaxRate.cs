using System.ComponentModel.DataAnnotations.Schema;

namespace Aspire.Data;

[Attr(DomainFolder = "CAT", Abbrv = "TAX", Lookup = true)]
public class TaxRate : CoreEntity
{
    public string? LabelOnInvoice  { get; set; } // Label On Invoice
    public bool IsDefault { get; set; }
    public decimal TaxValue_dec { get; set; } = 15; // Percentage (%)

    public TaxRate() { }
    public TaxRate(long tnantId, string name, string nameAr, bool isDefault)
    {
        TenantId = tnantId;
        
        this.Name = name;
        this.NameAr = nameAr;
        IsDefault = isDefault;
    }
}

public class ActiveTaxRateDTO
{
    public ActiveTaxRateDTO(TaxRate Tax)
    {
        this.Id = Tax.Id;
        this.Name = Tax.Name;
        this.NameAr = Tax.NameAr;
        this.TaxValue_dec = Tax.TaxValue_dec;
    }

    public string? Name { get; set; }
    public string? NameAr { get; set; }
    public decimal TaxValue_dec { get; set; } = 15; // Percentage (%)
    public long? Id { get; set; }
}


// Code-Player-Start-DTO //
public class TaxRateDTO
{
    public string? LabelOnInvoice { get; set; }
    public bool? IsDefault { get; set; }
    public decimal? TaxValue_dec { get; set; }
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

