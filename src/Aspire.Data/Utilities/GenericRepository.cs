using Aspire.Data;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Linq.Expressions;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aspire.Utilities;
public interface IGenericRepository<TEntity, TDTO, TContext>
    where TEntity : class where TDTO : class where TContext : DbContext
{
    TContext Context { get; }
    Task<PaginationModel<TDTO>> GetAllJoins(FilteringModel? filteringModel = null, bool? applyPagination = true, string? query = "", bool? applyTenantQuery = true, string? extraCols = "");
    ListsResponse GetLists(ListsRequest model);
    OptionsResponse GetOptions(OptionsRequest model);
    Task<TEntity?> GetByIdEntity(string Id);
    Task<TDTO?> GetByIdEntityDTO(string Id, string? extraCols = "");
    Task<PaginationModel<TDTO>> GetRootsEntityDTO(string? extraCols = "");
    Task<PaginationModel<TDTO>> GetByParentIdEntityDTO(string Id, string? extraCols = "");
    Task<TDTO?> GetByNameEntityDTO(string Name, string? extraCols = "");
    bool ValidateName(string Name, long? Id);
    Task<string> Save(TDTO entitydto, HttpContext context);
    Task Remove(TEntity entity);
    Task<string> Delete(string id, HttpContext context);
    Task DeleteRange(List<object> ids);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> Add(TEntity entity);
    Task AddRange(IEnumerable<TEntity> entities);
    Task<TEntity> Update(TEntity entity);
    Task UpdateRange(List<TEntity> entities);
    bool IsExist();
    Task<bool> IsExist(Expression<Func<TEntity, bool>> filter);
}

public class GenericRepository<TEntity, TDTO, TContext>(TContext context,
    ITenantProviderService _tenantService, IMapper _mapper) : IGenericRepository<TEntity, TDTO, TContext>
    where TEntity : class where TDTO : class where TContext : DbContext
{
    private DbSet<TEntity> _dbSet => context.Set<TEntity>();
    private DbSet<TDTO> _dbSetGetData => context.Set<TDTO>();
    public TContext Context => context;

    private string GetSqlEntityDTO(FilteringModel fltrModel, string? extraCols = "", string? WhrExtr = "")
    {
        var tableName = Context.Model.FindEntityType(typeof(TEntity)).GetTableName();

        Type EntityType = typeof(TEntity);
        bool hasName = EntityType.GetProperty("Name") != null;
        bool hasNameAr = EntityType.GetProperty("NameAr") != null;
        bool dtoHasName = typeof(TDTO).GetProperty("Name") != null;
        bool dtoHasNameAr = typeof(TDTO).GetProperty("NameAr") != null;

        string Cols = "Select ";
        if (hasName && hasNameAr)
        {
            Cols += "\"" + tableName + "\".\"NameAr\", \"" + tableName + "\".\"Name" + (_tenantService.GetLangAbbrv() == "English" ? "" : "Ar") + "\" AS \"Name\"";
        }
        else if (hasName)
        {
            if (dtoHasNameAr) Cols += "'' AS \"NameAr\", ";
            Cols += "\"" + tableName + "\".\"Name\" AS \"Name\"";
        }
        else
        {
            if (dtoHasNameAr) Cols += "'' AS \"NameAr\", ";
            if (dtoHasName) Cols += "'' AS \"Name\", ";
        }
        string Joins = "\r\nFrom \"public\".\"" + tableName + "\"";
        string WhrCond = "\r\nWhere \"" + tableName + "\".\"TenantId\" = '" + _tenantService.GetTenantId() + "'"; 
        string OrderBy = "\r\n Order By ";

        PropertyInfo[] entityCols = EntityType.GetProperties();
        for (int i = 0; i < entityCols.Length; i++)
        {
            if (entityCols[i].PropertyType.ToString().Contains("Aspire.Data") || entityCols[i].PropertyType.ToString().Contains("ICollection"))
                continue;

            if (entityCols[i].Name.ToUpper() == "NO" || entityCols[i].Name == "Name" || entityCols[i].Name == "NameAr")
                continue;
            
            string Comma = Cols == "Select " ? "\"" : ", \"";

            string FKTble = "";
            string Name = _tenantService.GetLangAbbrv() == "English" ? "Name" : "NameAr";

            Cols += Comma + tableName + "\".\"" + entityCols[i].Name + "\"";

            if (entityCols[i].Name.EndsWith("UsrId"))
            {
                string tbl = entityCols[i].Name.Substring(0, entityCols[i].Name.Length - 2);
                Cols += Comma + Many(tbl) + "\".\"" + Name + "\" As \"" + tbl + "Name\"";
                Cols += Comma + Many(tbl) + "\".\"PhotoUrl\" As \"" + tbl + "PhotoUrl\"";
                if (entityCols[i].Name == "ActionUsrId")
                    Cols += Comma + Many(tbl) + "\".\"Email\" As \"" + tbl + "Email\"";
                FKTble = "\"AspNetUsers\" AS \"" + Many(tbl) + "\"";
                Joins += "\r\nLeft Outer Join " + FKTble + " On \"" + Many(tbl) + "\".\"Id\" = \"" + tableName + "\".\"" + tbl + "Id\"";
            }
            else if (entityCols[i].Name.EndsWith("ParentId"))
            {
                Cols += Comma + "Parent\".\"" + Name + "\" As \"" + "ParentName\"";
                FKTble = "\"" + tableName + "\" AS \"Parent\"";
                Joins += "\r\nLeft Outer Join " + FKTble + " On \"Parent\".\"Id\" = \"" + tableName + "\".\"ParentId\"";
            }
            else if (entityCols[i].Name.EndsWith("Id") && !entityCols[i].Name.Equals("Id") && !entityCols[i].Name.Equals("ObjectId"))
            {
                string tbl = entityCols[i].Name.Substring(0, entityCols[i].Name.Length - 2);
                Cols += Comma + Many(tbl) + "\".\"" + Name + "\" As \"" + tbl + "Name\"";
                FKTble = "\"" + Many(tbl) + "\"";

                if (entityCols[i].Name.Contains("EntityStatusId") || entityCols[i].Name.Contains("EntityTypeId"))
                    Cols += Comma + Many(tbl) + "\".\"Color\" As \"" + tbl + "Color\"";

                Joins += "\r\nLeft Outer Join " + FKTble + " On \"" + Many(tbl) + "\".\"Id\" = \"" + tableName + "\".\"" + tbl + "Id\"";
            }
        }

        // Filtration => WhrCond
        if(WhrExtr != string.Empty)
            WhrCond += WhrExtr;
        else
            foreach (string key in fltrModel.FilterValues.Keys)
            {
                if (fltrModel.FilterValues[key] != null && fltrModel.FilterValues[key].ToString() == "0")
                {
                    //Ids (long)
                    var tblName = key.Contains(".") && !key.Contains(",") ? key.Split(".")[0] : tableName;
                    var colName = key.Contains(".") && !key.Contains(",") ? key.Split(".")[1] : key;
                    WhrCond += " AND \"" + tableName + "\".\"" + key + "\" IS NULL";
                }
                else if (fltrModel.FilterValues[key] != null && fltrModel.FilterValues[key].ToString() != "[]")
                {
                    string values = fltrModel.FilterValues[key].ToString().Replace("\"", "'").Replace("[", "(").Replace("]", ")");
                    var tblName = key.Contains(".") && !key.Contains(",") ? Many(key.Split(".")[0]) : tableName;
                    var colName = key.Contains(".") && !key.Contains(",") ? key.Split(".")[1] : key;

                    if (key.EndsWith("Date") || key.EndsWith("_val") || key.EndsWith("_num") || key.EndsWith("_cnt") || key.EndsWith("_sum") || key.EndsWith("_prc") || key.EndsWith("_ar") || key.EndsWith("_ap"))
                    {
                        string[] range = values.Replace("(", "").Replace(")", "").Replace("'", "").Split(",");
                        range[0] = range[0] == "null" ? (key.Contains("Date") ? "1900-01-01" : "0") : range[0];
                        range[1] = range[1] == "null" ? (key.Contains("Date") ? "2100-01-01" : "999999999") : range[1];

                        if (key.Contains("Date"))
                        {
                            DateTime dateFrom = DateTime.TryParse(range[0], null, System.Globalization.DateTimeStyles.RoundtripKind, out var df) ? df : new DateTime(1900, 1, 1);
                            DateTime dateTo = range.Length > 1 && DateTime.TryParse(range[1], null, System.Globalization.DateTimeStyles.RoundtripKind, out var dt) 
                                ? dt 
                                : (range[0] != "null" && DateTime.TryParse(range[0], null, System.Globalization.DateTimeStyles.RoundtripKind, out var dSingle) ? dSingle : new DateTime(2100, 1, 1));
                            WhrCond += " AND \"" + tblName + "\".\"" + colName + "\" Between '" + dateFrom.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dateTo.ToString("yyyy-MM-dd 23:59:59") + "'";
                        }
                        else
                        {
                            WhrCond += " AND \"" + tblName + "\".\"" + colName + "\" Between '" + range[0] + "' and '" + range[1] + "'";
                        }
                    }
                    else if (key.ToLower().EndsWith("name") || key.ToLower().EndsWith("namear"))
                    {// Search Textbox => One Value (Text)
                        string TextValue = values.Replace("(", "").Replace(")", "").Replace("'", "");
                        if(colName == "EntityName")
                            WhrCond += " AND \"" + tblName + "\".\"" + colName + "\" = '" + TextValue + "'";
                        else
                            WhrCond += " AND LOWER(\"" + tblName + "\".\"" + colName + "\") like '%" + TextValue.ToLower() + "%'";
                    }
                    else if (key.Contains(","))
                    {// Search Textbox => One Value (Text)
                        string[] keys = key.Split(",");
                        string TextValue = values.Replace("(", "").Replace(")", "").Replace("'", "");
                        string WhrCond_sub = " AND (";
                        for (int i = 0; i < keys.Length; i++)
                        {
                            var tblName_sub = key.Contains(".") ? key.Split(".")[0] : tableName;
                            var colName_sub = key.Contains(".") ? key.Split(".")[1] : keys[i];

                            WhrCond_sub += "\"" + tblName_sub + "\".\"" + colName_sub + "\" like '%" + TextValue + "%'";
                            WhrCond_sub += i < keys.Length - 1 ? " OR " : ")";
                        }

                        WhrCond += WhrCond_sub;
                    }
                    else if (tblName == "LineItems" && tableName != "LineItems")
                        WhrCond += " AND \"" + tableName + "\".\"Id\" IN (Select \"ObjectId\" From \"LineItems\" Where \"" + colName + "\" IN " + values + ")";
                    else if (values.StartsWith("(") && values.EndsWith(")"))
                        WhrCond += " AND \"" + tblName + "\".\"" + colName + "\" IN " + values;
                    else
                        WhrCond += " AND \"" + tblName + "\".\"" + colName + "\" = '" + values + "'";
                }
            }

        // Filtration (Where Clause)
        string ParentName = string.Empty; string ParentId = string.Empty;
        if(ParentName != string.Empty && ParentId != string.Empty)
            WhrCond += " AND \"" + tableName + "\".\"" + UpperFirstChar(ParentName) + "Id\" = '" + ParentId + "'";

        string? tblNm = fltrModel.SortBy.Contains(".") ? fltrModel.SortBy.Split(".")[0] : tableName;
        string? srtBy = fltrModel.SortBy.Contains(".") ? fltrModel.SortBy.Split(".")[1] : fltrModel.SortBy;
        srtBy += srtBy.EndsWith("Name") ? (_tenantService.GetLangAbbrv() == "English" ? "" : "Ar") : "";
        // Sorting (Order By Clause)
        OrderBy += "\"" + tblNm + "\".\"" + (srtBy ?? "Id") + "\"" + (fltrModel.SortDescending ? " DESC" : " ASC");

        if (extraCols != string.Empty)
            extraCols = " ," + extraCols;

        string RecordNo = ", '' AS \"No\"";
        if(tableName != "UserPreferences" && tableName != "ServiceCatalogues" && tableName != "AspNetRoles" && tableName != "AspNetUsers")
        {
            string? tblName = "";
            tblName = tableName == "AspNetUsers" ? "ApplicationUsers" : tableName;
            tblName = tableName == "AspNetRoles" ? "ApplicationRoles" : tableName;
            var Abbrv = context.Database.SqlQueryRaw<EntityAbbrvDTO>("Select \"Abbrv\" From \"Entities\" Where \"Name\" = '" + Single(tblName) + "'");
            if (Abbrv != null)
                if (Abbrv.ToList().Count == 1)
                    RecordNo = ", Concat('" + Abbrv.ToList()[0].Abbrv + "-', LPad(CAST(\"" + tableName + "\".\"Id\" AS VARCHAR), 7, '0')) AS \"No\"";
                else
                    RecordNo = ", Concat('***-', LPad(CAST(\"" + tableName + "\".\"Id\" AS VARCHAR), 7, '0')) AS \"No\"";
        }

        return Cols + extraCols + RecordNo + Joins + WhrCond + OrderBy;
    }

    private static string UpperFirstChar(string str)
    {
        return str.Substring(0, 1).ToUpper() + str.Substring(1);
    }

    private string GetSqlEntity(FilteringModel fltrModel)
    {
        var tableName = Context.Model.FindEntityType(typeof(TEntity)).GetTableName();

        string Cols = "Select * ";
        string Joins = "\r\nFrom \"public\".\"" + tableName + "\"";
        string WhrCond = "\r\nWhere \"" + tableName + "\".\"TenantId\" = '" + _tenantService.GetTenantId() + "'";
        string OrderBy = "\r\n Order By \"";

        // Filtration
        foreach (FilterField item in fltrModel.FilterFields)
        {
            if (item.Type == "text")
            {
                if (item.SimilarityType == SimilarityTypeEnum.Exact)
                {
                    string joinedVals = string.Join("','", item.Values);
                    WhrCond += " AND \"" + tableName + "\".\"" + item.Name + "\" IN ('" + joinedVals + "')";
                }
                else if (item.SimilarityType == SimilarityTypeEnum.Like)
                {
                    WhrCond += " AND \"" + tableName + "\".\"" + item.Name + "\" Like '%" + item.Values[0] + "%'";
                }
                else if (item.SimilarityType == SimilarityTypeEnum.NotEqual)
                {
                    WhrCond += " AND \"" + tableName + "\".\"" + item.Name + "\" <> '" + item.Values[0] + "'";
                }

            }
            else if (item.Type == "number" || item.Type == "date")
            {
                string joinedVals = "Between '" + item.Values[0] + "' and '" + item.Values[1] + "'";
                WhrCond += " AND \"" + tableName + "\".\"" + item.Name + "\" " + joinedVals;
            }
        }

        OrderBy += (fltrModel.SortBy ?? "Id") + "\"" + (fltrModel.SortDescending ? " DESC" : " ASC");

        return Cols + Joins + WhrCond + OrderBy;
    }

    private string Many(string Single)
    {
        if(Single.EndsWith("y"))
            return Single.Substring(0, Single.Length - 1) + "ies";
        else if (Single.EndsWith("s"))
            return Single + "es";
        return Single + "s";
    }

    private string Single(string Many)
    {
        if (Many.EndsWith("ies"))
            return Many.Substring(0, Many.Length - 3) + "y";
        else if (Many.EndsWith("ses"))
            return Many.Substring(0, Many.Length - 2);
        else if (Many.EndsWith("s"))
            return Many.Substring(0, Many.Length - 1);
        else
            return Many;
    }

    public ListsResponse GetLists(ListsRequest model)
    {
        try
        {
            long? tenantId = _tenantService.GetTenantId();
            ListsResponse result = new();
            string tableName = Many(model.Entity);
            string WhrCond = "\r\nWhere \"" + tableName + "\".\"TenantId\" = '" + tenantId + "'";

            if(!string.IsNullOrEmpty(model.SearchQuery))
                WhrCond += " And \"" + tableName + "\".\"Name\" Like '%" + model.SearchQuery + "%'";

            if (!string.IsNullOrEmpty(model.WhrCond))
                if (model.Entity == "EntityAttribute")
                    WhrCond += " And \"EntityId\" = '" + model.WhrCond + "'";

            string name = model.Entity == "EntityAttribute" ? "Concat(\"Name\", ' [', \"DataType\", ']')" : "\"Name\"";
            string sqlQuery = "Select CAST(\"Id\" AS VARCHAR), " + name + " As \"Value\" From \"public\".\"" + tableName + "\"" + WhrCond;

            if(!string.IsNullOrEmpty(model.SelectedId))
            {
                string sqlQuery1 = "Select CAST(\"Id\" AS VARCHAR), " + name + " As \"Value\" From \"public\".\"" + tableName + "\" Where \"Id\" = '" + model.SelectedId + "'";
                sqlQuery = sqlQuery1 + " UNION " + sqlQuery;
            }

            var entities = context.Database.SqlQueryRaw<UnifiedListDTO>(sqlQuery);
            var entities_sorted = entities.OrderBy(e => e.Value);
            result.Options = entities_sorted.Skip(model.First / model.PageSize * model.PageSize).Take(model.PageSize).ToList();

            return result;
        }
        catch (Exception ex)
        {
            string Msg = ex.Message;
            return null;
        }
    }

    public OptionsResponse GetOptions(OptionsRequest model)
    {
        try
        {
            long? tenantId = _tenantService.GetTenantId();
            OptionsResponse result = new();
            string tableName = "";
            if(model.Field.Count(f => f == '.') == 1)
            {
                model.EntityName = model.Field.Split('.')[0];
                model.Field = model.Field.Split('.')[1];
            }
                
            tableName = Many(model.Field.Remove(model.Field.Length - 2));
            string WhrCond = "\r\nWhere \"" + tableName + "\".\"TenantId\" = '" + tenantId + "'";
            string Cols_Company = "Null AS \"CustomerId\", '' AS \"CustomerName\"";
            string Cols_Status = "'' AS \"Color\"";
            string Cols_User = "'' AS \"PhotoUrl\"";
            string Joins = "";

            if(model.Field == "ParentId")
            {
                tableName = Many(model.EntityName);
                WhrCond = "\r\nWhere \"" + tableName + "\".\"ParentId\" < 0 AND \"TenantId\" = '" + tenantId + "'";
            }
            else if (model.Field == "EntityStatusId")
            {
                Cols_Status = "\"EntityStatuses\".\"Color\" AS \"Color\"";
                WhrCond += " And Lower(\"EntityStatuses\".\"EntityName\") = '" + model.EntityName.ToLower() + "'";
            }
            else if (model.Field == "EntityTypeId")
            {
                WhrCond += " And Lower(\"EntityTypes\".\"EntityName\") = '" + model.EntityName.ToLower() + "'";
            }
            if (model.Field == "AgreementId")
            {
                Cols_Company = "\"Customers\".\"Id\" AS \"CustomerId\", \"Customers\".\"Name\" AS \"CustomerName\"";
                Joins = "\r\nLeft Outer Join \"Customers\" On \"Customers\".\"Id\" = \"Agreements\".\"CustomerId\"";
            }
            else if (model.Field.EndsWith("UsrId") || model.Field.EndsWith("UserId") || model.Field == "AssignedToId" || model.Field == "AssignedTo" || model.Field == "AssignedToUsr")
            {
                tableName = "AspNetUsers";
                WhrCond = "\r\nWhere \"AspNetUsers\".\"Id\" IN (Select \"UserId\" From \"ApplicationUserTenants\" Where \"TenantId\" = '" + tenantId + "')";
            }
            else if (model.Field.EndsWith("_val"))
            {
                tableName = "Currency";
            }

            // Search box of the dropdown: case-insensitive "contains" on the option name. The text is a SQL parameter
            // (never concatenated) and LIKE wildcards typed by the user are escaped.
            object[] searchParams = [];
            if (!string.IsNullOrWhiteSpace(model.SearchQuery))
            {
                if (tableName == "AspNetUsers")
                {
                    WhrCond += " And (\"AspNetUsers\".\"Name\" ILIKE {0} Or \"AspNetUsers\".\"UserName\" ILIKE {0} Or \"AspNetUsers\".\"FirstName\" ILIKE {0} Or \"AspNetUsers\".\"LastName\" ILIKE {0})";
                }
                else
                {
                    WhrCond += " And \"" + tableName + "\".\"Name\" ILIKE {0}";
                }
                searchParams = ["%" + model.SearchQuery.Trim().Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_") + "%"];
            }

            string valueCol = tableName == "AspNetUsers" 
                ? "COALESCE(NULLIF(\"AspNetUsers\".\"Name\", ''), NULLIF(\"AspNetUsers\".\"UserName\", ''), CONCAT(\"AspNetUsers\".\"FirstName\", ' ', \"AspNetUsers\".\"LastName\"))" 
                : "\"" + tableName + "\".\"Name\"";
            string Cols = "CAST(\"" + tableName + "\".\"Id\" AS VARCHAR), " + valueCol + " AS \"Value\", " + Cols_Company + ", " + Cols_Status + ", " + Cols_User;
            string sqlQuery = "Select " + Cols + " From \"public\".\"" + tableName + "\"" + Joins + WhrCond;
            var entities = context.Database.SqlQueryRaw<UnifiedDTO>(sqlQuery, searchParams);
            var entities_sorted = entities.OrderBy(e => e.Value);
            result.Options = entities_sorted.Skip(model.First / model.PageSize * model.PageSize).Take(model.PageSize).ToList();

            return result;
        }
        catch (Exception ex)
        {
            string Msg = ex.Message;
            return null;
        }
    }

    public async Task<TEntity?> GetByIdEntity(string Id)
    {
        var entityName = Context.Model.FindEntityType(typeof(TEntity));
        var tableName = entityName.GetTableName();

        FilteringModel? fltrModel = new FilteringModel();
        fltrModel.FilterFields.Add(new FilterField("Id", FieldType.Text, Id));
        var entities = context.Database.SqlQueryRaw<TEntity>(GetSqlEntity(fltrModel));
        var items = entities.ToList();
        return items[0];
    }

    public async Task<TDTO?> GetByIdEntityDTO(string Id, string? extraCols = "")
    {
        var entityName = Context.Model.FindEntityType(typeof(TEntity));
        var tableName = entityName.GetTableName();
        FilteringModel? fltrModel = new FilteringModel();
        string WhrCond = "\r\nAND \"" + tableName + "\".\"Id\" = '" + Id + "'";
        string Sql = GetSqlEntityDTO(fltrModel, extraCols: extraCols, WhrExtr: WhrCond);
        var entities = context.Database.SqlQueryRaw<TDTO>(Sql);
        var items = entities.ToList();
        return items[0];
    }

    public async Task<TDTO?> GetByNameEntityDTO(string Name, string? extraCols = "")
    {
        var entityName = Context.Model.FindEntityType(typeof(TEntity));
        var tableName = entityName.GetTableName();
        FilteringModel? fltrModel = new FilteringModel();
        string WhrCond = "\r\nAND \"" + tableName + "\".\"Name\" = '" + Name + "'";
        string Sql = GetSqlEntityDTO(fltrModel, extraCols: extraCols, WhrExtr: WhrCond);
        var entities = context.Database.SqlQueryRaw<TDTO>(Sql);
        var items = entities.ToList();
        return items[0];
    }

    public async Task<PaginationModel<TDTO>> GetRootsEntityDTO(string? extraCols = "")
    {
        var entityName = Context.Model.FindEntityType(typeof(TEntity));
        var tableName = entityName.GetTableName();
        FilteringModel? fltrModel = new FilteringModel();
        string WhrCond = "\r\nAND \"" + tableName + "\".\"ParentId\" Is NULL";
        string Sql = GetSqlEntityDTO(fltrModel, extraCols: extraCols, WhrExtr: WhrCond);
        var entities = context.Database.SqlQueryRaw<TDTO>(Sql);

        // Pagination
        int totalItems = entities.Count();
        return new PaginationModel<TDTO>(entities.ToList(), 1, 999, totalItems, null);

    }

    public async Task<PaginationModel<TDTO>> GetByParentIdEntityDTO(string Id, string? extraCols = "")
    {
        var entityName = Context.Model.FindEntityType(typeof(TEntity));
        var tableName = entityName.GetTableName();
        FilteringModel? fltrModel = new FilteringModel();
        string WhrCond = "\r\nAND \"" + tableName + "\".\"ParentId\" = '" + Id + "'";
        string Sql = GetSqlEntityDTO(fltrModel, extraCols: extraCols, WhrExtr: WhrCond);
        var entities = context.Database.SqlQueryRaw<TDTO>(Sql);

        // Pagination
        int totalItems = entities.Count();
        return new PaginationModel<TDTO>(entities.ToList(), 1, 999, totalItems, null);

    }

    public bool ValidateName(string Name, long? Id)
    {
        var entityName = Context.Model.FindEntityType(typeof(TEntity));
        var tableName = entityName.GetTableName();

        FilteringModel? fltrModel = new FilteringModel();
        fltrModel.FilterFields.Add(new FilterField("Id", FieldType.Text, Id.ToString()));
        fltrModel.FilterFields.Add(new FilterField("Name", FieldType.Text, Name));
        var entities = context.Database.SqlQueryRaw<TEntity>(GetSqlEntity(fltrModel));

        return entities.Count() > 0;
    }

    public async Task<PaginationModel<TDTO>> GetAllJoins(FilteringModel? fltrModel = null, bool? applyPagination = true, string? query = "", bool? applyTenantQuery = true, string? extraCols = "")
    {
        if (fltrModel == null) fltrModel = new();

        var entityName = Context.Model.FindEntityType(typeof(TEntity));
        var schema = entityName.GetSchema() ?? "public";
        var tableName = entityName.GetTableName();

        string Sql = GetSqlEntityDTO(fltrModel, extraCols: extraCols);
        var entities = context.Database.SqlQueryRaw<TDTO>(Sql);

        // Aggregations
        var aggregates = new Dictionary<string, decimal>();
        if (fltrModel?.AggregateFields != null && fltrModel?.AggregateFields.Any() == true)
        {            
            foreach (var field in fltrModel.AggregateFields)
            {
                decimal result = 0;
                switch (field.Function.ToLower())
                {
                    case "sum":
                        result = await entities.SumAsync(e => EF.Property<decimal>(e, field.Name));
                        break;
                    case "count":
                        result = await entities.CountAsync();
                        break;
                    case "min":
                        result = await entities.MinAsync(e => EF.Property<decimal>(e, field.Name));
                        break;
                    case "max":
                        result = await entities.MaxAsync(e => EF.Property<decimal>(e, field.Name));
                        break;
                    case "avg":
                        result = await entities.AverageAsync(e => EF.Property<decimal>(e, field.Name));
                        break;
                    default:
                        throw new InvalidOperationException($"Unsupported aggregation function: {field.Function}");
                }

                aggregates.Add(field.Name, result);
            }
        }

        // Pagination
        int totalItems = entities.Count();
        entities = entities.Skip((fltrModel.PageNumber - 1) * fltrModel.PageSize).Take(fltrModel.PageSize);
        return new PaginationModel<TDTO>(entities.ToList(), fltrModel?.PageNumber ?? 1, fltrModel?.PageSize ?? 10, totalItems, aggregates);
    }

    public bool IsExist()
    {
        return _dbSet.Any();
    }

    public async Task Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
        await Save();
    }

    public async Task<string> Delete(string id, HttpContext context)
    {
        try
        {
            string Msg = "error:Access:Access_Denied";
            if (_tenantService.IsAllowed(context))
            {
                var entity = await GetByIdEntity(id);
                Msg = "error:Access:Item_Not_Found";
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    await Save();
                    Msg = "success:Info:Item_Deleted";
                }
            }

            return Msg;
        }
        catch (Exception ex)
        {
            return "error:Server Exception:" + ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        }
    }
    public async Task DeleteRange(List<object> ids)
    {
        if (ids == null || !ids.Any())
            return;

        var entityType = Context.Model.FindEntityType(typeof(TEntity));
        var keyProperty = entityType?.FindPrimaryKey()?.Properties.FirstOrDefault()?.Name;

        if (keyProperty == null)
            throw new InvalidOperationException("Unable to determine the primary key property for the entity.");

        await _dbSet
            .Where(e => ids.Contains(EF.Property<string>(e, keyProperty)))
            .ExecuteDeleteAsync();
    }
    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }
    public async Task<TEntity> Add(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await Save();
        return entity;
    }
    public async Task<string> Save(TDTO entitydto, HttpContext httpContext)
    {
        try
        {
            TEntity entity = _mapper.Map<TEntity>(entitydto);
            long? tenantId = _tenantService.GetTenantId();
            long? userId = _tenantService.GetUserId();
            string Msg = "error:Access:Access_Denied";

            if (_tenantService.IsAllowed(httpContext))
            {
                //if (ValidateName(entity.Name ?? "", entity.Id))
                //    return "error:Validation:Name_Exist";

                if (((IAuditableEntity)entity).Id == long.MinValue || ((IAuditableEntity)entity).Id == 0)
                {
                    ((IAuditableEntity)entity).TenantId = tenantId;
                    ((IAuditableEntity)entity).CreatedByUsrId = userId;
                    ((IAuditableEntity)entity).UpdatedByUsrId = userId;
                    ((IAuditableEntity)entity).CreateDate = DateTime.UtcNow;

                    await _dbSet.AddAsync(entity);
                    Msg = "success:Info:Item_Created:";

                    //var entityName = Context.Model.FindEntityType(typeof(TEntity));
                    //var tableName = entityName.GetTableName();
                    
                    //Entity? Ent = context.Database.SqlQueryRaw<Entity>("Select * From Entity Where Name = '" + tableName + "'").FirstOrDefault();
                    //entity.No = Ent.Abbrv + "-" + entity.Id.ToString().PadLeft(7, '0');
                }
                else
                {
                    ((IAuditableEntity)entity).TenantId = tenantId;
                    ((IAuditableEntity)entity).UpdatedByUsrId = userId;
                    ((IAuditableEntity)entity).UpdateDate = DateTime.UtcNow;

                    _dbSet.Update(entity);
                    Msg = "success:Info:Item_Updated:";
                }

                await Save();
            }

            return Msg + ((IAuditableEntity)entity).Id.ToString() + ":" + ((IAuditableEntity)entity).TenantId;
        }
        catch (Exception ex)
        {
            return "error:Server Exception:" + ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        }
    }

    public async Task AddRange(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        await Save();
    }
    public async Task<TEntity> Update(TEntity entity)
    {
        _dbSet.Update(entity);
        await Save();
        return entity;
    }
    public async Task UpdateRange(List<TEntity> entities)
    {
        //await context.BulkUpdateAsync(entities);
    }

    public async Task<bool> IsExist(Expression<Func<TEntity, bool>> filter)
    {
        return await _dbSet.AnyAsync(filter);
    }

    private async Task Save()
    {
        await context.SaveChangesAsync();
    }
}
