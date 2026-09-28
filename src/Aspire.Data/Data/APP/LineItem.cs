using Aspire.Utilities;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aspire.Data;

[Attr(DomainFolder = "APP", Abbrv = "LI", WithoutUI = true)]
public class LineItem : BaseEntityLinked
{
    // This Product Line Item Could be Linked to One of the Following Parents
    // [Deal, Quote, Pnl, Agreement, Program, Project, ChangeRequest, PurchaseOrder, SalesOrder, BillingPlan, InvoiceAR, InvoiceAP, CreditNote, DebitNote]

    public long? ProductId { get; set; }
    public long? ProductCategoryId { get; set; } // Get from Product
    public long? MarketUnitId { get; set; } // Get from Product
    public decimal calc_UnitCost_val { get; set; } = 0; // Get from Product - Can't be Updated
    public decimal UnitPrice_val { get; set; } = 0; // Get from Product and Can be Updated
    public decimal Quantity_dec { get; set; } = 0; // Input
    public decimal calc_TotalCost_val { get; set; } = 0; // Unit Cost X Quantity
    public decimal calc_TotalPrice_val { get; set; } = 0; // Unit Price X Quantity
    public long? DiscountTypeId { get; set; }
    public decimal Discount_val { get; set; } = 0; // [Value or % From calc_TotalPrice_val] Based On DiscountType
    public long? ManagementBufferTypeId { get; set; }
    public decimal ManagementBuffer_val { get; set; } = 0; // [Value or % From calc_TotalPrice_val] Based On ManagementBufferType
    public decimal calc_Amount_val { get; set; } = 0; // TotalPrice_val - Discount_val => Subject for Taxes
    public long? CurrencyId { get; set; }
    public long? TaxRateId { get; set; }
    public decimal calc_Tax_val { get; set; } // Tax Amount
    public decimal calc_Total_val { get; set; } // Amount Including Tax

    public decimal calc_Budget_val { get; set; } // TotalCost_val - ManagementBuffer_val
    public decimal calc_PlannedGP_val { get; set; } // Amount_val - Budget_val    


    public decimal calc_QuantityConsumed_dec { get; set; } = 0;
    public decimal calc_QuantityRemaining_dec { get; set; } = 0;
    public decimal calc_QuantityBilled_dec { get; set; } = 0;
    public decimal calc_QuantityUnBilled_dec { get; set; } = 0;
    public decimal calc_ActualCost_val { get; set; } // Sum of Expenses
    public decimal calc_ActualGP_val { get; set; } // Amount_val - (CostToComplete_val + ActualCost_val)    
    public decimal CostToComplete_val { get; set; } // Budget_val - ActualCost_val
    public decimal calc_RecgnzdRevn_val { get; set; } // The revenue the company has fulfilled its obligations on, and there is reasonable expectation of payment (Should be Billed)
    public decimal calc_UnRecgnzdRevn_val { get; set; } // Amount_val - RecognizedRevenue_val
    public decimal calc_Billed_val { get; set; } // Sum of Invoices (Billed)
    public decimal calc_UnBilled_val { get; set; } // RecgnzdRevn_val - Billed_val    
    public decimal calc_DueInv_val { get; set; } // Sum of Invoices (Due)
    public decimal calc_Collected_val { get; set; } // Sum of Invoices (Collected)    
    public decimal calc_UnCollected_val { get; set; } // Billed_val - Collected_val    
    public decimal calc_Paid_val { get; set; } = 0; // Sum of Invoices AP (Paid to the Supplier)
    public decimal calc_UnPaid_val { get; set; } = 0; // Billed_val - Paid_val
    public decimal calc_Planned_val { get; set; } // Sum of Billing Plan (Planned)
    public decimal calc_UnPlanned_val { get; set; }  // UnRecgnzdRevn_val - Planned_val

    public decimal Progress_prc { get; set; } = 0;
    public decimal Discount_prc { get; set; } = 0; // (Discount_val / TotalPrice_val) % Based on DiscountType or Manually => Update Discount_val
    public decimal ManagementBuffer_prc { get; set; } = 0; // (ManagementBuffer_val / TotalCost_val) => % of TotalCost_val (Manually) => Update ManagementBuffer_val
    public decimal calc_PlannedGP_prc { get; set; } // PlannedGP_val / Budget_val    
    public decimal calc_ActualGP_prc { get; set; } // ActualGP_val / Budget_val    
    public decimal calc_RecgnzdRevn_prc { get; set; } // RecgnzdRevn / Amount_val
    public decimal calc_Billed_prc { get; set; } // Billed_val / Amount_val
    public decimal calc_DueInv_prc { get; set; } // Due_val / Amount_val
    public decimal calc_Collected_prc { get; set; } // Collected_val / Amount_val

    public string? Term { get; set; }

    public long? BillingFrequencyId { get; set; }
    public long? DeliveryProofId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public long? CostCenterId { get; set; } // Either Cost Center or Profit Center (One of Them)
    public long? ProfitCenterId { get; set; } // Either Cost Center or Profit Center (One of Them)

    public LineItem() { }
    public LineItem(long tnantId, Product prod, string entityName, long? objectId, decimal qnty)
    {
        TenantId = tnantId;
        this.Name = prod.Name;
        this.NameAr = prod.NameAr;
        this.ProductId = prod.Id;
        this.UnitPrice_val = prod.UnitPrice_val;
        this.calc_TotalCost_val = prod.UnitCost_val * Convert.ToDecimal(qnty);
        this.calc_TotalPrice_val = prod.UnitPrice_val * Convert.ToDecimal(qnty);
        this.TaxRateId = prod.TaxRateId;
        this.Discount_val = 500;
        this.ManagementBuffer_val = 1000;
        this.calc_Amount_val = this.calc_TotalPrice_val - this.Discount_val;
        this.calc_Budget_val = calc_TotalCost_val - this.ManagementBuffer_val;
        this.calc_ActualCost_val = calc_Budget_val * 70 / 100;
        this.CostToComplete_val = calc_TotalCost_val * 105 / 100;
        this.calc_ActualGP_val = calc_Amount_val - CostToComplete_val;
        this.calc_RecgnzdRevn_val = calc_Amount_val * 70 / 100;
        this.calc_Billed_val = this.calc_RecgnzdRevn_val * 50 / 100;
        this.calc_Collected_val = this.calc_Billed_val * 50 / 100;
        this.calc_UnRecgnzdRevn_val = this.calc_Amount_val - this.calc_RecgnzdRevn_val;
        this.calc_Planned_val = this.calc_UnRecgnzdRevn_val * 90 / 100;
        this.calc_UnBilled_val = this.calc_UnRecgnzdRevn_val - this.calc_Billed_val;
        this.calc_UnCollected_val = this.calc_Billed_val - this.calc_Collected_val;
        this.calc_UnPlanned_val = this.calc_Amount_val - this.calc_Planned_val;

        this.EntityName = entityName;
        this.ObjectId = objectId;
        this.Quantity_dec = qnty;
        this.ProductCategoryId = prod.ProductCategoryId;
        this.MarketUnitId = prod.MarketUnitId;
    }
}

// Code-Player-Start-DTO //
public class LineItemDTO
{
    public long? ProductId { get; set; }
    public string? ProductName { get; set; }
    public long? ProductCategoryId { get; set; }
    public string? ProductCategoryName { get; set; }
    public long? MarketUnitId { get; set; }
    public string? MarketUnitName { get; set; }
    public decimal? calc_UnitCost_val { get; set; }
    public decimal? UnitPrice_val { get; set; }
    public decimal? Quantity_dec { get; set; }
    public decimal? calc_TotalCost_val { get; set; }
    public decimal? calc_TotalPrice_val { get; set; }
    public long? DiscountTypeId { get; set; }
    public string? DiscountTypeName { get; set; }
    public decimal? Discount_val { get; set; }
    public long? ManagementBufferTypeId { get; set; }
    public string? ManagementBufferTypeName { get; set; }
    public decimal? ManagementBuffer_val { get; set; }
    public decimal? calc_Amount_val { get; set; }
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
    public long? TaxRateId { get; set; }
    public string? TaxRateName { get; set; }
    public decimal? calc_Tax_val { get; set; }
    public decimal? calc_Total_val { get; set; }
    public decimal? calc_Budget_val { get; set; }
    public decimal? calc_PlannedGP_val { get; set; }
    public decimal? calc_QuantityConsumed_dec { get; set; }
    public decimal? calc_QuantityRemaining_dec { get; set; }
    public decimal? calc_QuantityBilled_dec { get; set; }
    public decimal? calc_QuantityUnBilled_dec { get; set; }
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
    public decimal? calc_Paid_val { get; set; }
    public decimal? calc_UnPaid_val { get; set; }
    public decimal? calc_Planned_val { get; set; }
    public decimal? calc_UnPlanned_val { get; set; }
    public decimal? Progress_prc { get; set; }
    public decimal? Discount_prc { get; set; }
    public decimal? ManagementBuffer_prc { get; set; }
    public decimal? calc_PlannedGP_prc { get; set; }
    public decimal? calc_ActualGP_prc { get; set; }
    public decimal? calc_RecgnzdRevn_prc { get; set; }
    public decimal? calc_Billed_prc { get; set; }
    public decimal? calc_DueInv_prc { get; set; }
    public decimal? calc_Collected_prc { get; set; }
    public string? Term { get; set; }
    public long? BillingFrequencyId { get; set; }
    public string? BillingFrequencyName { get; set; }
    public long? DeliveryProofId { get; set; }
    public string? DeliveryProofName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; }
    public long? ProfitCenterId { get; set; }
    public string? ProfitCenterName { get; set; }
    public string? EntityName { get; set; }
    public long? ObjectId { get; set; }
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