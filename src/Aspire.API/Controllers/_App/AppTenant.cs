using Aspire.Data;
using Aspire.Services;
using Aspire.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aspire.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class _AppTenant(ILogHandlingService logger, IRegisterService registerService,
    IAppTenant<DisciplineEmployee> _DisciplineEmployee, IAppTenant<DMSProjectDiscipline> _DMSProjectDiscipline,
    IAppTenant<Document> _Document, IAppTenant<DocumentWorkItem> _DocumentWorkItem, IAppTenant<DocumentType> _DocumentType, IAppTenant<DocumentTypeDiscipline> _DocumentTypeDiscipline, IAppTenant<Discipline> _Discipline, IAppTenant<DMSProject> _DMSProject, IAppTenant<DMSProjectOwner> _DMSProjectOwner, IAppTenant<Contractor> _Contractor, IAppTenant<ExternalUser> _ExternalUser,
    IAppTenant<ApplicationUser> _ApplicationUser, IAppTenant<ApplicationRole> _ApplicationRole,
    IAppTenant<SLA> _SLA, IAppTenant<ServicePriority> _ServicePriority, IAppTenant<SLAServicePriority> _SLAServicePriority, IAppTenant<Holiday> _Holiday,
    IAppTenant<Entity> _Entity, IAppTenant<EntityAttribute> _EntityAttribute, IAppTenant<EntityType> _EntityType, IAppTenant<EntityStatus> _EntityStatus, IAppTenant<EntityAttachment> _EntityAttachment, IAppTenant<EntityComment> _EntityComment, IAppTenant<Workflow> _Workflow, IAppTenant<WorkflowObject> _WorkflowObject, IAppTenant<ServiceCatalogue> _ServiceCatalogue, IAppTenant<LineItem> _LineItem,
    IAppTenant<Asset> _Asset, IAppTenant<Department> _Department, IAppTenant<ITSystem> _ITSystem, IAppTenant<Location> _Location, IAppTenant<Branch> _Branch, IAppTenant<Objective> _Objective, IAppTenant<Currency> _Currency, IAppTenant<Country> _Country, IAppTenant<Language> _Language, IAppTenant<DataConfidentiality> _DataConfidentiality, IAppTenant<Domain> _Domain, IAppTenant<Assessment> _Assessment, IAppTenant<Control> _Control, IAppTenant<Audit> _Audit, IAppTenant<AuditItem> _AuditItem, IAppTenant<AuditItemControl> _AuditItemControl, IAppTenant<AuditItemException> _AuditItemException, IAppTenant<Framework> _Framework, IAppTenant<FrameworkItem> _FrameworkItem,     
    IAppTenant<HRLeavePermission> _HRLeavePermission, IAppTenant<HRRemoteWork> _HRRemoteWork, IAppTenant<HRBusinessCard> _HRBusinessCard, IAppTenant<HRBusinessTrip> _HRBusinessTrip, IAppTenant<HRExpense> _HRExpense, IAppTenant<HRExitReentry> _HRExitReentry, IAppTenant<HRLoan> _HRLoan, IAppTenant<HROverTime> _HROverTime, IAppTenant<HRResignation> _HRResignation, IAppTenant<HRLetter> _HRLetter, IAppTenant<HRVacation> _HRVacation, IAppTenant<HRParking> _HRParking, IAppTenant<HRTraining> _HRTraining, 
    IAppTenant<ADAddUserGroup> _ADAddUserGroup, IAppTenant<ADRmvUserGroup> _ADRmvUserGroup, IAppTenant<ADUpdateUserMngr> _ADUpdateUserMngr, IAppTenant<ADUpdateUserInfo> _ADUpdateUserInfo, IAppTenant<ADUpdateUserMobl> _ADUpdateUserMobl, IAppTenant<ADCreateGroup> _ADCreateGroup, IAppTenant<ADEnableUser> _ADEnableUser, IAppTenant<ADEnableServAcc> _ADEnableServAcc, IAppTenant<ADEnableAdmnAcc> _ADEnableAdmnAcc, IAppTenant<ADDisableUser> _ADDisableUser, IAppTenant<ADDisableServAcc> _ADDisableServAcc, IAppTenant<ADDisableAdmnAcc> _ADDisableAdmnAcc, IAppTenant<ADCreateUser> _ADCreateUser, IAppTenant<ADCreateServAcc> _ADCreateServAcc, IAppTenant<ADCreateAdmnAcc> _ADCreateAdmnAcc, 
    IAppTenant<EMAddEmailGroup> _EMAddEmailGroup, IAppTenant<EMRmvEmailGroup> _EMRmvEmailGroup, IAppTenant<EMPermissionEmail> _EMPermissionEmail, IAppTenant<EMIncreaseMailboxSize> _EMIncreaseMailboxSize, IAppTenant<EMEditGroup> _EMEditGroup, IAppTenant<EMEditEmail> _EMEditEmail, IAppTenant<EMDeleteEmail> _EMDeleteEmail, IAppTenant<EMDeactivateEmail> _EMDeactivateEmail, IAppTenant<EMActivateEmail> _EMActivateEmail, IAppTenant<EMCreateMailbox> _EMCreateMailbox, IAppTenant<EMCreateEmailGroup> _EMCreateEmailGroup, 
    IAppTenant<EUEquipmentInstall> _EUEquipmentInstall, IAppTenant<EUEquipmentMove> _EUEquipmentMove, IAppTenant<EUITAccessories> _EUITAccessories, IAppTenant<EUITEquipment> _EUITEquipment, IAppTenant<EUMaintenance> _EUMaintenance, IAppTenant<EUStationary> _EUStationary, IAppTenant<EUWorkStationNew> _EUWorkStationNew, IAppTenant<EUWorkStationRemove> _EUWorkStationRemove, IAppTenant<EUWorkStationReplace> _EUWorkStationReplace, 
    IAppTenant<IDLoss> _IDLoss, IAppTenant<IDNewEmployee> _IDNewEmployee, IAppTenant<IDRenewal> _IDRenewal, IAppTenant<IDRestrictedArea> _IDRestrictedArea,
    IAppTenant<GPMaterial> _GPMaterial, IAppTenant<GPVehicle> _GPVehicle, IAppTenant<GPVisitor> _GPVisitor,
    IAppTenant<ORGComplaint> _ORGComplaint, IAppTenant<ORGSuggestion> _ORGSuggestion, IAppTenant<INCWorkstation> _INCWorkstation, IAppTenant<INCLogin> _INCLogin, IAppTenant<INCAccess> _INCAccess, IAppTenant<INCAccount> _INCAccount, IAppTenant<INCOTP> _INCOTP, IAppTenant<INCCloudStorage> _INCINCCloudStorage, IAppTenant<INCMonitor> _INCMonitor, IAppTenant<INCPrinter> _INCPrinter, IAppTenant<INCEmail> _INCEmail, IAppTenant<INCWIFI> _INCWIFI,
    IAppTenant<SARemoteAccess> _SARemoteAccess, IAppTenant<SASWLicense> _SASWLicense, IAppTenant<SASWService> _SASWService, 
    IAppTenant<InvoiceAR> _InvoiceAR, IAppTenant<InvoiceAP> _InvoiceAP, IAppTenant<CostCenter> _CostCenter, IAppTenant<ProfitCenter> _ProfitCenter, IAppTenant<Account> _Account, IAppTenant<PaymentTerm> _PaymentTerm, IAppTenant<PaymentTermLine> _PaymentTermLine, IAppTenant<CreditNoteAR> _CreditNoteAR, IAppTenant<DebitNoteAR> _DebitNoteAR, IAppTenant<CreditNoteAP> _CreditNoteAP, IAppTenant<DebitNoteAP> _DebitNoteAP, IAppTenant<PaymentAP> _PaymentAP, IAppTenant<InvoicePaymentAP> _InvoicePaymentAP, IAppTenant<PaymentAR> _PaymentAR, IAppTenant<InvoicePaymentAR> _InvoicePaymentAR, IAppTenant<Bank> _Bank, IAppTenant<BankAccount> _BankAccount, IAppTenant<Incoterm> _Incoterm,
    IAppTenant<Risk> _Risk, IAppTenant<Issue> _Issue, IAppTenant<Probability> _Probability, IAppTenant<Impact> _Impact, IAppTenant<Severity> _Severity, IAppTenant<RiskStrategy> _RiskStrategy, IAppTenant<Criticality> _Criticality, IAppTenant<Urgency> _Urgency,
    IAppTenant<Product> _Product, IAppTenant<PriceList> _PriceList, IAppTenant<MarketUnit> _MarketUnit, IAppTenant<ProductBundle> _ProductBundle, IAppTenant<ProductActivity> _ProductActivity, IAppTenant<ProductDeliverable> _ProductDeliverable, IAppTenant<BillingFrequency> _BillingFrequency, IAppTenant<ProductCategory> _ProductCategory, IAppTenant<DeliveryProof> _DeliveryProof, IAppTenant<DiscountType> _DiscountType, IAppTenant<Inventory> _Inventory, IAppTenant<TaxRate> _TaxRate, IAppTenant<TradingType> _TradingType,
    IAppTenant<DealLostReason> _DealLostReason, IAppTenant<CustomerPO> _CustomerPO, IAppTenant<LeadSource> _LeadSource, IAppTenant<Competitor> _Competitor, IAppTenant<Lead> _Lead, IAppTenant<Customer> _Customer, IAppTenant<Contact> _Contact, IAppTenant<Deal> _Deal, IAppTenant<Agreement> _Agreement, IAppTenant<Quote> _Quote, IAppTenant<Pipeline> _Pipeline, IAppTenant<Pnl> _Pnl, IAppTenant<SalesOrder> _SalesOrder, IAppTenant<Stage> _Stage, IAppTenant<Confidence> _Confidence, IAppTenant<Sector> _Sector, IAppTenant<Industry> _Industry,
    IAppTenant<Supplier> _Supplier, IAppTenant<Bid> _Bid, IAppTenant<SupplierQuote> _SupplierQuote, IAppTenant<PurchaseOrder> _PurchaseOrder,
    IAppTenant<WorkOrder> _WorkOrder, IAppTenant<Portfolio> _Portfolio, IAppTenant<Programme> _Programme, IAppTenant<Project> _Project, IAppTenant<Assumption> _Assumption, IAppTenant<BillingPlan> _BillingPlan, IAppTenant<Deliverable> _Deliverable, IAppTenant<Constrain> _Constrain, IAppTenant<ChangeRequest> _ChangeRequest, IAppTenant<MOM> _MOM, IAppTenant<Phase> _Phase, IAppTenant<Plan> _Plan, IAppTenant<Lesson> _Lesson, IAppTenant<ProjectKPI> _ProjectKPI, IAppTenant<Milestone> _Milestone, IAppTenant<Stakeholder> _Stakeholder, IAppTenant<WorkItem> _WorkItem, IAppTenant<WorkItemAssignee> _WorkItemAssignee, IAppTenant<TaskSize> _TaskSize, IAppTenant<TaskAllocation> _TaskAllocation, IAppTenant<TaskComplexity> _TaskComplexity, IAppTenant<TaskPriority> _TaskPriority, IAppTenant<DeliveryNote> _DeliveryNote, IAppTenant<COC> _COC, IAppTenant<TeamRoster> _TeamRoster,
    IAppTenant<Evaluation> _Evaluation, IAppTenant<EvaluationItem> _EvaluationItem, IAppTenant<Employee> _Employee, IAppTenant<CostItem> _CostItem, IAppTenant<EmployeeDependant> _EmployeeDependant, IAppTenant<EmployeeQualification> _EmployeeQualification, IAppTenant<CostItem> _EmployeeCostItem, IAppTenant<EmployeePost> _EmployeePost, IAppTenant<EmployeeSalaryItem> _EmployeeSalaryItem, IAppTenant<TeamMember> _TeamMember, IAppTenant<Gender> _Gender, IAppTenant<Grade> _Grade, IAppTenant<Job> _Job, IAppTenant<Marital> _Marital, IAppTenant<Medical> _Medical, IAppTenant<Post> _Post, IAppTenant<Payroll> _Payroll, IAppTenant<PayrollItem> _PayrollItem, IAppTenant<Relation> _Relation, IAppTenant<ResumeSource> _ResumeSource, IAppTenant<SalaryItem> _SalaryItem, IAppTenant<Team> _Team, IAppTenant<WorkShift> _WorkShift, IAppTenant<DeliveryAddress> _DeliveryAddress, IAppTenant<UOM> _UOM, IAppTenant<BloodGroup> _BloodGroup

    ) : ControllerBase
{

    [HttpPost("CreateTenant")]
    public async Task<IActionResult> CreateTenant()
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //long createdTenantId = registerService.CreateTenant();

            //UpdateEntitiesTable();
            
            registerService.Register(1);

            //_Framework.CreateFrameWork("NCA-ECC");

            return Ok();
        }
        catch (Exception e)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Error",
                Detail = e.Message,
                Status = 400
            };
            return BadRequest(problemDetails);
        }
    }

    private void UpdateEntitiesTable()
    {
        try
        {
            // APP
            _ApplicationUser.UpdateEntitiesTable();
            _ApplicationRole.UpdateEntitiesTable();
            _Entity.UpdateEntitiesTable();
            _EntityAttachment.UpdateEntitiesTable();
            _EntityComment.UpdateEntitiesTable();
            _EntityStatus.UpdateEntitiesTable();
            _EntityType.UpdateEntitiesTable();
            _EntityStatus.UpdateEntitiesTable();
            _EntityAttribute.UpdateEntitiesTable();
            _SLA.UpdateEntitiesTable();
            _ServicePriority.UpdateEntitiesTable();
            _SLAServicePriority.UpdateEntitiesTable();
            _Holiday.UpdateEntitiesTable();
            _Country.UpdateEntitiesTable();
            _Currency.UpdateEntitiesTable();
            _DataConfidentiality.UpdateEntitiesTable();
            _Language.UpdateEntitiesTable();
            _LineItem.UpdateEntitiesTable();
            _ServiceCatalogue.UpdateEntitiesTable();
            _Workflow.UpdateEntitiesTable();
            _WorkflowObject.UpdateEntitiesTable();

            //DMS
            _Document.UpdateEntitiesTable();
            _DocumentWorkItem.UpdateEntitiesTable();
            _DocumentType.UpdateEntitiesTable();
            _DocumentTypeDiscipline.UpdateEntitiesTable();
            _Discipline.UpdateEntitiesTable();
            _DisciplineEmployee.UpdateEntitiesTable();
            _DMSProjectDiscipline.UpdateEntitiesTable();
            _DMSProject.UpdateEntitiesTable();
            _DMSProjectOwner.UpdateEntitiesTable();
            _Contractor.UpdateEntitiesTable();
            _ExternalUser.UpdateEntitiesTable();

            // GRC
            _Domain.UpdateEntitiesTable();
            _Assessment.UpdateEntitiesTable();
            _Control.UpdateEntitiesTable();
            _Audit.UpdateEntitiesTable();
            _Framework.UpdateEntitiesTable();
            _FrameworkItem.UpdateEntitiesTable();
            _EvaluationItem.UpdateEntitiesTable();
            _AuditItem.UpdateEntitiesTable();
            _AuditItemControl.UpdateEntitiesTable();
            _AuditItemException.UpdateEntitiesTable();

            // ORG
            _Asset.UpdateEntitiesTable();
            _Department.UpdateEntitiesTable();
            _ITSystem.UpdateEntitiesTable();
            _Location.UpdateEntitiesTable();
            _Branch.UpdateEntitiesTable();
            _Objective.UpdateEntitiesTable();
            _Team.UpdateEntitiesTable();
            _TeamMember.UpdateEntitiesTable();

            // PMO
            _Project.UpdateEntitiesTable();
            _Portfolio.UpdateEntitiesTable();
            _Programme.UpdateEntitiesTable();
            _WorkOrder.UpdateEntitiesTable();
            _BillingPlan.UpdateEntitiesTable();
            _Assumption.UpdateEntitiesTable();
            _ChangeRequest.UpdateEntitiesTable();
            _Constrain.UpdateEntitiesTable();
            _Deliverable.UpdateEntitiesTable();
            _Lesson.UpdateEntitiesTable();
            _Milestone.UpdateEntitiesTable();
            _MOM.UpdateEntitiesTable();
            _Phase.UpdateEntitiesTable();
            _Plan.UpdateEntitiesTable();
            _ProjectKPI.UpdateEntitiesTable();
            _TaskSize.UpdateEntitiesTable();
            _TaskComplexity.UpdateEntitiesTable();
            _TaskAllocation.UpdateEntitiesTable();
            _TaskPriority.UpdateEntitiesTable();
            _DeliveryNote.UpdateEntitiesTable();
            _COC.UpdateEntitiesTable();
            _TeamRoster.UpdateEntitiesTable();
            _Stakeholder.UpdateEntitiesTable();
            _WorkItem.UpdateEntitiesTable();
            _WorkItemAssignee.UpdateEntitiesTable();

            // CRM
            _Customer.UpdateEntitiesTable();
            _DealLostReason.UpdateEntitiesTable();
            _Pnl.UpdateEntitiesTable();
            _Lead.UpdateEntitiesTable();
            _LeadSource.UpdateEntitiesTable();
            _Competitor.UpdateEntitiesTable();
            _Customer.UpdateEntitiesTable();
            _DeliveryAddress.UpdateEntitiesTable();
            _Contact.UpdateEntitiesTable();
            _Deal.UpdateEntitiesTable();
            _Agreement.UpdateEntitiesTable();
            _Quote.UpdateEntitiesTable();
            _Pipeline.UpdateEntitiesTable();
            _SalesOrder.UpdateEntitiesTable();
            _Stage.UpdateEntitiesTable();
            _Confidence.UpdateEntitiesTable();
            _Sector.UpdateEntitiesTable();
            _Industry.UpdateEntitiesTable();
            _MarketUnit.UpdateEntitiesTable();
            _CustomerPO.UpdateEntitiesTable();

            // SRM
            _Supplier.UpdateEntitiesTable();
            _PurchaseOrder.UpdateEntitiesTable();
            _SupplierQuote.UpdateEntitiesTable();
            _Bid.UpdateEntitiesTable();

            // HR
            _BloodGroup.UpdateEntitiesTable();
            _Evaluation.UpdateEntitiesTable();
            _Employee.UpdateEntitiesTable();
            _EmployeeQualification.UpdateEntitiesTable();
            _EmployeeDependant.UpdateEntitiesTable();
            _EmployeePost.UpdateEntitiesTable();
            _EmployeeSalaryItem.UpdateEntitiesTable();
            _EmployeeCostItem.UpdateEntitiesTable();
            _Gender.UpdateEntitiesTable();
            _Grade.UpdateEntitiesTable();
            _Job.UpdateEntitiesTable();
            _Marital.UpdateEntitiesTable();
            _Medical.UpdateEntitiesTable();
            _Post.UpdateEntitiesTable();
            _Payroll.UpdateEntitiesTable();
            _PayrollItem.UpdateEntitiesTable();
            _Relation.UpdateEntitiesTable();
            _ResumeSource.UpdateEntitiesTable();
            _SalaryItem.UpdateEntitiesTable();
            _CostItem.UpdateEntitiesTable();
            _WorkShift.UpdateEntitiesTable();

            // FIN
            _InvoiceAP.UpdateEntitiesTable();
            _InvoiceAR.UpdateEntitiesTable();
            _CostCenter.UpdateEntitiesTable();
            _ProfitCenter.UpdateEntitiesTable();
            _Account.UpdateEntitiesTable();
            _PaymentTerm.UpdateEntitiesTable();
            _PaymentTermLine.UpdateEntitiesTable();
            _CreditNoteAR.UpdateEntitiesTable();
            _CreditNoteAP.UpdateEntitiesTable();
            _DebitNoteAR.UpdateEntitiesTable();
            _DebitNoteAP.UpdateEntitiesTable();
            _Bank.UpdateEntitiesTable();
            _BankAccount.UpdateEntitiesTable();
            _PaymentAP.UpdateEntitiesTable();
            _InvoicePaymentAP.UpdateEntitiesTable();
            _PaymentAR.UpdateEntitiesTable();
            _InvoicePaymentAR.UpdateEntitiesTable();
            _Incoterm.UpdateEntitiesTable();

            // RSK
            _Risk.UpdateEntitiesTable();
            _Issue.UpdateEntitiesTable();
            _Probability.UpdateEntitiesTable();
            _Impact.UpdateEntitiesTable();
            _RiskStrategy.UpdateEntitiesTable();
            _Severity.UpdateEntitiesTable();
            _Criticality.UpdateEntitiesTable();
            _Urgency.UpdateEntitiesTable();

            // CAT
            _BillingFrequency.UpdateEntitiesTable();
            _Product.UpdateEntitiesTable();
            _ProductBundle.UpdateEntitiesTable();
            _ProductActivity.UpdateEntitiesTable();
            _ProductDeliverable.UpdateEntitiesTable();
            _ProductCategory.UpdateEntitiesTable();
            _DeliveryProof.UpdateEntitiesTable();
            _Inventory.UpdateEntitiesTable();
            _DiscountType.UpdateEntitiesTable();
            _TaxRate.UpdateEntitiesTable();
            _TradingType.UpdateEntitiesTable();
            _UOM.UpdateEntitiesTable();
            _PriceList.UpdateEntitiesTable();

            // SRV
            _ORGSuggestion.UpdateEntitiesTable();
            _ORGComplaint.UpdateEntitiesTable();
            _ADCreateUser.UpdateEntitiesTable();
            _ADCreateServAcc.UpdateEntitiesTable();
            _ADCreateAdmnAcc.UpdateEntitiesTable();
            _ADEnableUser.UpdateEntitiesTable();
            _ADEnableServAcc.UpdateEntitiesTable();
            _ADEnableAdmnAcc.UpdateEntitiesTable();
            _ADDisableUser.UpdateEntitiesTable();
            _ADDisableServAcc.UpdateEntitiesTable();
            _ADDisableAdmnAcc.UpdateEntitiesTable();
            _ADCreateGroup.UpdateEntitiesTable();
            _ADAddUserGroup.UpdateEntitiesTable();
            _ADRmvUserGroup.UpdateEntitiesTable();
            _ADUpdateUserInfo.UpdateEntitiesTable();
            _ADUpdateUserMngr.UpdateEntitiesTable();
            _ADUpdateUserMobl.UpdateEntitiesTable();

            _EMCreateMailbox.UpdateEntitiesTable();
            _EMCreateEmailGroup.UpdateEntitiesTable();
            _EMActivateEmail.UpdateEntitiesTable();
            _EMDeactivateEmail.UpdateEntitiesTable();
            _EMAddEmailGroup.UpdateEntitiesTable();
            _EMRmvEmailGroup.UpdateEntitiesTable();
            _EMPermissionEmail.UpdateEntitiesTable();
            _EMIncreaseMailboxSize.UpdateEntitiesTable();
            _EMEditEmail.UpdateEntitiesTable();
            _EMEditGroup.UpdateEntitiesTable();
            _EMDeleteEmail.UpdateEntitiesTable();

            _EUWorkStationNew.UpdateEntitiesTable();
            _EUWorkStationRemove.UpdateEntitiesTable();
            _EUWorkStationReplace.UpdateEntitiesTable();
            _EUITEquipment.UpdateEntitiesTable();
            _EUITAccessories.UpdateEntitiesTable();
            _EUEquipmentMove.UpdateEntitiesTable();
            _EUEquipmentInstall.UpdateEntitiesTable();
            _EUStationary.UpdateEntitiesTable();
            _EUMaintenance.UpdateEntitiesTable();

            _GPMaterial.UpdateEntitiesTable();
            _GPVehicle.UpdateEntitiesTable();
            _GPVisitor.UpdateEntitiesTable();

            _HRLeavePermission.UpdateEntitiesTable();
            _HRRemoteWork.UpdateEntitiesTable();
            _HRParking.UpdateEntitiesTable();
            _HRTraining.UpdateEntitiesTable();
            _HRBusinessCard.UpdateEntitiesTable();
            _HRBusinessTrip.UpdateEntitiesTable();
            _HRLoan.UpdateEntitiesTable();
            _HRLetter.UpdateEntitiesTable();
            _HRExitReentry.UpdateEntitiesTable();
            _HRExpense.UpdateEntitiesTable();
            _HRVacation.UpdateEntitiesTable();
            _HROverTime.UpdateEntitiesTable();
            _HRResignation.UpdateEntitiesTable();
                
            _IDLoss.UpdateEntitiesTable();
            _IDRenewal.UpdateEntitiesTable();
            _IDNewEmployee.UpdateEntitiesTable();
            _IDRestrictedArea.UpdateEntitiesTable();

            _INCINCCloudStorage.UpdateEntitiesTable();
            _INCAccess.UpdateEntitiesTable();
            _INCAccount.UpdateEntitiesTable();
            _INCOTP.UpdateEntitiesTable();
            _INCWorkstation.UpdateEntitiesTable();
            _INCLogin.UpdateEntitiesTable();
            _INCMonitor.UpdateEntitiesTable();
            _INCPrinter.UpdateEntitiesTable();
            _INCEmail.UpdateEntitiesTable();
            _INCWIFI.UpdateEntitiesTable();

            _SARemoteAccess.UpdateEntitiesTable();
            _SASWLicense.UpdateEntitiesTable();
            _SASWService.UpdateEntitiesTable();
        }
        catch (Exception ex)
        {
        }
    }

}
