using Aspire.Context;
using Aspire.Migration;
using Aspire.Migration.Seed;
using Aspire.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

if (args.Contains("--seed"))
{
    Console.WriteLine("Seed command started...");

    AppConfiguration.SetAppConfigurationValues();

    var services = new ServiceCollection();

    services.AddScoped<
        ITenantProviderService,
        MigrationTenantProvider>();

    services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseNpgsql(
            AppConfiguration.ConnectionString);
    });

    var serviceProvider = services.BuildServiceProvider();

    using var scope = serviceProvider.CreateScope();

    var dbContext = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    Console.WriteLine("DbContext created successfully.");

    var excelReader = new ExcelReader();

    var ownerSeeder = new DMSProjectOwnerSeeder(
        excelReader,
        dbContext);

    await ownerSeeder.SeedAsync();

    Console.WriteLine("Seed process completed successfully.");
}
else
{
    Console.WriteLine("No command specified.");
}