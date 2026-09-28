using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Aspire.Data;


namespace Aspire.Utilities;
public interface IEntityRepository<TContext>
    where TContext : DbContext
{
    TContext Context { get; }
    long? GetTenantId();
    List<Entity> GetEntities(string EntityName, string TenantId);
    List<EntityStatus> GetEntityStatuses(string EntityName, string TenantId);
    List<EntityType> GetEntityTypes(string EntityName, string TenantId);
}

public class EntityRepository<TContext>(TContext context) 
            : IEntityRepository<TContext>
    where TContext : DbContext
{
    public TContext Context => context;


    public long? GetTenantId()
    {
        string Sql = "Select * From \"public\".\"Tenants\"";

        var entities = context.Database.SqlQueryRaw<Tenant>(Sql);
        var items = entities.ToList();
        return items[0].Id;
    }

    public List<Entity> GetEntities(string EntityName, string TenantId)
    {
        string Sql = "Select * From \"public\".\"Entities\" Where \"Name\" = '" + EntityName + "' AND \"TenantId\" = '" + TenantId + "'";

        var entities = context.Database.SqlQueryRaw<Entity>(Sql);
        var items = entities.ToList();
        return items;
    }

    public List<EntityStatus> GetEntityStatuses(string EntityName, string TenantId)
    {
        string Sql = "Select * From \"public\".\"EntityStatuses\" Where \"EntityName\" = '" + EntityName + "' AND \"TenantId\" = '" + TenantId + "'";

        var entities = context.Database.SqlQueryRaw<EntityStatus>(Sql);
        var items = entities.ToList();
        return items;
    }

    public List<EntityType> GetEntityTypes(string EntityName, string TenantId)
    {
        string Sql = "Select * From \"public\".\"EntityTypes\" Where \"EntityName\" = '" + EntityName + "' AND \"TenantId\" = '" + TenantId + "'";

        var entities = context.Database.SqlQueryRaw<EntityType>(Sql);
        var items = entities.ToList();
        return items;
    }
}
