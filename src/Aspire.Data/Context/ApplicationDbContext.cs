using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Aspire.Data;
using Project = Aspire.Data.Project;
using Aspire.Utilities;

namespace Aspire.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        ITenantProviderService tenantProviderService) :
            IdentityDbContext<ApplicationUser, ApplicationRole, long, ApplicationUserClaim, ApplicationUserRole, 
            IdentityUserLogin<long>, ApplicationRoleClaim, IdentityUserToken<long>>(options)
    {
        
        public DbSet<OldWorkflow> OldWorkflows { get; set; }
        public DbSet<DMSProjectDiscipline> DMSProjectDisciplines { get; set; }
        public DbSet<DisciplineEmployee> DisciplineEmployees { get; set; }
        public DbSet<DocumentWorkItem> DocumentWorkItems { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<ExternalUser> ExternalUsers { get; set; }
        public DbSet<DMSProject> DMSProjects { get; set; }
        public DbSet<DMSProjectOwner> DMSProjectOwners { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<DocumentTypeDiscipline> DocumentTypeDisciplines { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<EntityLog> DBLogs { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<CustomerPO> CustomerPOs { get; set; }
        public DbSet<SupplierQuote> SupplierQuotes { get; set; }
        public DbSet<Competitor> Competitors { get; set; }
        public DbSet<LeadSource> LeadSources { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationUserTenant> ApplicationUserTenants { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<ApplicationUserClaim> ApplicationUserClaims { get; set; }
        public DbSet<ApplicationRoleClaim> ApplicationRoleClaims { get; set; }

        public DbSet<Entity> Entities { get; set; }
        public DbSet<EntityAttribute> EntityAttributes { get; set; }
        public DbSet<UserInvitation> UserInvitations { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }

        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }

        public DbSet<Aspire.Data.TimeZone> TimeZones { get; set; }
        public DbSet<SLA> SLAs { get; set; }
        public DbSet<ServicePriority> ServicePriorities { get; set; }
        public DbSet<SLAServicePriority> SLAServicePriorities { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<WorkShift> WorkShifts { get; set; }
        public DbSet<WorkShiftHoliday> WorkShiftHolidays { get; set; }

        public DbSet<FrameworkHelper> FrameworkHelpers { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Regulator> Regulators { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<Control> Controls { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<AuditItem> AuditItems { get; set; }
        public DbSet<AuditItemControl> AuditItemControls { get; set; }
        public DbSet<AuditItemException> AuditItemExceptions { get; set; }
        public DbSet<Framework> Frameworks { get; set; }
        public DbSet<FrameworkItem> FrameworkItems { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<DataConfidentiality> DataConfidentialities { get; set; }
        public DbSet<EntityAttachment> EntityAttachments { get; set; }
        public DbSet<EntityComment> EntityComments { get; set; }
        public DbSet<EntityStatus> EntityStatuses { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<WorkflowObject> WorkflowObjects { get; set; }
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<MarketUnit> MarketUnits { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBundle> ProductBundles { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<DeliveryProof> DeliveryProofs { get; set; }
        public DbSet<ProductActivity> ProductActivities { get; set; }
        public DbSet<BillingFrequency> BillingFrequencies { get; set; }
        public DbSet<DiscountType> DiscountTypes { get; set; }
        public DbSet<ManagementBufferType> ManagementBufferTypes { get; set; }
        public DbSet<TradingType> TradingTypes { get; set; }
        public DbSet<TaxRate> TaxRates { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<ProductDeliverable> ProductDeliverables { get; set; }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Confidence> Confidences { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<DealLostReason> DealLostReasons { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Pipeline> Pipelines { get; set; }
        public DbSet<Pnl> Pnls { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Evaluation> Evaluation { get; set; }
        public DbSet<EvaluationItem> EvaluationItems { get; set; }
        public DbSet<EvaluationSchema> EvaluationSchemas { get; set; }
        public DbSet<EvaluationSchemaItem> EvaluationSchemaItems { get; set; }
        public DbSet<CostItem> CostItems { get; set; }
        public DbSet<EmployeeCostItem> EmployeeCostItems { get; set; }
        public DbSet<EmployeeDependant> EmployeeDependants { get; set; }
        public DbSet<EmployeePost> EmployeePosts { get; set; }
        public DbSet<EmployeeQualification> EmployeeQualifications { get; set; }
        public DbSet<EmployeeSalaryItem> EmployeeSalaryItems { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Marital> Maritals { get; set; }
        public DbSet<Medical> Medicals { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<PayrollItem> PayrollItems { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public DbSet<ResumeSource> ResumeSources { get; set; }
        public DbSet<SalaryItem> SalaryItems { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<TimeAttendance> TimeAttendances { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<ITSystem> ITSystems { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Objective> Objectives { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Assumption> Assumptions { get; set; }
        public DbSet<BillingPlan> BillingPlans { get; set; }
        public DbSet<ChangeRequest> ChangeRequests { get; set; }
        public DbSet<Constrain> Constrains { get; set; }
        public DbSet<Deliverable> Deliverables { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<MOM> MOMs { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectKPI> ProjectKPIs { get; set; }
        public DbSet<Stakeholder> Stakeholders { get; set; }
        public DbSet<TaskPriority> TaskPriorities { get; set; }
        public DbSet<TaskAllocation> TaskAllocations { get; set; }
        public DbSet<TaskComplexity> TaskComplexities { get; set; }
        public DbSet<TaskSize> TaskSizes { get; set; }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<WorkItemAssignee> WorkItemAssignees { get; set; }
        public DbSet<Criticality> Criticalities { get; set; }
        public DbSet<Impact> Impacts { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Probability> Probabilities { get; set; }
        public DbSet<Risk> Risks { get; set; }
        public DbSet<RiskStrategy> RiskStrategies { get; set; }
        public DbSet<Severity> Severities { get; set; }
        public DbSet<Urgency> Urgencies { get; set; }
        public DbSet<InvoiceAP> InvoiceAPs { get; set; }
        public DbSet<InvoiceAR> InvoiceARs { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }
        public DbSet<ProfitCenter> ProfitCenters { get; set; }
        public DbSet<ServiceCatalogue> ServiceCatalogues { get; set; }
        public DbSet<HRBusinessTrip> HRBusinessTrips { get; set; }
        public DbSet<HRExpense> HRExpenses { get; set; }
        public DbSet<HRExitReentry> HRExitReentries { get; set; }
        public DbSet<HRLoan> HRLoans { get; set; }
        public DbSet<HRLetter> HRLetters { get; set; } 
        public DbSet<HRVacation> HRVacations { get; set; }
        public DbSet<HROverTime> HROverTimes { get; set; }
        public DbSet<HRResignation> HRResignations { get; set; }
        public DbSet<HRTraining> HRTrainings { get; set; }
        public DbSet<HRParking> HRParkings { get; set; }
        public DbSet<ADCreateUser> ADCreateUsers { get; set; }
        public DbSet<ADCreateServAcc> ADCreateServAccs { get; set; }
        public DbSet<ADCreateAdmnAcc> ADCreateAdmnAccs { get; set; }
        public DbSet<ADEnableUser> ADEnableUsers { get; set; }
        public DbSet<ADEnableServAcc> ADEnableServAccs { get; set; }
        public DbSet<ADEnableAdmnAcc> ADEnableAdmnAccs { get; set; }
        public DbSet<ADDisableUser> ADDisableUsers { get; set; }
        public DbSet<ADDisableServAcc> ADDisableServAccs { get; set; }
        public DbSet<ADDisableAdmnAcc> ADDisableAdmnAccs { get; set; }
        public DbSet<ADCreateGroup> ADCreateGroups { get; set; }
        public DbSet<ADAddUserGroup> ADAddUserGroups { get; set; }
        public DbSet<ADRmvUserGroup> ADRmvUserGroups { get; set; }
        public DbSet<ADUpdateUserInfo> ADUpdateUserInfos { get; set; }
        public DbSet<ADUpdateUserMngr> ADUpdateUserMngrs { get; set; }
        public DbSet<ADUpdateUserMobl> ADUpdateUserMobls { get; set; }
        public DbSet<EMCreateMailbox> EMCreateMailboxs { get; set; }
        public DbSet<EMCreateEmailGroup> EMCreateEmailGroups { get; set; }
        public DbSet<EMActivateEmail> EMActivateEmails { get; set; }
        public DbSet<EMDeactivateEmail> EMDeactivateEmails { get; set; }
        public DbSet<EMAddEmailGroup> EMAddEmailGroups { get; set; }
        public DbSet<EMRmvEmailGroup> EMRmvEmailGroups { get; set; }
        public DbSet<EMPermissionEmail> EMPermissionEmails { get; set; }
        public DbSet<EMIncreaseMailboxSize> EMIncreaseMailboxSizes { get; set; }
        public DbSet<EMEditEmail> EMEditEmails { get; set; }
        public DbSet<EMEditGroup> EMEditGroups { get; set; }
        public DbSet<EMDeleteEmail> EMDeleteEmails { get; set; }
        public DbSet<EUWorkStationNew> EUWorkStationNews { get; set; }
        public DbSet<EUWorkStationRemove> EUWorkStationRemoves { get; set; }
        public DbSet<EUWorkStationReplace> EUWorkStationReplaces { get; set; }
        public DbSet<EUITEquipment> EUITEquipments { get; set; }
        public DbSet<EUITAccessories> EUITAccessorieses { get; set; }
        public DbSet<EUEquipmentMove> EUEquipmentMoves { get; set; }
        public DbSet<EUEquipmentInstall> EUEquipmentInstalls { get; set; }
        public DbSet<EUStationary> EUStationaries { get; set; }
        public DbSet<EUMaintenance> EUMaintenances { get; set; }
        public DbSet<PaymentTerm> PaymentTerms { get; set; }
        public DbSet<PaymentTermLine> PaymentTermLines { get; set; }
        public DbSet<CreditNoteAR> CreditNoteARs { get; set; }
        public DbSet<CreditNoteAP> CreditNoteAPs { get; set; }
        public DbSet<DebitNoteAR> DebitNoteARs { get; set; }
        public DbSet<DebitNoteAP> DebitNoteAPs { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<PaymentAP> PaymentAPs { get; set; }
        public DbSet<InvoicePaymentAP> InvoicePaymentAPs { get; set; }
        public DbSet<PaymentAR> PaymentARs { get; set; }
        public DbSet<InvoicePaymentAR> InvoicePaymentARs { get; set; }
        public DbSet<Incoterm> Incoterms { get; set; }
        public DbSet<UOM> UOMs { get; set; }
        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<DeliveryNote> DeliveryNotes { get; set; }
        public DbSet<COC> COCs { get; set; }
        public DbSet<TeamRoster> TeamRosters { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<BloodGroup> BloodGroups { get; set; }

        public DbSet<HRLeavePermission> HRLeavePermissions { get; set; }
        public DbSet<HRRemoteWork> HRRemoteWorks { get; set; }
        public DbSet<ORGSuggestion> ORGSuggestions { get; set; }
        public DbSet<ORGComplaint> ORGComplaints { get; set; }
        public DbSet<INCCloudStorage> INCCloudStorages { get; set; }
        public DbSet<INCAccess> INCAccesses { get; set; }
        public DbSet<INCAccount> INCAccounts { get; set; }
        public DbSet<INCOTP> INCOTPs { get; set; }
        public DbSet<INCWorkstation> INCWorkstations { get; set; }
        public DbSet<INCLogin> INCLogins { get; set; }
        public DbSet<INCMonitor> INCMonitors { get; set; }
        public DbSet<INCPrinter> INCPrinters { get; set; }
        public DbSet<INCEmail> INCEmails { get; set; }
        public DbSet<INCWIFI> INCWIFIs { get; set; }
        public DbSet<GPMaterial> GPMaterials { get; set; }
        public DbSet<GPVehicle> GPVehicles { get; set; }
        public DbSet<GPVisitor> GPVisitors { get; set; }
        public DbSet<HRBusinessCard> HRBusinessCards { get; set; }
        public DbSet<IDLoss> IDLosses { get; set; }
        public DbSet<IDRenewal> IDRenewals { get; set; }
        public DbSet<IDNewEmployee> IDNewEmployees { get; set; }
        public DbSet<IDRestrictedArea> IDRestrictedAreas { get; set; }
        public DbSet<SARemoteAccess> SARemoteAccesses { get; set; }
        public DbSet<SASWLicense> SASWLicenses { get; set; }
        public DbSet<SASWService> SASWServices { get; set; }
        public DbSet<Holiday> Holidaies { get; set; }
        public DbSet<WorkItemDependency> WorkItemDependencies { get; set; }
// Code-Player-Inject-DbSet

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
        .UseNpgsql(AppConfiguration.ConnectionString);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            _ = builder.Entity<Account>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<WorkOrder>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<Competitor>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<LeadSource>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<Bid>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<CustomerPO>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<SupplierQuote>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<Nationality>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<PriceList>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<EntityLog>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<Document>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<DocumentWorkItem>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<DocumentType>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<DocumentTypeDiscipline>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<Discipline>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<DisciplineEmployee>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<DMSProjectDiscipline>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<OldWorkflow>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<DMSProject>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<DMSProjectOwner>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<Contractor>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<ExternalUser>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            #region Auth
            _ = builder.Entity<ApplicationUser>(entity =>
            {
                _ = entity.HasKey(u => u.Id);
            });

            _ = builder.Entity<ApplicationUserRole>(entity =>
            {
                _ = entity.HasKey(ur => new { ur.UserId, ur.RoleId, ur.TenantId });

                _ = entity.HasQueryFilter(ur => ur.TenantId == tenantProviderService.GetTenantId());

                _ = entity.Property(e => e.IsActive)
                     .HasDefaultValue(true);
            });

            _ = builder.Entity<ApplicationRole>(entity =>
            {
                _ = entity.HasKey(tr => tr.Id);

                _ = entity.HasQueryFilter(ur => ur.TenantId == tenantProviderService.GetTenantId());

                _ = entity.Property(e => e.Type)
                     .HasDefaultValue("License");
            });

            _ = builder.Entity<ApplicationRoleClaim>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(rc => rc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<ApplicationUserClaim>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
                _ = entity.HasQueryFilter(uc => uc.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<Tenant>(entity =>
            {
                _ = entity.HasKey(t => t.Id);
            });

            _ = builder.Entity<Entity>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(t => t.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<EntityAttribute>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(t => t.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<UserInvitation>(entity =>
            {
                _ = entity.HasKey(ui => ui.Id);
                _ = entity.HasQueryFilter(t => t.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<UserPreference>(entity =>
            {
                _ = entity.HasKey(ui => ui.Id);
                _ = entity.HasQueryFilter(t => t.TenantId == tenantProviderService.GetTenantId());
            });
            #endregion

            #region Entities

            _ = builder.Entity<ShippingMethod>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<Assessment>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<AuditItem>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
            });

            _ = builder.Entity<WorkItem>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<WorkItemAssignee>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<FrameworkItem>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EvaluationItem>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EntityAttachment>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EntityComment>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<AuditItemControl>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<AuditItemException>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ServiceCatalogue>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Stakeholder>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Regulator>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Domain>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Control>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ManagementBufferType>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());


            });

            _ = builder.Entity<Audit>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Framework>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EntityType>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EntityStatus>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Workflow>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<WorkflowObject>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Asset>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Department>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ITSystem>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Location>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Branch>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Objective>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Country>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Currency>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<DataConfidentiality>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<HRLeavePermission>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<HRRemoteWork>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<HRParking>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<HRTraining>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<HRBusinessTrip>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<HRLoan>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<HRLetter>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<HRExitReentry>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<HRExpense>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<HRVacation>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<HROverTime>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<HRResignation>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ADCreateUser>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ADCreateServAcc>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ADCreateAdmnAcc>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ADEnableUser>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ADEnableServAcc>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ADEnableAdmnAcc>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ADDisableUser>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ADDisableServAcc>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ADDisableAdmnAcc>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ADCreateGroup>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ADAddUserGroup>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ADRmvUserGroup>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ADUpdateUserInfo>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ADUpdateUserMngr>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ADUpdateUserMobl>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EMCreateMailbox>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EMCreateEmailGroup>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EMActivateEmail>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EMDeactivateEmail>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EMAddEmailGroup>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EMRmvEmailGroup>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EMPermissionEmail>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EMIncreaseMailboxSize>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EMEditEmail>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EMEditGroup>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EMDeleteEmail>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EUWorkStationNew>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EUWorkStationRemove>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EUWorkStationReplace>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EUITEquipment>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EUITAccessories>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EUEquipmentMove>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EUEquipmentInstall>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EUStationary>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EUMaintenance>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ORGSuggestion>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ORGComplaint>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<INCCloudStorage>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<INCAccess>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<INCAccount>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<INCOTP>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<INCWorkstation>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<INCLogin>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<INCMonitor>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<INCPrinter>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<INCEmail>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<INCWIFI>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<InvoiceAP>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<InvoiceAR>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<PurchaseOrder>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<CostCenter>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ProfitCenter>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Account>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<PaymentTerm>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<PaymentTermLine>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<CreditNoteAR>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<CreditNoteAP>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<DebitNoteAR>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<DebitNoteAP>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Bank>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<BankAccount>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<PaymentAP>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<InvoicePaymentAP>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<PaymentAR>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<InvoicePaymentAR>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Incoterm>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Risk>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Issue>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Probability>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Impact>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<RiskStrategy>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Severity>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Criticality>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Urgency>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<BillingFrequency>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Product>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ProductBundle>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ProductActivity>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ProductDeliverable>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ProductCategory>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<DeliveryProof>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Inventory>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<DiscountType>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<TaxRate>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<TradingType>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<UOM>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Lead>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Customer>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Supplier>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<DeliveryAddress>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Contact>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Deal>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<DealLostReason>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Agreement>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Quote>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Pipeline>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Pnl>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<SalesOrder>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Stage>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<LineItem>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Confidence>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Sector>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Industry>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Portfolio>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Programme>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Project>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<BillingPlan>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Assumption>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ChangeRequest>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Constrain>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Deliverable>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Lesson>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Milestone>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<MOM>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Phase>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Plan>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ProjectKPI>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<TaskSize>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<TaskComplexity>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<TaskAllocation>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<TaskPriority>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<DeliveryNote>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<COC>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<TeamRoster>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<BloodGroup>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Evaluation>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Employee>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<SLA>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ServicePriority>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<SLAServicePriority>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<TeamMember>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EmployeeQualification>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EmployeeDependant>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EmployeePost>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<EmployeeSalaryItem>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Gender>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Grade>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Job>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Marital>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Medical>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Post>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Payroll>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<PayrollItem>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Relation>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<ResumeSource>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<SalaryItem>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Team>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<TimeAttendance>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Holiday>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<WorkShift>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<TimeSheet>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<WorkShift>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<WorkShiftHoliday>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<MarketUnit>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<GPMaterial>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<GPVehicle>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<GPVisitor>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<HRBusinessCard>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<IDLoss>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<IDRenewal>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<IDNewEmployee>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<IDRestrictedArea>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<SARemoteAccess>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<SASWLicense>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<SASWService>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<CostItem>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<Language>(entity => 
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
                
                
            });

            _ = builder.Entity<WorkItemDependency>(entity =>
            {
                _ = entity.HasKey(e => e.Id);
                _ = entity.HasQueryFilter(c => c.TenantId == tenantProviderService.GetTenantId());
            });

// Code-Player-Inject-OnMdl

            #endregion
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            long? currentUserId = tenantProviderService.GetUserId();

            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<IAuditableEntity> entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedByUsrId = entry.Entity.CreatedByUsrId == long.MinValue ? currentUserId : entry.Entity.CreatedByUsrId;
                    entry.Entity.CreateDate = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedByUsrId = currentUserId;
                    entry.Entity.UpdateDate = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
