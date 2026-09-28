using Aspire.Context;
using Aspire.Data;
using Aspire.Utilities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Claims;
using static System.Collections.Specialized.BitVector32;

namespace Aspire.Services;

public interface IAppTenant<TEntity>
{
    void CreateFrameWork(string cat);
    void UpdateEntitiesTable();
}

public class AppTenant<TEntity>(ApplicationDbContext context,
    IEntityRepository<ApplicationDbContext> genericRepository) : IAppTenant<TEntity>
{
    private static string ASPTmpltPath = @"C:\Aspire\Templates\asp\";
    private static string VUETmpltPath = @"C:\Aspire\Templates\vue\";
    private static string ASPSrcCodePath = @"C:\Aspire\apis\src\";
    private static string VUESrcCodePath = @"C:\Aspire\Aspire-Frontend\";

    public void CreateFrameWork(string FrameworkName)
    {
        Framework f = context.Frameworks.Where(x => x.Name == FrameworkName).First();

        List<FrameworkHelper> lst = context.FrameworkHelpers.Where(x => x.Cat == FrameworkName).OrderBy(w => w.Key).ToList();
        foreach (FrameworkHelper h in lst.Where(x => !x.Key.Contains("-")))
            context.FrameworkItems.Add(new FrameworkItem(f.TenantId, f.Id, h.Key.Remove(0, 2).Trim(), null, h.Value, "Domain"));

        context.SaveChanges();

        for (int i = 1; i <= 5; i++)
        {
            foreach (FrameworkHelper h in lst.Where(x => x.Key.Count(c => c == '-') == i))
            {
                string Key = h.Key.Remove(0, 2);
                string? O = Key.EndsWith("-O") ? h.Value : null; // Objective
                string? T = Key.EndsWith("-T") ? h.Value : null; // Tools
                string? G = Key.EndsWith("-G") ? h.Value : null; // Guidelines
                string? D = Key.EndsWith("-D") ? h.Value : null; // Deliverables
                string? P = Key.EndsWith("-P") ? h.Value : null; // Principal

                FrameworkItem? prnt = context.FrameworkItems.Where(x => x.Key == Key.Substring(0, Key.LastIndexOf("-"))).FirstOrDefault();

                if (Key.EndsWith("-O") || Key.EndsWith("-T") || Key.EndsWith("-G") || Key.EndsWith("-D") || Key.EndsWith("-P"))
                {
                    prnt.Objective = O;
                    prnt.Tools = T;
                    prnt.Guidelines = G;
                    prnt.Deliverables = D;
                    prnt.Principal = P;
                    context.FrameworkItems.Update(prnt);
                }
                else
                {
                    string type = i == 1 ? "Sub-Domain" : "Control";
                    FrameworkItem fi = new FrameworkItem(f.TenantId, f.Id, Key.Trim(), prnt.Id, h.Value, type);
                    context.FrameworkItems.Add(fi);
                    context.SaveChanges();
                }
            }
        }

        context.SaveChanges();

        CreateAudit(f.Id);
    }

    private void CreateAudit(long FrameworkId)
    {
        Framework f = context.Frameworks.Where(x => x.Id == FrameworkId).First();
        Audit a = new Audit(f.TenantId, f.Name, f.Id);
        context.Audits.Add(a);
        context.SaveChanges();

        Dictionary<long, long?> AudMap = new Dictionary<long, long?>();
        List<FrameworkItem> itms = context.FrameworkItems.Where(x => x.FrameworkId == FrameworkId).OrderBy(x => x.Id).ToList();
        foreach (FrameworkItem itm in itms)
        {
            AuditItem AutItm;
            FrameworkItem? prnt = context.FrameworkItems.Where(x => x.Id == itm.ParentId).FirstOrDefault();
            if(prnt == null)
                AutItm = new AuditItem(f.TenantId, itm, a.Id, null);
            else
                AutItm = new AuditItem(f.TenantId, itm, a.Id, AudMap[prnt.Id]);

            context.AuditItems.Add(AutItm);
            context.SaveChanges();

            AudMap.Add(itm.Id, AutItm.Id);
        }
    }

    public void UpdateEntitiesTable()
    {
        Type EntityType = typeof(TEntity);
        var Attrs = (Attr[])EntityType.GetCustomAttributes(typeof(Attr), true);
        long? TenantId = genericRepository.GetTenantId();

        List<Entity> ent = genericRepository.GetEntities(EntityType.Name, TenantId.ToString());
        if (ent.Count == 0)
        {
            string Claims = "create,edit,delete,getall,edit statuses,edit types";
            string type = Attrs[0].Lookup ? "Lookup" : Attrs[0].Service ? "Service" : Attrs[0].Trans ? "Transactional" : "Business";
            if (type == "Lookup")
                Claims = "create,edit,delete,activate,deactivate";
            else if (type == "Service")
                Claims = "Submit";
            else if (type == "Transactional")
                Claims = "Submit";

            if(Attrs[0].Actions is not null)
            {
                for (int i = 0; i < Attrs[0].Actions.Length; i++)
                {
                    Claims += "," + Attrs[0].Actions[i].Split(":")[0].Trim().Replace(" ", "_").Replace("-", "");
                }
            }

            context.Entities.Add(new Entity(TenantId, Claims, Attrs[0].DomainFolder, type, Attrs[0].Abbrv, EntityType.Name, Attrs[0].NameAr, Attrs[0].Description, Attrs[0].DescriptionAr));
            context.SaveChanges();

            long EntId = context.Entities.Where(e => e.Name == EntityType.Name).FirstOrDefault().Id;

            // Add Entity Attributes
            DTOs_VueTypes(Attrs[0].DomainFolder, TenantId, EntId);
        }
    }

    private string DTOs_VueTypes(string fldr, long? TenantId = null, long? EntityId = null)
    {
        Type EntityType = typeof(TEntity);
        PropertyInfo[] entityTypes = EntityType.GetProperties();
        var EntAttrs = (Attr[])EntityType.GetCustomAttributes(typeof(Attr), true);

        string Prop = "";
        if (EntAttrs[0].ShowStakeholders)
        {
            Prop += "public class " + EntityType.Name + "TeamDTO : " + EntityType.Name + "DTO\r\n{\r\n"; // TeamDTO Prop
            Prop += "    public List<TeamMemberAvatarDTO> Stakeholders { get; set; } = new List<TeamMemberAvatarDTO>();\r\n}\r\n\r\n";
        }

        Prop += "public class " + EntityType.Name + "DTO\r\n{"; // DTO Prop
        string Type = "export interface " + EntityType.Name + " {"; // TS Prop
        string PoplObj = "";

        string[] arPropName = new string[entityTypes.Length];

        for (int i = 0; i < entityTypes.Length; i++)
        {
            bool DuplicateProp = false;
            for (int j = 0; j < arPropName.Length; j++)
                if (entityTypes[i].Name == arPropName[j])
                {
                    DuplicateProp = true;
                    break;
                }

            if (DuplicateProp)
                continue;

            string DataType = entityTypes[i].PropertyType.ToString();
            string DTOType = "string"; string VueType = "string";
            string PropName = entityTypes[i].Name;
            arPropName[i] = PropName;

            if (entityTypes[i].PropertyType.ToString().Contains("Aspire.Data") || entityTypes[i].PropertyType.ToString().Contains("ICollection"))
                continue;

            if (entityTypes[i].Name.ToLower() == "score" || entityTypes[i].Name.ToLower() == "ctrls")
            {
                int x = 0;
            }

            Type propertyType = entityTypes[i].PropertyType;
            bool IsAllowNull = !propertyType.IsValueType || (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>));
            string[] arIgnore = new string[] { "Id", "TenantId", "OrganizationId", "No", "Msg", "CreatedByUsrId", "UpdatedByUsrId", "CreateDate", "UpdateDate" };
            bool IsIgnore = arIgnore.Contains(entityTypes[i].Name);

            if (IsAllowNull && !IsIgnore)
                PoplObj += "        obj." + entityTypes[i].Name + " = entity." + entityTypes[i].Name + " is null ? obj." + entityTypes[i].Name + " : entity." + entityTypes[i].Name + ";\r\n";

            if (DataType.Contains("Int64"))
            {
                VueType = "number";
                DTOType = "long";
            }
            else if (DataType.Contains("Int32"))
            {
                VueType = "number";
                DTOType = "int";
            }
            else if (DataType.Contains("Decimal"))
            {
                VueType = "number";
                DTOType = "decimal";
            }
            else if (DataType.Contains("Float"))
            {
                VueType = "number";
                DTOType = "float";
            }
            else if (DataType.Contains("Double"))
            {
                VueType = "number";
                DTOType = "double";
            }
            else if (DataType.Contains("String"))
            {
                VueType = "string";
                DTOType = "string";
            }
            else if (DataType.Contains("DateTime"))
            {
                VueType = "string";
                DTOType = "DateTime";
            }
            else if (DataType.Contains("DateOnly"))
            {
                VueType = "string";
                DTOType = "DateOnly";
            }
            else if (DataType.Contains("TimeOnly"))
            {
                VueType = "string";
                DTOType = "TimeOnly";
            }
            else if (DataType.Contains("Boolean"))
            {
                VueType = "boolean";
                DTOType = "bool";
            }

            Prop += "\r\n    public " + DTOType + "? " + PropName + " { get; set; }";
            Type += "\r\n    " + PropName + ": " + VueType + ";";

            // One More Prop for FK Entity Name
            string FKEntName = entityTypes[i].Name.Substring(0, entityTypes[i].Name.Length - 2);
            if (entityTypes[i].Name.EndsWith("Id") && !entityTypes[i].Name.Equals("Id") && !entityTypes[i].Name.Equals("ObjectId"))
            {
                PropName = FKEntName + "Name";
                Prop += "\r\n    public string? " + PropName + " { get; set; }";
                Type += "\r\n    " + PropName + ": string;";
            }

            // One More Prop for Users PhotoUrl + Status Color
            if (entityTypes[i].Name.EndsWith("UsrId"))
            {
                Prop += "\r\n    public string? " + FKEntName + "PhotoUrl { get; set; }";
                Type += "\r\n    " + FKEntName + "PhotoUrl: string;";
            }
            else if (entityTypes[i].Name.EndsWith("EntityStatusId"))
            {
                Prop += "\r\n    public string? " + FKEntName + "Color { get; set; }";
                Type += "\r\n    " + FKEntName + "Color: string;";
            }
            else if (entityTypes[i].Name.EndsWith("EntityTypeId"))
            {
                Prop += "\r\n    public string? " + FKEntName + "Color { get; set; }";
                Type += "\r\n    " + FKEntName + "Color: string;";
            }

            context.EntityAttributes.Add(new EntityAttribute(TenantId, EntityId, PropName, DTOType));
        }

        Prop = Prop + "\r\n}";
        Type = Type + "\r\n}";

        context.SaveChanges();

        return PoplObj;
    }
}