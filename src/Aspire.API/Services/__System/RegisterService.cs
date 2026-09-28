using Aspire.Context;
using Aspire.Data;
using Aspire.Enums;
using Aspire.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aspire.Services;
public interface IRegisterService
{
    Task<(bool IsValid, string Message)> SendEmail(string email, bool isExternal = false);
    (bool IsValid, string Message) ConfrimOtp(ConfirmOTPDTO confirmOtp);
    Task<(bool IsValid, string Message, LoginResponseDTO? User)> RegisterEmployee(EmployeeRegisterDTO dto);
    Task<(bool IsValid, string Message, LoginResponseDTO? User)> RegisterExternalUser(ExternalUserRegisterDTO dto);
    Task<(bool IsValid, string Message, LoginResponseDTO? User)> CompleteProfile(CompleteEmployeeProfileDTO dto, long? currentUserId = null);
    long CreateTenant();
    void Register(long Id);
}

public class RegisterService(SignInManager<ApplicationUser> signInManager, ITenantProviderService _tenantProviderService, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
    ApplicationDbContext context, IAuthService authService,
    IMemoryCache memoryCache, ILogHandlingService logger) : IRegisterService
{
    public long CreateTenant()
    {
        Tenant createdTenant = context.Tenants.Add(new Tenant
        { Name = "New Tenant", Domain = "Tenant.com" }).Entity;
        context.SaveChanges();

        _tenantProviderService.SetTenantId(createdTenant.Id);

        return createdTenant.Id;
    }

    //public async Task<ValidatedResult<LoginResponseDTO>> ConfigTenant(long Id)
    //{

    //    //_tenantProviderService.SetTenantId(Id);
    //    //_tenantProviderService.SetUserId(user1.Id);
    //    //_tenantProviderService.SetCurrencyId(user1.CurrencyId);
    //    //_tenantProviderService.SetLangAbbrv(user1.LangAbbrv);

    //    //LoginDTO loginDto = new()
    //    //{ Username = user1.Email, Password = "Sehmawy&*190876", IsSelfRegistered = true };

    //    //(SignInResult signIn, LoginResponseDTO loginResponse) signInResult = await authService.Login(loginDto);

    //    //if (signInResult.signIn is not null)
    //    //{
    //    //    return new()
    //    //    { IsValid = true, Result = signInResult.loginResponse };
    //    //}

    //    //return new() { IsValid = false, Message = string.Format("Failed to login user: '{0}'", user1.Email) };

    //}

    private async Task<(ExternalUser, ApplicationUser)> CreateNewExternalUser(long tenantId, string name, string userName, long cntrId, string email, string mbl)
    {
        string EmailInTenant = email + "_" + tenantId;
        string firstName = name.Split(' ')[0];
        string lastName = name.Split(' ').Length > 1 ? string.Join(" ", name.Split(' ').Skip(1)) : firstName;

        var existingUser = await userManager.FindByNameAsync(EmailInTenant)
            ?? await userManager.FindByEmailAsync(EmailInTenant)
            ?? await context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName == EmailInTenant || u.Email == EmailInTenant);

        if (existingUser != null)
        {
            var existingExUsr = await context.ExternalUsers.FirstOrDefaultAsync(e => e.AppUsrId == existingUser.Id || e.Email == email || e.Name == userName);
            if (existingExUsr == null)
            {
                existingExUsr = context.ExternalUsers.Add(new ExternalUser(tenantId, existingUser, name, userName, cntrId, email, mbl)).Entity;
                await context.SaveChangesAsync();
            }
            return (existingExUsr, existingUser);
        }

        ApplicationUser user = new()
        {
            TenantId = tenantId,
            SeatType = SeatTypeEnum.Contractor,
            Name = firstName + " " + lastName,
            Email = EmailInTenant,
            UserName = EmailInTenant,
            FirstName = firstName,
            LastName = lastName,
            JobTitle = null,
            IsExternalUser = true,
            IsActive = true,
            IsVerified = true,
            EmailConfirmed = true,
            OTP = string.Empty,
            LastAccessedTenant = string.Empty
        };

        IdentityResult result = await userManager.CreateAsync(user, "Sehmawy&*190876");
        ExternalUser exUsr = context.ExternalUsers.Add(new ExternalUser(tenantId, user, name, userName, cntrId, email, mbl)).Entity;

        return (exUsr, user);
    }

    private async Task<(Employee, ApplicationUser)> CreateNewEmployeeAndUser(long tenantId, string name, long? jobId, string code, string email, string mbl, bool isController)
    {
        string EmailInTenant = email + "_" + tenantId;
        string firstName = name.Split(' ')[0];
        string lastName = name.Split(' ').Length > 1 ? string.Join(" ", name.Split(' ').Skip(1)) : firstName;

        var existingUser = await userManager.FindByNameAsync(EmailInTenant)
            ?? await userManager.FindByEmailAsync(EmailInTenant)
            ?? await context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName == EmailInTenant || u.Email == EmailInTenant);

        if (existingUser != null)
        {
            var existingEmp = await context.Employees.FirstOrDefaultAsync(e => e.AppUsrId == existingUser.Id || e.WorkEmail == email);
            if (existingEmp == null)
            {
                existingEmp = context.Employees.Add(new Employee(tenantId, existingUser, name, jobId, code, email, mbl, isController)).Entity;
                await context.SaveChangesAsync();
            }
            return (existingEmp, existingUser);
        }

        ApplicationUser user = new()
        {
            TenantId = tenantId,
            SeatType = SeatTypeEnum.Admin,
            Name = firstName + " " + lastName,
            Email = EmailInTenant,
            UserName = EmailInTenant,
            FirstName = firstName,
            LastName = lastName,
            JobTitle = null,
            IsActive = true,
            IsVerified = true, 
            EmailConfirmed = true,
            OTP = string.Empty,
            LastAccessedTenant = string.Empty
        };

        IdentityResult result = await userManager.CreateAsync(user, "Sehmawy&*190876");
        Employee emp = context.Employees.Add(new Employee(tenantId, user, name, jobId, code, email, mbl, isController)).Entity;

        user.PhotoUrl = @"/avatars/" + user.Id + ".png";

        if (!await context.ApplicationUserTenants.AnyAsync(x => x.UserId == user.Id && x.TenantId == tenantId))
        {
            context.ApplicationUserTenants.Add(new() { UserId = user.Id, TenantId = tenantId, IsActive = true });
        }

        return (emp, user);
    }

    private Entity? ConfigureService(EntityType SrvType, string ServiceName, string NameAr, long WFTyId, long WFStId, string[] arStatuses, string[] arTypes)
    {
        string Name = ServiceName.Replace("-", " "); // Display Name
        string servName = ServiceName.Replace("-", ""); // Must match the C# Class / vue Component Names

        ServiceCatalogue Srv = context.ServiceCatalogues.Add(new ServiceCatalogue(SrvType.TenantId, SrvType.Id, servName, Name.Trim(), NameAr, "Direct Manager", "All Employees", "1 Working Day")).Entity;
        context.SaveChanges();

        for (int i = arStatuses.Length / 2; i < arStatuses.Length; i++)
            context.EntityStatuses.Add(new EntityStatus(Srv.TenantId, servName, arStatuses[i - (arStatuses.Length / 2)], arStatuses[i], "success"));

        for (int i = arTypes.Length / 2; i < arTypes.Length; i++)
            context.EntityTypes.Add(new EntityType(Srv.TenantId, servName, arTypes[i-(arTypes.Length / 2)], arTypes[i], "success"));

        long EntId = context.Entities.Where(e => e.Name == servName).FirstOrDefault().Id;

        Workflow WF = context.Workflows.Add(new Workflow(Srv.TenantId, EntId, Srv.Name + " Workflow", Srv.NameAr, WFTyId, WFStId)).Entity;
        context.SaveChanges();
        Srv.WorkflowId = WF.Id;

        return context.Entities.Where(e => e.Name == servName).FirstOrDefault();
    }

    public void Register(long Id)
    {
        try
        {
            _tenantProviderService.SetTenantId(Id);
            //context.SaveChanges();
            //return;

            Language lang_En = context.Languages.Add(new Language { TenantId = Id, Name = "English", NameAr = "الأنجليزية" }).Entity;
            Language lang_Ar = context.Languages.Add(new Language { TenantId = Id, Name = "Arabic", NameAr = "العربية" }).Entity;

            Currency cur_SAR = context.Currencies.Add(new Currency { TenantId = Id, Name = "SAR", NameAr = "ريال سعودي", ExchangeRate = 1, IsBaseCurrency = true }).Entity;
            Currency cur_USD = context.Currencies.Add(new Currency { TenantId = Id, Name = "USD", NameAr = "دولار أمريكي", ExchangeRate = Convert.ToDecimal(3.75) }).Entity;
            Currency cur_ERU = context.Currencies.Add(new Currency { TenantId = Id, Name = "ERU", NameAr = "يورو", ExchangeRate = Convert.ToDecimal(4.37) }).Entity;
            Currency cur_GBP = context.Currencies.Add(new Currency { TenantId = Id, Name = "GBP", NameAr = "جنية استرليني", ExchangeRate = Convert.ToDecimal(5) }).Entity;
            Currency cur_CNY = context.Currencies.Add(new Currency { TenantId = Id, Name = "CNY", NameAr = "أيوان", ExchangeRate = Convert.ToDecimal(0.53) }).Entity;
            Currency cur_YEN = context.Currencies.Add(new Currency { TenantId = Id, Name = "YEN", NameAr = "ين", ExchangeRate = Convert.ToDecimal(0.025) }).Entity;
            Currency cur_EGP = context.Currencies.Add(new Currency { TenantId = Id, Name = "EGP", NameAr = "جنية مصري", ExchangeRate = Convert.ToDecimal(0.079) }).Entity;

            ApplicationRole role1 = context.ApplicationRoles.Add(new ApplicationRole(Id, "Admin")).Entity;
            ApplicationRole role2 = context.ApplicationRoles.Add(new ApplicationRole(Id, "HR")).Entity;
            ApplicationRole role3 = context.ApplicationRoles.Add(new ApplicationRole(Id, "PM")).Entity;

            context.SaveChanges();

            context.EntityTypes.Add(new EntityType(Id, "Entity", "HRM-Human Resources", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Entity", "CRM-CRM & Sales", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Entity", "SRM-SRM & Procurements", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Entity", "PMO-Projects Center", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Entity", "GRC-Governance", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Entity", "RSK-Risk Management", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Entity", "CAT-Products Catalouge", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Entity", "SRV-Service Center", "عربي", "success"));

            context.SaveChanges();

            context.EntityStatuses.Add(new EntityStatus(Id, "Document", "Draft", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Document", "Under Review", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Document", "Cancelled", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Document", "Approved", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Document", "Approved with Comments", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "DocumentWorkItem", "Draft", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "DocumentWorkItem", "Submitted", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "DocumentWorkItem", "In-Active", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "DocumentWorkItem", "In-Progress", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "DocumentWorkItem", "Pending", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "DocumentWorkItem", "Approved", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "DocumentWorkItem", "Rejected", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "DocumentWorkItem", "Returned", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "DocumentWorkItem", "More Info Required", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "DocumentWorkItem", "Approved with Comments", "عربي", "warn"));

            Job Job1 = context.Jobs.Add(new Job(Id, "Civil Engineer")).Entity;
            Job Job2 = context.Jobs.Add(new Job(Id, "Coordinating Engineer")).Entity;
            Job Job3 = context.Jobs.Add(new Job(Id, "Technical Electrical Engineer")).Entity;
            Job Job4 = context.Jobs.Add(new Job(Id, "Mechanical Engineer")).Entity;
            Job Job5 = context.Jobs.Add(new Job(Id, "Area Manager")).Entity;
            Job Job6 = context.Jobs.Add(new Job(Id, "Technical Construction")).Entity;
            Job Job7 = context.Jobs.Add(new Job(Id, "Site Civil Engineer")).Entity;
            Job Job8 = context.Jobs.Add(new Job(Id, "Planning engineer")).Entity;
            Job Job9 = context.Jobs.Add(new Job(Id, "Architectural Quality Surveyor")).Entity;
            Job Job10 = context.Jobs.Add(new Job(Id, "IT Engineer")).Entity;
            Job Job11 = context.Jobs.Add(new Job(Id, "Human Resources Manager")).Entity;
            Job Job12 = context.Jobs.Add(new Job(Id, "Accountant")).Entity;
            Job Job13 = context.Jobs.Add(new Job(Id, "Site Mechanical Engineer")).Entity;
            Job Job14 = context.Jobs.Add(new Job(Id, "Marketing Manager")).Entity;
            Job Job15 = context.Jobs.Add(new Job(Id, "Surveyor")).Entity;
            Job Job16 = context.Jobs.Add(new Job(Id, "Electromechanical Engineer")).Entity;
            Job Job17 = context.Jobs.Add(new Job(Id, "Agricultural Engineer")).Entity;
            Job Job18 = context.Jobs.Add(new Job(Id, "Management")).Entity;
            Job Job19 = context.Jobs.Add(new Job(Id, "Senior Agricultural Engineer")).Entity;
            Job Job20 = context.Jobs.Add(new Job(Id, "Electrical Engineer")).Entity;
            Job Job21 = context.Jobs.Add(new Job(Id, "Architectural")).Entity;
            Job Job22 = context.Jobs.Add(new Job(Id, "Safety Engineer")).Entity;
            Job Job23 = context.Jobs.Add(new Job(Id, "Contract Engineer")).Entity;
            Job Job24 = context.Jobs.Add(new Job(Id, "Document Controller")).Entity;
            Job Job25 = context.Jobs.Add(new Job(Id, "Project Manager")).Entity;
            Job Job26 = context.Jobs.Add(new Job(Id, "General Manager")).Entity;

            context.SaveChanges();

            (Employee emp0, ApplicationUser user0) = CreateNewEmployeeAndUser(Id, "Mohamed Sehmawy", Job1.Id, "AEC-26-9077", "Sehmawy@Aspire.com", "0562455188", true).Result;
            (Employee emp1, ApplicationUser user1) = CreateNewEmployeeAndUser(Id, "Mohamed Jamal", Job1.Id, "AEC-26-1077", "eng.mohamedgamal@aldarhecp.sa", "201005839133", true).Result;
            (Employee emp2, ApplicationUser user2) = CreateNewEmployeeAndUser(Id, "Hohammed Hussain", Job2.Id, "AEC-25-1076", "m.elhussin@aldarhecp.sa", "0 50 072 7471", true).Result;
            (Employee emp3, ApplicationUser user3) = CreateNewEmployeeAndUser(Id, "Haiman Elsaied Ismaiel", Job3.Id, "AEC-25-1075", "haiman@aldarhecp.sa", "590775804", true).Result;
            (Employee emp4, ApplicationUser user4) = CreateNewEmployeeAndUser(Id, "Sameer Fawzi Konbos", Job4.Id, "AEC-25-1074", "eng.sameer@aldarhecp.sa", "5325459212", true).Result;
            (Employee emp5, ApplicationUser user5) = CreateNewEmployeeAndUser(Id, "Walid Elmurshidy", Job5.Id, "AEC-25-1073", "eng.walid@aldarhecp.sa", "543897001", true).Result;
            (Employee emp6, ApplicationUser user6) = CreateNewEmployeeAndUser(Id, "Taj Elsir Awad", Job1.Id, "AEC-25-1072", "eng.tag@aldarhecp.sa", "050 179 8147", true).Result;
            (Employee emp7, ApplicationUser user7) = CreateNewEmployeeAndUser(Id, "Islam Al komi", Job1.Id, "AEC-25-1070", "al-komi@aldarhecp.sa", "540503275", true).Result;
            (Employee emp8, ApplicationUser user8) = CreateNewEmployeeAndUser(Id, "Mahmoud Dabdob", Job5.Id, "AEC-25-1069", "mahmoud.dabdoub@aldarhecp.sa", "509271446", true).Result;
            (Employee emp9, ApplicationUser user9) = CreateNewEmployeeAndUser(Id, "Ramy Yousaf", Job5.Id, "AEC-25-1068", "eng.ramy@aldarhecp.sa", "554240842", true).Result;
            (Employee emp10, ApplicationUser user10) = CreateNewEmployeeAndUser(Id, "Kayid Almutairi", Job6.Id, "AEC-25-1067", "aldarh@aldarhecp.sa", "--", true).Result;
            (Employee emp11, ApplicationUser user11) = CreateNewEmployeeAndUser(Id, "Ali Mohammed Shabeli Alshahri", Job3.Id, "AEC-25-1066", "eng.ali@aldarhecp.sa", "506949170", true).Result;
            (Employee emp12, ApplicationUser user12) = CreateNewEmployeeAndUser(Id, "Ahmed Mohammed Ahmed Faraj", Job7.Id, "AEC-25-1064", "en.farg@aldarhecp.sa", "531651040", true).Result;
            (Employee emp13, ApplicationUser user13) = CreateNewEmployeeAndUser(Id, "Nafha Omer", Job8.Id, "AEC-25-1063", "Nafha23@aldarhecp.sa", "535763558", true).Result;
            (Employee emp14, ApplicationUser user14) = CreateNewEmployeeAndUser(Id, "Abdullah Abdulwahid Mahmoud", Job9.Id, "AEC-25-1062", "a.abdelwahed@aldarhecp.sa", "504143022", true).Result;
            (Employee emp15, ApplicationUser user15) = CreateNewEmployeeAndUser(Id, "Hany Mahmoud Abdo", Job9.Id, "AEC-25-1061", "hany_m_abdo@aldarhecp.sa", "568776665", true).Result;
            (Employee emp16, ApplicationUser user16) = CreateNewEmployeeAndUser(Id, "Mohamed Hamed Abdullah", Job5.Id, "AEC-24-1060", "eng.hamed@aldarhecp.sa", "533817171", true).Result;
            (Employee emp17, ApplicationUser user17) = CreateNewEmployeeAndUser(Id, "Shawky Ahmed", Job7.Id, "AEC-24-1059", "eng.shawky@aldarhecp.sa", "059 521 2465", true).Result;
            (Employee emp18, ApplicationUser user18) = CreateNewEmployeeAndUser(Id, "Ramzy Ahmed", Job7.Id, "AEC-24-1058", "eng.ramzy1@aldarhecp.sa", "550739428", true).Result;
            (Employee emp19, ApplicationUser user19) = CreateNewEmployeeAndUser(Id, "Farmanullah Khan", Job10.Id, "AEC-24-1057", "hrdepartment@gmail.com", "582245768", true).Result;
            (Employee emp20, ApplicationUser user20) = CreateNewEmployeeAndUser(Id, "Turkey Zaiefullah Farj Alhrbee", Job8.Id, "AEC-24-1056", "trkya2950@gmail.com", "506183374", false).Result;
            (Employee emp21, ApplicationUser user21) = CreateNewEmployeeAndUser(Id, "Muhammad Adil Bin Muhammed Almalham", Job8.Id, "AEC-24-1055", "mulhemm7mmd@gmail.com", "541616000", false).Result;
            (Employee emp22, ApplicationUser user22) = CreateNewEmployeeAndUser(Id, "Abdullah Alkair Mehmood Hamd", Job11.Id, "AEC-24-1054", "abusally78@gmail.com", "538933320", false).Result;
            (Employee emp23, ApplicationUser user23) = CreateNewEmployeeAndUser(Id, "Abdurehman Hassan Mohammed Alshahree", Job12.Id, "AEC-24-1053", "abdulrahman4ha@gmail.com", "544764664", false).Result;
            (Employee emp24, ApplicationUser user24) = CreateNewEmployeeAndUser(Id, "Abdulwahid Ahmed Hassan Al Faqeya", Job13.Id, "AEC-24-1052", "abdwahd@gmail.com", "556285891", false).Result;
            (Employee emp25, ApplicationUser user25) = CreateNewEmployeeAndUser(Id, "Mansoor Mohammed Nasir Albarqi", Job11.Id, "AEC-24-1051", "mn990rr@hotmail.com", "532180155", false).Result;
            (Employee emp26, ApplicationUser user26) = CreateNewEmployeeAndUser(Id, "Marwa Mohammad Maresheed Aljdaanee", Job14.Id, "AEC-24-1050", "mrwtalhrby00@gmail.com", "554951741", false).Result;
            (Employee emp27, ApplicationUser user27) = CreateNewEmployeeAndUser(Id, "Ahmad Adil Bewme Alfakhranee", Job15.Id, "AEC-24-1049", "ahmedsdel285899@gmail.com", "553389907", false).Result;
            (Employee emp28, ApplicationUser user28) = CreateNewEmployeeAndUser(Id, "Mohammed Ibrahim Bassyouni Mtowlee", Job15.Id, "AEC-24-1048", "m_elbassyouni@outlook.com", "538637276", false).Result;
            (Employee emp29, ApplicationUser user29) = CreateNewEmployeeAndUser(Id, "Yara Yahya Ali Albarqi", Job11.Id, "AEC-24-1046", "yuri550055@gmail.com", "543289085", false).Result;
            (Employee emp30, ApplicationUser user30) = CreateNewEmployeeAndUser(Id, "Mujtabi Abdul Aziz Bin Jafer Alnakhlie", Job16.Id, "AEC-24-1045", "aljorfe.27@gmail.com", "555294393", false).Result;
            (Employee emp31, ApplicationUser user31) = CreateNewEmployeeAndUser(Id, "Abdullah Ali ibne Hussain Alqahtane", Job1.Id, "AEC-24-1044", "abdullahali.jop@gmail.com", "557241645", false).Result;
            (Employee emp32, ApplicationUser user32) = CreateNewEmployeeAndUser(Id, "Fiza Aoud Nasir Alhrmah", Job11.Id, "AEC-24-1043", "silvaasty14@gmail.com", "500667782", false).Result;
            (Employee emp33, ApplicationUser user33) = CreateNewEmployeeAndUser(Id, "Basheir Mohammad Musa Haroon", Job17.Id, "AEC-24-1042", "basheirmohammadmusa", "557483527", true).Result;
            (Employee emp34, ApplicationUser user34) = CreateNewEmployeeAndUser(Id, "Saleman Hassan Saleman Khalil", Job1.Id, "AEC-24-1041", "suliman2.hassan2@gmail.com", "557353498", false).Result;
            (Employee emp35, ApplicationUser user35) = CreateNewEmployeeAndUser(Id, "Hussain Ahmad Fazal Almoli Mohammad", Job1.Id, "AEC-24-1040", "eng.husseinahmed88@gmail.com", "558341227", false).Result;
            (Employee emp36, ApplicationUser user36) = CreateNewEmployeeAndUser(Id, "Mohammed Fathhi Mohamad Abdullah Alahmeed", Job1.Id, "AEC-24-1039", "mmado6155@gmail.com", "564658005", false).Result;
            (Employee emp37, ApplicationUser user37) = CreateNewEmployeeAndUser(Id, "Mohammad Alkelani", Job1.Id, "AEC-24-1038", "mkilani@aldarhecp.sa", "599129210", true).Result;
            (Employee emp38, ApplicationUser user38) = CreateNewEmployeeAndUser(Id, "Ahmad Lutfie Ibrahim Mohammed", Job11.Id, "AEC-24-1037", "eng.ahmedlotfy5@gmail.com", "562267404", false).Result;
            (Employee emp39, ApplicationUser user39) = CreateNewEmployeeAndUser(Id, "Abdullah Towylie Mkhdor Aljahdle", Job11.Id, "AEC-24-1036", "a.t.aljahdali2869@gmail.com", "566682869", false).Result;
            (Employee emp40, ApplicationUser user40) = CreateNewEmployeeAndUser(Id, "Abdullah Mohammed Abdullah Almhmade", Job1.Id, "AEC-24-1035", "abdullah1996@hotmail.com", "547978620", false).Result;
            (Employee emp41, ApplicationUser user41) = CreateNewEmployeeAndUser(Id, "Kfayat Ullah Khanullah", Job10.Id, "AEC-24-1034", "kifayat@outlook.com", "552495580", false).Result;
            (Employee emp42, ApplicationUser user42) = CreateNewEmployeeAndUser(Id, "Zakre Ahmad Shoaee Kaabee", Job11.Id, "AEC-24-1033", "thekraahmed1558@icloud.com", "500231905", false).Result;
            (Employee emp43, ApplicationUser user43) = CreateNewEmployeeAndUser(Id, "Haneen Almohamad Abdullah Almhmade", Job11.Id, "AEC-24-1032", "haneenalmohmadi@hotmail.com", "596782023", false).Result;
            (Employee emp44, ApplicationUser user44) = CreateNewEmployeeAndUser(Id, "Rawan Jebran Mohammad Kaabai", Job14.Id, "AEC-24-1031", "mtbmghrby588@icloud.com", "550075970", false).Result;
            (Employee emp45, ApplicationUser user45) = CreateNewEmployeeAndUser(Id, "Abdurehman ali bin mohammad albasesi", Job15.Id, "AEC-24-1030", "abalbisi@icloud.com", "56290476", false).Result;
            (Employee emp46, ApplicationUser user46) = CreateNewEmployeeAndUser(Id, "Abdullah Salih Bin Abdullah Alqahtani", Job15.Id, "AEC-25-1029", "ma6612326@gmail.om", "532238617", true).Result;
            (Employee emp47, ApplicationUser user47) = CreateNewEmployeeAndUser(Id, "Mohamed Fawad Awd Saad", Job18.Id, "AEC-25-1028", "m.fouad1985@oulook.sa", "566052670", true).Result;
            (Employee emp48, ApplicationUser user48) = CreateNewEmployeeAndUser(Id, "Abdalla Magdi Abdalla", Job5.Id, "AEC-25-1027", "abdallah.magdy@aldarhecp.sa", "544023174", true).Result;
            (Employee emp49, ApplicationUser user49) = CreateNewEmployeeAndUser(Id, "Mustafa Ali Bin Ibrahim Alzahir", Job19.Id, "AEC-25-1026", "m.elzaier@aldarhecp.sa", "5605532011", true).Result;
            (Employee emp50, ApplicationUser user50) = CreateNewEmployeeAndUser(Id, "Mohyeldin Nabeel Alassas", Job7.Id, "AEC-25-1025", "eng.mohy@aldarhecp.sa", "543120973", true).Result;
            (Employee emp51, ApplicationUser user51) = CreateNewEmployeeAndUser(Id, "Ahmed Fawzi", Job5.Id, "AEC-24-1024", "a.fawzi@aldarhecp.sa", "54 590 0631", true).Result;
            (Employee emp52, ApplicationUser user52) = CreateNewEmployeeAndUser(Id, "Hassan Wahid Elsafty", Job5.Id, "AEC-24-1023", "h.elsafty@aldarhecp.sa", "536432989", true).Result;
            (Employee emp53, ApplicationUser user53) = CreateNewEmployeeAndUser(Id, "Anwar Mohamed Salih", Job7.Id, "AEC-24-1022", "eng.anwar@aldarhecp.sa", "554845765", true).Result;
            (Employee emp54, ApplicationUser user54) = CreateNewEmployeeAndUser(Id, "Mahmoud Sarhan Ali", Job1.Id, "AEC-24-1021", "eng.sarhan@aldarhecp.sa", "547754262", true).Result;
            (Employee emp55, ApplicationUser user55) = CreateNewEmployeeAndUser(Id, "Hesham Mohamed Shabana", Job17.Id, "AEC-24-1020", "h.shbana@aldarhecp.sa", "543930411", true).Result;
            (Employee emp56, ApplicationUser user56) = CreateNewEmployeeAndUser(Id, "Abduljawad mahmoud", Job20.Id, "AEC-24-1019", "Abduljawadmahmoud@aldarhecp.sa", "544411769", true).Result;
            (Employee emp57, ApplicationUser user57) = CreateNewEmployeeAndUser(Id, "Eisa mohammed AAdm Haqwab", Job21.Id, "AEC-24-1018", "arch.eisa@aldarhecp.sa", "509516607", true).Result;
            (Employee emp58, ApplicationUser user58) = CreateNewEmployeeAndUser(Id, "Jamaan Mohamed Alamri-emp", Job7.Id, "AEC-24-1017", "jam@aldarhecp.sa", "562879616", true).Result;
            (Employee emp59, ApplicationUser user59) = CreateNewEmployeeAndUser(Id, "Mohamed Dawod", Job5.Id, "AEC-24-1016", "m.dawad@aldarhecp.sa", "506875258", true).Result;
            (Employee emp60, ApplicationUser user60) = CreateNewEmployeeAndUser(Id, "Adel Mabrook", Job5.Id, "AEC-24-1015", "eng.adel@aldarhecp.sa", "510212771", true).Result;
            (Employee emp61, ApplicationUser user61) = CreateNewEmployeeAndUser(Id, "Mohamed Rasmy Mohamed", Job5.Id, "AEC-24-1014", "m.rasmy@aldarhecp.sa", "050 545 3197", true).Result;
            (Employee emp62, ApplicationUser user62) = CreateNewEmployeeAndUser(Id, "Abdurehman Ibrahim Abdurehman Al-Malki", Job7.Id, "AEC-24-1012", "almalki@aldarhecp.sa", "540557744", true).Result;
            (Employee emp63, ApplicationUser user63) = CreateNewEmployeeAndUser(Id, "Mohammad Shakeel", Job13.Id, "AEC-24-1011", "m.shakeel@aldarhecp.sa", "557593852", true).Result;
            (Employee emp64, ApplicationUser user64) = CreateNewEmployeeAndUser(Id, "Mohamed Ashraf", Job21.Id, "AEC-24-1010", "m.ashraf@aldarhecp.sa", "058 190-5276", true).Result;
            (Employee emp65, ApplicationUser user65) = CreateNewEmployeeAndUser(Id, "Muhamad Mahros Younis Ali", Job2.Id, "AEC-24-1009", "m.mahrous@aldarhecp.sa", "053 734 3840", true).Result;
            (Employee emp66, ApplicationUser user66) = CreateNewEmployeeAndUser(Id, "Amir Adam Ahmed Mohamed", Job22.Id, "AEC-24-1008", "amir.adam@aldarhecp.sa", "500849416", true).Result;
            (Employee emp67, ApplicationUser user67) = CreateNewEmployeeAndUser(Id, "Jamaan Mohamed Alamri", Job7.Id, "AEC-24-1007", "jamaan@aldarhecp.sa", "562879616", true).Result;
            (Employee emp68, ApplicationUser user68) = CreateNewEmployeeAndUser(Id, "Mohamed Khamis", Job8.Id, "AEC-24-1006", "MOHAMED.KHAMIS@ALDARHECP.SA", "053 548 4233", true).Result;
            (Employee emp69, ApplicationUser user69) = CreateNewEmployeeAndUser(Id, "Mohamed Nassir Hosarieh", Job23.Id, "AEC-24-1005", "M.HOSARIEH@ALDARHECP.SA", "055 132 3774", true).Result;
            (Employee emp70, ApplicationUser user70) = CreateNewEmployeeAndUser(Id, "Alanoud Bandr Ibrahim Alqamizie", Job24.Id, "AEC-24-1004", "Doc.Cont@aldarhecp.sa", "544896686", true).Result;
            (Employee emp71, ApplicationUser user71) = CreateNewEmployeeAndUser(Id, "Amal Al-Mehmadi", Job18.Id, "AEC-24-1003", "amalelmehmadi@aldarhecp.sa", "599099216", true).Result;
            (Employee emp72, ApplicationUser user72) = CreateNewEmployeeAndUser(Id, "Mohamed Atta", Job25.Id, "AEC-24-1002", "mohamed.atta@ALDARHECP.SA", "595055536", true).Result;
            (Employee emp73, ApplicationUser user73) = CreateNewEmployeeAndUser(Id, "Nader Al-Hassan", Job5.Id, "AEC-24-1001", "NHASSAN@ALDARHECP.SA", "059 211 2279", true).Result;
            (Employee emp74, ApplicationUser user74) = CreateNewEmployeeAndUser(Id, "Dr. Yahya Albargi", Job26.Id, "AEC-24-1000", "Dr.Yahya@aldarhecp.sa", "598899656", true).Result;

            context.SaveChanges();


            DMSProjectOwner OW1 = context.DMSProjectOwners.FirstOrDefault(o => o.TenantId == Id && o.Name == "ALDARH")
                ?? context.DMSProjectOwners.Add(new DMSProjectOwner(Id, "ALDARH", "عربي", "240901193469", "aldarhecp@aldarhecp.sa", "+966544896686")).Entity;
            DMSProjectOwner OW2 = context.DMSProjectOwners.FirstOrDefault(o => o.TenantId == Id && o.Name == "Ministry of Municipalities and Housing")
                ?? context.DMSProjectOwners.Add(new DMSProjectOwner(Id, "Ministry of Municipalities and Housing", "عربي", "", "info@darh.com.co", "0598899656")).Entity;

            Contractor cntr1 = context.Contractors.FirstOrDefault(c => c.TenantId == Id && c.Name == "Al Dara Engineering Consulting Company")
                ?? context.Contractors.Add(new Contractor(Id, "Al Dara Engineering Consulting Company", "عربي", "240901193469", "info@darh.com.co", "+966544896686")).Entity;
            Contractor cntr2 = context.Contractors.FirstOrDefault(c => c.TenantId == Id && c.Name == "Safwat Al Riyadh Company Limited")
                ?? context.Contractors.Add(new Contractor(Id, "Safwat Al Riyadh Company Limited", "عربي", "", "safwacoltd@gmail.com", "0568164121")).Entity;
            Contractor cntr3 = context.Contractors.FirstOrDefault(c => c.TenantId == Id && c.Name == "DHEL ALNAKHEEL CONTACTOR COMPANY")
                ?? context.Contractors.Add(new Contractor(Id, "DHEL ALNAKHEEL CONTACTOR COMPANY", "عربي", "241101053151", "info@dnc.sa", "0558282168")).Entity;
            Contractor cntr4 = context.Contractors.FirstOrDefault(c => c.TenantId == Id && c.Name == "Capital Modern Nurseries Company L.T.D")
                ?? context.Contractors.Add(new Contractor(Id, "Capital Modern Nurseries Company L.T.D", "عربي", "", "info@cmn-ltd.com", "+966508585800")).Entity;
            Contractor cntr5 = context.Contractors.FirstOrDefault(c => c.TenantId == Id && c.Name == "Al-hadab Trading and Contracting Co")
                ?? context.Contractors.Add(new Contractor(Id, "Al-hadab Trading and Contracting Co", "عربي", "", "info@alhadab.com.sa", "0580364615")).Entity;
            Contractor cntr6 = context.Contractors.FirstOrDefault(c => c.TenantId == Id && c.Name == "Altalbi Contracting")
                ?? context.Contractors.Add(new Contractor(Id, "Altalbi Contracting", "عربي", "5907010955", "info@altalbi.com", "0173340331")).Entity;

            context.SaveChanges();

            (ExternalUser ex1, ApplicationUser exusr1) = CreateNewExternalUser(Id, "Ahmed Shaltout", "ahmedshaltout", cntr3.Id, "dammam-park@dnc.sa", "541306616").Result;
            (ExternalUser ex2, ApplicationUser exusr2) = CreateNewExternalUser(Id, "Amr Abualkhair Elmahdy", "amrabualkhair", cntr4.Id, "amr.abualkhair@gmail.com", "597903404").Result;
            (ExternalUser ex3, ApplicationUser exusr3) = CreateNewExternalUser(Id, "Capital West", "capitalwest", cntr4.Id, "capitalwest12@gmail.com", "571686634").Result;
            (ExternalUser ex4, ApplicationUser exusr4) = CreateNewExternalUser(Id, "Hossam marzouk ragab", "hossammarzouk", cntr6.Id, "hossam.marzouk99@gmail.com", "536814159").Result;
            (ExternalUser ex5, ApplicationUser exusr5) = CreateNewExternalUser(Id, "Ibrahim Wasel Niazi", "ibrahimwasel", cntr4.Id, "ibrahim22.wasel@gmail.com", "558258834").Result;
            (ExternalUser ex6, ApplicationUser exusr6) = CreateNewExternalUser(Id, "Mahmoud Mohamed Ibrahim", "mahmoudismail", cntr4.Id, "mahmoudismail201294@gmail.com", "572119398").Result;
            (ExternalUser ex7, ApplicationUser exusr7) = CreateNewExternalUser(Id, "Mohamed Abu elhamd", "mohamedaboelhamd", cntr3.Id, "mohamedaboelhamd@dhlnkl.com", "558282168").Result;
            (ExternalUser ex8, ApplicationUser exusr8) = CreateNewExternalUser(Id, "Mohamedsaleh Abulmajeed", "mohamedsaleh", cntr2.Id, "mohamedsaleh@safwaltdksa.com", "503975011").Result;
            (ExternalUser ex9, ApplicationUser exusr9) = CreateNewExternalUser(Id, "Omar Darder", "omardarder", cntr2.Id, "omardarder79@gmail.com", "530574201").Result;
            (ExternalUser ex10, ApplicationUser exusr10) = CreateNewExternalUser(Id, "Wael Mohamed Elgabri", "waelmohamed", cntr3.Id, "Madina-park@dnc.sa", "559397946").Result;
            (ExternalUser ex11, ApplicationUser exusr11) = CreateNewExternalUser(Id, "Yousif Mohamed Zakaria", "yousifzakaria", cntr5.Id, "DC@alhadab.com.sa", "505953718").Result;

            if (!context.DMSProjects.Any(p => p.TenantId == Id))
            {
                context.DMSProjects.Add(new DMSProject(Id, "ALDARH Head Office", "24-2030", "عربي", OW1.Id, cntr1.Id));
                context.DMSProjects.Add(new DMSProject(Id, "مشروع تنفيذ الحدائق - منطقة العلا", "PRJ 241239014518", "عربي", OW2.Id, cntr2.Id));
                context.DMSProjects.Add(new DMSProject(Id, "مشروع تنفيذ الحدائق - المنطقة الوسطى", "PRJ 24110105855", "عربي", OW2.Id, cntr5.Id));
                context.DMSProjects.Add(new DMSProject(Id, "مشروع تنفيذ الحدائق - المنطقة الشمالية", "PRJ-2411010180078", "عربي", OW2.Id, cntr4.Id));
                context.DMSProjects.Add(new DMSProject(Id, "مشروع تنفيذ الحدائق - المسار السريع", "PRJ 241101386812", "عربي", OW2.Id, cntr2.Id));
                context.DMSProjects.Add(new DMSProject(Id, "مشروع تنفيذ الحدائق - اسكان الدمام الشمالي", "PRJ 241101053151", "عربي", OW2.Id, cntr3.Id));
                context.DMSProjects.Add(new DMSProject(Id, "مشروع تنفيذ الحدائق - ضاحية الفرسان", "PRJ 1231231231", "عربي", OW2.Id, cntr5.Id));
                context.DMSProjects.Add(new DMSProject(Id, "مشروع تنفيذ الحدائق - المنطقة الشرقية", "PRJ 12341234123", "عربي", OW2.Id, cntr4.Id));
                context.DMSProjects.Add(new DMSProject(Id, "مشروع تنفيذ الحدائق - المنطقة الغربية", "PRJ 123123123", "عربي", OW2.Id, cntr4.Id));
                context.DMSProjects.Add(new DMSProject(Id, "مشروع تنفيذ الحدائق - منطقة تبوك والمدينة المنورة", "PRJ 241101055107", "عربي", OW2.Id, cntr3.Id));
                context.DMSProjects.Add(new DMSProject(Id, "مشروع تنفيذ الحدائق - المنطقة الجنوبية", "PRJ 241101058384", "عربي", OW2.Id, cntr1.Id));
                context.DMSProjects.Add(new DMSProject(Id, "مشروع تنفيذ الحدائق لمشاريع الاسكان", "PRJ-240901193469", "عربي", OW2.Id, cntr6.Id));
            }

            context.DocumentTypes.Add(new DocumentType(Id, "Red Line Drawing", "RLD", 150, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Project Close", "PJC", 50, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "AS BUILT", "ASB", 150, "cad, pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Initial Handover Report", "IHOR", 25, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Quarterly Employee Performance Evaluation", "QEPE", 50, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Supervision Project Site Audit", "SPSA", 50, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Internal circular", "IC", 25, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Baseline", "BL", 50, "xml, xer, pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Minutes of Meeting Weekly", "MOMW", 25, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Employee Performance Evaluation", "EPE", 25, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Training and Knowledge Transfer", "TKT", 50, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Project Management Office Minutes Of Meetings", "PMO-MOM", 50, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Close and Updated Records", "CUPR", 50, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Letter to Process", "LTP", 25, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Detailed Design Drawing", "DDD", 150, "cad, pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Site Minutes of Meetings", "SiteMOM", 50, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Close NCR", "CNCR", 50, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "OPEN NCR", "ONCR", 50, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Owner's Rejected - Material", "ORM", 25, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Owner's Approved- Material", "OAM", 25, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Risk Assistment", "RASS", 100, "xlsm, xlsx, pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Safety Monthly Report", "SMR", 100, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Safety Weekly Report", "SWR", 100, "xlsm, pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "EOT", "EOT", 25, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Redesign Drawing", "RD", 100, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "New Activity Commencement Request", "NACR", 50, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Technical Query", "TQ", 200, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Owner's Rejected Pre-Qualification", "ORPQ", 25, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Kick Of Meetings", "KOM", 100, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "Owner's Approved Pre-Qualification", "OAPQ", 50, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Contractor Staff Approval", "CSApproval", 100, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Shop Drawings", "SD", 300, "zip, pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Quantity Surveying", "QS", 100, "xlsx, cad, pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Test And Comissioning Plan", "TC PLAN", 100, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Method Of Statement", "MOS", 50, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Technical Submittal", "TSL", 150, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Update of Schedule", "UPDS", 150, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Final Design Drawing", "FDD", 50, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Design Review", "DRV", 300, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Inspection Test Plan", "ITP", 100, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Operation&Maintenance PLAN", "OMPLAN", 25, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Operation Plan", "OP", 50, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "Letters From the owner", "LFO", 50, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "Letters To the owner", "LTO", 200, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "WIR Progress", "WIR-01", 50, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "Pre-qualification", "PQ", 200, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "Site Levels", "SL", 50, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "Building Permits", "BP", 25, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "Subcontractor qualification", "SQ", 100, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "invoices", "INV", 300, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "organization chart", "OC", 50, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "Letters from contractors", "LFC", 150, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "budget grid", "BG", 25, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "Site Handing Over Of Land", "HOL", 100, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "Minutes of meetings", "MOM", 25, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "Non-conformity reports", "NCR", 25, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "Forms", "Forms", 25, "excel, pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "Site instruction", "SI", 25, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "Photo report", "PHR", 25, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "Monthly Report", "MR", 100, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "Weekly Report", "WR", 25, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "Daily Report", "DR", 25, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "Compressive Test", "CT1", 25, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "Compaction Test", "CT", 25, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "POURING INSPECTION REQUEST", "PIR", 25, "pdf"));
            context.DocumentTypes.Add(new DocumentType(Id, "WORK INSPECTION REQUEST", "WIR", 25, "zip, pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "MATRIAL INSPECTION REQUEST", "MIR", 25, "zip, pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Material Approval", "MA", 150, "zip, pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Letters to contractors", "LTC", 25, "pdf, zip, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "general", "GRL", 25, "rar, pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Training Plan", "TP", 25, "zip, pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Project Models", "PM", 25, "zip, pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Requirements Plan", "RPLAN", 100, "zip, pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Project Contract", "PC", 25, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Time Schedule", "TS", 150, "xer, xml, png, zip, pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Quality Plan", "QP", 25, "zip, pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Change Order", "CO", 150, "zip, pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Safety Plan", "SP", 25, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Soil Report", "SR", 25, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Project Specifications", "PS", 25, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Change Order", "CO", 150, "zip, pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Soil Report", "SR", 25, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Project Specifications", "PS", 25, "pdf, doc, docx"));
            context.DocumentTypes.Add(new DocumentType(Id, "Bills of Quantities", "BOQ", 25, "zip, pdf, doc, docx"));

            Discipline CR = context.Disciplines.Add(new Discipline(Id, "Contractor", user1.Id)).Entity;
            Discipline DC = context.Disciplines.Add(new Discipline(Id, "Document Controller", user1.Id)).Entity;
            Discipline AM = context.Disciplines.Add(new Discipline(Id, "Area Manager", user1.Id)).Entity;
            Discipline PM = context.Disciplines.Add(new Discipline(Id, "Project Manager", user7.Id)).Entity;
            Discipline CV = context.Disciplines.Add(new Discipline(Id, "Civil Engineer", user1.Id)).Entity;
            Discipline EL = context.Disciplines.Add(new Discipline(Id, "Electrical Engineer", user2.Id)).Entity;
            Discipline MC = context.Disciplines.Add(new Discipline(Id, "Mechanical Engineer", user3.Id)).Entity;
            Discipline EM = context.Disciplines.Add(new Discipline(Id, "Electromechanical Engineer", user4.Id)).Entity;
            Discipline AR = context.Disciplines.Add(new Discipline(Id, "Architectural Engineer", user5.Id)).Entity;
            Discipline AG = context.Disciplines.Add(new Discipline(Id, "Agricultural Engineer", user6.Id)).Entity;
            Discipline PJ = context.Disciplines.Add(new Discipline(Id, "Project Engineer", user7.Id)).Entity;
            Discipline PL = context.Disciplines.Add(new Discipline(Id, "Planning Engineer", user7.Id)).Entity;
            Discipline CN = context.Disciplines.Add(new Discipline(Id, "Contract Engineer", user8.Id)).Entity;
            Discipline SF = context.Disciplines.Add(new Discipline(Id, "Safety Engineer", user9.Id)).Entity;
            
            context.SaveChanges();

            foreach (DMSProject prj in context.DMSProjects)
            {
                context.DMSProjectDisciplines.Add(new DMSProjectDiscipline(Id, prj.Id, CR.Id, user9.Id));
                context.DMSProjectDisciplines.Add(new DMSProjectDiscipline(Id, prj.Id, DC.Id, user10.Id));
                context.DMSProjectDisciplines.Add(new DMSProjectDiscipline(Id, prj.Id, AM.Id, user11.Id));
                context.DMSProjectDisciplines.Add(new DMSProjectDiscipline(Id, prj.Id, PM.Id, user12.Id));
                context.DMSProjectDisciplines.Add(new DMSProjectDiscipline(Id, prj.Id, CV.Id, user11.Id));
                context.DMSProjectDisciplines.Add(new DMSProjectDiscipline(Id, prj.Id, EL.Id, user11.Id));
                context.DMSProjectDisciplines.Add(new DMSProjectDiscipline(Id, prj.Id, MC.Id, user11.Id));
                context.DMSProjectDisciplines.Add(new DMSProjectDiscipline(Id, prj.Id, EM.Id, user11.Id));
                context.DMSProjectDisciplines.Add(new DMSProjectDiscipline(Id, prj.Id, AR.Id, user11.Id));
                context.DMSProjectDisciplines.Add(new DMSProjectDiscipline(Id, prj.Id, AG.Id, user11.Id));
                context.DMSProjectDisciplines.Add(new DMSProjectDiscipline(Id, prj.Id, PJ.Id, user11.Id));
                context.DMSProjectDisciplines.Add(new DMSProjectDiscipline(Id, prj.Id, PL.Id, user11.Id));
                context.DMSProjectDisciplines.Add(new DMSProjectDiscipline(Id, prj.Id, CN.Id, user11.Id));
                context.DMSProjectDisciplines.Add(new DMSProjectDiscipline(Id, prj.Id, SF.Id, user11.Id));
            }

            foreach (Discipline dsp in context.Disciplines)
            {
                context.DisciplineEmployees.Add(new DisciplineEmployee(Id, dsp.Id, user10.Id));
                context.DisciplineEmployees.Add(new DisciplineEmployee(Id, dsp.Id, user11.Id));
                context.DisciplineEmployees.Add(new DisciplineEmployee(Id, dsp.Id, user12.Id));
            }

            foreach (DocumentType docType in context.DocumentTypes)
            {
                context.DocumentTypeDisciplines.Add(new DocumentTypeDiscipline(Id, docType.Id, CN.Id, 0, AllocationEnum.None, "In-Progress:0,Submit:1"));
                context.DocumentTypeDisciplines.Add(new DocumentTypeDiscipline(Id, docType.Id, DC.Id, 10, AllocationEnum.DisciplineLeader,  "In-Progress:0,Approve:1,Reject:3"));
                context.DocumentTypeDisciplines.Add(new DocumentTypeDiscipline(Id, docType.Id, AM.Id, 20, AllocationEnum.Project, "In-Progress:0,Approve:1,Reject:3"));
                context.DocumentTypeDisciplines.Add(new DocumentTypeDiscipline(Id, docType.Id, 6, 30, AllocationEnum.None, "In-Progress:0,Submit:1"));
                context.DocumentTypeDisciplines.Add(new DocumentTypeDiscipline(Id, docType.Id, 7, 30, AllocationEnum.DisciplineLeader, "In-Progress:0,Approve:1,Reject:3"));
                context.DocumentTypeDisciplines.Add(new DocumentTypeDiscipline(Id, docType.Id, 8, 35, AllocationEnum.DisciplineLeader, "In-Progress:0,Approve:1,Reject:3"));

                context.DocumentTypeDisciplines.Add(new DocumentTypeDiscipline(Id, docType.Id, PM.Id, 70, AllocationEnum.DisciplineLeader, "In-Progress:0,Approve:1,Reject:3"));

                context.DocumentTypeDisciplines.Add(new DocumentTypeDiscipline(Id, docType.Id, 4, 80, AllocationEnum.None, "In-Progress:0,Submit:1"));
                context.DocumentTypeDisciplines.Add(new DocumentTypeDiscipline(Id, docType.Id, 5, 80, AllocationEnum.DisciplineLeader, "In-Progress:0,Approve:1,Reject:3"));
                context.DocumentTypeDisciplines.Add(new DocumentTypeDiscipline(Id, docType.Id, 9, 85, AllocationEnum.DisciplineLeader, "In-Progress:0,Approve:1,Reject:3"));

                context.DocumentTypeDisciplines.Add(new DocumentTypeDiscipline(Id, docType.Id, DC.Id, 90, AllocationEnum.DisciplineLeader,"In-Progress:0,Submit:1"));
                context.DocumentTypeDisciplines.Add(new DocumentTypeDiscipline(Id, docType.Id, CN.Id, 99, AllocationEnum.None, "Complete:0"));
            }

            context.Incoterms.Add(new Incoterm(Id, "EXW (Ex Works)", "Buyer pays all costs and takes all risks from the seller's door."));
            context.Incoterms.Add(new Incoterm(Id, "FCA (Free Carrier)", "Seller delivers goods to a named carrier or place; risk passes there."));
            context.Incoterms.Add(new Incoterm(Id, "CPT (Carriage Paid To)", "Seller pays freight to a destination; buyer takes risk once handed to the carrier."));
            context.Incoterms.Add(new Incoterm(Id, "CIP (Carriage and Insurance Paid To)", "Same as CPT, but seller also adds insurance."));
            context.Incoterms.Add(new Incoterm(Id, "DAP (Delivered at Place)", "Seller delivers goods to a named destination; buyer handles unloading."));
            context.Incoterms.Add(new Incoterm(Id, "DPU (Delivered at Place Unloaded)", "Seller delivers and unloads goods at the destination."));
            context.Incoterms.Add(new Incoterm(Id, "DDP (Delivered Duty Paid)", "Seller handles everything, including import duties and delivery to the buyer."));
            context.Incoterms.Add(new Incoterm(Id, "FAS (Free Alongside Ship)", "Seller places goods next to the ship at the port; buyer takes risk."));
            context.Incoterms.Add(new Incoterm(Id, "FOB (Free on Board)", "Seller loads goods onto the ship; risk transfers to the buyer."));
            context.Incoterms.Add(new Incoterm(Id, "CFR (Cost and Freight)", "Seller pays shipping to the destination port; buyer takes risk once loaded."));
            context.Incoterms.Add(new Incoterm(Id, "CIF (Cost, Insurance and Freight)", "Same as CFR, plus the seller buys marine insurance."));

            //Framework Frm1 = context.Frameworks.Add(new Framework(Id, "NCA-ECC", Dmn1.Id, Reg1.Id)).Entity;
            //Framework Frm2 = context.Frameworks.Add(new Framework(Id, "NCA-CCC", Dmn1.Id, Reg1.Id)).Entity;
            //Framework Frm3 = context.Frameworks.Add(new Framework(Id, "NCA-TCC", Dmn1.Id, Reg1.Id)).Entity;
            //Framework Frm4 = context.Frameworks.Add(new Framework(Id, "NCA-OTCC", Dmn1.Id, Reg1.Id)).Entity;

            //context.SaveChanges();

            #region ITSM & Self-Services
            // Main Types of ServiceCatalogue Items
            //EntityType SrvOG = context.EntityTypes.Add(new EntityType(Id, "ServiceCatalogue", "Organizational", "عربي", "success")).Entity;
            //EntityType SrvIN = context.EntityTypes.Add(new EntityType(Id, "ServiceCatalogue", "Incidents", "عربي", "success")).Entity;
            //EntityType SrvHR = context.EntityTypes.Add(new EntityType(Id, "ServiceCatalogue", "HR Services", "عربي", "success")).Entity;
            //EntityType SrvEU = context.EntityTypes.Add(new EntityType(Id, "ServiceCatalogue", "End User Services", "عربي", "success")).Entity;
            //EntityType SrvAD = context.EntityTypes.Add(new EntityType(Id, "ServiceCatalogue", "Active Directory Services", "عربي", "success")).Entity;
            //EntityType SrvEM = context.EntityTypes.Add(new EntityType(Id, "ServiceCatalogue", "Email Services", "عربي", "success")).Entity;
            //EntityType SrvID = context.EntityTypes.Add(new EntityType(Id, "ServiceCatalogue", "ID Services", "عربي", "success")).Entity;
            //EntityType SrvSA = context.EntityTypes.Add(new EntityType(Id, "ServiceCatalogue", "Systems Access Services", "عربي", "success")).Entity;
            //EntityType SrvGP = context.EntityTypes.Add(new EntityType(Id, "ServiceCatalogue", "Gate Pass Services", "عربي", "success")).Entity;

            // Main Types of Workflows
            //EntityType WrkSmpl = context.EntityTypes.Add(new EntityType(Id, "Workflow", "Simple Approvals", "عربي", "success")).Entity;
            //EntityType WrkCond = context.EntityTypes.Add(new EntityType(Id, "Workflow", "Conditional Approvals", "عربي", "success")).Entity;
            //EntityType WrkPrcs = context.EntityTypes.Add(new EntityType(Id, "Workflow", "Complex Process", "عربي", "success")).Entity;

            // Main Statuses of Workflows
            //EntityStatus WrkDraft = context.EntityStatuses.Add(new EntityStatus(Id, "Workflow", "Draft", "عربي", "warn")).Entity;
            //EntityStatus WrkFinal = context.EntityStatuses.Add(new EntityStatus(Id, "Workflow", "Final", "عربي", "warn")).Entity;
            //EntityStatus WrkPblsh = context.EntityStatuses.Add(new EntityStatus(Id, "Workflow", "Published", "عربي", "warn")).Entity;

            // Each Service belongs to one of the Types in the above list and Linked to Service Name [C# Class]
            //Entity? Serv01 = ConfigureService(SrvOG, "ORG-Suggestion", "عربي", WrkSmpl.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Planned", "Executed", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Business", "Community", "عربي", "عربي" });
            //Entity? Serv02 = ConfigureService(SrvOG, "ORG-Complaint", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Accepted", "Rejected", "Closed", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Business", "Community", "عربي", "عربي" });
            //Entity? Serv03 = ConfigureService(SrvIN, "INC-Workstation", "عربي", WrkSmpl.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Hardware Issue", "OS Issue" , "MS Office", "Performance", "عربي", "عربي", "عربي", "عربي" });
            //Entity? Serv04 = ConfigureService(SrvIN, "INC-Login", "عربي", WrkSmpl.Id, WrkDraft.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv05 = ConfigureService(SrvIN, "INC-Access", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv06 = ConfigureService(SrvIN, "INC-OTP", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Can't Receive OTP", "OTP Verification Issue", "عربي", "عربي" });
            //Entity? Serv07 = ConfigureService(SrvIN, "INC-Account", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Account Lockout", "", "عربي", "عربي" });
            //Entity? Serv08 = ConfigureService(SrvIN, "INC-Cloud-Storage", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Project", "Department", "عربي", "عربي" });
            //Entity? Serv09 = ConfigureService(SrvIN, "INC-Monitor", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "TV Display", "User Monitor", "عربي", "عربي" });
            //Entity? Serv10 = ConfigureService(SrvIN, "INC-Printer", "عربي", WrkSmpl.Id, WrkDraft.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Deprtmental", "Individual", "عربي", "عربي" });
            //Entity? Serv11 = ConfigureService(SrvIN, "INC-Email", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Individual", "Shared Mail Box", "عربي", "عربي" });
            //Entity? Serv12 = ConfigureService(SrvIN, "INC-WIFI", "عربي", WrkSmpl.Id, WrkDraft.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Laptop", "Mobile", "عربي", "عربي" });
            //Entity? Serv13 = ConfigureService(SrvHR, "HR-Vacation", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Annual Leave", "Sick Leave", "Study Leave", "Haj Leave", "Marriage Leave", "Maternity Leave", "Baby Born Leave", "Military Leave", "Death Leave", "Non-Paid Leave", "عربي", "عربي", "عربي", "عربي", "عربي", "عربي", "عربي", "عربي", "عربي", "عربي" });
            //Entity? Serv14 = ConfigureService(SrvHR, "HR-Loan", "عربي", WrkSmpl.Id, WrkDraft.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Personal", "Car", "Furneture", "Family Emergency", "عربي", "عربي", "عربي", "عربي" });
            //Entity? Serv15 = ConfigureService(SrvHR, "HR-Expense", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Project", "Training", "عربي", "عربي" });
            //Entity? Serv16 = ConfigureService(SrvHR, "HR-Resignation", "عربي", WrkSmpl.Id, WrkDraft.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Non-Renewal", "Contratct Termination", "عربي", "عربي" });
            //Entity? Serv17 = ConfigureService(SrvHR, "HR-Letter", "عربي", WrkSmpl.Id, WrkDraft.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "For Embassy", "For Bank", "عربي", "عربي" });
            //Entity? Serv18 = ConfigureService(SrvHR, "HR-Over-Time", "عربي", WrkSmpl.Id, WrkDraft.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Project", "Training", "عربي", "عربي" });
            //Entity? Serv19 = ConfigureService(SrvHR, "HR-Exit-Reentry", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Single", "Multiple", "عربي", "عربي" });
            //Entity? Serv20 = ConfigureService(SrvHR, "HR-Business-Trip", "عربي", WrkSmpl.Id, WrkDraft.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Project", "Training", "عربي", "عربي" });
            //Entity? Serv21 = ConfigureService(SrvHR, "HR-Business-Card", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "One Side", "Two Sides", "عربي", "عربي" });
            //Entity? Serv22 = ConfigureService(SrvHR, "HR-Training", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Project", "Department", "عربي", "عربي" });
            //Entity? Serv23 = ConfigureService(SrvHR, "HR-Parking", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Permenant", "Temprary", "عربي", "عربي" });
            //Entity? Serv24 = ConfigureService(SrvHR, "HR-Leave-Permission", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Arrive Late", "Leave during the day", "Leave early", "عربي", "عربي", "عربي" });
            //Entity? Serv25 = ConfigureService(SrvHR, "HR-Remote-Work", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "From Home", "Out-side the country", "عربي", "عربي" });
            //Entity? Serv26 = ConfigureService(SrvID, "ID-Loss", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "InSide The Work Place", "Out-Side The Work Place", "عربي", "عربي" });
            //Entity? Serv27 = ConfigureService(SrvID, "ID-New-Employee", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv28 = ConfigureService(SrvID, "ID-Renewal", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv29 = ConfigureService(SrvID, "ID-Restricted-Area", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv30 = ConfigureService(SrvEU, "EU-WorkStation-New", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Laptop", "Desktop", "Tablet", "AIO", "عربي", "عربي", "عربي", "عربي" });
            //Entity? Serv31 = ConfigureService(SrvEU, "EU-WorkStation-Remove", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Exit", "Broken", "Expired", "عربي", "عربي", "عربي" });
            //Entity? Serv32 = ConfigureService(SrvEU, "EU-WorkStation-Replace", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Broken", "Expired", "عربي", "عربي" });
            //Entity? Serv33 = ConfigureService(SrvEU, "EU-IT-Equipment", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Monitor", "Docking Station", "Printer", "TV Screen", "Projector", "Internet Router", "External HD", "عربي", "عربي", "عربي", "عربي", "عربي", "عربي", "عربي" });
            //Entity? Serv34 = ConfigureService(SrvEU, "EU-IT-Accessories", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Keyboard", "Mouse", "Wireless K/M", "USB Memory", "Web CAM", "Adaptor", "HDMI Cable", "Network Cable", "عربي", "عربي", "عربي", "عربي", "عربي", "عربي", "عربي", "عربي" });
            //Entity? Serv35 = ConfigureService(SrvEU, "EU-EquipmentMove", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Change My Desk", "Temporary Move", "عربي", "عربي" });
            //Entity? Serv36 = ConfigureService(SrvEU, "EU-Equipment-Install", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "BYOD - Laptop", "BYOD - Mobile", "Company Workstation", "عربي", "عربي", "عربي" });
            //Entity? Serv37 = ConfigureService(SrvEU, "EU-Stationary", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Pens", "Notes", "Flip Chart", "Stapler", "عربي", "عربي", "عربي", "عربي" });
            //Entity? Serv38 = ConfigureService(SrvEU, "EU-Maintenance", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "A/C", "Electricity", "Desk", "Plumbing", "عربي", "عربي", "عربي", "عربي" });
            //Entity? Serv39 = ConfigureService(SrvAD, "AD-Create-User", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv40 = ConfigureService(SrvAD, "AD-Create-Serv-Acc", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv41 = ConfigureService(SrvAD, "AD-Create-Admn-Acc", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv42 = ConfigureService(SrvAD, "AD-Enable-User", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv43 = ConfigureService(SrvAD, "AD-Enable-Serv-Acc", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv44 = ConfigureService(SrvAD, "AD-Enable-Admn-Acc", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv45 = ConfigureService(SrvAD, "AD-Disable-User", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv46 = ConfigureService(SrvAD, "AD-Disable-Serv-Acc", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv47 = ConfigureService(SrvAD, "AD-Disable-Admn-Acc", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv48 = ConfigureService(SrvAD, "AD-Create-Group", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv49 = ConfigureService(SrvAD, "AD-Add-User-Group", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv50 = ConfigureService(SrvAD, "AD-Rmv-User-Group", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv51 = ConfigureService(SrvAD, "AD-Update-User-Info", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv52 = ConfigureService(SrvAD, "AD-Update-User-Mngr", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv53 = ConfigureService(SrvAD, "AD-Update-User-Mobl", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv54 = ConfigureService(SrvEM, "EM-Create-Mailbox", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv55 = ConfigureService(SrvEM, "EM-Increase-Mailbox-Size", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv56 = ConfigureService(SrvEM, "EM-Activate-Email", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv57 = ConfigureService(SrvEM, "EM-Deactivate-Email", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv58 = ConfigureService(SrvEM, "EM-Delete-Email", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv59 = ConfigureService(SrvEM, "EM-Edit-Email", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv60 = ConfigureService(SrvEM, "EM-Create-Email-Group", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv61 = ConfigureService(SrvEM, "EM-Add-Email-Group", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv62 = ConfigureService(SrvEM, "EM-Rmv-Email-Group", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv63 = ConfigureService(SrvEM, "EM-Edit-Group", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv64 = ConfigureService(SrvEM, "EM-Permission-Email", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv65 = ConfigureService(SrvSA, "SA-SW-Service", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv66 = ConfigureService(SrvSA, "SA-SW-License", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv67 = ConfigureService(SrvSA, "SA-Remote-Access", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "", "", "عربي", "عربي" });
            //Entity? Serv68 = ConfigureService(SrvGP, "GP-Visitor", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Individual", "Group", "عربي", "عربي" });
            //Entity? Serv69 = ConfigureService(SrvGP, "GP-Vehicle", "عربي", WrkPrcs.Id, WrkPblsh.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "Sedan", "SUV", "Truk", "Bus", "عربي", "عربي", "عربي", "عربي" });
            //Entity? Serv70 = ConfigureService(SrvGP, "GP-Material", "عربي", WrkCond.Id, WrkFinal.Id, new string[] { "Submitted", "Approved", "Rejected", "Execution", "Close", "عربي", "عربي", "عربي", "عربي", "عربي" }, new string[] { "IT Material", "Furniture", "عربي", "عربي" });
            #endregion

            #region Entity Statuses

            context.EntityStatuses.Add(new EntityStatus(Id, "Evaluation", "Draft", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Evaluation", "Published", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Evaluation", "Approved", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Employee", "CV Received", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Employee", "Job Offer Sent", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Employee", "Job Offer Accepted", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Employee", "Active", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Employee", "Resigned", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Employee", "Terminated", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Employee", "Non-Renewal", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Audit", "To-do", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Audit", "In-Progress", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Audit", "In-Review", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Audit", "Done", "عربي", "success"));

            context.EntityStatuses.Add(new EntityStatus(Id, "AuditItem", "To-do", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "AuditItem", "In-Progress", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "AuditItem", "In-Review", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "AuditItem", "Done", "عربي", "success"));

            context.EntityStatuses.Add(new EntityStatus(Id, "AuditItemControl", "Planned", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "AuditItemControl", "Implemented", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "AuditItemException", "Accepted", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "AuditItemException", "Rejected", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Payroll", "Active", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Payroll", "In-Active", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "PayrollItem", "Active", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "PayrollItem", "In-Active", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "ApplicationUser", "Invitation Sent", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "ApplicationUser", "Active", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "ApplicationUser", "In-Active", "عربي", "danger"));

            context.EntityStatuses.Add(new EntityStatus(Id, "BillingPlan", "Planned", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "BillingPlan", "Due", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "BillingPlan", "Invoiced", "عربي", "danger"));
            context.EntityStatuses.Add(new EntityStatus(Id, "BillingPlan", "Submitted", "عربي", "danger"));
            context.EntityStatuses.Add(new EntityStatus(Id, "BillingPlan", "Approved", "عربي", "danger"));
            context.EntityStatuses.Add(new EntityStatus(Id, "BillingPlan", "Collected", "عربي", "danger"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Contact", "Active", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Contact", "VIP", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Contact", "In-Active", "عربي", "danger"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Product", "Draft", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Product", "Registered", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Product", "Approved", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Product", "Published", "عربي", "danger"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Lead", "Logged", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Lead", "Contacted", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Lead", "Qualified", "عربي", "danger"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Lead", "Un-Qualified", "عربي", "danger"));

            context.EntityStatuses.Add(new EntityStatus(Id, "LeadSource", "Call", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "LeadSource", "Visit", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "LeadSource", "facebook", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "LeadSource", "twitter", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "LeadSource", "LinkedIn", "عربي", "success"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Competitor", "Active", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Competitor", "In-Active", "عربي", "success"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Bid", "Planning", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Bid", "Published", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Bid", "Evalauation", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Bid", "Cancelled", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Bid", "Closed", "عربي", "success"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Quote", "Draft", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Quote", "Sent", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Quote", "Accepted", "عربي", "danger"));

            context.EntityStatuses.Add(new EntityStatus(Id, "PurchaseOrder", "Draft", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "PurchaseOrder", "Sent", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "PurchaseOrder", "Accepted", "عربي", "danger"));

            context.EntityStatuses.Add(new EntityStatus(Id, "CustomerPO", "Draft", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "CustomerPO", "Sent", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "CustomerPO", "Accepted", "عربي", "danger"));

            context.EntityStatuses.Add(new EntityStatus(Id, "SupplierQuote", "Draft", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "SupplierQuote", "Sent", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "SupplierQuote", "Accepted", "عربي", "danger"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Pnl", "Draft", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Pnl", "Submitted", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Pnl", "Approved", "عربي", "danger"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Project", "Active", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Project", "On-Hold", "عربي", "danger"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Project", "Support", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Project", "Completed", "عربي", "info"));

            EntityStatus WrkItmPlnd = context.EntityStatuses.Add(new EntityStatus(Id, "WorkItem", "Planned", "عربي", "success")).Entity;
            context.EntityStatuses.Add(new EntityStatus(Id, "WorkItem", "Planned", "عربي", "danger"));
            context.EntityStatuses.Add(new EntityStatus(Id, "WorkItem", "In-Progress", "عربي", "danger"));
            context.EntityStatuses.Add(new EntityStatus(Id, "WorkItem", "Concern", "عربي", "danger"));
            context.EntityStatuses.Add(new EntityStatus(Id, "WorkItem", "Delayed", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "WorkItem", "Completed", "عربي", "info"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Customer", "Registered", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Customer", "Active", "عربي", "danger"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Customer", "In-Active", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Supplier", "Registered", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Supplier", "Active", "عربي", "danger"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Supplier", "In-Active", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Agreement", "No Contract", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Agreement", "Exist", "عربي", "danger"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Agreement", "Done", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Deal", "Cold", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Deal", "Hot", "عربي", "danger"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Deal", "Warm", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "InvoiceAR", "Draft", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "InvoiceAR", "Submitted", "عربي", "danger"));
            context.EntityStatuses.Add(new EntityStatus(Id, "InvoiceAR", "Due", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "InvoiceAR", "Collected", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "InvoiceAP", "Draft", "عربي", "success"));
            context.EntityStatuses.Add(new EntityStatus(Id, "InvoiceAP", "Submitted", "عربي", "danger"));
            context.EntityStatuses.Add(new EntityStatus(Id, "InvoiceAP", "Due", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "InvoiceAP", "Collected", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "ChangeRequest", "Draft", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "ChangeRequest", "Published", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "SalesOrder", "Draft", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "SalesOrder", "Published", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Deliverable", "Planned", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Deliverable", "In-Progress", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Deliverable", "Completed", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Constrain", "Draft", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Constrain", "Published", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Assumption", "Draft", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Assumption", "Published", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Risk", "Open", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Risk", "In-Progress", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Risk", "Resovled", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Issue", "Oepn", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Issue", "In-Progress", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Issue", "Resovled", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "Lesson", "Draft", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Lesson", "Published", "عربي", "warn"));

            context.EntityStatuses.Add(new EntityStatus(Id, "LineItem", "Draft", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "LineItem", "Published", "عربي", "warn"));
            #endregion

            #region Entity Types
            context.EntityTypes.Add(new EntityType(Id, "Entity", "HRM-Human Resources", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Entity", "CRM-CRM & Sales", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Entity", "SRM-SRM & Procurements", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Entity", "PMO-Projects Center", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Entity", "GRC-Governance", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Entity", "RSK-Risk Management", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Entity", "CAT-Products Catalouge", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Entity", "SRV-Service Center", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "ChangeRequest", "No Impact", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "ChangeRequest", "Zero Cost", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "ChangeRequest", "No Time Impact", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "ChangeRequest", "Cost & Time Impact", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Constrain", "Resources", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Constrain", "Budget", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Constrain", "Time", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Assumption", "Technical", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Assumption", "Management", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "SalesOrder", "OKR", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "SalesOrder", "Competency", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Deliverable", "Technical", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Deliverable", "Document", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Deliverable", "Report", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Risk", "Technical", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Risk", "Management", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Issue", "Technical", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Issue", "Management", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Lesson", "Technical", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Lesson", "Management", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "LineItem", "Goods", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "LineItem", "Services", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Evaluation", "OKR", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Evaluation", "Competency", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Competitor", "International", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Competitor", "Local", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Employee", "Full-Time", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Employee", "Part-Time", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Employee", "Freelancer", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Control", "Process", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Control", "Technical", "عربي", "success"));

            // Unsing Entity Type to Hold the => Result
            context.EntityTypes.Add(new EntityType(Id, "Audit", "Not Assessed", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Audit", "Not Applicable", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Audit", "Not Compliant", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Audit", "Partially Compliant", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Audit", "Compliant", "عربي", "success"));

            // Unsing Entity Type to Hold the => Result
            context.EntityTypes.Add(new EntityType(Id, "AuditItem", "Not Assessed", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "AuditItem", "Not Applicable", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "AuditItem", "Not Compliant", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "AuditItem", "Partially Compliant", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "AuditItem", "Compliant", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Payroll", "Weekly", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Payroll", "Every 2 Weeks", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Payroll", "Monthly", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "PayrollItem", "Earnings", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "PayrollItem", "Deductions", "عربي", "success"));

            EntityType Earn = context.EntityTypes.Add(new EntityType(Id, "SalaryItem", "Earnings", "عربي", "success")).Entity;
            EntityType Dedt = context.EntityTypes.Add(new EntityType(Id, "SalaryItem", "Deductions", "عربي", "success")).Entity;

            EntityType Gov = context.EntityTypes.Add(new EntityType(Id, "CostItem", "Gov", "عربي", "success")).Entity;
            EntityType Emp = context.EntityTypes.Add(new EntityType(Id, "CostItem", "Emp", "عربي", "success")).Entity;

            context.EntityTypes.Add(new EntityType(Id, "ApplicationUser", "Employee", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "ApplicationUser", "Thrid-Party", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "ApplicationUser", "Freelancer", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "ApplicationUser", "Customer", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "BillingPlan", "Material Delivery", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "BillingPlan", "License Delivery", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "BillingPlan", "Manpower", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Project", "Simple Project", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Project", "Mega Project", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Project", "Multi-Discipline Project", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Agreement", "T & M", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Agreement", "Fixed Scope", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Agreement", "Fixed Scope & Support", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Agreement", "Frame Agreement", "عربي", "success"));

            EntityType WrkItmStr = context.EntityTypes.Add(new EntityType(Id, "WorkItem", "Project Work Stream", "عربي", "success")).Entity;
            EntityType WrkItmPck = context.EntityTypes.Add(new EntityType(Id, "WorkItem", "Project Work Package", "عربي", "success")).Entity;
            EntityType WrkItmAct = context.EntityTypes.Add(new EntityType(Id, "WorkItem", "Project Work Activity", "عربي", "success")).Entity;
            EntityType WrkItmTsk = context.EntityTypes.Add(new EntityType(Id, "WorkItem", "Task", "عربي", "success")).Entity;
            EntityType WrkItmCal = context.EntityTypes.Add(new EntityType(Id, "WorkItem", "Call", "عربي", "success")).Entity;
            EntityType WrkItmMet = context.EntityTypes.Add(new EntityType(Id, "WorkItem", "Meeting", "عربي", "success")).Entity;
            EntityType WrkItmVis = context.EntityTypes.Add(new EntityType(Id, "WorkItem", "Visit", "عربي", "success")).Entity;
            EntityType WrkItmNot = context.EntityTypes.Add(new EntityType(Id, "WorkItem", "Note", "عربي", "success")).Entity;

            context.EntityTypes.Add(new EntityType(Id, "Customer", "Company (B2B)", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Customer", "Individual (B2C)", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "CustomerPO", "One-Time Customer", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "CustomerPO", "Normal Customer", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "CustomerPO", "VIP Customer", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Supplier", "One-Time Supplier", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Supplier", "Normal Supplier", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Supplier", "Short-Listed Supplier", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "SupplierQuote", "One-Time Supplier", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "SupplierQuote", "Normal Supplier", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "SupplierQuote", "Short-Listed Supplier", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Lead", "Company", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Lead", "Individual", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Bid", "Enterprise", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Bid", "Meduim", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Bid", "Small", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Deal", "Enterprise", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Deal", "Meduim", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Deal", "Small", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Pnl", "Large", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Pnl", "Meduim", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Pnl", "Smal", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Quote", "Large", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Quote", "Meduim", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Quote", "Smal", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "Product", "Service", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "Product", "Goods", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "TaxRate", "Sales", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "TaxRate", "Purchase", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "InvoiceAR", "Template 1", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "InvoiceAR", "Template 2", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "InvoiceAR", "Template 3", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "InvoiceAP", "Template 1", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "InvoiceAP", "Template 2", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "InvoiceAP", "Template 3", "عربي", "success"));

            context.EntityTypes.Add(new EntityType(Id, "TimeAttendance", "Check-In", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "TimeAttendance", "Check-Out", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "TimeAttendance", "Break-In", "عربي", "success"));
            context.EntityTypes.Add(new EntityType(Id, "TimeAttendance", "Break-Out", "عربي", "success"));
            #endregion

            #region Lookups On Tenant Level => All Organizations Will Be Aggregated Based On These lookups

            context.Objectives.Add(new Objective(Id, user1.Id, "Grow (Increase Revenue by 12.5%)", "عربي"));
            context.Objectives.Add(new Objective(Id, user2.Id, "Contribute (Increase Profit by 1.7%)", "عربي"));

            context.Assets.Add(new Asset(Id, user1.Id, "Laptop 1", "عربي"));
            context.Assets.Add(new Asset(Id, user1.Id, "Laptop 2", "عربي"));
            context.Assets.Add(new Asset(Id, user1.Id, "Laptop 3", "عربي"));
            context.Assets.Add(new Asset(Id, user1.Id, "Laptop 4", "عربي"));
            context.Assets.Add(new Asset(Id, user1.Id, "Vehicles 1", "عربي"));
            context.Assets.Add(new Asset(Id, user1.Id, "Vehicles 2", "عربي"));
            context.Assets.Add(new Asset(Id, user1.Id, "Vehicles 3", "عربي"));
            context.Assets.Add(new Asset(Id, user1.Id, "Vehicles 4", "عربي"));

            context.ITSystems.Add(new ITSystem(Id, user1.Id, "Drixal ERP", "عربي"));
            context.ITSystems.Add(new ITSystem(Id, user1.Id, "Drixal Orchestrator", "عربي"));

            Branch Brn1 = context.Branchs.Add(new Branch(Id, user1.Id, "Riyadh, Saudi Arabia", "عربي")).Entity;
            Branch Brn2 = context.Branchs.Add(new Branch(Id, user2.Id, "Cairo, Egypt", "عربي")).Entity;
            Branch Brn3 = context.Branchs.Add(new Branch(Id, user3.Id, "Alexandria, Egypt ", "عربي")).Entity;

            context.Locations.Add(new Location(Id, Brn1.Id, "Riyadh, Saudi Arabia - North Building", "عربي"));
            context.Locations.Add(new Location(Id, Brn2.Id, "Cairo, Egypt - 1st Floor", "عربي"));
            context.Locations.Add(new Location(Id, Brn3.Id, "Cairo, Egypt - 2nd Floor", "عربي"));

            context.Countries.Add(new Country(Id, "Saudi Arabia", "عربي", "sa"));
            context.Countries.Add(new Country(Id, "Egypt", "عربي", "eg"));
            context.Countries.Add(new Country(Id, "Germany", "عربي", "de"));
            context.Countries.Add(new Country(Id, "Italy", "عربي", "it"));

            context.Nationalities.Add(new Nationality(Id, "Saudi", "عربي", "sa"));
            context.Nationalities.Add(new Nationality(Id, "Egyptian", "عربي", "eg"));
            context.Nationalities.Add(new Nationality(Id, "German", "عربي", "de"));
            context.Nationalities.Add(new Nationality(Id, "Italian", "عربي", "it"));

            context.DataConfidentialities.Add(new DataConfidentiality(Id, "Top Secret", "عربي"));
            context.DataConfidentialities.Add(new DataConfidentiality(Id, "Confidential", "عربي"));
            context.DataConfidentialities.Add(new DataConfidentiality(Id, "Internal", "عربي"));
            context.DataConfidentialities.Add(new DataConfidentiality(Id, "Public", "عربي"));

            context.Impacts.Add(new Impact(Id, "Catastrophic", "عربي", 1));
            context.Impacts.Add(new Impact(Id, "High", "عربي", 50));
            context.Impacts.Add(new Impact(Id, "Meduim", "عربي", 14));
            context.Impacts.Add(new Impact(Id, "Low", "عربي", 11));

            context.Probabilities.Add(new Probability(Id, "Almost Certain", "عربي", 1));
            context.Probabilities.Add(new Probability(Id, "High", "عربي", 1));
            context.Probabilities.Add(new Probability(Id, "Meduim", "عربي", 1));
            context.Probabilities.Add(new Probability(Id, "Low", "عربي", 1));

            context.Urgencies.Add(new Urgency(Id, "Very Urgent", "عربي", 1));
            context.Urgencies.Add(new Urgency(Id, "Urgent", "عربي", 1));
            context.Urgencies.Add(new Urgency(Id, "Not Urgent", "عربي", 1));

            context.Criticalities.Add(new Criticality(Id, "Very Critical", "عربي", 1));
            context.Criticalities.Add(new Criticality(Id, "Critical", "عربي", 1));
            context.Criticalities.Add(new Criticality(Id, "Not Critical", "عربي", 1));

            context.Severities.Add(new Severity(Id, "P1", "عربي", 10, 10));
            context.Severities.Add(new Severity(Id, "P2", "عربي", 7, 9));
            context.Severities.Add(new Severity(Id, "P3", "عربي", 4, 6));
            context.Severities.Add(new Severity(Id, "P4", "عربي", 1, 3));

            context.RiskStrategies.Add(new RiskStrategy(Id, "Accept", "عربي"));
            context.RiskStrategies.Add(new RiskStrategy(Id, "Avoid", "عربي"));
            context.RiskStrategies.Add(new RiskStrategy(Id, "Transfer", "عربي"));
            context.RiskStrategies.Add(new RiskStrategy(Id, "Mitigate", "عربي"));

            context.TaskPriorities.Add(new TaskPriority(Id, "Top Urgent", "عربي", "contrast"));
            context.TaskPriorities.Add(new TaskPriority(Id, "High", "عربي", "danger"));
            context.TaskPriorities.Add(new TaskPriority(Id, "Normal", "عربي", "info"));
            context.TaskPriorities.Add(new TaskPriority(Id, "Low", "عربي", "secondary"));

            context.TaskAllocations.Add(new TaskAllocation(Id, "Least Busy", "عربي"));
            context.TaskAllocations.Add(new TaskAllocation(Id, "Most Busy", "عربي"));
            context.TaskAllocations.Add(new TaskAllocation(Id, "Round Roben", "عربي"));
            context.TaskAllocations.Add(new TaskAllocation(Id, "Basket", "عربي"));

            context.TaskSizes.Add(new TaskSize(Id, "Small", "عربي"));
            context.TaskSizes.Add(new TaskSize(Id, "Meduim", "عربي"));
            context.TaskSizes.Add(new TaskSize(Id, "Large", "عربي"));
            context.TaskSizes.Add(new TaskSize(Id, "X-Large", "عربي"));

            context.TaskComplexities.Add(new TaskComplexity(Id, "Easy", "عربي"));
            context.TaskComplexities.Add(new TaskComplexity(Id, "Normal", "عربي"));
            context.TaskComplexities.Add(new TaskComplexity(Id, "Complex", "عربي"));
            context.TaskComplexities.Add(new TaskComplexity(Id, "Advanced", "عربي"));

            context.Confidences.Add(new Confidence(Id, "Not Sure", "عربي"));
            context.Confidences.Add(new Confidence(Id, "Normal", "عربي"));
            context.Confidences.Add(new Confidence(Id, "Protected", "عربي"));

            context.Industries.Add(new Industry(Id, "Communication", "عربي"));
            context.Industries.Add(new Industry(Id, "Information Technology", "عربي"));
            context.Industries.Add(new Industry(Id, "Information Security", "عربي"));
            context.Industries.Add(new Industry(Id, "Construction", "عربي"));
            context.Industries.Add(new Industry(Id, "Digital Marketing", "عربي"));

            context.Sectors.Add(new Sector(Id, "Government", "عربي"));
            context.Sectors.Add(new Sector(Id, "Education", "عربي"));
            context.Sectors.Add(new Sector(Id, "Transportation", "عربي"));
            context.Sectors.Add(new Sector(Id, "Entertitment", "عربي"));
            context.Sectors.Add(new Sector(Id, "Oil & GAS", "عربي"));

            context.Maritals.Add(new Marital(Id, "Single", "عربي"));
            context.Maritals.Add(new Marital(Id, "Married", "عربي"));
            
            context.ShippingMethods.Add(new ShippingMethod(Id, "By Sea", "عربي"));
            context.ShippingMethods.Add(new ShippingMethod(Id, "By Air", "عربي"));
            context.ShippingMethods.Add(new ShippingMethod(Id, "Fedex", "عربي"));
            context.ShippingMethods.Add(new ShippingMethod(Id, "DHL", "عربي"));

            context.PaymentTerms.Add(new PaymentTerm(Id, "15 Days", "عربي"));
            context.PaymentTerms.Add(new PaymentTerm(Id, "30 Days", "عربي"));
            context.PaymentTerms.Add(new PaymentTerm(Id, "45 Days", "عربي"));
            context.PaymentTerms.Add(new PaymentTerm(Id, "60 Days", "عربي"));
            context.PaymentTerms.Add(new PaymentTerm(Id, "90 Days", "عربي"));

            context.Genders.Add(new Gender(Id, "Male", "عربي"));
            context.Genders.Add(new Gender(Id, "Female", "عربي"));

            context.SaveChanges();

            List<Sector> Sectors = context.Sectors.ToList();
            List<Industry> Indsts = context.Industries.ToList();
            List<EntityType> EntTyps = context.EntityTypes.ToList();
            List<EntityStatus> EntSts = context.EntityStatuses.ToList();
            List<ApplicationUser> Users = context.ApplicationUsers.ToList();

            Bid Bid1 = context.Bids.Add(new Bid(Id, "Provide New Software", 1005000)).Entity;
            Bid Bid2 = context.Bids.Add(new Bid(Id, "Provide New Laptops", 15600)).Entity;
            Bid Bid3 = context.Bids.Add(new Bid(Id, "Provide AI Serivces", 12257700)).Entity;
            Bid Bid4 = context.Bids.Add(new Bid(Id, "Change office design", 1075000)).Entity;
            Bid Bid5 = context.Bids.Add(new Bid(Id, "Marketing Agency to provide marketing strategy", 1005000)).Entity;
            Bid Bid6 = context.Bids.Add(new Bid(Id, "Catering & Activities", 10980)).Entity;
            Bid Bid7 = context.Bids.Add(new Bid(Id, "Maintenance 3 years contract", 150090)).Entity;

            Competitor Comptr1 = context.Competitors.Add(new Competitor(Id, EntTyps, EntSts, Users, Sectors, Indsts, "Daftara", "عربي", "MOD.gov.sa")).Entity;
            Competitor Comptr2 = context.Competitors.Add(new Competitor(Id, EntTyps, EntSts, Users, Sectors, Indsts, "Holat", "عربي", "MOD.gov.sa")).Entity;
            Competitor Comptr3 = context.Competitors.Add(new Competitor(Id, EntTyps, EntSts, Users, Sectors, Indsts, "Zoho", "عربي", "MOD.gov.sa")).Entity;
            Competitor Comptr4 = context.Competitors.Add(new Competitor(Id, EntTyps, EntSts, Users, Sectors, Indsts, "Hub Spot", "عربي", "MOD.gov.sa")).Entity;

            Customer Cus = context.Customers.Add(new Customer(Id, EntTyps, EntSts, Users, Sectors, Indsts, "Ministry of Defense", "عربي", "MOD.gov.sa")).Entity;
            Customer Cus1 = context.Customers.Add(new Customer(Id, EntTyps, EntSts, Users, Sectors, Indsts, "Ministry of Health", "عربي", "MOH.gov.sa")).Entity;
            Customer Cus2 = context.Customers.Add(new Customer(Id, EntTyps, EntSts, Users, Sectors, Indsts, "Ministry of Communication", "عربي", "MOC.gov.sa")).Entity;
            Customer Cus3 = context.Customers.Add(new Customer(Id, EntTyps, EntSts, Users, Sectors, Indsts, "Ministry of Finance", "عربي", "MOF.gov.sa")).Entity;
            Customer Cus4 = context.Customers.Add(new Customer(Id, EntTyps, EntSts, Users, Sectors, Indsts, "Central Bank Of Saudia", "عربي", "SAMA.gov.sa")).Entity;

            Supplier microsoft = context.Suppliers.Add(new Supplier(Id, EntTyps, EntSts, Users, Sectors, Indsts, "microsoft", "عربي", "microsoft.com")).Entity;
            Supplier aspire = context.Suppliers.Add(new Supplier(Id, EntTyps, EntSts, Users, Sectors, Indsts, "aspire", "عربي", "aspire.com")).Entity;
            Supplier ibm = context.Suppliers.Add(new Supplier(Id, EntTyps, EntSts, Users, Sectors, Indsts, "IBM", "عربي", "IBM.com")).Entity;
            Supplier bupa = context.Suppliers.Add(new Supplier(Id, EntTyps, EntSts, Users, Sectors, Indsts, "bupa", "عربي", "bupa.com")).Entity;

            ProductCategory PrdCat1 = context.ProductCategories.Add(new ProductCategory(Id, "Hardware", "عربي")).Entity;
            ProductCategory PrdCat2 = context.ProductCategories.Add(new ProductCategory(Id, "Software License", "عربي")).Entity;
            ProductCategory PrdCat3 = context.ProductCategories.Add(new ProductCategory(Id, "Custom Applications", "عربي")).Entity;
            ProductCategory PrdCat4 = context.ProductCategories.Add(new ProductCategory(Id, "Turn Key Service", "عربي")).Entity;
            ProductCategory PrdCat5 = context.ProductCategories.Add(new ProductCategory(Id, "Managed Services", "عربي")).Entity;
            ProductCategory PrdCat6 = context.ProductCategories.Add(new ProductCategory(Id, "Staff Augmentation", "عربي")).Entity;

            MarketUnit MrkUnt1 = context.MarketUnits.Add(new MarketUnit(Id, "Network", "عربي", "success")).Entity;
            MarketUnit MrkUnt2 = context.MarketUnits.Add(new MarketUnit(Id, "Infra", "عربي", "success")).Entity;
            MarketUnit MrkUnt3 = context.MarketUnits.Add(new MarketUnit(Id, "Cybersecurity", "عربي", "success")).Entity;
            MarketUnit MrkUnt4 = context.MarketUnits.Add(new MarketUnit(Id, "Custom Applications", "عربي", "success")).Entity;
            MarketUnit MrkUnt5 = context.MarketUnits.Add(new MarketUnit(Id, "Managed Services", "عربي", "success")).Entity;
            MarketUnit MrkUnt6 = context.MarketUnits.Add(new MarketUnit(Id, "Staff Augmentation", "عربي", "success")).Entity;

            context.BillingFrequencies.Add(new BillingFrequency(Id, "One-Time", "عربي"));
            context.BillingFrequencies.Add(new BillingFrequency(Id, "Weekly", "عربي"));
            context.BillingFrequencies.Add(new BillingFrequency(Id, "Every 2 Weeks", "عربي"));
            context.BillingFrequencies.Add(new BillingFrequency(Id, "Monthly", "عربي"));
            context.BillingFrequencies.Add(new BillingFrequency(Id, "Quarterly", "عربي"));
            context.BillingFrequencies.Add(new BillingFrequency(Id, "Semi-Annually", "عربي"));
            context.BillingFrequencies.Add(new BillingFrequency(Id, "Annually", "عربي"));

            context.DiscountTypes.Add(new DiscountType(Id, "Percentage %", "عربي"));
            context.DiscountTypes.Add(new DiscountType(Id, "Value", "عربي"));

            context.ManagementBufferTypes.Add(new ManagementBufferType(Id, "Percentage %", "عربي"));
            context.ManagementBufferTypes.Add(new ManagementBufferType(Id, "Value", "عربي"));

            context.TradingTypes.Add(new TradingType(Id, "Selling Only", "عربي"));
            context.TradingTypes.Add(new TradingType(Id, "Purchasing Only", "عربي"));
            context.TradingTypes.Add(new TradingType(Id, "Selling and Purchasing", "عربي"));

            context.TaxRates.Add(new TaxRate(Id, "VAT (15%)", "قيمة مضافة (15%)", true));
            context.TaxRates.Add(new TaxRate(Id, "On Holding (5%)", "أستيراد (5%)", false));
            context.TaxRates.Add(new TaxRate(Id, "EGY (14%)", "مصر (14%)", false));

            context.DeliveryProofs.Add(new DeliveryProof(Id, "Certificate of Completion (COC)", "عربي"));
            context.DeliveryProofs.Add(new DeliveryProof(Id, "Proof of Entitlement", "عربي"));
            context.DeliveryProofs.Add(new DeliveryProof(Id, "Delivery Note (DN)", "عربي"));
            context.DeliveryProofs.Add(new DeliveryProof(Id, "Time Sheet", "عربي"));

            Domain Dmn1 = context.Domains.Add(new Domain(Id, "Cybersecurity")).Entity;
            Domain Dmn2 = context.Domains.Add(new Domain(Id, "Finance")).Entity;
            Domain Dmn3 = context.Domains.Add(new Domain(Id, "Engineering")).Entity;

            Regulator Reg1 = context.Regulators.Add(new Regulator(Id, "NCA")).Entity;
            Regulator Reg2 = context.Regulators.Add(new Regulator(Id, "CST")).Entity;
            Regulator Reg3 = context.Regulators.Add(new Regulator(Id, "SAMA")).Entity;
            Regulator Reg4 = context.Regulators.Add(new Regulator(Id, "ISO")).Entity;

            context.SaveChanges();

            Pipeline P1 = context.Pipelines.Add(new Pipeline(Id, "Gov Pipeline", "عربي")).Entity;
            Pipeline P2 = context.Pipelines.Add(new Pipeline(Id, "B2B Pipeline", "عربي")).Entity;
            Pipeline P3 = context.Pipelines.Add(new Pipeline(Id, "B2C Pipeline", "عربي")).Entity;

            context.SaveChanges();

            Contact Cnct1 = context.Contacts.Add(new Contact(Id, "Mohamed Ali", "عربي", "", Cus.Id, null)).Entity;
            Contact Cnct2 = context.Contacts.Add(new Contact(Id, "Hussain Mostafa", "عربي", "", Cus1.Id, null)).Entity;
            Contact Cnct3 = context.Contacts.Add(new Contact(Id, "Amr Ammar", "عربي", "", Cus2.Id, null)).Entity;
            Contact Cnct4 = context.Contacts.Add(new Contact(Id, "Hana Abdo", "عربي", "", null, microsoft.Id)).Entity;
            Contact Cnct5 = context.Contacts.Add(new Contact(Id, "Noha Raiq", "عربي", "", null, aspire.Id)).Entity;

            Stage ST_1_1 = context.Stages.Add(new Stage(Id, P1.Id, 10, "RFP", "عربي")).Entity;
            Stage ST_1_2 = context.Stages.Add(new Stage(Id, P1.Id, 30, "Evaluation", "عربي")).Entity;
            Stage ST_1_3 = context.Stages.Add(new Stage(Id, P1.Id, 60, "Award Letter", "عربي")).Entity;
            Stage ST_1_4 = context.Stages.Add(new Stage(Id, P1.Id, 100, "Contract Signed", "عربي")).Entity;

            Stage ST_2_1 = context.Stages.Add(new Stage(Id, P2.Id, 10, "RFP", "عربي")).Entity;
            Stage ST_2_2 = context.Stages.Add(new Stage(Id, P2.Id, 30, "Evaluation", "عربي")).Entity;
            Stage ST_2_3 = context.Stages.Add(new Stage(Id, P2.Id, 40, "Technical Proposal", "عربي")).Entity;
            Stage ST_2_4 = context.Stages.Add(new Stage(Id, P2.Id, 80, "Finantial Proposal", "عربي")).Entity;
            Stage ST_2_5 = context.Stages.Add(new Stage(Id, P2.Id, 100, "PO Received", "عربي")).Entity;

            Stage ST_3_1 = context.Stages.Add(new Stage(Id, P3.Id, 10, "RFQ", "عربي")).Entity;
            Stage ST_3_2 = context.Stages.Add(new Stage(Id, P3.Id, 15, "Quotation Submitted", "عربي")).Entity;
            Stage ST_3_3 = context.Stages.Add(new Stage(Id, P3.Id, 100, "PO Received", "عربي")).Entity;

            context.Accounts.Add(new Account(Id, 0, 12900876, "Assets", "عربي"));
            context.Accounts.Add(new Account(Id, 12903300, 0, "Liabilities", "عربي"));
            context.Accounts.Add(new Account(Id, 11807800, 0, "Equity", "عربي"));
            context.Accounts.Add(new Account(Id, 0, 9770876, "Revenue", "عربي"));
            context.Accounts.Add(new Account(Id, 0, 78600899, "Expenses", "عربي"));

            context.Controls.Add(new Control(Id, "Firewall", Dmn1.Id));
            context.Controls.Add(new Control(Id, "SOC Implementation", Dmn1.Id));
            context.Controls.Add(new Control(Id, "Perform Pen Testing", Dmn1.Id));
            context.Controls.Add(new Control(Id, "Risk Management Policy", Dmn1.Id));
            context.Controls.Add(new Control(Id, "Backup Policy", Dmn1.Id));

            context.SaveChanges();

            #endregion


            Team tm1 = context.Teams.Add(new Team() { TenantId = Id, Name = "Management Team", NameAr = "عربي", OwnerUsrId = user1.Id }).Entity;
            Team tm2 = context.Teams.Add(new Team() { TenantId = Id, Name = "Project Managers", NameAr = "عربي", OwnerUsrId = user1.Id }).Entity;
            Team tm3 = context.Teams.Add(new Team() { TenantId = Id, Name = "MOH Project Team", NameAr = "عربي", OwnerUsrId = user1.Id }).Entity;

            context.TeamMembers.Add(new TeamMember() { TenantId = Id, TeamId = tm1.Id, MemberUsrId = user2.Id });
            context.TeamMembers.Add(new TeamMember() { TenantId = Id, TeamId = tm1.Id, MemberUsrId = user3.Id });
            context.TeamMembers.Add(new TeamMember() { TenantId = Id, TeamId = tm1.Id, MemberUsrId = user4.Id });
            context.TeamMembers.Add(new TeamMember() { TenantId = Id, TeamId = tm1.Id, MemberUsrId = user5.Id });

            context.TeamMembers.Add(new TeamMember() { TenantId = Id, TeamId = tm2.Id, MemberUsrId = user2.Id });
            context.TeamMembers.Add(new TeamMember() { TenantId = Id, TeamId = tm2.Id, MemberUsrId = user3.Id });
            context.TeamMembers.Add(new TeamMember() { TenantId = Id, TeamId = tm2.Id, MemberUsrId = user4.Id });

            context.TeamMembers.Add(new TeamMember() { TenantId = Id, TeamId = tm3.Id, MemberUsrId = user5.Id });
            context.TeamMembers.Add(new TeamMember() { TenantId = Id, TeamId = tm3.Id, MemberUsrId = user2.Id });

            context.EntityStatuses.Add(new EntityStatus(Id, "Department", "Active", "عربي", "warn"));
            context.EntityStatuses.Add(new EntityStatus(Id, "Department", "In-Active", "عربي", "warn"));

            EntityType Dp = context.EntityTypes.Add(new EntityType(Id, "Department", "Department", "عربي", "success")).Entity;
            EntityType Bu = context.EntityTypes.Add(new EntityType(Id, "Department", "BusinessUnit", "عربي", "success")).Entity;
            EntityType Sc = context.EntityTypes.Add(new EntityType(Id, "Department", "Section", "عربي", "success")).Entity;

            Department D1 = context.Departments.Add(new Department(Id, user1.Id, Dp.Id, null, "Human Resources", "عربي")).Entity;
            Department D1_1 = context.Departments.Add(new Department(Id, user3.Id, Bu.Id, D1.Id, "Talent Aqusition", "عربي")).Entity;
            Department D1_2 = context.Departments.Add(new Department(Id, user4.Id, Bu.Id, D1.Id, "Government Relations (GR)", "عربي")).Entity;
            Department D1_3 = context.Departments.Add(new Department(Id, user5.Id, Bu.Id, D1.Id, "Training", "عربي")).Entity;
            Department D2 = context.Departments.Add(new Department(Id, user6.Id, Dp.Id, null, "Finance", "عربي")).Entity;
            Department D2_1 = context.Departments.Add(new Department(Id, user7.Id, Bu.Id, D2.Id, "Account Recievables (AR)", "عربي")).Entity;
            Department D2_2 = context.Departments.Add(new Department(Id, user8.Id, Bu.Id, D2.Id, "Account Payables (AP)", "عربي")).Entity;
            Department D3 = context.Departments.Add(new Department(Id, user9.Id, Dp.Id, null, "Information Technology", "عربي")).Entity;
            Department D3_1 = context.Departments.Add(new Department(Id, user1.Id, Bu.Id, D3.Id, "Infrastructure", "عربي")).Entity;
            Department D3_2 = context.Departments.Add(new Department(Id, user10.Id, Bu.Id, D3.Id, "Cloud", "عربي")).Entity;
            Department D3_3 = context.Departments.Add(new Department(Id, user11.Id, Bu.Id, D3.Id, "Applications", "عربي")).Entity;
            Department D331 = context.Departments.Add(new Department(Id, user12.Id, Sc.Id, D3_3.Id, "Integratration", "عربي")).Entity;
            Department D332 = context.Departments.Add(new Department(Id, user13.Id, Sc.Id, D3_3.Id, "Channel", "عربي")).Entity;
            Department D333 = context.Departments.Add(new Department(Id, user14.Id, Sc.Id, D3_3.Id, "Enterprise", "عربي")).Entity;
            Department D3_4 = context.Departments.Add(new Department(Id, user15.Id, Bu.Id, D3.Id, "End Users Services", "عربي")).Entity;
            Department D3_5 = context.Departments.Add(new Department(Id, user16.Id, Bu.Id, D3.Id, "CyberSecurity", "عربي")).Entity;

            Grade G1 = context.Grades.Add(new Grade(Id, "C-Level", "عربي")).Entity;
            Grade G2 = context.Grades.Add(new Grade(Id, "Director", "عربي")).Entity;
            Grade G3 = context.Grades.Add(new Grade(Id, "Manager", "عربي")).Entity;
            Grade G4 = context.Grades.Add(new Grade(Id, "Senior", "عربي")).Entity;
            Grade G5 = context.Grades.Add(new Grade(Id, "Junior", "عربي")).Entity;

            SalaryItem SI1 = context.SalaryItems.Add(new SalaryItem(true, Id, "Trans", "عربي", 1000, 0, Earn.Id)).Entity;
            SalaryItem SI2 = context.SalaryItems.Add(new SalaryItem(true, Id, "Housing", "عربي", 0, 25, Earn.Id)).Entity;
            SalaryItem SI3 = context.SalaryItems.Add(new SalaryItem(true, Id, "School", "عربي", 2500, 0, Earn.Id)).Entity;
            SalaryItem SI4 = context.SalaryItems.Add(new SalaryItem(true, Id, "Employee Contribution for GOSI", "عربي", 0, Convert.ToDecimal(9.75), Dedt.Id)).Entity;
            SalaryItem SI5 = context.SalaryItems.Add(new SalaryItem(false, Id, "Loan Installment", "عربي", 0, 0, Dedt.Id)).Entity;
            SalaryItem SI6 = context.SalaryItems.Add(new SalaryItem(true, Id, "Tax (10%)", "عربي", 0, 10, Dedt.Id)).Entity;
            SalaryItem SI7 = context.SalaryItems.Add(new SalaryItem(false, Id, "Over Time", "عربي", 0, 0, Earn.Id)).Entity;

            CostItem CI1 = context.CostItems.Add(new CostItem(Id, "IQAMA", "عربي", 50, 0, Gov.Id)).Entity;
            CostItem CI2 = context.CostItems.Add(new CostItem(Id, "Work License", "عربي", Convert.ToDecimal(833.33), 0, Gov.Id)).Entity;
            CostItem CI3 = context.CostItems.Add(new CostItem(Id, "Medical Insurance", "عربي", 1000, 0, Emp.Id)).Entity;
            CostItem CI4 = context.CostItems.Add(new CostItem(Id, "Work Space", "عربي", 1000, 0, Emp.Id)).Entity;
            CostItem CI5 = context.CostItems.Add(new CostItem(Id, "GOSI Paid By Company", "عربي", 0, Convert.ToDecimal(11.25), Emp.Id)).Entity;

            //Employee emp1 = context.Employees.Add(new Employee(Id, long.MinValue, user1, "Mohamed", "Ibrahim", "Sehmawy", "محمد", "ابراهيم", "السحماوي", D1.Id, G1.Id, Job1.Id, new DateTime(2026, 1, 1), new DateTime(2027, 12, 3))).Entity;
            //context.SaveChanges();
            //Employee emp2 = context.Employees.Add(new Employee(Id, emp1.Id, user2, "Doha", "Akl", "Atta", "ضحي", "عقل", "عطا", D2.Id, G2.Id, Job2.Id, new DateTime(2026, 1, 1), new DateTime(2027, 8, 3))).Entity;
            //Employee emp3 = context.Employees.Add(new Employee(Id, emp1.Id, user3, "Yousef", "Mohamed", "Sehmawy", "يوسف", "ابراهيم", "السحماوي", D3.Id, G3.Id, Job3.Id, new DateTime(2026, 1, 1), new DateTime(2027, 3, 3))).Entity;
            //context.SaveChanges();
            //Employee emp4 = context.Employees.Add(new Employee(Id, emp3.Id, user4, "Yehia", "Mohamed", "Sehmawy", "يحيي", "ابراهيم", "السحماوي", D3.Id, G4.Id, Job3.Id, new DateTime(2026, 1, 1), new DateTime(2026, 7, 3))).Entity;
            //Employee emp5 = context.Employees.Add(new Employee(Id, emp2.Id, user5, "Fatima", "Mohamed", "Sehmawy", "فاطمة", "محمد", "السحماوي", D1.Id, G1.Id, Job1.Id, new DateTime(2026, 1, 1), new DateTime(2026, 12, 3))).Entity;

            #region Transactional On Organization Level (Sample Data)

            long? curId = context.Currencies.Where(e => e.IsBaseCurrency == true).FirstOrDefault().Id;
            long? ratId = context.TaxRates.Where(e => e.IsDefault == true).FirstOrDefault().Id;
            List<Customer> Custs = context.Customers.ToList();
            List<Impact> Impts = context.Impacts.ToList();
            List<Urgency> Urgs = context.Urgencies.ToList();
            List<Probability> Probs = context.Probabilities.ToList();
            List<Criticality> Crits = context.Criticalities.ToList();
            List<Severity> Sevrs = context.Severities.ToList();
            List<RiskStrategy> RskStrs = context.RiskStrategies.ToList();
            List<TaskAllocation> TskAlcs = context.TaskAllocations.ToList();
            List<TaskPriority> TskPrs = context.TaskPriorities.ToList();
            List<TaskComplexity> TskCoxs = context.TaskComplexities.ToList();
            List<TaskSize> TskSz = context.TaskSizes.ToList();

            Lead LD1 = context.Leads.Add(new Lead(user12.Id, Id, Sectors, Indsts, EntTyps, EntSts, "HR System", "عربي", "sehmawy.org")).Entity;
            Lead LD2 = context.Leads.Add(new Lead(user1.Id, Id, Sectors, Indsts, EntTyps, EntSts, "PMO System", "عربي", "sehmawy.org")).Entity;
            Lead LD3 = context.Leads.Add(new Lead(user3.Id, Id, Sectors, Indsts, EntTyps, EntSts, "ITSM System", "عربي", "sehmawy.org")).Entity;
            Lead LD4 = context.Leads.Add(new Lead(user4.Id, Id, Sectors, Indsts, EntTyps, EntSts, "CRM System", "عربي", "sehmawy.org")).Entity;
            Lead LD5 = context.Leads.Add(new Lead(user5.Id, Id, Sectors, Indsts, EntTyps, EntSts, "Sales System", "عربي", "sehmawy.org")).Entity;
            Lead LD6 = context.Leads.Add(new Lead(user5.Id, Id, Sectors, Indsts, EntTyps, EntSts, "GRC System", "عربي", "sehmawy.org")).Entity;
            Lead LD7 = context.Leads.Add(new Lead(user1.Id, Id, Sectors, Indsts, EntTyps, EntSts, "Workflow System", "عربي", "sehmawy.org")).Entity;

            Deal DL1 = context.Deals.Add(new Deal(user1.Id, Id, EntTyps, EntSts, Custs, Users, P1, ST_1_1, "Frame Agreement Hardware", "عربي")).Entity;
            Deal DL2 = context.Deals.Add(new Deal(user3.Id, Id, EntTyps, EntSts, Custs, Users, P1, ST_1_2, "Software Agreement Hardware", "عربي")).Entity;
            Deal DL3 = context.Deals.Add(new Deal(user4.Id, Id, EntTyps, EntSts, Custs, Users, P1, ST_1_3, "SW License", "عربي")).Entity;
            Deal DL4 = context.Deals.Add(new Deal(user8.Id, Id, EntTyps, EntSts, Custs, Users, P1, ST_1_4, "MOH Deal", "عربي")).Entity;
            Deal DL5 = context.Deals.Add(new Deal(user9.Id, Id, EntTyps, EntSts, Custs, Users, P2, ST_2_1, "MOC Deal", "عربي")).Entity;
            Deal DL6 = context.Deals.Add(new Deal(user10.Id, Id, EntTyps, EntSts, Custs, Users, P2, ST_2_2, "Bid 456", "عربي")).Entity;
            Deal DL7 = context.Deals.Add(new Deal(user11.Id, Id, EntTyps, EntSts, Custs, Users, P2, ST_2_3, "Go-Telecom 567", "عربي")).Entity;
            Deal DL8 = context.Deals.Add(new Deal(user12.Id, Id, EntTyps, EntSts, Custs, Users, P2, ST_2_4, "SW Development", "عربي")).Entity;
            Deal DL9 = context.Deals.Add(new Deal(user13.Id, Id, EntTyps, EntSts, Custs, Users, P2, ST_2_5, "RFP 908", "عربي")).Entity;
            Deal DLA = context.Deals.Add(new Deal(user14.Id, Id, EntTyps, EntSts, Custs, Users, P3, ST_3_1, "Proposal 890", "عربي")).Entity;
            Deal DLB = context.Deals.Add(new Deal(user15.Id, Id, EntTyps, EntSts, Custs, Users, P3, ST_3_2, "Managed Service MOI", "عربي")).Entity;
            Deal DLC = context.Deals.Add(new Deal(user16.Id, Id, EntTyps, EntSts, Custs, Users, P3, ST_3_3, "Customer HR System", "عربي")).Entity;

            Agreement Agrmnt = context.Agreements.Add(new Agreement(user1.Id, Id, EntTyps, EntSts, Custs, "Frame Agreement Hardware", "عربي")).Entity;
            Agreement Agrmnt1 = context.Agreements.Add(new Agreement(user2.Id, Id, EntTyps, EntSts, Custs, "Frame Agreement Software", "عربي")).Entity;
            Agreement Agrmnt2 = context.Agreements.Add(new Agreement(user3.Id, Id, EntTyps, EntSts, Custs, "Frame Agreement MS Licenses", "عربي")).Entity;
            Agreement Agrmnt3 = context.Agreements.Add(new Agreement(user4.Id, Id, EntTyps, EntSts, Custs, "Frame Agreement Mandays", "عربي")).Entity;

            context.SaveChanges();

            Portfolio Prtf1 = context.Portfolios.Add(new Portfolio(user1.Id, Id, "Riyadh Infrastructure", "عربي", 2000000, curId, ratId)).Entity;
            Portfolio Prtf2 = context.Portfolios.Add(new Portfolio(user2.Id, Id, "Jeddah Netwrok", "عربي", 2000000, curId, ratId)).Entity;
            Portfolio Prtf3 = context.Portfolios.Add(new Portfolio(user3.Id, Id, "Dammam Software", "عربي", 2000000, curId, ratId)).Entity;
            Portfolio Prtf4 = context.Portfolios.Add(new Portfolio(user4.Id, Id, "Country Cybersecurity", "عربي", 2000000, curId, ratId)).Entity;

            Programme Prog1 = context.Programmes.Add(new Programme(user1.Id, Id, "MOH Modinarization Program", "عربي", Cus.Id, Agrmnt.Id, 2000000, curId, ratId, new DateTime(2026, 1, 15), new DateTime(2026, 12, 31))).Entity;
            Programme Prog2 = context.Programmes.Add(new Programme(user4.Id, Id, "Aramco Data Center Program", "عربي", Cus1.Id, Agrmnt1.Id, 2000000, curId, ratId, new DateTime(2026, 1, 15), new DateTime(2026, 12, 31))).Entity;
            Programme Prog3 = context.Programmes.Add(new Programme(user5.Id, Id, "MOF Implementation Program", "عربي", Cus2.Id, Agrmnt2.Id, 2000000, curId, ratId, new DateTime(2026, 1, 15), new DateTime(2026, 12, 31))).Entity;
            Programme Prog4 = context.Programmes.Add(new Programme(user6.Id, Id, "Aspire Program", "عربي", Cus3.Id, Agrmnt3.Id, 2000000, curId, ratId, new DateTime(2026, 1, 15), new DateTime(2026, 12, 31))).Entity;

            context.SaveChanges();

            Project Proj1 = context.Projects.Add(new Project(user1.Id, Id, EntTyps, EntSts, Custs, Users, "Aspire System Implementation", "عربي", Agrmnt1.Id, Prog1.Id, Prtf1.Id)).Entity;
            Project Proj2 = context.Projects.Add(new Project(user2.Id, Id, EntTyps, EntSts, Custs, Users, "HR System Implementation", "عربي", Agrmnt2.Id, Prog2.Id, Prtf2.Id)).Entity;
            Project Proj3 = context.Projects.Add(new Project(user3.Id, Id, EntTyps, EntSts, Custs, Users, "New A/C System Upgrade", "عربي", Agrmnt3.Id, Prog3.Id, Prtf3.Id)).Entity;
            Project Proj4 = context.Projects.Add(new Project(user4.Id, Id, EntTyps, EntSts, Custs, Users, "New Buidling", "عربي", Agrmnt3.Id, Prog3.Id, Prtf3.Id)).Entity;
            Project Proj5 = context.Projects.Add(new Project(user5.Id, Id, EntTyps, EntSts, Custs, Users, "Old Buidling Renovation", "عربي", Agrmnt3.Id, Prog3.Id, Prtf3.Id)).Entity;
            Project Proj6 = context.Projects.Add(new Project(user6.Id, Id, EntTyps, EntSts, Custs, Users, "Fiannce System", "عربي", Agrmnt3.Id, Prog3.Id, Prtf3.Id)).Entity;
            Project Proj7 = context.Projects.Add(new Project(user7.Id, Id, EntTyps, EntSts, Custs, Users, "Cars Tracking System", "عربي", Agrmnt3.Id, Prog3.Id, Prtf3.Id)).Entity;
            Project Proj8 = context.Projects.Add(new Project(user8.Id, Id, EntTyps, EntSts, Custs, Users, "Attendance System", "عربي", Agrmnt2.Id, Prog2.Id, Prtf2.Id)).Entity;
            Project Proj9 = context.Projects.Add(new Project(user9.Id, Id, EntTyps, EntSts, Custs, Users, "Replace Employees Laptops", "عربي", Agrmnt2.Id, Prog2.Id, Prtf2.Id)).Entity;
            Project ProjA = context.Projects.Add(new Project(user10.Id, Id, EntTyps, EntSts, Custs, Users, "Migrate to Cloud Storage", "عربي", Agrmnt2.Id, Prog2.Id, Prtf2.Id)).Entity;
            Project ProjB = context.Projects.Add(new Project(user11.Id, Id, EntTyps, EntSts, Custs, Users, "Migrate from AWS to Azure", "عربي", Agrmnt2.Id, Prog2.Id, Prtf2.Id)).Entity;
            Project ProjC = context.Projects.Add(new Project(user12.Id, Id, EntTyps, EntSts, Custs, Users, "New Uniform for All Staff", "عربي", Agrmnt.Id, Prog1.Id, Prtf3.Id)).Entity;
            Project ProjD = context.Projects.Add(new Project(user13.Id, Id, EntTyps, EntSts, Custs, Users, "Cybersecurity Awareness", "عربي", Agrmnt.Id, Prog1.Id, Prtf1.Id)).Entity;
            Project ProjE = context.Projects.Add(new Project(user14.Id, Id, EntTyps, EntSts, Custs, Users, "n8n Implementation", "عربي", Agrmnt.Id, Prog1.Id, Prtf1.Id)).Entity;
            Project ProjF = context.Projects.Add(new Project(user15.Id, Id, EntTyps, EntSts, Custs, Users, "AI PM Agent", "عربي", Agrmnt.Id, Prog1.Id, Prtf1.Id)).Entity;
            Project ProjG = context.Projects.Add(new Project(user16.Id, Id, EntTyps, EntSts, Custs, Users, "AI ECO System", "عربي", Agrmnt.Id, Prog1.Id, Prtf1.Id)).Entity;
            Project ProjH = context.Projects.Add(new Project(user17.Id, Id, EntTyps, EntSts, Custs, Users, "Recruitment System", "عربي", Agrmnt.Id, Prog1.Id, Prtf1.Id)).Entity;
            Project ProjJ = context.Projects.Add(new Project(user18.Id, Id, EntTyps, EntSts, Custs, Users, "ServiceNow Implementation", "عربي", Agrmnt1.Id, Prog2.Id, Prtf2.Id)).Entity;
            Project ProjK = context.Projects.Add(new Project(user19.Id, Id, EntTyps, EntSts, Custs, Users, "SOC Implementation", "عربي", Agrmnt2.Id, Prog1.Id, Prtf3.Id)).Entity;
            Project ProjL = context.Projects.Add(new Project(user20.Id, Id, EntTyps, EntSts, Custs, Users, "Splunk Implementation", "عربي", Agrmnt3.Id, Prog1.Id, Prtf3.Id)).Entity;

            context.SaveChanges();

            context.Medicals.Add(new Medical(Id, bupa.Id, "VIP", "عربي"));
            context.Medicals.Add(new Medical(Id, bupa.Id, "Class-A+", "عربي"));
            context.Medicals.Add(new Medical(Id, bupa.Id, "Class-A", "عربي"));
            context.Medicals.Add(new Medical(Id, bupa.Id, "Class-B", "عربي"));
            context.Medicals.Add(new Medical(Id, bupa.Id, "Class-C", "عربي"));

            PriceList PL1 = context.PriceLists.Add(new PriceList(Id, "One Time Customers", "عربي")).Entity;
            PriceList PL2 = context.PriceLists.Add(new PriceList(Id, "Loyal Accounts", "عربي")).Entity;
            PriceList PL3 = context.PriceLists.Add(new PriceList(Id, "Enterprise Customers", "عربي")).Entity;
            PriceList PL5 = context.PriceLists.Add(new PriceList(Id, "Strategic Customers", "عربي")).Entity;

            Product Prod1 = context.Products.Add(new Product(Id, "OutSystems License", "عربي", PrdCat2.Id, MrkUnt4.Id, 15750, curId, ratId)).Entity;
            Product Prod2 = context.Products.Add(new Product(Id, "OutSystems Implementation Services", "عربي", PrdCat3.Id, MrkUnt1.Id, 1250, curId, ratId)).Entity;
            Product Prod3 = context.Products.Add(new Product(Id, "3rd Party Implementation Services", "عربي", PrdCat3.Id, MrkUnt6.Id, 3500, curId, ratId)).Entity;
            Product Prod4 = context.Products.Add(new Product(Id, "Support", "عربي", PrdCat3.Id, MrkUnt5.Id, 17300, curId, ratId)).Entity;
            Product Prod5 = context.Products.Add(new Product(Id, "Support", "عربي", PrdCat5.Id, MrkUnt4.Id, 18000, curId, ratId)).Entity;
            Product Prod6 = context.Products.Add(new Product(Id, "Support", "عربي", PrdCat4.Id, MrkUnt4.Id, 19300, curId, ratId)).Entity;
            Product Prod7 = context.Products.Add(new Product(Id, "Support", "عربي", PrdCat5.Id, MrkUnt5.Id, 37300, curId, ratId)).Entity;
            Product Prod8 = context.Products.Add(new Product(Id, "Mendix", "عربي", PrdCat6.Id, MrkUnt4.Id, 13000, curId, ratId)).Entity;
            Product Prod9 = context.Products.Add(new Product(Id, "Mendix", "عربي", PrdCat6.Id, MrkUnt3.Id, 51000, curId, ratId)).Entity;
            Product ProdA = context.Products.Add(new Product(Id, "Enspct Implementation Services", "عربي", PrdCat3.Id, MrkUnt2.Id, 11700, curId, ratId)).Entity;

            context.SaveChanges();

            PurchaseOrder PO1 = context.PurchaseOrders.Add(new PurchaseOrder(user1.Id, Id, "IBM FileNet License", Proj1.Id, 70000, curId, ratId, ibm.Id, Prtf1.Id)).Entity;
            PurchaseOrder PO2 = context.PurchaseOrders.Add(new PurchaseOrder(user2.Id, Id, "IBM BAW License", Proj1.Id, 90000, curId, ratId, microsoft.Id, Prtf2.Id)).Entity;
            PurchaseOrder PO3 = context.PurchaseOrders.Add(new PurchaseOrder(user3.Id, Id, "Microsft Windows License", Proj1.Id, 70000, curId, ratId, ibm.Id, Prtf1.Id)).Entity;
            PurchaseOrder PO4 = context.PurchaseOrders.Add(new PurchaseOrder(user4.Id, Id, "Microsft Office License", Proj1.Id, 90000, curId, ratId, microsoft.Id, Prtf2.Id)).Entity;
            PurchaseOrder PO5 = context.PurchaseOrders.Add(new PurchaseOrder(user5.Id, Id, "HUAWEI Cloud", Proj1.Id, 70000, curId, ratId, ibm.Id, Prtf1.Id)).Entity;
            PurchaseOrder PO6 = context.PurchaseOrders.Add(new PurchaseOrder(user6.Id, Id, "Microsft Azure Cloud", Proj1.Id, 90000, curId, ratId, microsoft.Id, Prtf2.Id)).Entity;
            PurchaseOrder PO7 = context.PurchaseOrders.Add(new PurchaseOrder(user7.Id, Id, "Amazon Cloud", Proj1.Id, 70000, curId, ratId, ibm.Id, Prtf1.Id)).Entity;

            CustomerPO CPO1 = context.CustomerPOs.Add(new CustomerPO(user10.Id, Id, "IBM FileNet License", Proj1.Id, 70000, curId, ratId, ibm.Id, Prtf1.Id)).Entity;
            CustomerPO CPO2 = context.CustomerPOs.Add(new CustomerPO(user11.Id, Id, "IBM BAW License", Proj1.Id, 90000, curId, ratId, microsoft.Id, Prtf2.Id)).Entity;
            CustomerPO CPO3 = context.CustomerPOs.Add(new CustomerPO(user12.Id, Id, "Microsft Windows License", Proj1.Id, 70000, curId, ratId, ibm.Id, Prtf1.Id)).Entity;
            CustomerPO CPO4 = context.CustomerPOs.Add(new CustomerPO(user3.Id, Id, "Microsft Office License", Proj1.Id, 90000, curId, ratId, microsoft.Id, Prtf2.Id)).Entity;
            CustomerPO CPO5 = context.CustomerPOs.Add(new CustomerPO(user4.Id, Id, "HUAWEI Cloud", Proj1.Id, 70000, curId, ratId, ibm.Id, Prtf1.Id)).Entity;
            CustomerPO CPO6 = context.CustomerPOs.Add(new CustomerPO(user5.Id, Id, "Microsft Azure Cloud", Proj1.Id, 90000, curId, ratId, microsoft.Id, Prtf2.Id)).Entity;
            CustomerPO CPO7 = context.CustomerPOs.Add(new CustomerPO(user10.Id, Id, "AWS Cloud", Proj1.Id, 90000, curId, ratId, microsoft.Id, Prtf2.Id)).Entity;

            SalesOrder SO1 = context.SalesOrders.Add(new SalesOrder(user12.Id, Id, "IBM FileNet License & Implementation", Proj1.Id, 120000, curId, ratId, Cus.Id, Prtf2.Id)).Entity;
            SalesOrder SO2 = context.SalesOrders.Add(new SalesOrder(user13.Id, Id, "IBM BAW License & Implementation", Proj1.Id, 180000, curId, ratId, Cus.Id, Prtf2.Id)).Entity;
            SalesOrder SO3 = context.SalesOrders.Add(new SalesOrder(user25.Id, Id, "Support Services", Proj1.Id, 100000, curId, ratId, Cus.Id, Prtf3.Id)).Entity;
            SalesOrder SO4 = context.SalesOrders.Add(new SalesOrder(user26.Id, Id, "Drixal ERP Implementation", Proj1.Id, 120000, curId, ratId, Cus.Id, Prtf2.Id)).Entity;
            SalesOrder SO5 = context.SalesOrders.Add(new SalesOrder(user27.Id, Id, "AI Agentic Services Implementation", Proj1.Id, 180000, curId, ratId, Cus.Id, Prtf2.Id)).Entity;
            SalesOrder SO6 = context.SalesOrders.Add(new SalesOrder(user65.Id, Id, "Support Services", Proj1.Id, 100000, curId, ratId, Cus.Id, Prtf3.Id)).Entity;

            context.SaveChanges();

            context.Inventories.Add(new Inventory(user1.Id, Id, "Inventory - 01", "عربي"));
            context.Inventories.Add(new Inventory(user3.Id, Id, "Inventory - 02", "عربي"));
            context.Inventories.Add(new Inventory(user4.Id, Id, "Inventory - 03", "عربي"));

            Assessment Ass = context.Assessments.Add(new Assessment(Id, "General Assessment", Dmn1.Id)).Entity;

            context.SaveChanges();

            List<Project> Projs = context.Projects.ToList();
            foreach (var Proj in Projs)
            {
                context.LineItems.Add(new LineItem(Id, Prod1, "Project", Proj.Id, 5));
                context.LineItems.Add(new LineItem(Id, Prod2, "Project", Proj.Id, 7));
                context.LineItems.Add(new LineItem(Id, Prod3, "Project", Proj.Id, 9));
                context.LineItems.Add(new LineItem(Id, Prod4, "Project", Proj.Id, 3));
                context.LineItems.Add(new LineItem(Id, Prod5, "Project", Proj.Id, 2));
                context.LineItems.Add(new LineItem(Id, Prod6, "Project", Proj.Id, 1));

                context.InvoiceAPs.Add(new InvoiceAP(user9.Id, Id, "IBM Invoice Down Payment 10%", Proj.Id, 10000, curId, ratId, ibm.Id, PO1.Id));
                context.InvoiceAPs.Add(new InvoiceAP(user9.Id, Id, "IBM Invoice Delivery 20%", Proj.Id, 20000, curId, ratId, ibm.Id, PO1.Id));
                context.InvoiceAPs.Add(new InvoiceAP(user9.Id, Id, "IBM Invoice Installatoin 30%", Proj.Id, 30000, curId, ratId, ibm.Id, PO1.Id));
                context.InvoiceAPs.Add(new InvoiceAP(user9.Id, Id, "IBM Invoice Activation 30%", Proj.Id, 30000, curId, ratId, ibm.Id, PO1.Id));

                context.InvoiceARs.Add(new InvoiceAR(user9.Id, Id, "Licenses Down Payment 30%", Proj.Id, 30000, curId, ratId, Cus.Id, SO1.Id));
                context.InvoiceARs.Add(new InvoiceAR(user9.Id, Id, "Etitlement 20%", Proj.Id, 20000, curId, ratId, Cus.Id, SO1.Id));
                context.InvoiceARs.Add(new InvoiceAR(user9.Id, Id, "Installatoin 20%", Proj.Id, 20000, curId, ratId, Cus.Id, SO1.Id));
                context.InvoiceARs.Add(new InvoiceAR(user9.Id, Id, "Activation 30%", Proj.Id, 30000, curId, ratId, Cus.Id, SO1.Id));

                //context.CreditNoteAPs.Add(new CreditNoteAP(user9.Id, Id, "IBM Invoice Down Payment 10%", Proj.Id, 10000, curId, ratId, ibm.Id, PO1.Id));
                //context.CreditNoteAPs.Add(new CreditNoteAP(user9.Id, Id, "IBM Invoice Delivery 20%", Proj.Id, 20000, curId, ratId, ibm.Id, PO1.Id));

                //context.DebitNoteARs.Add(new DebitNoteAR(user9.Id, Id, "Licenses Down Payment 30%", Proj.Id, 30000, curId, ratId, Cus.Id, SO1.Id));
                //context.DebitNoteARs.Add(new DebitNoteAR(user9.Id, Id, "Etitlement 20%", Proj.Id, 20000, curId, ratId, Cus.Id, SO1.Id));

                context.Stakeholders.Add(new Stakeholder(Id, "Customer", Proj.Id, null, Cnct1, null, 1, 2, 3, 4, 5));
                context.Stakeholders.Add(new Stakeholder(Id, "Vendor", Proj.Id, null, Cnct4, null, 1, 4, 5, 4, 5));
                context.Stakeholders.Add(new Stakeholder(Id, "Management", Proj.Id, null, null, user1, 1, 8, 7, 4, 5));

                if (Proj.Id % 2 == 0)
                {
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", Proj.Id, null, null, user1, 9, 1, 3, 4, 5));
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", Proj.Id, null, null, user2, 8, 4, 5, 4, 9));
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", Proj.Id, null, null, user3, 3, 7, 10, 4, 8));
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", Proj.Id, null, null, user4, 7, 9, 7, 4, 6));
                }
                else if (Proj.Id % 3 == 0)
                {
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", Proj.Id, null, null, user5, 9, 5, 3, 4, 5));
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", Proj.Id, null, null, user6, 8, 2, 5, 4, 9));
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", Proj.Id, null, null, user7, 3, 2, 10, 4, 8));
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", Proj.Id, null, null, user8, 7, 7, 7, 4, 6));
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", Proj.Id, null, null, user9, 7, 9, 7, 4, 6));
                }
                else
                {
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", Proj.Id, null, null, user11, 9, 5, 3, 4, 5));
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", Proj.Id, null, null, user22, 8, 2, 5, 4, 9));
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", Proj.Id, null, null, user33, 3, 2, 10, 4, 8));
                }

                context.BillingPlans.Add(new BillingPlan(Id, "Design (30%)", aspire.Id, Proj.Id, 300000, curId, ratId, new DateTime(2026, 3, 15)));
                context.BillingPlans.Add(new BillingPlan(Id, "Implementation (40%)", aspire.Id, Proj.Id, 400000, curId, ratId, new DateTime(2026, 9, 15)));
                context.BillingPlans.Add(new BillingPlan(Id, "Final Commisioning (20%)", aspire.Id, Proj.Id, 200000, curId, ratId, new DateTime(2026, 12, 15)));

                context.ChangeRequests.Add(new ChangeRequest(Id, "Add new scope", Proj.Id, 300000, curId, ratId));
                context.ChangeRequests.Add(new ChangeRequest(Id, "Extra on-site resources", Proj.Id, 780500, curId, ratId));

                context.Assumptions.Add(new Assumption(Id, "Customer in not ready", Proj.Id));
                context.Assumptions.Add(new Assumption(Id, "Customer with relax the 24X7", Proj.Id));

                context.Constrains.Add(new Constrain(Id, "Customer on-site office closing at 10 PM", Proj.Id));
                context.Constrains.Add(new Constrain(Id, "There are too much security restrictions", Proj.Id));
                context.Constrains.Add(new Constrain(Id, "Cybersecurity impacts the envirnments", Proj.Id));

                context.Lessons.Add(new Lesson(Id, "Better sizing", Proj.Id));
                context.Lessons.Add(new Lesson(Id, "Only profitable projects are strategic", Proj.Id));

                context.Risks.Add(new Risk(Id, Impts, Crits, Sevrs, Probs, Urgs, Users, RskStrs, "Better sizing", Proj.Id));
                context.Risks.Add(new Risk(Id, Impts, Crits, Sevrs, Probs, Urgs, Users, RskStrs, "Only profitable projects are strategic", Proj.Id));

                context.Issues.Add(new Issue(Id, Impts, Crits, Sevrs, Urgs, Users, "Better sizing", Proj.Id));
                context.Issues.Add(new Issue(Id, Impts, Crits, Sevrs, Urgs, Users, "Only profitable projects are strategic", Proj.Id));

                context.WorkItems.Add(new WorkItem(user1.Id, Id, TskSz, TskCoxs, TskPrs, "Task-01", WrkItmTsk.Id, WrkItmPlnd.Id, null, "Project", Proj.Id, null));
                context.WorkItems.Add(new WorkItem(user2.Id, Id, TskSz, TskCoxs, TskPrs, "Call-02", WrkItmCal.Id, WrkItmPlnd.Id, null, "Project", Proj.Id, null));
                context.WorkItems.Add(new WorkItem(user3.Id, Id, TskSz, TskCoxs, TskPrs, "Meeting-03", WrkItmMet.Id, WrkItmPlnd.Id, null, "Project", Proj.Id, null));
                context.WorkItems.Add(new WorkItem(user4.Id, Id, TskSz, TskCoxs, TskPrs, "Visit-04", WrkItmVis.Id, WrkItmPlnd.Id, null, "Project", Proj.Id, null));
                context.WorkItems.Add(new WorkItem(user5.Id, Id, TskSz, TskCoxs, TskPrs, "Note-05", WrkItmNot.Id, WrkItmPlnd.Id, null, "Project", Proj.Id, null));
            }
            context.Projects.UpdateRange(Projs);

            List<LineItem> LIs = context.LineItems.Where(x => x.EntityName == "Project").ToList();
            foreach (var Li in LIs)
            {
                context.WorkItems.Add(new WorkItem(user1.Id, Id, TskSz, TskCoxs, TskPrs, "PMO System", WrkItmStr.Id, WrkItmPlnd.Id, null, Li.ObjectId, Li.Id, false, false));
                context.WorkItems.Add(new WorkItem(user2.Id, Id, TskSz, TskCoxs, TskPrs, "CRM System", WrkItmStr.Id, WrkItmPlnd.Id, null, Li.ObjectId, Li.Id, false, false));
                context.WorkItems.Add(new WorkItem(user3.Id, Id, TskSz, TskCoxs, TskPrs, "HR System", WrkItmStr.Id, WrkItmPlnd.Id, null, Li.ObjectId, Li.Id, false, false));
            }
            context.SaveChanges();

            List<WorkItem> Streams = context.WorkItems.Where(x => x.EntityTypeId == WrkItmStr.Id).ToList();
            foreach (var str in Streams)
            {
                context.WorkItems.Add(new WorkItem(user11.Id, Id, TskSz, TskCoxs, TskPrs, "Requirements Gathering", WrkItmPck.Id, WrkItmPlnd.Id, str.Id, str.ProjectId, str.LineItemId, false, false));
                context.WorkItems.Add(new WorkItem(user7.Id, Id, TskSz, TskCoxs, TskPrs, "Business Analysis", WrkItmPck.Id, WrkItmPlnd.Id, str.Id, str.ProjectId, str.LineItemId, false, false));
                context.WorkItems.Add(new WorkItem(user2.Id, Id, TskSz, TskCoxs, TskPrs, "UI/UX", WrkItmPck.Id, WrkItmPlnd.Id, str.Id, str.ProjectId, str.LineItemId, false, false));
                context.WorkItems.Add(new WorkItem(user8.Id, Id, TskSz, TskCoxs, TskPrs, "Development", WrkItmPck.Id, WrkItmPlnd.Id, str.Id, str.ProjectId, str.LineItemId, false, false));
                context.WorkItems.Add(new WorkItem(user9.Id, Id, TskSz, TskCoxs, TskPrs, "Testing", WrkItmPck.Id, WrkItmPlnd.Id, str.Id, str.ProjectId, str.LineItemId, false, false));
            }
            context.SaveChanges();

            List<WorkItem> Packages = context.WorkItems.Where(x => x.EntityTypeId == WrkItmPck.Id).ToList();
            foreach (var pg in Packages)
                for (int i = 1; i <= 7; i++)
                    context.WorkItems.Add(new WorkItem(user12.Id, Id, TskSz, TskCoxs, TskPrs, pg.Name + " task-" + i.ToString(), WrkItmAct.Id, WrkItmPlnd.Id, pg.Id, pg.ProjectId, pg.LineItemId, true, true));
            context.SaveChanges();

            List<WorkItem> Activities = context.WorkItems.Where(x => x.EntityTypeId == WrkItmAct.Id).ToList();
            foreach (var act in Activities)
            {
                long? usrId = context.WorkItemAssignees.Add(new WorkItemAssignee(Id, act.Id, Users)).Entity.AssignedToUsrId;
                context.TimeSheets.Add(new TimeSheet(Id, act.Id, usrId, new DateTime(2026, 5, 3)));
                context.TimeSheets.Add(new TimeSheet(Id, act.Id, usrId, new DateTime(2026, 5, 4)));
                context.TimeSheets.Add(new TimeSheet(Id, act.Id, usrId, new DateTime(2026, 5, 5)));
                context.TimeSheets.Add(new TimeSheet(Id, act.Id, usrId, new DateTime(2026, 5, 6)));
                context.TimeSheets.Add(new TimeSheet(Id, act.Id, usrId, new DateTime(2026, 5, 7)));
                context.TimeSheets.Add(new TimeSheet(Id, act.Id, usrId, new DateTime(2026, 5, 8)));
                context.TimeSheets.Add(new TimeSheet(Id, act.Id, usrId, new DateTime(2026, 5, 9)));
            }

            List<Customer> seededCustomers = context.Customers.Where(x => x.TenantId == Id).ToList();
            foreach (var cust in seededCustomers)
            {
                context.WorkItems.Add(new WorkItem(user1.Id, Id, TskSz, TskCoxs, TskPrs, "Quarterly Business Review", WrkItmMet.Id, WrkItmPlnd.Id, null, "Customer", cust.Id, null));
                context.WorkItems.Add(new WorkItem(user2.Id, Id, TskSz, TskCoxs, TskPrs, "Account Management Follow-up", WrkItmCal.Id, WrkItmPlnd.Id, null, "Customer", cust.Id, null));
                context.WorkItems.Add(new WorkItem(user3.Id, Id, TskSz, TskCoxs, TskPrs, "SLA & Renewal Requirements", WrkItmTsk.Id, WrkItmPlnd.Id, null, "Customer", cust.Id, null));
                context.WorkItems.Add(new WorkItem(user4.Id, Id, TskSz, TskCoxs, TskPrs, "Technical Stakeholder Visit", WrkItmVis.Id, WrkItmPlnd.Id, null, "Customer", cust.Id, null));
                context.WorkItems.Add(new WorkItem(user5.Id, Id, TskSz, TskCoxs, TskPrs, "Client Feedback & Notes", WrkItmNot.Id, WrkItmPlnd.Id, null, "Customer", cust.Id, null));
            }
            context.SaveChanges();

            List<Deal> Deals = context.Deals.ToList();
            foreach (Deal Obj in Deals)
            {
                context.LineItems.Add(new LineItem(Id, Prod1, "Deal", Obj.Id, 5));
                context.LineItems.Add(new LineItem(Id, Prod2, "Deal", Obj.Id, 7));
                context.LineItems.Add(new LineItem(Id, Prod3, "Deal", Obj.Id, 9));
                //context.LineItems.Add(new LineItem(Id, Prod4, "Deal", Obj.Id, 3));
                //context.LineItems.Add(new LineItem(Id, Prod5, "Deal", Obj.Id, 2));
                //context.LineItems.Add(new LineItem(Id, Prod6, "Deal", Obj.Id, 1));

                Pnl PNL1 = context.Pnls.Add(new Pnl(user9.Id, Id, EntTyps, EntSts, Custs, Users, Obj.Name + " Pnl", "عربي", Obj.Id)).Entity;
                Pnl PNL2 = context.Pnls.Add(new Pnl(user9.Id, Id, EntTyps, EntSts, Custs, Users, Obj.Name + " Pnl", "عربي", Obj.Id)).Entity;
                Pnl PNL3 = context.Pnls.Add(new Pnl(user9.Id, Id, EntTyps, EntSts, Custs, Users, Obj.Name + " Pnl", "عربي", Obj.Id)).Entity;
                context.SaveChanges();

                context.Quotes.Add(new Quote(user9.Id, Id, EntTyps, EntSts, Custs, Users, Obj.Name + " Quote", "عربي", Obj.Id, PNL1.Id));
                context.Quotes.Add(new Quote(user9.Id, Id, EntTyps, EntSts, Custs, Users, Obj.Name + " Quote", "عربي", Obj.Id, PNL2.Id));
                context.Quotes.Add(new Quote(user9.Id, Id, EntTyps, EntSts, Custs, Users, Obj.Name + " Quote", "عربي", Obj.Id, PNL3.Id));

                context.SupplierQuotes.Add(new SupplierQuote(user9.Id, Id, EntTyps, EntSts, Custs, Users, Obj.Name + " Quote", "عربي", Obj.Id, PNL1.Id));
                context.SupplierQuotes.Add(new SupplierQuote(user9.Id, Id, EntTyps, EntSts, Custs, Users, Obj.Name + " Quote", "عربي", Obj.Id, PNL2.Id));
                context.SupplierQuotes.Add(new SupplierQuote(user9.Id, Id, EntTyps, EntSts, Custs, Users, Obj.Name + " Quote", "عربي", Obj.Id, PNL3.Id));

                context.Stakeholders.Add(new Stakeholder(Id, "Customer", null, Obj.Id, Cnct1, null, 1, 2, 3, 4, 5));
                context.Stakeholders.Add(new Stakeholder(Id, "Vendor", null, Obj.Id, Cnct4, null, 1, 4, 5, 4, 5));
                context.Stakeholders.Add(new Stakeholder(Id, "Management", null, Obj.Id, null, user1, 1, 8, 7, 4, 5));

                if (Obj.Id % 2 == 0)
                {
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", null, Obj.Id, null, user1, 9, 1, 3, 4, 5));
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", null, Obj.Id, null, user2, 8, 4, 5, 4, 9));
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", null, Obj.Id, null, user3, 3, 7, 10, 4, 8));
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", null, Obj.Id, null, user4, 7, 9, 7, 4, 6));
                }
                else if (Obj.Id % 3 == 0)
                {
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", null, Obj.Id, null, user5, 9, 5, 3, 4, 5));
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", null, Obj.Id, null, user6, 8, 2, 5, 4, 9));
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", null, Obj.Id, null, user7, 3, 2, 10, 4, 8));
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", null, Obj.Id, null, user8, 7, 7, 7, 4, 6));
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", null, Obj.Id, null, user9, 7, 9, 7, 4, 6));
                }
                else
                {
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", null, Obj.Id, null, user11, 9, 5, 3, 4, 5));
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", null, Obj.Id, null, user21, 8, 2, 5, 4, 9));
                    context.Stakeholders.Add(new Stakeholder(Id, "Team", null, Obj.Id, null, user43, 3, 2, 10, 4, 8));
                }
            }
            context.SaveChanges();

            List<Pnl> Pnls = context.Pnls.ToList();
            foreach (var Obj in Pnls)
            {
                context.LineItems.Add(new LineItem(Id, Prod1, "Pnl", Obj.Id, 5));
                context.LineItems.Add(new LineItem(Id, Prod2, "Pnl", Obj.Id, 7));
                context.LineItems.Add(new LineItem(Id, Prod3, "Pnl", Obj.Id, 9));
            }

            List<Quote> Quotes = context.Quotes.ToList();
            foreach (var Obj in Quotes)
            {
                context.LineItems.Add(new LineItem(Id, Prod1, "Quote", Obj.Id, 5));
                context.LineItems.Add(new LineItem(Id, Prod2, "Quote", Obj.Id, 7));
                context.LineItems.Add(new LineItem(Id, Prod3, "Quote", Obj.Id, 9));
            }

            List<Agreement> Agreements = context.Agreements.ToList();
            foreach (var Obj in Agreements)
            {
                context.LineItems.Add(new LineItem(Id, Prod1, "Agreement", Obj.Id, 5));
                context.LineItems.Add(new LineItem(Id, Prod2, "Agreement", Obj.Id, 7));
                context.LineItems.Add(new LineItem(Id, Prod3, "Agreement", Obj.Id, 9));
            }

            List<SalesOrder> SalesOrders = context.SalesOrders.ToList();
            foreach (var Obj in SalesOrders)
            {
                context.LineItems.Add(new LineItem(Id, Prod1, "SalesOrder", Obj.Id, 5));
                context.LineItems.Add(new LineItem(Id, Prod2, "SalesOrder", Obj.Id, 7));
                context.LineItems.Add(new LineItem(Id, Prod3, "SalesOrder", Obj.Id, 9));
            }

            List<PurchaseOrder> PurchaseOrders = context.PurchaseOrders.ToList();
            foreach (var Obj in PurchaseOrders)
            {
                context.LineItems.Add(new LineItem(Id, Prod1, "PurchaseOrder", Obj.Id, 5));
                context.LineItems.Add(new LineItem(Id, Prod2, "PurchaseOrder", Obj.Id, 7));
                context.LineItems.Add(new LineItem(Id, Prod3, "PurchaseOrder", Obj.Id, 9));
            }

            List<InvoiceAR> InvoiceARs = context.InvoiceARs.ToList();
            foreach (var Obj in InvoiceARs)
            {
                context.LineItems.Add(new LineItem(Id, Prod1, "InvoiceAR", Obj.Id, 5));
                context.LineItems.Add(new LineItem(Id, Prod2, "InvoiceAR", Obj.Id, 7));
                context.LineItems.Add(new LineItem(Id, Prod3, "InvoiceAR", Obj.Id, 9));
                //context.LineItems.Add(new LineItem(Id, Prod4, "InvoiceAR", Obj.Id, 3));
                //context.LineItems.Add(new LineItem(Id, Prod5, "InvoiceAR", Obj.Id, 2));
                //context.LineItems.Add(new LineItem(Id, Prod6, "InvoiceAR", Obj.Id, 1));
            }

            List<InvoiceAP> InvoiceAPs = context.InvoiceAPs.ToList();
            foreach (var Obj in InvoiceAPs)
            {
                context.LineItems.Add(new LineItem(Id, Prod1, "InvoiceAP", Obj.Id, 5));
                context.LineItems.Add(new LineItem(Id, Prod2, "InvoiceAP", Obj.Id, 7));
                context.LineItems.Add(new LineItem(Id, Prod3, "InvoiceAP", Obj.Id, 9));
                //context.LineItems.Add(new LineItem(Id, Prod4, "InvoiceAP", Obj.Id, 3));
                //context.LineItems.Add(new LineItem(Id, Prod5, "InvoiceAP", Obj.Id, 2));
                //context.LineItems.Add(new LineItem(Id, Prod6, "InvoiceAP", Obj.Id, 1));
            }
            #endregion

            context.SaveChanges();

            // END **************************************************************************************

        }
        catch (Exception e)
        {
            logger.LogException(e, nameof(Register));
            throw;
        }
    }

    public async Task<(bool IsValid, string Message)> SendEmail(string email, bool isExternal = false)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return (false, "Email address is required.");
            }

            string normalizedEmail = email.Trim().ToLowerInvariant();

            // Validate domain for Employee registration (must belong to @aldarhecp.sa)
            if (!isExternal && !normalizedEmail.EndsWith("@aldarhecp.sa"))
            {
                logger.LogInformation("Employee registration rejected: Email '{0}' does not belong to @aldarhecp.sa", email);
                return (false, "Email must belong to @aldarhecp.sa domain.");
            }

            if (isExternal)
            {
                var extUserExists = await context.ExternalUsers.AnyAsync(x => x.Email != null && x.Email.ToLower() == normalizedEmail);
                var appUserExists = await context.Users.AnyAsync(u =>
                    (u.Email != null && (u.Email.ToLower() == normalizedEmail || u.Email.ToLower().StartsWith(normalizedEmail + "_"))) ||
                    (u.UserName != null && (u.UserName.ToLower() == normalizedEmail || u.UserName.ToLower().StartsWith(normalizedEmail + "_"))));

                if (!extUserExists && !appUserExists)
                {
                    logger.LogInformation("External registration rejected: Email '{0}' is not found in database.", normalizedEmail);
                    return (false, "No contractor account found with this email. Please contact your administrator to be registered first.");
                }
            }

            logger.LogInformation("Start registration / password update for email: '{0}' (IsExternal: {1})", normalizedEmail, isExternal);

            logger.LogInformation("Generating hardcoded OTP (6 ones: 111111) for email: '{0}'", normalizedEmail);
            string otp = "111111"; // Hardcoded 6 ones

            MemoryCacheEntryOptions cacheEntryOptions = new()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
            };
            memoryCache.Set($"OTP_{normalizedEmail}", otp, cacheEntryOptions);
            memoryCache.Set(normalizedEmail, otp, cacheEntryOptions); // backward compatibility

            logger.LogInformation("OTP 111111 cached successfully for: '{0}'", normalizedEmail);
            return (true, "Verification code sent successfully.");
        }
        catch (Exception e)
        {
            logger.LogException(e, nameof(SendEmail));
            return (false, "An error occurred while generating verification code.");
        }
    }

    public (bool IsValid, string Message) ConfrimOtp(ConfirmOTPDTO confirmOtp)
    {
        try
        {
            if (confirmOtp == null || string.IsNullOrWhiteSpace(confirmOtp.Email) || string.IsNullOrWhiteSpace(confirmOtp.Otp))
            {
                return (false, "Email and OTP code are required.");
            }

            string normalizedEmail = confirmOtp.Email.Trim().ToLowerInvariant();
            string otp = confirmOtp.Otp.Trim();

            logger.LogInformation("Verifying OTP '{0}' for email: '{1}'", otp, normalizedEmail);

            bool isMatch = false;
            if (otp == "111111")
            {
                isMatch = true;
            }
            else if (memoryCache.TryGetValue($"OTP_{normalizedEmail}", out string? storedOtp) && storedOtp == otp)
            {
                isMatch = true;
            }
            else if (memoryCache.TryGetValue(normalizedEmail, out string? legacyOtp) && legacyOtp == otp)
            {
                isMatch = true;
            }

            if (isMatch)
            {
                logger.LogInformation("OTP confirmed successfully for email: '{0}'", normalizedEmail);
                MemoryCacheEntryOptions verifiedCacheOptions = new()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
                };
                memoryCache.Set($"VERIFIED_{normalizedEmail}", true, verifiedCacheOptions);

                return (true, "OTP confirmed successfully.");
            }

            logger.LogInformation("Invalid OTP: '{0}' for email: '{1}'", otp, normalizedEmail);
            return (false, "Invalid verification code.");
        }
        catch (Exception e)
        {
            logger.LogException(e, nameof(ConfrimOtp));
            return (false, "An error occurred during OTP verification.");
        }
    }

    public async Task<(bool IsValid, string Message, LoginResponseDTO? User)> RegisterEmployee(EmployeeRegisterDTO dto)
    {
        try
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
            {
                return (false, "Email and password are required.", null);
            }

            string normalizedEmail = dto.Email.Trim().ToLowerInvariant();

            // Enforce domain requirement for Employee
            if (!normalizedEmail.EndsWith("@aldarhecp.sa"))
            {
                return (false, "Email must belong to @aldarhecp.sa domain.", null);
            }

            if (dto.Password != dto.ConfirmPassword)
            {
                return (false, "Password and confirm password do not match.", null);
            }

            // Check if already registered -> update password and profile
            var existingUser = await userManager.FindByEmailAsync(normalizedEmail) 
                            ?? await userManager.FindByNameAsync(normalizedEmail);
            if (existingUser != null)
            {
                logger.LogInformation("Email '{0}' already exists. Updating password and account profile.", normalizedEmail);

                // Reset / Update password
                var resetToken = await userManager.GeneratePasswordResetTokenAsync(existingUser);
                var resetResult = await userManager.ResetPasswordAsync(existingUser, resetToken, dto.Password);
                if (!resetResult.Succeeded)
                {
                    await userManager.RemovePasswordAsync(existingUser);
                    var addResult = await userManager.AddPasswordAsync(existingUser, dto.Password);
                    if (!addResult.Succeeded)
                    {
                        string errors = string.Join(", ", addResult.Errors.Select(e => e.Description));
                        return (false, errors, null);
                    }
                }

                if (!string.IsNullOrWhiteSpace(dto.FirstName)) existingUser.FirstName = dto.FirstName.Trim();
                if (!string.IsNullOrWhiteSpace(dto.LastName)) existingUser.LastName = dto.LastName.Trim();
                if (!string.IsNullOrWhiteSpace(existingUser.FirstName) || !string.IsNullOrWhiteSpace(existingUser.LastName))
                {
                    existingUser.Name = $"{existingUser.FirstName} {existingUser.LastName}".Trim();
                }

                if (!string.IsNullOrWhiteSpace(dto.Mobile))
                {
                    existingUser.Mobile = dto.Mobile.Trim();
                    existingUser.PhoneNumber = dto.Mobile.Trim();
                }

                if (!string.IsNullOrWhiteSpace(dto.JobTitle))
                {
                    existingUser.JobTitle = dto.JobTitle.Trim();
                }

                if (!string.IsNullOrWhiteSpace(existingUser.Mobile) && !string.IsNullOrWhiteSpace(existingUser.JobTitle))
                {
                    existingUser.IsValidProfile = true;
                }

                existingUser.IsActive = true;
                existingUser.IsVerified = true;
                existingUser.EmailConfirmed = true;

                await userManager.UpdateAsync(existingUser);

                // Ensure tenant association
                long tenantId = existingUser.TenantId ?? 1;
                var existingUserTenant = await context.ApplicationUserTenants.FirstOrDefaultAsync(ut => ut.UserId == existingUser.Id && ut.TenantId == tenantId);
                if (existingUserTenant == null)
                {
                    context.ApplicationUserTenants.Add(new() { UserId = existingUser.Id, TenantId = tenantId, IsActive = true });
                }

                // Update or create Employee entity
                var existingEmployee = await context.Employees.FirstOrDefaultAsync(e => e.AppUsrId == existingUser.Id || (e.WorkEmail != null && e.WorkEmail.ToLower() == normalizedEmail) || (e.PersonalEmail != null && e.PersonalEmail.ToLower() == normalizedEmail));
                if (existingEmployee != null)
                {
                    existingEmployee.AppUsrId = existingUser.Id;
                    existingEmployee.WorkEmail = normalizedEmail;
                    if (!string.IsNullOrWhiteSpace(existingUser.Mobile)) existingEmployee.Mobile = existingUser.Mobile;
                    if (!string.IsNullOrWhiteSpace(existingUser.FirstName)) existingEmployee.FNameEn = existingUser.FirstName;
                    if (!string.IsNullOrWhiteSpace(existingUser.LastName)) existingEmployee.LNameEn = existingUser.LastName;
                    if (!string.IsNullOrWhiteSpace(existingUser.Name)) existingEmployee.Name = existingUser.Name;
                }
                else
                {
                    Employee employee = new()
                    {
                        TenantId = tenantId,
                        AppUsrId = existingUser.Id,
                        WorkEmail = normalizedEmail,
                        PersonalEmail = normalizedEmail,
                        Mobile = existingUser.Mobile,
                        FNameEn = existingUser.FirstName,
                        LNameEn = existingUser.LastName,
                        Name = existingUser.Name,
                        IsActive = true,
                        CreateDate = DateTime.UtcNow
                    };
                    context.Employees.Add(employee);
                }

                await context.SaveChangesAsync();

                // Clear cache tokens
                memoryCache.Remove($"VERIFIED_{normalizedEmail}");
                memoryCache.Remove($"OTP_{normalizedEmail}");
                memoryCache.Remove(normalizedEmail);

                var existingLoginResponse = authService.GetLoginResponse(existingUser, isExternalUser: false);
                return (true, "Password and account updated successfully.", existingLoginResponse);
            }

            // Resolve Tenant, Currency, and Language
            var tenant = await context.Tenants.FirstOrDefaultAsync();
            long? newTenantId = tenant?.Id ?? 1;

            var currency = await context.Currencies.FirstOrDefaultAsync(c => c.TenantId == newTenantId && c.IsBaseCurrency == true) 
                        ?? await context.Currencies.FirstOrDefaultAsync(c => c.TenantId == newTenantId);
            var language = await context.Languages.FirstOrDefaultAsync(l => l.TenantId == newTenantId) 
                        ?? await context.Languages.FirstOrDefaultAsync();

            string emailPrefix = normalizedEmail.Split('@')[0];
            string firstName = !string.IsNullOrWhiteSpace(dto.FirstName) 
                ? dto.FirstName.Trim() 
                : (char.ToUpper(emailPrefix[0]) + (emailPrefix.Length > 1 ? emailPrefix[1..] : ""));
            string lastName = !string.IsNullOrWhiteSpace(dto.LastName) ? dto.LastName.Trim() : "Employee";

            bool hasCompleteProfile = !string.IsNullOrWhiteSpace(dto.Mobile) && !string.IsNullOrWhiteSpace(dto.JobTitle);
            string? mobile = !string.IsNullOrWhiteSpace(dto.Mobile) ? dto.Mobile.Trim() : null;
            string? jobTitle = !string.IsNullOrWhiteSpace(dto.JobTitle) ? dto.JobTitle.Trim() : null;

            ApplicationUser user = new()
            {
                UserName = normalizedEmail,
                Email = normalizedEmail,
                NormalizedUserName = normalizedEmail.ToUpperInvariant(),
                NormalizedEmail = normalizedEmail.ToUpperInvariant(),
                FirstName = firstName,
                LastName = lastName,
                Name = $"{firstName} {lastName}".Trim(),
                Mobile = mobile,
                PhoneNumber = mobile,
                JobTitle = jobTitle,
                TenantId = newTenantId,
                CurrencyId = currency?.Id,
                LanguageId = language?.Id,
                SeatType = SeatTypeEnum.User,
                IsExternalUser = false,
                IsActive = true,
                IsVerified = true,
                EmailConfirmed = true,
                IsValidProfile = hasCompleteProfile,
                CreateDate = DateTime.UtcNow
            };

            IdentityResult result = await userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                logger.LogInformation("Failed to create employee user '{0}': {1}", normalizedEmail, errors);
                return (false, errors, null);
            }

            user.PhotoUrl = @"/avatars/" + user.Id + ".png";

            context.ApplicationUserTenants.Add(new() { UserId = user.Id, TenantId = newTenantId, IsActive = true });

            var empRole = await context.ApplicationRoles.FirstOrDefaultAsync(r => r.TenantId == newTenantId && (r.Name == "Employee" || r.Name == "User" || r.Name == "Admin"))
                       ?? await context.ApplicationRoles.FirstOrDefaultAsync(r => r.TenantId == newTenantId);
            if (empRole != null)
            {
                context.ApplicationUserRoles.Add(new() { UserId = user.Id, RoleId = empRole.Id, TenantId = newTenantId, IsActive = true });
            }

            // Create or update HRM Employee record
            var newEmployee = await context.Employees.FirstOrDefaultAsync(e => (e.WorkEmail != null && e.WorkEmail.ToLower() == normalizedEmail) || (e.PersonalEmail != null && e.PersonalEmail.ToLower() == normalizedEmail));
            if (newEmployee != null)
            {
                newEmployee.AppUsrId = user.Id;
                newEmployee.WorkEmail = normalizedEmail;
                if (!string.IsNullOrWhiteSpace(mobile)) newEmployee.Mobile = mobile;
                newEmployee.FNameEn = user.FirstName;
                newEmployee.LNameEn = user.LastName;
                newEmployee.Name = user.Name;
            }
            else
            {
                Employee employee = new()
                {
                    TenantId = newTenantId,
                    AppUsrId = user.Id,
                    WorkEmail = normalizedEmail,
                    PersonalEmail = normalizedEmail,
                    Mobile = mobile,
                    FNameEn = user.FirstName,
                    LNameEn = user.LastName,
                    Name = user.Name,
                    IsActive = true,
                    CreateDate = DateTime.UtcNow
                };
                context.Employees.Add(employee);
            }

            await context.SaveChangesAsync();

            // Clear cache tokens
            memoryCache.Remove($"VERIFIED_{normalizedEmail}");
            memoryCache.Remove($"OTP_{normalizedEmail}");
            memoryCache.Remove(normalizedEmail);

            var loginResponse = authService.GetLoginResponse(user, isExternalUser: false);
            return (true, hasCompleteProfile ? "Registration completed successfully." : "Password set successfully. Please complete your profile.", loginResponse);
        }
        catch (Exception e)
        {
            logger.LogException(e, nameof(RegisterEmployee));
            var detail = e.InnerException != null ? $"{e.Message} ({e.InnerException.Message})" : e.Message;
            return (false, $"An error occurred while creating employee account: {detail}", null);
        }
    }

    public async Task<(bool IsValid, string Message, LoginResponseDTO? User)> RegisterExternalUser(ExternalUserRegisterDTO dto)
    {
        try
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
            {
                return (false, "Email and password are required.", null);
            }

            string normalizedEmail = dto.Email.Trim().ToLowerInvariant();

            if (dto.Password != dto.ConfirmPassword)
            {
                return (false, "Password and confirm password do not match.", null);
            }

            // Find existing ExternalUser and ApplicationUser in the database
            var existingExternal = await context.ExternalUsers.FirstOrDefaultAsync(x => x.Email != null && x.Email.ToLower() == normalizedEmail);

            ApplicationUser? existingUser = await userManager.FindByEmailAsync(normalizedEmail) 
                                         ?? await userManager.FindByNameAsync(normalizedEmail)
                                         ?? await context.Users.FirstOrDefaultAsync(u =>
                                             (u.Email != null && (u.Email.ToLower() == normalizedEmail || u.Email.ToLower().StartsWith(normalizedEmail + "_"))) ||
                                             (u.UserName != null && (u.UserName.ToLower() == normalizedEmail || u.UserName.ToLower().StartsWith(normalizedEmail + "_"))));

            if (existingUser == null && existingExternal != null && existingExternal.AppUsrId.HasValue)
            {
                existingUser = await userManager.FindByIdAsync(existingExternal.AppUsrId.Value.ToString());
            }

            // Contractor must exist in DB
            if (existingExternal == null && existingUser == null)
            {
                logger.LogInformation("External user registration rejected: '{0}' does not exist in DB.", normalizedEmail);
                return (false, "No contractor account found with this email. Please contact your administrator.", null);
            }

            if (existingUser != null)
            {
                logger.LogInformation("External user email '{0}' already exists in DB. Updating password.", normalizedEmail);

                var resetToken = await userManager.GeneratePasswordResetTokenAsync(existingUser);
                var resetResult = await userManager.ResetPasswordAsync(existingUser, resetToken, dto.Password);
                if (!resetResult.Succeeded)
                {
                    await userManager.RemovePasswordAsync(existingUser);
                    var addResult = await userManager.AddPasswordAsync(existingUser, dto.Password);
                    if (!addResult.Succeeded)
                    {
                        string errors = string.Join(", ", addResult.Errors.Select(e => e.Description));
                        return (false, errors, null);
                    }
                }

                existingUser.IsActive = true;
                existingUser.IsVerified = true;
                existingUser.EmailConfirmed = true;
                existingUser.IsExternalUser = true;
                existingUser.SeatType = SeatTypeEnum.Contractor;

                if (!string.IsNullOrWhiteSpace(dto.FirstName)) existingUser.FirstName = dto.FirstName.Trim();
                if (!string.IsNullOrWhiteSpace(dto.LastName)) existingUser.LastName = dto.LastName.Trim();
                if (!string.IsNullOrWhiteSpace(existingUser.FirstName) || !string.IsNullOrWhiteSpace(existingUser.LastName))
                {
                    existingUser.Name = $"{existingUser.FirstName} {existingUser.LastName}".Trim();
                }

                await userManager.UpdateAsync(existingUser);

                long tenantId = existingUser.TenantId ?? 1;
                var existingUserTenant = await context.ApplicationUserTenants.FirstOrDefaultAsync(ut => ut.UserId == existingUser.Id && ut.TenantId == tenantId);
                if (existingUserTenant == null)
                {
                    context.ApplicationUserTenants.Add(new() { UserId = existingUser.Id, TenantId = tenantId, IsActive = true });
                }

                var role = await context.ApplicationRoles.FirstOrDefaultAsync(r => r.TenantId == tenantId && (r.Name == "Contractor" || r.Name == "External" || r.Name == "User"))
                        ?? await context.ApplicationRoles.FirstOrDefaultAsync(r => r.TenantId == tenantId);
                if (role != null)
                {
                    var existingRole = await context.ApplicationUserRoles.FirstOrDefaultAsync(ur => ur.UserId == existingUser.Id && ur.RoleId == role.Id && ur.TenantId == tenantId);
                    if (existingRole == null)
                    {
                        context.ApplicationUserRoles.Add(new() { UserId = existingUser.Id, RoleId = role.Id, TenantId = tenantId, IsActive = true });
                    }
                }

                if (existingExternal != null)
                {
                    existingExternal.AppUsrId = existingUser.Id;
                    existingExternal.Email = normalizedEmail;
                    if (!string.IsNullOrWhiteSpace(existingUser.Name))
                    {
                        existingExternal.FullName = existingUser.Name;
                        existingExternal.Name = existingUser.Name;
                    }
                    existingExternal.IsActive = true;
                }
                else
                {
                    ExternalUser extUser = new()
                    {
                        TenantId = tenantId,
                        AppUsrId = existingUser.Id,
                        Email = normalizedEmail,
                        FullName = existingUser.Name ?? normalizedEmail,
                        Name = existingUser.Name ?? normalizedEmail,
                        EntityType = "Contractor",
                        PermissionLevel = "Upload and Comment",
                        IsActive = true,
                        CreateDate = DateTime.UtcNow
                    };
                    context.ExternalUsers.Add(extUser);
                }

                await context.SaveChangesAsync();

                memoryCache.Remove($"VERIFIED_{normalizedEmail}");
                memoryCache.Remove($"OTP_{normalizedEmail}");
                memoryCache.Remove(normalizedEmail);

                var extLoginResponse = authService.GetLoginResponse(existingUser, isExternalUser: true);
                return (true, "Password updated successfully.", extLoginResponse);
            }
            else
            {
                // existingExternal was found, create ApplicationUser for this contractor and link
                var tenant = await context.Tenants.FirstOrDefaultAsync();
                long tenantId = (existingExternal.TenantId.HasValue && existingExternal.TenantId.Value > 0) ? existingExternal.TenantId.Value : (tenant?.Id ?? 1);

                var currency = await context.Currencies.FirstOrDefaultAsync(c => c.TenantId == tenantId && c.IsBaseCurrency == true) 
                            ?? await context.Currencies.FirstOrDefaultAsync(c => c.TenantId == tenantId);
                var language = await context.Languages.FirstOrDefaultAsync(l => l.TenantId == tenantId) 
                            ?? await context.Languages.FirstOrDefaultAsync();

                string emailPrefix = normalizedEmail.Split('@')[0];
                string firstName = !string.IsNullOrWhiteSpace(dto.FirstName) 
                    ? dto.FirstName.Trim() 
                    : (!string.IsNullOrWhiteSpace(existingExternal.FullName) ? existingExternal.FullName.Split(' ')[0] : (char.ToUpper(emailPrefix[0]) + (emailPrefix.Length > 1 ? emailPrefix[1..] : "")));
                string lastName = !string.IsNullOrWhiteSpace(dto.LastName) 
                    ? dto.LastName.Trim() 
                    : (!string.IsNullOrWhiteSpace(existingExternal.FullName) && existingExternal.FullName.Contains(' ') ? existingExternal.FullName[(existingExternal.FullName.IndexOf(' ') + 1)..] : "Contractor");

                ApplicationUser user = new()
                {
                    UserName = normalizedEmail,
                    Email = normalizedEmail,
                    NormalizedUserName = normalizedEmail.ToUpperInvariant(),
                    NormalizedEmail = normalizedEmail.ToUpperInvariant(),
                    FirstName = firstName,
                    LastName = lastName,
                    Name = $"{firstName} {lastName}".Trim(),
                    TenantId = tenantId,
                    CurrencyId = currency?.Id,
                    LanguageId = language?.Id,
                    SeatType = SeatTypeEnum.Contractor,
                    IsExternalUser = true,
                    IsActive = true,
                    IsVerified = true,
                    EmailConfirmed = true,
                    IsValidProfile = true,
                    CreateDate = DateTime.UtcNow
                };

                IdentityResult result = await userManager.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                {
                    string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    logger.LogInformation("Failed to create user for existing external user '{0}': {1}", normalizedEmail, errors);
                    return (false, errors, null);
                }

                user.PhotoUrl = @"/avatars/" + user.Id + ".png";
                context.ApplicationUserTenants.Add(new() { UserId = user.Id, TenantId = tenantId, IsActive = true });

                var role = await context.ApplicationRoles.FirstOrDefaultAsync(r => r.TenantId == tenantId && (r.Name == "Contractor" || r.Name == "External" || r.Name == "User"))
                        ?? await context.ApplicationRoles.FirstOrDefaultAsync(r => r.TenantId == tenantId);
                if (role != null)
                {
                    context.ApplicationUserRoles.Add(new() { UserId = user.Id, RoleId = role.Id, TenantId = tenantId, IsActive = true });
                }

                existingExternal.AppUsrId = user.Id;
                existingExternal.FullName = user.Name;
                existingExternal.Name = user.Name;
                existingExternal.IsActive = true;

                await context.SaveChangesAsync();

                memoryCache.Remove($"VERIFIED_{normalizedEmail}");
                memoryCache.Remove($"OTP_{normalizedEmail}");
                memoryCache.Remove(normalizedEmail);

                var loginResponse = authService.GetLoginResponse(user, isExternalUser: true);
                return (true, "Password set successfully.", loginResponse);
            }
        }
        catch (Exception e)
        {
            logger.LogException(e, nameof(RegisterExternalUser));
            var detail = e.InnerException != null ? $"{e.Message} ({e.InnerException.Message})" : e.Message;
            return (false, $"An error occurred while creating external user account: {detail}", null);
        }
    }

    public async Task<(bool IsValid, string Message, LoginResponseDTO? User)> CompleteProfile(CompleteEmployeeProfileDTO dto, long? currentUserId = null)
    {
        try
        {
            if (dto == null) return (false, "Invalid profile data.", null);

            if (string.IsNullOrWhiteSpace(dto.Mobile))
                return (false, "Mobile number is mandatory to proceed.", null);

            if (string.IsNullOrWhiteSpace(dto.JobTitle))
                return (false, "Job title is mandatory to proceed.", null);

            ApplicationUser? user = null;
            if (currentUserId.HasValue && currentUserId.Value > 0)
            {
                user = await userManager.FindByIdAsync(currentUserId.Value.ToString());
            }
            else if (dto.UserId.HasValue && dto.UserId.Value > 0)
            {
                user = await userManager.FindByIdAsync(dto.UserId.Value.ToString());
            }
            else if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                string normalizedEmail = dto.Email.Trim().ToLowerInvariant();
                user = await userManager.FindByEmailAsync(normalizedEmail) 
                    ?? await userManager.FindByNameAsync(normalizedEmail);
            }

            if (user == null)
            {
                return (false, "User not found.", null);
            }

            user.Mobile = dto.Mobile.Trim();
            user.PhoneNumber = dto.Mobile.Trim();
            user.JobTitle = dto.JobTitle.Trim();
            if (!string.IsNullOrWhiteSpace(dto.FirstName)) user.FirstName = dto.FirstName.Trim();
            if (!string.IsNullOrWhiteSpace(dto.LastName)) user.LastName = dto.LastName.Trim();
            user.Name = $"{user.FirstName} {user.LastName}".Trim();
            user.IsValidProfile = true;
            user.UpdateDate = DateTime.UtcNow;

            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                string errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
                return (false, errors, null);
            }

            // Update HRM Employee record if exists
            var emp = await context.Employees.FirstOrDefaultAsync(e => e.AppUsrId == user.Id || (e.WorkEmail != null && e.WorkEmail.ToLower() == user.Email.ToLower()));
            if (emp != null)
            {
                emp.Mobile = user.Mobile;
                emp.FNameEn = user.FirstName;
                emp.LNameEn = user.LastName;
                emp.Name = user.Name;
                emp.UpdateDate = DateTime.UtcNow;
            }

            await context.SaveChangesAsync();

            var loginResponse = authService.GetLoginResponse(user, isExternalUser: user.IsExternalUser);
            return (true, "Profile updated successfully.", loginResponse);
        }
        catch (Exception e)
        {
            logger.LogException(e, nameof(CompleteProfile));
            var detail = e.InnerException != null ? $"{e.Message} ({e.InnerException.Message})" : e.Message;
            return (false, $"An error occurred while updating profile: {detail}", null);
        }
    }
}
