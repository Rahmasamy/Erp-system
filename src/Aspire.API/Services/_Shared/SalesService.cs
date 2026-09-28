using Aspire.Context;
using Aspire.Data;
using System.Data;

namespace Aspire.Services;

public class SalesService(ApplicationDbContext context)
{
    public void TimeSheet_Approval(TimeSheet Obj)
    {
        Employee Emp = context.Employees.Where(x => x.AppUsrId == Obj.AssignedToUsrId).ToList().First();
        decimal EmpHourRate = Emp.TotalCost_val / Emp.WorkingHours_dec;
        //Obj.RegularCost_val = Obj.RegularApproved ? Obj.RegularHours_dec * EmpHourRate : 0;
        //Obj.OverTimeCost_val = Obj.OverTimeApproved ? Obj.OverTimeHours_dec * EmpHourRate : 0;
        //Obj.WeekEndCost_val = Obj.WeekEndApproved ? Obj.WeekEndHours_dec * EmpHourRate : 0;

        //Obj.TotalHours_dec = Obj.RegularHours_dec + Obj.OverTimeHours_dec + Obj.WeekEndHours_dec;
        //Obj.TotalCost_val = Obj.RegularCost_val + Obj.OverTimeCost_val + Obj.WeekEndCost_val;

        //WorkItem WI = context.WorkItems.Where(x => x.Id == Obj.AssignedToId).ToList().First();
        //if(WI.ActualEffort == 0 && Obj.TotalHours_dec > 0)
        //    WI.ActualStartDate = DateTime.Today;

        //if (Obj.Progress_prc == 100)
        //{
        //    WI.ActualEndDate = DateTime.Today;
        //    //WI.ActualDuration = DateTime. DateTime.Today - WI.ActualStartDate;
        //}

        //WI.ActualEffort += Obj.TotalHours_dec;
        //WI.ActualCost_val += Obj.TotalCost_val;

        //LineItem LI = context.LineItems.Where(x => x.Id == WI.LineItemId).ToList().First();
        //LI.calc_TotalCost_val += Obj.TotalCost_val;

        //Project Prj = context.Projects.Where(x => x.Id == LI.ObjectId && LI.EntityName == "Project").ToList().First();
        //Prj.calc_TotalCost_val += Obj.TotalCost_val;

        //Agreement Agr = context.Agreements.Where(x => x.Id == Prj.AgreementId).ToList().First();
        //Agr.calc_TotalCost_val += Obj.TotalCost_val;

        //Programme Prg = context.Programmes.Where(x => x.Id == Prj.ProgrammeId).ToList().First();
        //Prg.calc_TotalCost_val += Obj.TotalCost_val;

        //Portfolio Prf = context.Portfolios.Where(x => x.Id == Prj.PortfolioId).ToList().First();
        //Prf.calc_TotalCost_val += Obj.TotalCost_val;
    }

    public void InvoiceAR_Posting(InvoiceAR Obj)
    {
        List<LineItem> LIs = context.LineItems.Where(x => x.ObjectId == Obj.Id && x.EntityName == "InvoiceAR").ToList();
        foreach (LineItem LI in LIs)
            Obj.calc_Billed_val += LI.calc_Amount_val;

        Project Prj = context.Projects.Where(x => x.Id == Obj.ProjectId).ToList().First();
        Prj.calc_Billed_val += Obj.calc_Billed_val;

        Agreement Agr = context.Agreements.Where(x => x.Id == Prj.AgreementId).ToList().First();
        Agr.calc_Billed_val += Obj.calc_Billed_val;

        Programme Prg = context.Programmes.Where(x => x.Id == Prj.ProgrammeId).ToList().First();
        Prg.calc_Billed_val += Obj.calc_Billed_val;

        Portfolio Prf = context.Portfolios.Where(x => x.Id == Prj.PortfolioId).ToList().First();
        Prf.calc_Billed_val += Obj.calc_Billed_val;
    }


    public void Project(Project Obj)
    {
        CalcSales calc = new CalcSales();

        List<LineItem> LIs = context.LineItems.Where(x => x.ObjectId == Obj.Id && x.EntityName == "Project").ToList();
        foreach (LineItem SubObj in LIs)
        {
            calc.Amount_val += SubObj.calc_Amount_val;
            calc.Budget_val += SubObj.calc_Budget_val;
            calc.GP_val += SubObj.calc_PlannedGP_val;
        }

        List<InvoiceAR> InvARs = context.InvoiceARs.Where(x => x.ProjectId == Obj.Id).ToList();
        foreach (InvoiceAR SubObj in InvARs)
            calc.Billed_val += SubObj.calc_Amount_val;

        List<InvoiceAP> InvAPs = context.InvoiceAPs.Where(x => x.ProjectId == Obj.Id).ToList();
        foreach (InvoiceAP SubObj in InvAPs)
            calc.ActualCost_val += SubObj.calc_Amount_val;

        List<WorkItem> WrkItms = context.WorkItems.Where(x => x.ObjectId == Obj.Id && x.EntityName == "Project").ToList();
        foreach (WorkItem SubObj in WrkItms)
            calc.ActualCost_val += SubObj.calc_ActualCost_val;

        context.SaveChangesAsync();
    }

    public void UpdateLineItem(long Id)
    {
        LineItem Li = context.LineItems.Where(x => x.Id == Id).ToList().First();

        EntityWithLineItems Obj = null;

        if (Li.EntityName == "Deal")
            Obj = context.Deals.Where(x => x.Id == Li.ObjectId).ToList().First();
        else if (Li.EntityName == "Pnl")
            Obj = context.Pnls.Where(x => x.Id == Li.ObjectId).ToList().First();
        else if (Li.EntityName == "Quote")
            Obj = context.Quotes.Where(x => x.Id == Li.ObjectId).ToList().First();
        else if (Li.EntityName == "Agreement")
            Obj = context.Agreements.Where(x => x.Id == Li.ObjectId).ToList().First();
        else if (Li.EntityName == "SalesOrder")
            Obj = context.SalesOrders.Where(x => x.Id == Li.ObjectId).ToList().First();
        else if (Li.EntityName == "PurchaseOrder")
            Obj = context.PurchaseOrders.Where(x => x.Id == Li.ObjectId).ToList().First();
        else if (Li.EntityName == "InvoiceAR")
            Obj = context.InvoiceARs.Where(x => x.Id == Li.ObjectId).ToList().First();
        else if (Li.EntityName == "InvoiceAP")
            Obj = context.InvoiceAPs.Where(x => x.Id == Li.ObjectId).ToList().First();
        else if (Li.EntityName == "Project")
            Obj = context.Projects.Where(x => x.Id == Li.ObjectId).ToList().First();

        AggregateLineItems(Obj, Li);
    }

    private void AggregateLineItems(EntityWithLineItems Obj, LineItem Li)
    {
        decimal calc_Amount_val = 0; decimal calc_Budget_val = 0; decimal calc_GP_val = 0; decimal calc_ActualCost_val = 0; decimal calc_ActualGP_val = 0;
        List<LineItem> lstLineItems = context.LineItems.Where(x => x.ObjectId == Li.ObjectId && x.EntityName == Li.EntityName).ToList();
        foreach (LineItem LiObj in lstLineItems)
        {
            calc_Amount_val += LiObj.calc_Amount_val;
            calc_Budget_val += LiObj.calc_Budget_val;
            calc_GP_val += LiObj.calc_PlannedGP_val;
            calc_ActualCost_val += LiObj.calc_ActualCost_val;
            calc_ActualGP_val += LiObj.calc_ActualGP_val;
        }

        Obj.calc_Amount_val = calc_Amount_val;
        Obj.calc_Budget_val = calc_Budget_val;
        Obj.calc_PlannedGP_val = calc_GP_val;
        Obj.calc_ActualCost_val = calc_ActualCost_val;
        Obj.calc_ActualGP_val = calc_ActualGP_val;

        //context.SaveChangesAsync();
    }


}
