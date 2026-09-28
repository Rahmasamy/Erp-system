using Aspire.Context;
using Aspire.Data;
using Aspire.Enums;
using Aspire.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aspire.Services;

public class SeedResultDTO
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, int> RecordsSeeded { get; set; } = new();
    public DateTime SeededAt { get; set; } = DateTime.UtcNow;
}

public interface IDatabaseSeederService
{
    Task<SeedResultDTO> SeedAllAsync();
    Task<SeedResultDTO> SeedTenantAndUsersAsync();
    Task<SeedResultDTO> SeedMissingActivitiesAndAttachmentsAsync(long? targetTenantId = null);
    Task<Dictionary<string, int>> GetSeedStatusAsync();
    Task<SeedResultDTO> EnsureAdminUserAsync(string username = "admin", string email = "admin@aspire.com", string password = "Sehmawy&*190876");
}

public class DatabaseSeederService(
    ApplicationDbContext context,
    IRegisterService registerService,
    ITenantProviderService tenantProviderService,
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    ILogHandlingService logger
) : IDatabaseSeederService
{
    public async Task<SeedResultDTO> EnsureAdminUserAsync(string username = "admin", string email = "admin@aspire.com", string password = "Sehmawy&*190876")
    {
        var result = new SeedResultDTO();
        try
        {
            var tenant = await context.Tenants.FirstOrDefaultAsync();
            if (tenant == null)
            {
                long tenantId = registerService.CreateTenant();
                registerService.Register(tenantId);
                tenant = await context.Tenants.FirstOrDefaultAsync();
            }

            if (tenant == null)
            {
                result.Success = false;
                result.Message = "Could not find or create a tenant.";
                return result;
            }

            tenantProviderService.SetTenantId(tenant.Id);

            var adminRole = await context.ApplicationRoles.FirstOrDefaultAsync(r => r.TenantId == tenant.Id && r.Name == "Admin");
            if (adminRole == null)
            {
                adminRole = context.ApplicationRoles.Add(new ApplicationRole(tenant.Id, "Admin")).Entity;
                await context.SaveChangesAsync();
            }

            var cleanUsername = username.Trim();
            var cleanEmail = email.Trim();
            var cleanNormalized = cleanUsername.ToUpperInvariant();

            var user = await userManager.FindByNameAsync(cleanUsername)
                ?? await userManager.FindByEmailAsync(cleanEmail)
                ?? await context.Users.FirstOrDefaultAsync(u => u.UserName == cleanUsername || u.Email == cleanEmail || u.NormalizedUserName == cleanNormalized);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    TenantId = tenant.Id,
                    SeatType = SeatTypeEnum.Admin,
                    Name = "Admin User",
                    FirstName = "Admin",
                    LastName = "User",
                    Email = cleanEmail,
                    UserName = cleanUsername,
                    NormalizedUserName = cleanNormalized,
                    NormalizedEmail = cleanEmail.ToUpperInvariant(),
                    JobTitle = "System Administrator",
                    IsActive = true,
                    IsVerified = true,
                    EmailConfirmed = true,
                    OTP = string.Empty,
                    LastAccessedTenant = string.Empty
                };

                var createRes = await userManager.CreateAsync(user, password);
                if (!createRes.Succeeded)
                {
                    result.Success = false;
                    result.Message = string.Join("; ", createRes.Errors.Select(e => e.Description));
                    return result;
                }
            }
            else
            {
                user.IsActive = true;
                user.IsVerified = true;
                user.EmailConfirmed = true;
                user.SeatType = SeatTypeEnum.Admin;

                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                await userManager.ResetPasswordAsync(user, token, password);
                await userManager.UpdateAsync(user);
            }

            // Ensure ApplicationUserTenants
            if (!await context.ApplicationUserTenants.AnyAsync(x => x.UserId == user.Id && x.TenantId == tenant.Id))
            {
                context.ApplicationUserTenants.Add(new ApplicationUserTenant { UserId = user.Id, TenantId = tenant.Id, IsActive = true });
            }

            // Ensure ApplicationUserRoles
            if (adminRole != null && !await context.ApplicationUserRoles.AnyAsync(x => x.UserId == user.Id && x.RoleId == adminRole.Id))
            {
                context.ApplicationUserRoles.Add(new ApplicationUserRole { TenantId = tenant.Id, UserId = user.Id, RoleId = adminRole.Id, IsActive = true });
            }

            // Ensure Employee record
            var emp = await context.Employees.FirstOrDefaultAsync(e => e.AppUsrId == user.Id || e.WorkEmail == cleanEmail);
            if (emp == null)
            {
                var job = await context.Jobs.FirstOrDefaultAsync(j => j.TenantId == tenant.Id);
                context.Employees.Add(new Employee(tenant.Id, user, "Admin User", job?.Id, "ADM-001", cleanEmail, "0500000000", true));
            }

            await context.SaveChangesAsync();

            result.Success = true;
            result.Message = $"Admin user '{cleanUsername}' ({cleanEmail}) created/updated successfully with role Admin. Password: {password}";
            result.RecordsSeeded = await GetSeedStatusAsync();
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Error ensuring admin user: {ex.Message}";
            logger.LogError("EnsureAdminUserAsync error: {0}", ex.ToString());
        }

        return result;
    }

    public async Task<Dictionary<string, int>> GetSeedStatusAsync()
    {
        return new Dictionary<string, int>
        {
            { "Tenants", await context.Tenants.CountAsync() },
            { "Users", await context.Users.CountAsync() },
            { "Roles", await context.Roles.CountAsync() },
            { "Customers", await context.Customers.CountAsync() },
            { "Contacts", await context.Contacts.CountAsync() },
            { "Leads", await context.Leads.CountAsync() },
            { "Deals", await context.Deals.CountAsync() },
            { "Suppliers", await context.Suppliers.CountAsync() },
            { "WorkItems", await context.WorkItems.CountAsync() },
            { "Attachments", await context.EntityAttachments.CountAsync() },
            { "Comments", await context.EntityComments.CountAsync() },
            { "Currencies", await context.Currencies.CountAsync() },
            { "Languages", await context.Languages.CountAsync() }
        };
    }

    public async Task<SeedResultDTO> SeedAllAsync()
    {
        var result = new SeedResultDTO();
        try
        {
            logger.LogInformation("Starting Database Seeding...");

            var tenant = await context.Tenants.FirstOrDefaultAsync();
            if (tenant == null)
            {
                long tenantId = registerService.CreateTenant();
                registerService.Register(tenantId);
                result.Message = "Default Tenant, Users, and all system modules seeded successfully.";
            }
            else
            {
                tenantProviderService.SetTenantId(tenant.Id);
                var customerCount = await context.Customers.CountAsync();
                if (customerCount == 0)
                {
                    try
                    {
                        registerService.Register(tenant.Id);
                        result.Message = "Existing Tenant found. System modules and demo data seeded successfully.";
                    }
                    catch (Exception regEx)
                    {
                        var inner = regEx.InnerException != null ? (" Inner: " + regEx.InnerException.Message + (regEx.InnerException.InnerException != null ? " Inner2: " + regEx.InnerException.InnerException.Message : "")) : "";
                        logger.LogError("Module register notice: {0} {1}", regEx.ToString(), inner);
                        result.Message = $"Tenant verified. Modules seeding notice: {regEx.Message}{inner}";
                    }
                }
                else
                {
                    result.Message = "Database is already seeded with tenant and modules.";
                }
            }

            // Seed activities, attachments, and comments for all entities
            await SeedMissingActivitiesAndAttachmentsAsync(tenant?.Id);

            // Ensure Admin user exists and is ready
            await EnsureAdminUserAsync();

            result.RecordsSeeded = await GetSeedStatusAsync();
            result.Success = true;
            logger.LogInformation("Database Seeding Finished Successfully: {0}", result.Message);
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Seeding failed: {ex.Message}";
            logger.LogError("Database seeding error: {0}", ex.ToString());
        }

        return result;
    }

    public async Task<SeedResultDTO> SeedTenantAndUsersAsync()
    {
        var result = new SeedResultDTO();
        try
        {
            var tenant = await context.Tenants.FirstOrDefaultAsync();
            if (tenant == null)
            {
                long tenantId = registerService.CreateTenant();
                registerService.Register(tenantId);
            }
            else
            {
                tenantProviderService.SetTenantId(tenant.Id);
            }

            await EnsureAdminUserAsync();

            result.RecordsSeeded = await GetSeedStatusAsync();
            result.Success = true;
            result.Message = "Default Tenant, Admin User, and configuration verified.";
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }
        return result;
    }

    public async Task<SeedResultDTO> SeedMissingActivitiesAndAttachmentsAsync(long? targetTenantId = null)
    {
        var result = new SeedResultDTO();
        try
        {
            var tenants = targetTenantId.HasValue
                ? await context.Tenants.Where(t => t.Id == targetTenantId.Value).ToListAsync()
                : await context.Tenants.ToListAsync();

            if (!tenants.Any())
            {
                result.Success = false;
                result.Message = "No tenants found in the database. Please seed a tenant first.";
                return result;
            }

            int activitiesCount = 0;
            int attachmentsCount = 0;
            int commentsCount = 0;

            foreach (var tenant in tenants)
            {
                long tId = tenant.Id;
                tenantProviderService.SetTenantId(tId);

                // Lookups for WorkItem
                var tskSizes = await context.TaskSizes.Where(x => x.TenantId == tId).ToListAsync();
                if (!tskSizes.Any())
                {
                    tskSizes.Add(context.TaskSizes.Add(new TaskSize(tId, "Small", "صغير")).Entity);
                    tskSizes.Add(context.TaskSizes.Add(new TaskSize(tId, "Medium", "متوسط")).Entity);
                    tskSizes.Add(context.TaskSizes.Add(new TaskSize(tId, "Large", "كبير")).Entity);
                    await context.SaveChangesAsync();
                }

                var tskComplexities = await context.TaskComplexities.Where(x => x.TenantId == tId).ToListAsync();
                if (!tskComplexities.Any())
                {
                    tskComplexities.Add(context.TaskComplexities.Add(new TaskComplexity(tId, "Simple", "بسيط")).Entity);
                    tskComplexities.Add(context.TaskComplexities.Add(new TaskComplexity(tId, "Normal", "عادي")).Entity);
                    tskComplexities.Add(context.TaskComplexities.Add(new TaskComplexity(tId, "Complex", "معقد")).Entity);
                    await context.SaveChangesAsync();
                }

                var tskPriorities = await context.TaskPriorities.Where(x => x.TenantId == tId).ToListAsync();
                if (!tskPriorities.Any())
                {
                    tskPriorities.Add(context.TaskPriorities.Add(new TaskPriority(tId, "Low", "منخفض", "info")).Entity);
                    tskPriorities.Add(context.TaskPriorities.Add(new TaskPriority(tId, "Normal", "عادي", "primary")).Entity);
                    tskPriorities.Add(context.TaskPriorities.Add(new TaskPriority(tId, "High", "مرتفع", "warning")).Entity);
                    tskPriorities.Add(context.TaskPriorities.Add(new TaskPriority(tId, "Urgent", "عاجل", "danger")).Entity);
                    await context.SaveChangesAsync();
                }

                // EntityTypes for WorkItem
                var entTypes = await context.EntityTypes.Where(x => x.TenantId == tId && x.EntityName == "WorkItem").ToListAsync();
                var typeTask = entTypes.FirstOrDefault(x => x.Name == "Task") ?? context.EntityTypes.Add(new EntityType(tId, "WorkItem", "Task", "مهمة", "primary")).Entity;
                var typeCall = entTypes.FirstOrDefault(x => x.Name == "Call") ?? context.EntityTypes.Add(new EntityType(tId, "WorkItem", "Call", "مكالمة", "success")).Entity;
                var typeMeeting = entTypes.FirstOrDefault(x => x.Name == "Meeting") ?? context.EntityTypes.Add(new EntityType(tId, "WorkItem", "Meeting", "اجتماع", "info")).Entity;
                var typeVisit = entTypes.FirstOrDefault(x => x.Name == "Visit") ?? context.EntityTypes.Add(new EntityType(tId, "WorkItem", "Visit", "زيارة", "warning")).Entity;
                var typeNote = entTypes.FirstOrDefault(x => x.Name == "Note") ?? context.EntityTypes.Add(new EntityType(tId, "WorkItem", "Note", "ملاحظة", "secondary")).Entity;
                await context.SaveChangesAsync();

                // EntityStatus for WorkItem
                var entStatuses = await context.EntityStatuses.Where(x => x.TenantId == tId && x.EntityName == "WorkItem").ToListAsync();
                var statusPlanned = entStatuses.FirstOrDefault(x => x.Name == "Planned") ?? context.EntityStatuses.Add(new EntityStatus(tId, "WorkItem", "Planned", "مخطط", "secondary")).Entity;
                var statusInProgress = entStatuses.FirstOrDefault(x => x.Name == "In Progress") ?? context.EntityStatuses.Add(new EntityStatus(tId, "WorkItem", "In Progress", "قيد التنفيذ", "info")).Entity;
                var statusCompleted = entStatuses.FirstOrDefault(x => x.Name == "Completed") ?? context.EntityStatuses.Add(new EntityStatus(tId, "WorkItem", "Completed", "مكتمل", "success")).Entity;
                await context.SaveChangesAsync();

                // Users
                var users = await context.Users.Where(u => u.TenantId == tId).ToListAsync();
                if (!users.Any()) users = await context.Users.ToListAsync();
                long defaultUserId = users.FirstOrDefault()?.Id ?? 1;

                var entityTargets = new List<(string EntityName, List<long> Ids)>
                {
                    ("Customer", await context.Customers.Where(c => c.TenantId == tId).Select(c => c.Id).ToListAsync()),
                    ("Project", await context.Projects.Where(p => p.TenantId == tId).Select(p => p.Id).ToListAsync()),
                    ("Deal", await context.Deals.Where(d => d.TenantId == tId).Select(d => d.Id).ToListAsync()),
                    ("Lead", await context.Leads.Where(l => l.TenantId == tId).Select(l => l.Id).ToListAsync()),
                    ("Supplier", await context.Suppliers.Where(s => s.TenantId == tId).Select(s => s.Id).ToListAsync()),
                    ("Agreement", await context.Agreements.Where(a => a.TenantId == tId).Select(a => a.Id).ToListAsync()),
                    ("Contact", await context.Contacts.Where(c => c.TenantId == tId).Select(c => c.Id).ToListAsync()),
                    ("SalesOrder", await context.SalesOrders.Where(s => s.TenantId == tId).Select(s => s.Id).ToListAsync()),
                    ("PurchaseOrder", await context.PurchaseOrders.Where(p => p.TenantId == tId).Select(p => p.Id).ToListAsync()),
                    ("Portfolio", await context.Portfolios.Where(p => p.TenantId == tId).Select(p => p.Id).ToListAsync()),
                    ("Programme", await context.Programmes.Where(p => p.TenantId == tId).Select(p => p.Id).ToListAsync()),
                    ("WorkOrder", await context.WorkOrders.Where(w => w.TenantId == tId).Select(w => w.Id).ToListAsync())
                };

                var activityTemplates = new[]
                {
                    (Title: "Quarterly Review & Strategic Alignment", TypeId: typeMeeting.Id, StatusId: statusPlanned.Id, Priority: tskPriorities.First().Id, Desc: "Strategic review session discussing milestones, performance indicators, and deliverables alignment."),
                    (Title: "Follow-up & Coordination Call", TypeId: typeCall.Id, StatusId: statusCompleted.Id, Priority: tskPriorities.Last().Id, Desc: "Follow-up discussion with key stakeholders regarding upcoming deliverables and timeline confirmations."),
                    (Title: "Requirements & Scope Verification", TypeId: typeTask.Id, StatusId: statusInProgress.Id, Priority: tskPriorities.Skip(1).First().Id, Desc: "Review and verify scope requirements, technical specifications, and resource allocations."),
                    (Title: "On-Site Stakeholder Visit & Inspection", TypeId: typeVisit.Id, StatusId: statusPlanned.Id, Priority: tskPriorities.First().Id, Desc: "On-site visit to inspect work progress, review site readiness, and align on project schedule."),
                    (Title: "Action Items & Execution Notes", TypeId: typeNote.Id, StatusId: statusCompleted.Id, Priority: tskPriorities.First().Id, Desc: "Summary of discussion points, agreed actions, and responsible team owners.")
                };

                foreach (var target in entityTargets)
                {
                    foreach (var objId in target.Ids)
                    {
                        bool hasActivities = await context.WorkItems.AnyAsync(w => w.EntityName == target.EntityName && w.ObjectId == objId);
                        if (!hasActivities)
                        {
                            int index = 0;
                            var createdList = new List<WorkItem>();
                            foreach (var template in activityTemplates)
                            {
                                long assignedUserId = users.Any() ? users[index % users.Count].Id : defaultUserId;
                                var workItem = new WorkItem
                                {
                                    TenantId = tId,
                                    EntityName = target.EntityName,
                                    ObjectId = objId,
                                    Name = $"{template.Title}",
                                    Description = template.Desc,
                                    EntityTypeId = template.TypeId,
                                    EntityStatusId = template.StatusId,
                                    TaskPriorityId = template.Priority,
                                    TaskSizeId = tskSizes.First().Id,
                                    TaskComplexityId = tskComplexities.First().Id,
                                    AssignedToUsrId = assignedUserId,
                                    StartDate = DateTime.UtcNow.AddDays(-5 + index * 2),
                                    EndDate = DateTime.UtcNow.AddDays(10 + index * 3),
                                    CreateDate = DateTime.UtcNow.AddDays(-7 + index),
                                    CreatedByUsrId = assignedUserId,
                                    IsLeaf = true,
                                    IsTimeSheet = true,
                                    calc_Comments_num = 2
                                };
                                context.WorkItems.Add(workItem);
                                createdList.Add(workItem);
                                activitiesCount++;
                                index++;
                            }
                            await context.SaveChangesAsync();

                            foreach (var wi in createdList)
                            {
                                context.EntityComments.Add(new EntityComment
                                {
                                    TenantId = tId,
                                    EntityName = "WorkItem",
                                    ObjectId = wi.Id,
                                    Notes = "Task has been initialized with the assigned team. Requirements and dependencies are mapped out.",
                                    Description = "Task has been initialized with the assigned team. Requirements and dependencies are mapped out.",
                                    CreatedByUsrId = wi.AssignedToUsrId ?? defaultUserId,
                                    CreateDate = DateTime.UtcNow.AddDays(-2)
                                });
                                context.EntityComments.Add(new EntityComment
                                {
                                    TenantId = tId,
                                    EntityName = "WorkItem",
                                    ObjectId = wi.Id,
                                    Notes = "Progress update: Deliverables are on track for completion according to the defined schedule.",
                                    Description = "Progress update: Deliverables are on track for completion according to the defined schedule.",
                                    CreatedByUsrId = defaultUserId,
                                    CreateDate = DateTime.UtcNow.AddHours(-4)
                                });
                                commentsCount += 2;
                            }
                        }

                        bool hasAttachments = await context.EntityAttachments.AnyAsync(a => a.EntityName == target.EntityName && a.ObjectId == objId);
                        if (!hasAttachments)
                        {
                            context.EntityAttachments.Add(new EntityAttachment
                            {
                                TenantId = tId,
                                EntityName = target.EntityName,
                                ObjectId = objId,
                                Name = "Executive_Summary_and_Scope.pdf",
                                FileName = "Executive_Summary_and_Scope.pdf",
                                FileExtension = ".pdf",
                                ContentType = "application/pdf",
                                FileSize = 1485760,
                                DownloadLink = "/Aspire-Attachments/Executive_Summary_and_Scope.pdf",
                                CreatedByUsrId = defaultUserId,
                                CreateDate = DateTime.UtcNow.AddDays(-4)
                            });
                            context.EntityAttachments.Add(new EntityAttachment
                            {
                                TenantId = tId,
                                EntityName = target.EntityName,
                                ObjectId = objId,
                                Name = "Official_Agreement_Signed.pdf",
                                FileName = "Official_Agreement_Signed.pdf",
                                FileExtension = ".pdf",
                                ContentType = "application/pdf",
                                FileSize = 2194304,
                                DownloadLink = "/Aspire-Attachments/Official_Agreement_Signed.pdf",
                                CreatedByUsrId = defaultUserId,
                                CreateDate = DateTime.UtcNow.AddDays(-3)
                            });
                            context.EntityAttachments.Add(new EntityAttachment
                            {
                                TenantId = tId,
                                EntityName = target.EntityName,
                                ObjectId = objId,
                                Name = "Financial_and_Cost_Breakdown.xlsx",
                                FileName = "Financial_and_Cost_Breakdown.xlsx",
                                FileExtension = ".xlsx",
                                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                FileSize = 724800,
                                DownloadLink = "/Aspire-Attachments/Financial_and_Cost_Breakdown.xlsx",
                                CreatedByUsrId = defaultUserId,
                                CreateDate = DateTime.UtcNow.AddDays(-1)
                            });
                            attachmentsCount += 3;
                        }
                    }
                }

                await context.SaveChangesAsync();
            }

            result.Success = true;
            result.Message = $"Successfully seeded {activitiesCount} activities, {attachmentsCount} attachments, and {commentsCount} comments across all entities.";
            result.RecordsSeeded = new Dictionary<string, int>
            {
                { "ActivitiesAdded", activitiesCount },
                { "AttachmentsAdded", attachmentsCount },
                { "CommentsAdded", commentsCount },
                { "TotalWorkItemsInDB", await context.WorkItems.CountAsync() },
                { "TotalAttachmentsInDB", await context.EntityAttachments.CountAsync() },
                { "TotalCommentsInDB", await context.EntityComments.CountAsync() }
            };
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Seeding activities error: {ex.Message}";
            logger.LogError("SeedMissingActivitiesAndAttachmentsAsync error: {0}", ex.ToString());
        }

        return result;
    }
}
