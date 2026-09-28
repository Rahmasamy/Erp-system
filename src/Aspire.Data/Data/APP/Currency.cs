namespace Aspire.Data;

[Attr(DomainFolder = "APP", Abbrv = "CU", Lookup = true)]
public class Currency : CoreEntity
{
    public decimal? ExchangeRate { get; set; }
    public DateTime? ExchangeRateTimeStamp { get; set; }
    public bool? IsBaseCurrency { get; set; } = false;

}

public class ActiveCurrencyDTO
{
    public ActiveCurrencyDTO(Currency Curr)
    {
        this.Id = Curr.Id;

        if(Curr.IsBaseCurrency == true)
        {
            this.LongName = Curr.Name + " (Base Currency)";
            this.LongNameAr = Curr.NameAr + " (العملة الأساسية)";
        }
        else
        {            
            this.LongName = Curr.Name + " Exchange: " + Math.Round(decimal.Parse(Curr.ExchangeRate.ToString()), 2);
            this.LongNameAr = Curr.NameAr + " التحويل: " + Math.Round(decimal.Parse(Curr.ExchangeRate.ToString()), 2);
        }

        this.Name = Curr.Name;
        this.NameAr = Curr.NameAr;
        this.ExchangeRate = Curr.ExchangeRate;
        this.IsBaseCurrency = Curr.IsBaseCurrency;
        this.TenantId = Curr.TenantId;
    }

    public decimal? ExchangeRate { get; set; }
    public bool? IsBaseCurrency { get; set; }
    public string? Name { get; set; }
    public string? NameAr { get; set; }
    public string? LongName { get; set; }
    public string? LongNameAr { get; set; }
    public long? Id { get; set; }
    public long? TenantId { get; set; }
}

// Code-Player-Start-DTO //
public class CurrencyDTO
{
    public decimal? ExchangeRate { get; set; }
    public DateTime? ExchangeRateTimeStamp { get; set; }
    public bool? IsBaseCurrency { get; set; }
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