using Aspire.Data;
using AutoMapper;

namespace Aspire.Utilities;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ProjectTeamDTO, ProjectDTO>();
        CreateMap<ProjectDTO, ProjectTeamDTO>();

        CreateMap<EntityAttachmentDTO, EntityAttachment>();
        CreateMap<EntityAttachment, EntityAttachmentDTO>();

        CreateMap<EntityLogDTO, EntityLog>();
        CreateMap<EntityLog, EntityLogDTO>();

        CreateMap<EntityCommentDTO, EntityComment>()
            .ForMember(dest => dest.UpdateDate, opt => opt.Ignore());

        CreateMap<EntityComment, EntityCommentDTO>()
            .ForMember(dest => dest.CreatedByUsrName, opt => opt.Ignore());

        // Mapping CreateTenantDto to Tenants
        CreateMap<CreateAndUpdateTenantDTO, Tenant>()
            .ForMember(dest => dest.UpdateDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedByUsrId, opt => opt.Ignore())
            .ForMember(dest => dest.SubscriptionStartDate, opt => opt.Ignore())
            //.ForMember(dest => dest.SubscriptionPlanId, opt => opt.Ignore())
            .ForMember(dest => dest.SubscriptionEndDate, opt => opt.Ignore())
            .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdateDate, opt => opt.Ignore());

        CreateMap<UserPreferenceDTO, UserPreference>()
            //.ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedByUsrId, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedByUsrId, opt => opt.Ignore())
            .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdateDate, opt => opt.Ignore())
            .ForMember(dest => dest.Preference, opt => opt.MapFrom(src => src.Preference))
            .ForMember(dest => dest.TableName, opt => opt.MapFrom(src => src.TableName))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => src.TenantId));

        CreateMap<UserPreference, UserPreferenceDTO>();

        CreateMap<EntityDTO, EntityDTO>()
            .ForMember(dest => dest.CreatedByUsrName, opt => opt.Ignore());
        CreateMap<EntityDTO, Entity>()
            .ForMember(dest => dest.UpdateDate, opt => opt.Ignore());

        CreateMap<ApplicationRole, ApplicationRoleDTO>();
        CreateMap<ApplicationRoleDTO, ApplicationRole>();

        CreateMap<ApplicationUserRole, ApplicationUserRoleDTO>();
        CreateMap<ApplicationUserRoleDTO, ApplicationUserRole>();


        CreateMap<WorkflowDTO, Workflow>();
        CreateMap<Workflow, WorkflowDTO>();

        CreateMap<DomainDTO, Domain>();
        CreateMap<Domain, DomainDTO>();

        CreateMap<AssessmentDTO, Assessment>();
        CreateMap<Assessment, AssessmentDTO>();

        CreateMap<ControlDTO, Control>();
        CreateMap<Control, ControlDTO>();

        CreateMap<AuditDTO, Audit>();
        CreateMap<Audit, AuditDTO>();

        CreateMap<FrameworkDTO, Framework>();
        CreateMap<Framework, FrameworkDTO>();

        CreateMap<FrameworkItemDTO, FrameworkItem>();
        CreateMap<FrameworkItem, FrameworkItemDTO>();

        CreateMap<EvaluationItemDTO, EvaluationItem>();
        CreateMap<EvaluationItem, EvaluationItemDTO>();

        CreateMap<AssetDTO, Asset>();
        CreateMap<Asset, AssetDTO>();

        CreateMap<DepartmentDTO, Department>();
        CreateMap<Department, DepartmentDTO>();

        CreateMap<ITSystemDTO, ITSystem>();
        CreateMap<ITSystem, ITSystemDTO>();

        CreateMap<LocationDTO, Location>();
        CreateMap<Location, LocationDTO>();

        CreateMap<BranchDTO, Branch>();
        CreateMap<Branch, BranchDTO>();

        CreateMap<ObjectiveDTO, Objective>();
        CreateMap<Objective, ObjectiveDTO>();

        CreateMap<CountryDTO, Country>();
        CreateMap<Country, CountryDTO>();

        CreateMap<CurrencyDTO, Currency>();
        CreateMap<Currency, CurrencyDTO>();

        CreateMap<DataConfidentialityDTO, DataConfidentiality>();
        CreateMap<DataConfidentiality, DataConfidentialityDTO>();

        CreateMap<TeamDTO, Team>();
        CreateMap<Team, TeamDTO>();

        CreateMap<TeamTeamDTO, TeamDTO>();
        CreateMap<TeamDTO, TeamTeamDTO>();
        CreateMap<TeamMemberDTO, TeamMember>();
        CreateMap<TeamMember, TeamMemberDTO>();

        CreateMap<PortfolioDTO, Portfolio>();
        CreateMap<Portfolio, PortfolioDTO>();

        CreateMap<ProgrammeDTO, Programme>();
        CreateMap<Programme, ProgrammeDTO>();

        CreateMap<BillingPlanDTO, BillingPlan>();
        CreateMap<BillingPlan, BillingPlanDTO>();

        CreateMap<AssumptionDTO, Assumption>();
        CreateMap<Assumption, AssumptionDTO>();

        CreateMap<ChangeRequestDTO, ChangeRequest>();
        CreateMap<ChangeRequest, ChangeRequestDTO>();

        CreateMap<ConstrainDTO, Constrain>();
        CreateMap<Constrain, ConstrainDTO>();

        CreateMap<DeliverableDTO, Deliverable>();
        CreateMap<Deliverable, DeliverableDTO>();

        CreateMap<LessonDTO, Lesson>();
        CreateMap<Lesson, LessonDTO>();

        CreateMap<MilestoneDTO, Milestone>();
        CreateMap<Milestone, MilestoneDTO>();

        CreateMap<MOMDTO, MOM>();
        CreateMap<MOM, MOMDTO>();

        CreateMap<PhaseDTO, Phase>();
        CreateMap<Phase, PhaseDTO>();

        CreateMap<PlanDTO, Plan>();
        CreateMap<Plan, PlanDTO>();

        CreateMap<ProjectKPIDTO, ProjectKPI>();
        CreateMap<ProjectKPI, ProjectKPIDTO>();

        CreateMap<TaskSizeDTO, TaskSize>();
        CreateMap<TaskSize, TaskSizeDTO>();

        CreateMap<TaskComplexityDTO, TaskComplexity>();
        CreateMap<TaskComplexity, TaskComplexityDTO>();

        CreateMap<TaskAllocationDTO, TaskAllocation>();
        CreateMap<TaskAllocation, TaskAllocationDTO>();

        CreateMap<TaskPriorityDTO, TaskPriority>();
        CreateMap<TaskPriority, TaskPriorityDTO>();

        CreateMap<DeliveryNoteDTO, DeliveryNote>();
        CreateMap<DeliveryNote, DeliveryNoteDTO>();

        CreateMap<COCDTO, COC>();
        CreateMap<COC, COCDTO>();

        CreateMap<TeamRosterDTO, TeamRoster>();
        CreateMap<TeamRoster, TeamRosterDTO>();

        CreateMap<StakeholderDTO, Stakeholder>();
        CreateMap<Stakeholder, StakeholderDTO>();

        CreateMap<WorkItemDTO, WorkItem>();
        CreateMap<WorkItem, WorkItemDTO>();

        CreateMap<WorkItemAssigneeDTO, WorkItemAssignee>();
        CreateMap<WorkItemAssignee, WorkItemAssigneeDTO>();

        CreateMap<PnlDTO, Pnl>();
        CreateMap<Pnl, PnlDTO>();

        CreateMap<LeadDTO, Lead>();
        CreateMap<Lead, LeadDTO>();

        CreateMap<CustomerDTO, Customer>();
        CreateMap<Customer, CustomerDTO>();

        CreateMap<SupplierDTO, Supplier>();
        CreateMap<Supplier, SupplierDTO>();

        CreateMap<DeliveryAddressDTO, DeliveryAddress>();
        CreateMap<DeliveryAddress, DeliveryAddressDTO>();

        CreateMap<ContactDTO, Contact>();
        CreateMap<Contact, ContactDTO>();

        CreateMap<DealDTO, Deal>();
        CreateMap<Deal, DealDTO>();

        CreateMap<DealTeamDTO, DealDTO>();
        CreateMap<DealDTO, DealTeamDTO>();
        CreateMap<AgreementDTO, Agreement>();
        CreateMap<Agreement, AgreementDTO>();

        CreateMap<QuoteDTO, Quote>();
        CreateMap<Quote, QuoteDTO>();

        CreateMap<PipelineDTO, Pipeline>();
        CreateMap<Pipeline, PipelineDTO>();

        CreateMap<SalesOrderDTO, SalesOrder>();
        CreateMap<SalesOrder, SalesOrderDTO>();

        CreateMap<StageDTO, Stage>();
        CreateMap<Stage, StageDTO>();

        CreateMap<LineItemDTO, LineItem>();
        CreateMap<LineItem, LineItemDTO>();

        CreateMap<ConfidenceDTO, Confidence>();
        CreateMap<Confidence, ConfidenceDTO>();

        CreateMap<SectorDTO, Sector>();
        CreateMap<Sector, SectorDTO>();

        CreateMap<IndustryDTO, Industry>();
        CreateMap<Industry, IndustryDTO>();

        CreateMap<MarketUnitDTO, MarketUnit>();
        CreateMap<MarketUnit, MarketUnitDTO>();

        CreateMap<BloodGroupDTO, BloodGroup>();
        CreateMap<BloodGroup, BloodGroupDTO>();

        CreateMap<EvaluationDTO, Evaluation>();
        CreateMap<Evaluation, EvaluationDTO>();

        CreateMap<EmployeeDTO, Employee>();
        CreateMap<Employee, EmployeeDTO>();

        CreateMap<EmployeeTeamDTO, EmployeeDTO>();
        CreateMap<EmployeeDTO, EmployeeTeamDTO>();
        CreateMap<EmployeeQualificationDTO, EmployeeQualification>();
        CreateMap<EmployeeQualification, EmployeeQualificationDTO>();

        CreateMap<EmployeeDependantDTO, EmployeeDependant>();
        CreateMap<EmployeeDependant, EmployeeDependantDTO>();

        CreateMap<EmployeePostDTO, EmployeePost>();
        CreateMap<EmployeePost, EmployeePostDTO>();

        CreateMap<EmployeeSalaryItemDTO, EmployeeSalaryItem>();
        CreateMap<EmployeeSalaryItem, EmployeeSalaryItemDTO>();

        CreateMap<EmployeeCostItemDTO, EmployeeCostItem>();
        CreateMap<EmployeeCostItem, EmployeeCostItemDTO>();

        CreateMap<CostItemDTO, CostItem>();
        CreateMap<CostItem, CostItemDTO>();

        CreateMap<GenderDTO, Gender>();
        CreateMap<Gender, GenderDTO>();

        CreateMap<GradeDTO, Grade>();
        CreateMap<Grade, GradeDTO>();

        CreateMap<JobDTO, Job>();
        CreateMap<Job, JobDTO>();

        CreateMap<MaritalDTO, Marital>();
        CreateMap<Marital, MaritalDTO>();

        CreateMap<MedicalDTO, Medical>();
        CreateMap<Medical, MedicalDTO>();

        CreateMap<PostDTO, Post>();
        CreateMap<Post, PostDTO>();

        CreateMap<PayrollDTO, Payroll>();
        CreateMap<Payroll, PayrollDTO>();


        CreateMap<PayrollItemDTO, PayrollItem>();
        CreateMap<PayrollItem, PayrollItemDTO>();

        CreateMap<RelationDTO, Relation>();
        CreateMap<Relation, RelationDTO>();

        CreateMap<ResumeSourceDTO, ResumeSource>();
        CreateMap<ResumeSource, ResumeSourceDTO>();

        CreateMap<SalaryItemDTO, SalaryItem>();
        CreateMap<SalaryItem, SalaryItemDTO>();

        CreateMap<WorkShiftDTO, WorkShift>();
        CreateMap<WorkShift, WorkShiftDTO>();

        CreateMap<InvoiceAPDTO, InvoiceAP>();
        CreateMap<InvoiceAP, InvoiceAPDTO>();

        CreateMap<InvoiceARDTO, InvoiceAR>();
        CreateMap<InvoiceAR, InvoiceARDTO>();

        CreateMap<PurchaseOrderDTO, PurchaseOrder>();
        CreateMap<PurchaseOrder, PurchaseOrderDTO>();

        CreateMap<CostCenterDTO, CostCenter>();
        CreateMap<CostCenter, CostCenterDTO>();

        CreateMap<ProfitCenterDTO, ProfitCenter>();
        CreateMap<ProfitCenter, ProfitCenterDTO>();

        CreateMap<AccountDTO, Account>();
        CreateMap<Account, AccountDTO>();

        CreateMap<PaymentTermDTO, PaymentTerm>();
        CreateMap<PaymentTerm, PaymentTermDTO>();

        CreateMap<PaymentTermLineDTO, PaymentTermLine>();
        CreateMap<PaymentTermLine, PaymentTermLineDTO>();

        CreateMap<CreditNoteARDTO, CreditNoteAR>();
        CreateMap<CreditNoteAR, CreditNoteARDTO>();

        CreateMap<CreditNoteAPDTO, CreditNoteAP>();
        CreateMap<CreditNoteAP, CreditNoteAPDTO>();

        CreateMap<DebitNoteARDTO, DebitNoteAR>();
        CreateMap<DebitNoteAR, DebitNoteARDTO>();

        CreateMap<DebitNoteAPDTO, DebitNoteAP>();
        CreateMap<DebitNoteAP, DebitNoteAPDTO>();

        CreateMap<BankDTO, Bank>();
        CreateMap<Bank, BankDTO>();

        CreateMap<BankAccountDTO, BankAccount>();
        CreateMap<BankAccount, BankAccountDTO>();

        CreateMap<PaymentAPDTO, PaymentAP>();
        CreateMap<PaymentAP, PaymentAPDTO>();

        CreateMap<InvoicePaymentAPDTO, InvoicePaymentAP>();
        CreateMap<InvoicePaymentAP, InvoicePaymentAPDTO>();

        CreateMap<PaymentARDTO, PaymentAR>();
        CreateMap<PaymentAR, PaymentARDTO>();

        CreateMap<InvoicePaymentARDTO, InvoicePaymentAR>();
        CreateMap<InvoicePaymentAR, InvoicePaymentARDTO>();

        CreateMap<IncotermDTO, Incoterm>();
        CreateMap<Incoterm, IncotermDTO>();

        CreateMap<RiskDTO, Risk>();
        CreateMap<Risk, RiskDTO>();

        CreateMap<IssueDTO, Issue>();
        CreateMap<Issue, IssueDTO>();

        CreateMap<ProbabilityDTO, Probability>();
        CreateMap<Probability, ProbabilityDTO>();

        CreateMap<ImpactDTO, Impact>();
        CreateMap<Impact, ImpactDTO>();

        CreateMap<RiskStrategyDTO, RiskStrategy>();
        CreateMap<RiskStrategy, RiskStrategyDTO>();

        CreateMap<SeverityDTO, Severity>();
        CreateMap<Severity, SeverityDTO>();

        CreateMap<CriticalityDTO, Criticality>();
        CreateMap<Criticality, CriticalityDTO>();

        CreateMap<UrgencyDTO, Urgency>();
        CreateMap<Urgency, UrgencyDTO>();

        CreateMap<BillingFrequencyDTO, BillingFrequency>();
        CreateMap<BillingFrequency, BillingFrequencyDTO>();

        CreateMap<ProductDTO, Product>();
        CreateMap<Product, ProductDTO>();

        CreateMap<ProductBundleDTO, ProductBundle>();
        CreateMap<ProductBundle, ProductBundleDTO>();

        CreateMap<ProductActivityDTO, ProductActivity>();
        CreateMap<ProductActivity, ProductActivityDTO>();

        CreateMap<ProductDeliverableDTO, ProductDeliverable>();
        CreateMap<ProductDeliverable, ProductDeliverableDTO>();

        CreateMap<DeliveryProofDTO, DeliveryProof>();
        CreateMap<DeliveryProof, DeliveryProofDTO>();

        CreateMap<InventoryDTO, Inventory>();
        CreateMap<Inventory, InventoryDTO>();

        CreateMap<DiscountTypeDTO, DiscountType>();
        CreateMap<DiscountType, DiscountTypeDTO>();

        CreateMap<TaxRateDTO, TaxRate>();
        CreateMap<TaxRate, TaxRateDTO>();

        CreateMap<TradingTypeDTO, TradingType>();
        CreateMap<TradingType, TradingTypeDTO>();

        CreateMap<UOMDTO, UOM>();
        CreateMap<UOM, UOMDTO>();

        CreateMap<ORGSuggestionDTO, ORGSuggestion>();
        CreateMap<ORGSuggestion, ORGSuggestionDTO>();

        CreateMap<ORGComplaintDTO, ORGComplaint>();
        CreateMap<ORGComplaint, ORGComplaintDTO>();

        CreateMap<ADCreateUserDTO, ADCreateUser>();
        CreateMap<ADCreateUser, ADCreateUserDTO>();

        CreateMap<ADCreateServAccDTO, ADCreateServAcc>();
        CreateMap<ADCreateServAcc, ADCreateServAccDTO>();

        CreateMap<ADCreateAdmnAccDTO, ADCreateAdmnAcc>();
        CreateMap<ADCreateAdmnAcc, ADCreateAdmnAccDTO>();

        CreateMap<ADEnableUserDTO, ADEnableUser>();
        CreateMap<ADEnableUser, ADEnableUserDTO>();

        CreateMap<ADEnableServAccDTO, ADEnableServAcc>();
        CreateMap<ADEnableServAcc, ADEnableServAccDTO>();

        CreateMap<ADEnableAdmnAccDTO, ADEnableAdmnAcc>();
        CreateMap<ADEnableAdmnAcc, ADEnableAdmnAccDTO>();

        CreateMap<ADDisableUserDTO, ADDisableUser>();
        CreateMap<ADDisableUser, ADDisableUserDTO>();

        CreateMap<ADDisableServAccDTO, ADDisableServAcc>();
        CreateMap<ADDisableServAcc, ADDisableServAccDTO>();

        CreateMap<ADDisableAdmnAccDTO, ADDisableAdmnAcc>();
        CreateMap<ADDisableAdmnAcc, ADDisableAdmnAccDTO>();

        CreateMap<ADCreateGroupDTO, ADCreateGroup>();
        CreateMap<ADCreateGroup, ADCreateGroupDTO>();

        CreateMap<ADAddUserGroupDTO, ADAddUserGroup>();
        CreateMap<ADAddUserGroup, ADAddUserGroupDTO>();

        CreateMap<ADRmvUserGroupDTO, ADRmvUserGroup>();
        CreateMap<ADRmvUserGroup, ADRmvUserGroupDTO>();

        CreateMap<ADUpdateUserInfoDTO, ADUpdateUserInfo>();
        CreateMap<ADUpdateUserInfo, ADUpdateUserInfoDTO>();

        CreateMap<ADUpdateUserMngrDTO, ADUpdateUserMngr>();
        CreateMap<ADUpdateUserMngr, ADUpdateUserMngrDTO>();

        CreateMap<ADUpdateUserMoblDTO, ADUpdateUserMobl>();
        CreateMap<ADUpdateUserMobl, ADUpdateUserMoblDTO>();

        CreateMap<EMCreateMailboxDTO, EMCreateMailbox>();
        CreateMap<EMCreateMailbox, EMCreateMailboxDTO>();

        CreateMap<EMCreateEmailGroupDTO, EMCreateEmailGroup>();
        CreateMap<EMCreateEmailGroup, EMCreateEmailGroupDTO>();

        CreateMap<EMActivateEmailDTO, EMActivateEmail>();
        CreateMap<EMActivateEmail, EMActivateEmailDTO>();

        CreateMap<EMDeactivateEmailDTO, EMDeactivateEmail>();
        CreateMap<EMDeactivateEmail, EMDeactivateEmailDTO>();

        CreateMap<EMAddEmailGroupDTO, EMAddEmailGroup>();
        CreateMap<EMAddEmailGroup, EMAddEmailGroupDTO>();

        CreateMap<EMRmvEmailGroupDTO, EMRmvEmailGroup>();
        CreateMap<EMRmvEmailGroup, EMRmvEmailGroupDTO>();

        CreateMap<EMPermissionEmailDTO, EMPermissionEmail>();
        CreateMap<EMPermissionEmail, EMPermissionEmailDTO>();

        CreateMap<EMIncreaseMailboxSizeDTO, EMIncreaseMailboxSize>();
        CreateMap<EMIncreaseMailboxSize, EMIncreaseMailboxSizeDTO>();

        CreateMap<EMEditEmailDTO, EMEditEmail>();
        CreateMap<EMEditEmail, EMEditEmailDTO>();

        CreateMap<EMEditGroupDTO, EMEditGroup>();
        CreateMap<EMEditGroup, EMEditGroupDTO>();

        CreateMap<EMDeleteEmailDTO, EMDeleteEmail>();
        CreateMap<EMDeleteEmail, EMDeleteEmailDTO>();

        CreateMap<EUWorkStationNewDTO, EUWorkStationNew>();
        CreateMap<EUWorkStationNew, EUWorkStationNewDTO>();

        CreateMap<EUWorkStationRemoveDTO, EUWorkStationRemove>();
        CreateMap<EUWorkStationRemove, EUWorkStationRemoveDTO>();

        CreateMap<EUWorkStationReplaceDTO, EUWorkStationReplace>();
        CreateMap<EUWorkStationReplace, EUWorkStationReplaceDTO>();

        CreateMap<EUITEquipmentDTO, EUITEquipment>();
        CreateMap<EUITEquipment, EUITEquipmentDTO>();

        CreateMap<EUITAccessoriesDTO, EUITAccessories>();
        CreateMap<EUITAccessories, EUITAccessoriesDTO>();

        CreateMap<EUEquipmentMoveDTO, EUEquipmentMove>();
        CreateMap<EUEquipmentMove, EUEquipmentMoveDTO>();

        CreateMap<EUEquipmentInstallDTO, EUEquipmentInstall>();
        CreateMap<EUEquipmentInstall, EUEquipmentInstallDTO>();

        CreateMap<EUStationaryDTO, EUStationary>();
        CreateMap<EUStationary, EUStationaryDTO>();

        CreateMap<EUMaintenanceDTO, EUMaintenance>();
        CreateMap<EUMaintenance, EUMaintenanceDTO>();

        CreateMap<GPMaterialDTO, GPMaterial>();
        CreateMap<GPMaterial, GPMaterialDTO>();

        CreateMap<GPVehicleDTO, GPVehicle>();
        CreateMap<GPVehicle, GPVehicleDTO>();

        CreateMap<GPVisitorDTO, GPVisitor>();
        CreateMap<GPVisitor, GPVisitorDTO>();

        CreateMap<HRLeavePermissionDTO, HRLeavePermission>();
        CreateMap<HRLeavePermission, HRLeavePermissionDTO>();

        CreateMap<HRRemoteWorkDTO, HRRemoteWork>();
        CreateMap<HRRemoteWork, HRRemoteWorkDTO>();

        CreateMap<HRParkingDTO, HRParking>();
        CreateMap<HRParking, HRParkingDTO>();

        CreateMap<HRTrainingDTO, HRTraining>();
        CreateMap<HRTraining, HRTrainingDTO>();

        CreateMap<HRBusinessCardDTO, HRBusinessCard>();
        CreateMap<HRBusinessCard, HRBusinessCardDTO>();

        CreateMap<HRBusinessTripDTO, HRBusinessTrip>();
        CreateMap<HRBusinessTrip, HRBusinessTripDTO>();

        CreateMap<HRLoanDTO, HRLoan>();
        CreateMap<HRLoan, HRLoanDTO>();

        CreateMap<HRLetterDTO, HRLetter>();
        CreateMap<HRLetter, HRLetterDTO>();

        CreateMap<HRExitReentryDTO, HRExitReentry>();
        CreateMap<HRExitReentry, HRExitReentryDTO>();

        CreateMap<HRExpenseDTO, HRExpense>();
        CreateMap<HRExpense, HRExpenseDTO>();

        CreateMap<HRVacationDTO, HRVacation>();
        CreateMap<HRVacation, HRVacationDTO>();

        CreateMap<HROverTimeDTO, HROverTime>();
        CreateMap<HROverTime, HROverTimeDTO>();

        CreateMap<HRResignationDTO, HRResignation>();
        CreateMap<HRResignation, HRResignationDTO>();

        CreateMap<IDLossDTO, IDLoss>();
        CreateMap<IDLoss, IDLossDTO>();

        CreateMap<IDRenewalDTO, IDRenewal>();
        CreateMap<IDRenewal, IDRenewalDTO>();

        CreateMap<IDNewEmployeeDTO, IDNewEmployee>();
        CreateMap<IDNewEmployee, IDNewEmployeeDTO>();

        CreateMap<IDRestrictedAreaDTO, IDRestrictedArea>();
        CreateMap<IDRestrictedArea, IDRestrictedAreaDTO>();

        CreateMap<INCCloudStorageDTO, INCCloudStorage>();
        CreateMap<INCCloudStorage, INCCloudStorageDTO>();

        CreateMap<INCAccessDTO, INCAccess>();
        CreateMap<INCAccess, INCAccessDTO>();

        CreateMap<INCAccountDTO, INCAccount>();
        CreateMap<INCAccount, INCAccountDTO>();

        CreateMap<INCOTPDTO, INCOTP>();
        CreateMap<INCOTP, INCOTPDTO>();

        CreateMap<INCWorkstationDTO, INCWorkstation>();
        CreateMap<INCWorkstation, INCWorkstationDTO>();


        CreateMap<SASWLicenseDTO, SASWLicense>();
        CreateMap<SASWLicense, SASWLicenseDTO>();

        CreateMap<SASWServiceDTO, SASWService>();
        CreateMap<SASWService, SASWServiceDTO>();

        CreateMap<ProjectDTO, Project>();
        CreateMap<Project, ProjectDTO>();

        CreateMap<WorkOrderDTO, WorkOrder>();
        CreateMap<WorkOrder, WorkOrderDTO>();

        CreateMap<EntityDTO, Entity>();
        CreateMap<Entity, EntityDTO>();

        CreateMap<EntityCommentDTO, EntityComment>();
        CreateMap<EntityComment, EntityCommentDTO>();

        CreateMap<EntityStatusDTO, EntityStatus>();
        CreateMap<EntityStatus, EntityStatusDTO>();

        CreateMap<EntityTypeDTO, EntityType>();
        CreateMap<EntityType, EntityTypeDTO>();

        CreateMap<LanguageDTO, Language>();
        CreateMap<Language, LanguageDTO>();

        CreateMap<WorkflowObjectDTO, WorkflowObject>();
        CreateMap<WorkflowObject, WorkflowObjectDTO>();

        CreateMap<ServiceCatalogueDTO, ServiceCatalogue>();
        CreateMap<ServiceCatalogue, ServiceCatalogueDTO>();

        CreateMap<WorkOrderTeamDTO, WorkOrderDTO>();
        CreateMap<WorkOrderDTO, WorkOrderTeamDTO>();
        CreateMap<ProductCategoryDTO, ProductCategory>();
        CreateMap<ProductCategory, ProductCategoryDTO>();

        CreateMap<HolidayDTO, Holiday>();
        CreateMap<Holiday, HolidayDTO>();

        CreateMap<SLADTO, SLA>();
        CreateMap<SLA, SLADTO>();

        CreateMap<ServicePriorityDTO, ServicePriority>();
        CreateMap<ServicePriority, ServicePriorityDTO>();

        CreateMap<SLAServicePriorityDTO, SLAServicePriority>();
        CreateMap<SLAServicePriority, SLAServicePriorityDTO>();

        CreateMap<AuditItemDTO, AuditItem>();
        CreateMap<AuditItem, AuditItemDTO>();
        CreateMap<CompetitorDTO, Competitor>();
        CreateMap<Competitor, CompetitorDTO>();

        CreateMap<LeadSourceDTO, LeadSource>();
        CreateMap<LeadSource, LeadSourceDTO>();

        CreateMap<CustomerPODTO, CustomerPO>();
        CreateMap<CustomerPO, CustomerPODTO>();

        CreateMap<SupplierQuoteDTO, SupplierQuote>();
        CreateMap<SupplierQuote, SupplierQuoteDTO>();

        CreateMap<BidDTO, Bid>();
        CreateMap<Bid, BidDTO>();

        CreateMap<DealLostReasonDTO, DealLostReason>();
        CreateMap<DealLostReason, DealLostReasonDTO>();

        CreateMap<ApplicationUserDTO, ApplicationUser>();
        CreateMap<ApplicationUser, ApplicationUserDTO>();

        CreateMap<ApplicationRoleDTO, ApplicationRole>();
        CreateMap<ApplicationRole, ApplicationRoleDTO>();

        CreateMap<PriceListDTO, PriceList>();
        CreateMap<PriceList, PriceListDTO>();

        CreateMap<AuditItemControlDTO, AuditItemControl>();
        CreateMap<AuditItemControl, AuditItemControlDTO>();

        CreateMap<AuditItemExceptionDTO, AuditItemException>();
        CreateMap<AuditItemException, AuditItemExceptionDTO>();

        CreateMap<DocumentDTO, Document>();
        CreateMap<Document, DocumentDTO>();

        CreateMap<DocumentWorkItemDTO, DocumentWorkItem>();
        CreateMap<DocumentWorkItem, DocumentWorkItemDTO>();

        CreateMap<DocumentTypeDTO, DocumentType>();
        CreateMap<DocumentType, DocumentTypeDTO>();

        CreateMap<DisciplineEmployeeDTO, DisciplineEmployee>();
        CreateMap<DisciplineEmployee, DisciplineEmployeeDTO>();

        CreateMap<DocumentTypeDisciplineDTO, DocumentTypeDiscipline>();
        CreateMap<DocumentTypeDiscipline, DocumentTypeDisciplineDTO>();

        CreateMap<DisciplineDTO, Discipline>();
        CreateMap<Discipline, DisciplineDTO>();

        CreateMap<DMSProjectDisciplineDTO, DMSProjectDiscipline>();
        CreateMap<DMSProjectDiscipline, DMSProjectDisciplineDTO>();

        CreateMap<DMSProjectDTO, DMSProject>();
        CreateMap<DMSProject, DMSProjectDTO>();

        CreateMap<DMSProjectOwnerDTO, DMSProjectOwner>();
        CreateMap<DMSProjectOwner, DMSProjectOwnerDTO>();

        CreateMap<ContractorDTO, Contractor>();
        CreateMap<Contractor, ContractorDTO>();

        CreateMap<ExternalUserDTO, ExternalUser>();
        CreateMap<ExternalUser, ExternalUserDTO>();

        CreateMap<INCLoginDTO, INCLogin>();
        CreateMap<INCLogin, INCLoginDTO>();

        CreateMap<INCMonitorDTO, INCMonitor>();
        CreateMap<INCMonitor, INCMonitorDTO>();

        CreateMap<INCPrinterDTO, INCPrinter>();
        CreateMap<INCPrinter, INCPrinterDTO>();

        CreateMap<INCEmailDTO, INCEmail>();
        CreateMap<INCEmail, INCEmailDTO>();

        CreateMap<INCWIFIDTO, INCWIFI>();
        CreateMap<INCWIFI, INCWIFIDTO>();

        CreateMap<SARemoteAccessDTO, SARemoteAccess>();
        CreateMap<SARemoteAccess, SARemoteAccessDTO>();

        CreateMap<WorkItemDependencyDTO, WorkItemDependency>();
        CreateMap<WorkItemDependency, WorkItemDependencyDTO>();

        CreateMap<OldWorkflowDTO, OldWorkflow>();
        CreateMap<OldWorkflow, OldWorkflowDTO>();

// Code-Player-Inject-MAP
    }
}