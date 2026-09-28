using Aspire.Context;
using Aspire.Data;
using Aspire.Data;
using Aspire.Utilities;
using IdentityModel;
using System.Data;
using System.Security.Claims;

namespace Aspire.Services;

public class StakeholdersService()
{
    public static List<ApplicationUser> Stakeholders(ProjectDTO entity, ApplicationDbContext context)
    {
        List<ApplicationUser> StkHldrs = context.ApplicationUsers.ToList();

        return StkHldrs;
    }

    public static List<ApplicationUser> Stakeholders(DealDTO entity, ApplicationDbContext context)
    {
        List<ApplicationUser> StkHldrs = context.ApplicationUsers.ToList();

        return StkHldrs;
    }

    public static List<ApplicationUser> Stakeholders(EmployeeDTO entity, ApplicationDbContext context)
    {
        List<ApplicationUser> StkHldrs = context.ApplicationUsers.ToList();

        return StkHldrs;
    }

    public static List<ApplicationUser> Stakeholders(TeamDTO entity, ApplicationDbContext context)
    {
        List<ApplicationUser> StkHldrs = context.ApplicationUsers.ToList();

        return StkHldrs;
    }

    public static List<ApplicationUser> Stakeholders(AuditDTO entity, ApplicationDbContext context)
    {
        List<ApplicationUser> StkHldrs = context.ApplicationUsers.ToList();

        return StkHldrs;
    }

    public static List<ApplicationUser> Stakeholders(AssessmentDTO entity, ApplicationDbContext context)
    {
        List<ApplicationUser> StkHldrs = context.ApplicationUsers.ToList();

        return StkHldrs;
    }

    public static List<ApplicationUser> Stakeholders(WorkOrderDTO entity, ApplicationDbContext context)
    {
        List<ApplicationUser> StkHldrs = context.ApplicationUsers.ToList();

        return StkHldrs;
    }

    // Code-Player-Inject-NewClaim
}
