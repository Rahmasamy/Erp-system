using Aspire.Context;
using Aspire.Data;
using Microsoft.EntityFrameworkCore;

namespace Aspire.Migration.Seed;

public class DMSProjectOwnerSeeder
{
    private readonly ExcelReader _excelReader;
    private readonly ApplicationDbContext _dbContext;

    public DMSProjectOwnerSeeder(
        ExcelReader excelReader,
        ApplicationDbContext dbContext)
    {
        _excelReader = excelReader;
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        var rows = _excelReader.ReadRows("DMSProjectOwner");

        var owners = new List<DMSProjectOwner>();

        foreach (var row in rows)
        {
            var oldId = GetValue(row, "_id");

            if (string.IsNullOrWhiteSpace(oldId))
                continue;

            var owner = new DMSProjectOwner
            {
                TenantId = 1,
                OldId = oldId,
                Name = GetValue(row, "name"),
                PhotoUrl = GetValue(row, "avatar"),
                Email = GetValue(row, "email"),
                Phone = GetValue(row, "phoneNumber"),
                Mobile = GetValue(row, "mobileNumber"),
                Description = GetValue(row, "description")
            };

            owners.Add(owner);
        }

        if (owners.Count == 0)
        {
            Console.WriteLine("No DMS Project Owners found in Excel.");
            return;
        }

        // Get the old IDs from Excel
        var oldIds = owners
            .Select(x => x.OldId)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();

        // Get existing records from database
        var existingOldIds = await _dbContext.DMSProjectOwners
            .Where(x =>
                x.TenantId == 1 &&
                x.OldId != null &&
                oldIds.Contains(x.OldId))
            .Select(x => x.OldId!)
            .ToListAsync();

        // Only insert records that don't already exist
        var newOwners = owners
            .Where(x => x.OldId != null &&
                        !existingOldIds.Contains(x.OldId))
            .ToList();

        if (newOwners.Count == 0)
        {
            Console.WriteLine("No new DMS Project Owners to seed.");
            return;
        }

        await _dbContext.DMSProjectOwners.AddRangeAsync(newOwners);
        await _dbContext.SaveChangesAsync();

        Console.WriteLine(
            $"Successfully seeded {newOwners.Count} new DMS Project Owners.");

        Console.WriteLine(
            $"Skipped {owners.Count - newOwners.Count} existing records.");
    }

    private static string? GetValue(
        ExcelRow row,
        string columnName)
    {
        return row.Values.TryGetValue(columnName, out var value)
            ? value?.Trim()
            : null;
    }
}