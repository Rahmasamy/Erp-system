namespace Aspire.Data;

[Attr(DomainFolder = "FIN", Abbrv = "INCO", Lookup = true)]
/*
Incoterms (International Commercial Terms) are 11 standardized three-letter rules created by 
the International Chamber of Commerce that define the tasks, costs, and risks 
shared between buyers and sellers in global and domestic trade.
*/
public class Incoterm : BaseLookupEntity
{
    public string? Code { get; set; }
    public Incoterm() { }
    public Incoterm(long? tenantId, string code, string description)
    {
        this.TenantId = tenantId;
        this.Name = code;
        this.Code = code.Substring(0,3);
        this.Description = description;
    }
}

// Code-Player-Start-DTO //
public class IncotermDTO
{
    public string? Code { get; set; }
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
