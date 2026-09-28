
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "CRM", Abbrv = "CS", List = true, Card = true, Tree = true,
    Actions = new string[] { "Block:Dlg:lock", "Un-Block:Dlg:unlock", "Assign-Price-List:Dlg:credit-card", "Opening-Balance:Dlg:credit-card", "Create-Invoice:FIN_InvoiceAR:money-bill", "Add-Payment:FIN_PaymentAR:dollar" },
    GroupBy = new string[] { "AccountManagerUsrId", "IndustryId", "SectorId", "CountryId" },
    FilterBy = new string[] { "AccountManagerUsrId", "IndustryId", "SectorId", "CountryId" },
    Cols = new string[] { "EntityStatusId", "AccountManagerUsrId", "Domain", "IndustryId", "SectorId", "calc_Amount_val", "calc_DueInv_val" },
    Aggregate = new string[] { "calc_Amount_val", "calc_Budget_val", "calc_PlannedGP_val", "calc_ActualCost_val", "CostToComplete_val", "calc_ActualGP_val", "calc_RecgnzdRevn_val", "calc_UnRecgnzdRevn_val", "calc_Billed_val", "calc_UnBilled_val", "calc_DueInv_val", "calc_Collected_val", "calc_UnCollected_val", "calc_Planned_val", "calc_UnPlanned_val" },
    DisplayValues = new string[] { "AccountManagerUsrId", "CRNo", "calc_DueInv_val" },
    SummaryValues = new string[] { "calc_PipelineAmount_val", "calc_Amount_val", "calc_PlannedGP_val", "calc_ActualGP_val", "calc_RecgnzdRevn_val", "calc_Billed_val", "calc_UnBilled_val", "calc_DueInv_val" },
    Menu = new string[] {
        "Contacts?CRM_Contact_ListSub",
        "Sales?CRM_Deal_ListSub:CRM_Pnl_ListSub:CRM_Quote_ListSub",
        "Projects?CRM_Agreement_ListSub:PMO_Project_ListSub",
        "Financials?FIN_SalesOrder_ListSub:FIN_InvoiceAR_ListSub"
    })]
public class Customer : CompanyEntity
{
    // Sum of Projects
    [Attr(Section = "Financials")]
    public decimal OpeningBalance_val { get; set; } = 0;
    public bool IsBlock { get; set; } = false;
    public long? PriceListId { get; set; }
    public decimal? calc_PipelineAmount_val { get; set; }
    public decimal? calc_TotalPrice_val { get; set; }
    public decimal? calc_TotalCost_val { get; set; }
    public decimal? calc_Discount_val { get; set; }
    public decimal? calc_ManagementBuffer_val { get; set; }
    public double? calc_Discount_prc { get; set; }
    public decimal? calc_PlannedGP_prc { get; set; }
    public decimal? calc_ActualGP_prc { get; set; }
    public double? calc_ManagementBuffer_prc { get; set; }
    public decimal? calc_Amount_val { get; set; } // TotalPrice_val - DiscountValue_val => Subject for Taxes
    public long? Amount_rat { get; set; } // Tax Rate Id
    public decimal? calc_Amount_tax { get; set; } // Tax Amount
    public decimal? calc_Amount_tot { get; set; } // Amount Including Tax

    public decimal? calc_Budget_val { get; set; }
    public decimal? calc_PlannedGP_val { get; set; }
    public decimal? calc_ActualCost_val { get; set; }
    public decimal? calc_ActualGP_val { get; set; }
    public decimal? CostToComplete_val { get; set; }
    public decimal? calc_RecgnzdRevn_val { get; set; }
    public decimal? calc_UnRecgnzdRevn_val { get; set; }
    public decimal? calc_Billed_val { get; set; }
    public decimal? calc_UnBilled_val { get; set; }
    public decimal? calc_DueInv_val { get; set; }
    public decimal? calc_Collected_val { get; set; }
    public decimal? calc_UnCollected_val { get; set; }
    public decimal? calc_Planned_val { get; set; }
    public decimal? calc_UnPlanned_val { get; set; }

    public Customer() { }
    public Customer(long tnantId, List<EntityType> EntTyps, List<EntityStatus> EntSts, List<ApplicationUser> Users, List<Sector> Sectors, List<Industry> Indsts, string name, string nameAr, string domain)
    {
        TenantId = tnantId;
        
        Name = name;
        NameAr = nameAr;
        Domain = domain;
        Website = "https://WWW." + domain;
        CRNo = "10099777829";
        VATRegNo = "3445662829";

        Random rnd = new Random();
        AccountManagerUsrId = Users != null && Users.Any() ? Users[rnd.Next(0, Users.Count)].Id : null;
        var custEntTyps = EntTyps?.Where(x => x.EntityName == "Customer").ToList();
        EntityTypeId = custEntTyps != null && custEntTyps.Any() ? custEntTyps[rnd.Next(0, custEntTyps.Count)].Id : EntTyps?.FirstOrDefault()?.Id;
        var custEntSts = EntSts?.Where(x => x.EntityName == "Customer").ToList();
        EntityStatusId = custEntSts != null && custEntSts.Any() ? custEntSts[rnd.Next(0, custEntSts.Count)].Id : EntSts?.FirstOrDefault()?.Id;
        SectorId = Sectors != null && Sectors.Any() ? Sectors[rnd.Next(0, Sectors.Count)].Id : null;
        IndustryId = Indsts != null && Indsts.Any() ? Indsts[rnd.Next(0, Indsts.Count)].Id : null;
    }
}

// Code-Player-Start-DTO //
public class CustomerDTO
{
    public decimal? OpeningBalance_val { get; set; }
    public bool? IsBlock { get; set; }
    public long? PriceListId { get; set; }
    public string? PriceListName { get; set; }
    public decimal? calc_PipelineAmount_val { get; set; }
    public decimal? calc_TotalPrice_val { get; set; }
    public decimal? calc_TotalCost_val { get; set; }
    public decimal? calc_Discount_val { get; set; }
    public decimal? calc_ManagementBuffer_val { get; set; }
    public double? calc_Discount_prc { get; set; }
    public decimal? calc_PlannedGP_prc { get; set; }
    public decimal? calc_ActualGP_prc { get; set; }
    public double? calc_ManagementBuffer_prc { get; set; }
    public decimal? calc_Amount_val { get; set; }
    public long? Amount_rat { get; set; }
    public decimal? calc_Amount_tax { get; set; }
    public decimal? calc_Amount_tot { get; set; }
    public decimal? calc_Budget_val { get; set; }
    public decimal? calc_PlannedGP_val { get; set; }
    public decimal? calc_ActualCost_val { get; set; }
    public decimal? calc_ActualGP_val { get; set; }
    public decimal? CostToComplete_val { get; set; }
    public decimal? calc_RecgnzdRevn_val { get; set; }
    public decimal? calc_UnRecgnzdRevn_val { get; set; }
    public decimal? calc_Billed_val { get; set; }
    public decimal? calc_UnBilled_val { get; set; }
    public decimal? calc_DueInv_val { get; set; }
    public decimal? calc_Collected_val { get; set; }
    public decimal? calc_UnCollected_val { get; set; }
    public decimal? calc_Planned_val { get; set; }
    public decimal? calc_UnPlanned_val { get; set; }
    public long? AccountManagerUsrId { get; set; }
    public string? AccountManagerUsrName { get; set; }
    public string? AccountManagerUsrPhotoUrl { get; set; }
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
    public string? Domain { get; set; }
    public string? Website { get; set; }
    public string? CRNo { get; set; }
    public string? VATRegNo { get; set; }
    public string? RegistrationNo { get; set; }
    public long? IndustryId { get; set; }
    public string? IndustryName { get; set; }
    public long? SectorId { get; set; }
    public string? SectorName { get; set; }
    public int? Employees_num { get; set; }
    public decimal? AnnualRevenue_val { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }
    public long? CountryId { get; set; }
    public string? CountryName { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Address { get; set; }
    public string? Street { get; set; }
    public string? ZipCode { get; set; }
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
