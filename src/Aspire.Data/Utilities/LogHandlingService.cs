
using Aspire.Context;
using Aspire.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aspire.Utilities;
public interface ILogHandlingService
{
    void LogException(Exception ex, string serviceName, string additionalContext = null);
    void LogInformation(string message, params object[] args);
    void LogWarning(string message, params object[] args);
    void LogError(string message, params object[] args);
}

public static class DBLog
{
    public static async Task<List<EntityLogDTO>> GetLogs(long ObjectId, ApplicationDbContext context, string? entityName = null)
    {
        var query = context.DBLogs.Where(x => x.ObjectId == ObjectId);
        if (!string.IsNullOrEmpty(entityName))
        {
            query = query.Where(x => x.EntityName == entityName);
        }

        var logs = await query.OrderByDescending(x => x.ActionDate).ToListAsync();
        var userIds = logs.Where(x => x.ActionUsrId.HasValue).Select(x => x.ActionUsrId!.Value).Distinct().ToList();

        var users = await context.Users
            .Where(u => userIds.Contains(u.Id))
            .ToDictionaryAsync(u => u.Id, u => new
            {
                Name = !string.IsNullOrEmpty(u.Name) ? u.Name : $"{u.FirstName} {u.LastName}".Trim(),
                u.PhotoUrl,
                u.Email
            });

        return logs.Select(l => new EntityLogDTO
        {
            Id = l.Id,
            TenantId = l.TenantId,
            ObjectId = l.ObjectId,
            EntityName = l.EntityName,
            Action = l.Action,
            ActionComment = l.ActionComment,
            ActionUsrId = l.ActionUsrId,
            ActionUsrName = l.ActionUsrId.HasValue && users.ContainsKey(l.ActionUsrId.Value) ? users[l.ActionUsrId.Value].Name : null,
            ActionUsrPhotoUrl = l.ActionUsrId.HasValue && users.ContainsKey(l.ActionUsrId.Value) ? users[l.ActionUsrId.Value].PhotoUrl : null,
            ActionUsrEmail = l.ActionUsrId.HasValue && users.ContainsKey(l.ActionUsrId.Value) ? users[l.ActionUsrId.Value].Email : null,
            ActionDate = l.ActionDate.HasValue ? DateTime.SpecifyKind(l.ActionDate.Value, DateTimeKind.Utc) : null
        }).ToList();
    }

    public static async Task<EntityLog> Action(long ObjectId, string Entity, string Action, string Comment, ApplicationDbContext context, ITenantProviderService _tenantService)
    {
        EntityLog newlog = new EntityLog();
        newlog.TenantId = _tenantService.GetTenantId();
        newlog.ActionUsrId = _tenantService.GetUserId(); ;
        newlog.EntityName = Entity;
        newlog.Action = Action;
        newlog.ActionComment = Comment;
        newlog.ObjectId = ObjectId;
        newlog.ActionDate = DateTime.UtcNow;
        context.DBLogs.Add(newlog);
        await context.SaveChangesAsync();
        return newlog;
    }
}


public class LogHandlingService(Serilog.ILogger logger) : ILogHandlingService
{
    public void LogException(Exception ex, string serviceName, string additionalContext = null)
    {
        var message = $"An error occurred in {serviceName}";

        if (!string.IsNullOrEmpty(additionalContext))
        {
            message += $", Additional Context: {additionalContext}";
        }

        logger.Error(ex, message);
    }

    public void LogInformation(string message, params object[] args)
    {
        logger.Information(message, args);
    }

    public void LogWarning(string message, params object[] args)
    {
        logger.Warning(message, args);
    }
    public void LogError(string message, params object[] args)
    {
        logger.Error(message, args);
    }
}
