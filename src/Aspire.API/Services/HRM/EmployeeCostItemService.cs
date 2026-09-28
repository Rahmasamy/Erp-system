using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;

namespace Aspire.Services;

public class EmployeeCostItemService()
{

// Code-Player-Start-DTO //
    public static async Task<EmployeeCostItem> Create(EmployeeCostItem entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        // Perform the Validation
        if (entity.Msg != "OK")
            return entity;

        entity.TenantId = _tenantService.GetTenantId();
        entity.CreatedByUsrId = _tenantService.GetUserId();
        entity.CreateDate = DateTime.UtcNow;
        entity.Msg = "OK";
        context.EmployeeCostItems.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public static async Task<EmployeeCostItem> Update(EmployeeCostItem entity, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        entity.Msg = "OK";
        if (entity.Msg != "OK")
            return entity;

        EmployeeCostItem? obj = context.EmployeeCostItems.Where(e => e.Id == entity.Id).FirstOrDefault();

        if (obj is null)
        {
            entity.Msg = "This Id (" + entity.Id + ") is not exist in the database";
            return entity;
        }

        obj.EmployeeId = entity.EmployeeId is null ? obj.EmployeeId : entity.EmployeeId;
        obj.CostItemId = entity.CostItemId is null ? obj.CostItemId : entity.CostItemId;
        obj.CostValue_val = entity.CostValue_val is null ? obj.CostValue_val : entity.CostValue_val;
        obj.IsActive = entity.IsActive is null ? obj.IsActive : entity.IsActive;
        obj.ActionComment = entity.ActionComment is null ? obj.ActionComment : entity.ActionComment;
        obj.PhotoUrl = entity.PhotoUrl is null ? obj.PhotoUrl : entity.PhotoUrl;

        obj.Msg = "OK";
        obj.UpdatedByUsrId = _tenantService.GetUserId();
        obj.UpdateDate = DateTime.UtcNow;
        await context.SaveChangesAsync();
        return obj;
    }

    public static string OnDelete(long Id, ApplicationDbContext context)
    {
        string msg = "OK";
        return msg;
    }
// Code-Player-End-DTO //
}
