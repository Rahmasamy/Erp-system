using Aspire.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using Aspire.Context;
using Aspire.DI;
using Aspire.Utilities;
using Aspire.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up Identity");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.Limits.MaxRequestHeadersTotalSize = 1048576; // 1 MB (default is 32 KB)
        options.Limits.MaxRequestBufferSize = 1048576;
    });
    //SecretManager.ConfigureAppSecrets();
    AppConfiguration.SetAppConfigurationValues();
    builder.Services.AddApplicationStrapping();
    builder.Services.AddInfrastructureStrapping();
    _ = builder.Services.AddMemoryCache();

    _ = builder.Host.UseSerilog((ctx, lc) => lc
       .WriteTo.Console(outputTemplate: "[{Timestamp: dd/MM/yyy HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
       .Enrich.FromLogContext().Enrich.WithProperty("Application", "Identity").ReadFrom.Configuration(ctx.Configuration));

    //_ = builder.Services.AddCors(options => options.AddPolicy("localhost", e =>
    //{
    //    e.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    //}));


    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy
                .SetIsOriginAllowed(_ => true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });

    _ = builder.Services.AddControllers(options =>
    {
        options.Filters.Add<TenantActionFilter>();
        //_ = options.Filters.Add<UserActionFilter>();
        //_ = options.Filters.Add<CurrencyActionFilter>();
    });

    _ = builder.Services.AddHttpContextAccessor();

    _ = builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
    {
        config.Password.RequiredLength = 12;
        config.Password.RequireDigit = true;
        config.Password.RequireNonAlphanumeric = true;
        config.Password.RequireUppercase = true;
    })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

    _ = builder.Services.ConfigureApplicationCookie(config =>
    {
        config.Cookie.Name = "IdentityServer.Cookie";
        config.Cookie.SameSite = SameSiteMode.None;
        config.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        config.LoginPath = "/Auth/Login";
        config.LogoutPath = "/Auth/Logout";
    });

    _ = builder.Services.AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
        options.EmitStaticAudienceClaim = true;
    }).AddAspNetIdentity<ApplicationUser>()
      .AddInMemoryClients(Config.Clients)
      .AddInMemoryIdentityResources(Config.IdentityResources)
      .AddInMemoryApiResources(Config.ApiResources)
      .AddInMemoryApiScopes(Config.ApiScopes)
      .AddDeveloperSigningCredential();

    _ = builder.Services.AddAuthentication("IdentityApiKey")
        .AddJwtBearer("IdentityApiKey", options =>
        {
            options.RequireHttpsMetadata = false;
            options.Authority = "http://localhost:5000";
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = false,
                ValidIssuer = AppConfiguration.JWT_ISSUER,
                ValidAudience = AppConfiguration.JWT_AUDIENCE,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfiguration.JWT_KEY))
            };
        });

    _ = builder.Services.AddEndpointsApiExplorer();
    _ = builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "IdentityApiKey", Version = "v1" });
    });

    WebApplication app = builder.Build();

    string avatarsPath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "avatars");
    if (!Directory.Exists(avatarsPath))
    {
        Directory.CreateDirectory(avatarsPath);
    }

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(avatarsPath),
        ServeUnknownFileTypes = true,
        RequestPath = "/avatars"
    });

    _ = app.UseDeveloperExceptionPage();
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
    _ = app.UseStaticFiles();

    _ = app.UseRouting();

    app.UseCors("AllowAll");

    _ = app.UseSerilogRequestLogging();
    _ = app.UseAuthentication();
    _ = app.UseAuthorization();
    _ = app.UseHttpsRedirection();
    _ = app.MapDefaultControllerRoute();
    _ = app.MapControllers();

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        try
        {
            await db.Database.ExecuteSqlRawAsync(@"
                ALTER TABLE IF EXISTS ""Customers"" ADD COLUMN IF NOT EXISTS ""GenderId"" bigint;
                ALTER TABLE IF EXISTS ""Customers"" ADD COLUMN IF NOT EXISTS ""FirstName"" text;
                ALTER TABLE IF EXISTS ""Customers"" ADD COLUMN IF NOT EXISTS ""LastName"" text;
                ALTER TABLE IF EXISTS ""Customers"" ADD COLUMN IF NOT EXISTS ""Email"" text;
                ALTER TABLE IF EXISTS ""Customers"" ADD COLUMN IF NOT EXISTS ""Mobile"" text;
                ALTER TABLE IF EXISTS ""Customers"" ADD COLUMN IF NOT EXISTS ""JobTitle"" text;
                ALTER TABLE IF EXISTS ""Customers"" ADD COLUMN IF NOT EXISTS ""Phone"" text;
                ALTER TABLE IF EXISTS ""Customers"" ADD COLUMN IF NOT EXISTS ""CompanyEmail"" text;
                ALTER TABLE IF EXISTS ""Leads"" ADD COLUMN IF NOT EXISTS ""CustomerId"" bigint;
                ALTER TABLE IF EXISTS ""Leads"" ADD COLUMN IF NOT EXISTS ""ContactId"" bigint;
                ALTER TABLE IF EXISTS ""Suppliers"" ADD COLUMN IF NOT EXISTS ""PriceListId"" bigint;
                ALTER TABLE IF EXISTS ""AspNetUsers"" ADD COLUMN IF NOT EXISTS ""IsValidProfile"" boolean DEFAULT false;
                ALTER TABLE IF EXISTS ""AspNetUsers"" ADD COLUMN IF NOT EXISTS ""IsExternalUser"" boolean DEFAULT false;
                ALTER TABLE IF EXISTS ""AspNetUsers"" ADD COLUMN IF NOT EXISTS ""Mobile"" text;
                ALTER TABLE IF EXISTS ""AspNetUsers"" ADD COLUMN IF NOT EXISTS ""JobTitle"" text;
                ALTER TABLE IF EXISTS ""Employees"" ADD COLUMN IF NOT EXISTS ""Code"" text;
                ALTER TABLE IF EXISTS ""Employees"" ADD COLUMN IF NOT EXISTS ""IsController"" boolean DEFAULT false;
                ALTER TABLE IF EXISTS ""Employees"" ADD COLUMN IF NOT EXISTS ""IsValidProfile"" boolean DEFAULT false;
                ALTER TABLE IF EXISTS ""Employees"" ADD COLUMN IF NOT EXISTS ""PersonalEmail"" text;
                ALTER TABLE IF EXISTS ""Employees"" ADD COLUMN IF NOT EXISTS ""WorkEmail"" text;
                ALTER TABLE IF EXISTS ""Employees"" ADD COLUMN IF NOT EXISTS ""FNameEn"" text;
                ALTER TABLE IF EXISTS ""Employees"" ADD COLUMN IF NOT EXISTS ""FNameAr"" text;
                ALTER TABLE IF EXISTS ""Employees"" ADD COLUMN IF NOT EXISTS ""MNameEn"" text;
                ALTER TABLE IF EXISTS ""Employees"" ADD COLUMN IF NOT EXISTS ""MNameAr"" text;
                ALTER TABLE IF EXISTS ""Employees"" ADD COLUMN IF NOT EXISTS ""LNameEn"" text;
                ALTER TABLE IF EXISTS ""Employees"" ADD COLUMN IF NOT EXISTS ""LNameAr"" text;
                ALTER TABLE IF EXISTS ""Employees"" ADD COLUMN IF NOT EXISTS ""Mobile"" text;
                ALTER TABLE IF EXISTS ""Employees"" ADD COLUMN IF NOT EXISTS ""AppUsrId"" bigint;
                ALTER TABLE IF EXISTS ""Documents"" ADD COLUMN IF NOT EXISTS ""ContractNo"" text;
                ALTER TABLE IF EXISTS ""Documents"" ADD COLUMN IF NOT EXISTS ""ContractorId"" bigint;
                ALTER TABLE IF EXISTS ""Documents"" ADD COLUMN IF NOT EXISTS ""DMSProjectId"" bigint;
                ALTER TABLE IF EXISTS ""Documents"" ADD COLUMN IF NOT EXISTS ""DocumentTypeId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""ContractorId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""DMSProjectId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""DocumentTypeId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""DocumentId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""DisciplineId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""ActionUsrId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""ActionRole"" text;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""DMSActionId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""StepNo"" integer;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""IsOpen"" boolean DEFAULT true;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""StartDate"" timestamp without time zone;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""DueDate"" timestamp without time zone;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""ActionDate"" timestamp without time zone;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""ActionComment"" text;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""EntityStatusId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""EntityTypeId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""DataConfidentialityId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""Name"" text;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""NameAr"" text;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""Description"" text;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""DescriptionAr"" text;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""No"" text;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""TenantId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""IsActive"" boolean;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""Msg"" text;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""PhotoUrl"" text;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""CreatedByUsrId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""UpdatedByUsrId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""CreateDate"" timestamp without time zone;
                ALTER TABLE IF EXISTS ""DocumentWorkItems"" ADD COLUMN IF NOT EXISTS ""UpdateDate"" timestamp without time zone;
                ALTER TABLE IF EXISTS ""DBLogs"" ADD COLUMN IF NOT EXISTS ""TenantId"" bigint;
                ALTER TABLE IF EXISTS ""DBLogs"" ADD COLUMN IF NOT EXISTS ""ObjectId"" bigint;
                ALTER TABLE IF EXISTS ""DBLogs"" ADD COLUMN IF NOT EXISTS ""EntityName"" text;
                ALTER TABLE IF EXISTS ""DBLogs"" ADD COLUMN IF NOT EXISTS ""Action"" text;
                ALTER TABLE IF EXISTS ""DBLogs"" ADD COLUMN IF NOT EXISTS ""ActionComment"" text;
                ALTER TABLE IF EXISTS ""DBLogs"" ADD COLUMN IF NOT EXISTS ""ActionUsrId"" bigint;
                ALTER TABLE IF EXISTS ""DBLogs"" ADD COLUMN IF NOT EXISTS ""ActionDate"" timestamp without time zone;
                ALTER TABLE IF EXISTS ""ExternalUsers"" ADD COLUMN IF NOT EXISTS ""ContractorId"" bigint;
                ALTER TABLE IF EXISTS ""ExternalUsers"" ADD COLUMN IF NOT EXISTS ""DMSProjectOwnerId"" bigint;
                ALTER TABLE IF EXISTS ""ExternalUsers"" ADD COLUMN IF NOT EXISTS ""FullName"" text;
                ALTER TABLE IF EXISTS ""ExternalUsers"" ADD COLUMN IF NOT EXISTS ""Email"" text;
                ALTER TABLE IF EXISTS ""ExternalUsers"" ADD COLUMN IF NOT EXISTS ""Phone"" text;
                ALTER TABLE IF EXISTS ""ExternalUsers"" ADD COLUMN IF NOT EXISTS ""AssignedProjects"" text;
                ALTER TABLE IF EXISTS ""ExternalUsers"" ADD COLUMN IF NOT EXISTS ""PermissionLevel"" text;
                ALTER TABLE IF EXISTS ""ExternalUsers"" ADD COLUMN IF NOT EXISTS ""AppUsrId"" bigint;
                ALTER TABLE IF EXISTS ""Contractors"" ADD COLUMN IF NOT EXISTS ""AssignedProjects"" text;
                ALTER TABLE IF EXISTS ""DMSProjects"" ADD COLUMN IF NOT EXISTS ""DMSProjectOwnerId"" bigint;
                ALTER TABLE IF EXISTS ""DocumentTypeDisciplines"" ADD COLUMN IF NOT EXISTS ""TaskName"" text;

                DO $BODY$
                BEGIN
                    IF EXISTS (
                        SELECT 1 FROM information_schema.columns 
                        WHERE table_name = 'ExternalUsers' AND column_name = 'Email' AND data_type = 'bigint'
                    ) THEN
                        ALTER TABLE ""ExternalUsers"" ALTER COLUMN ""Email"" TYPE text USING ""Email""::text;
                    END IF;
                    IF EXISTS (
                        SELECT 1 FROM information_schema.columns 
                        WHERE table_name = 'ExternalUsers' AND column_name = 'FullName' AND data_type = 'bigint'
                    ) THEN
                        ALTER TABLE ""ExternalUsers"" ALTER COLUMN ""FullName"" TYPE text USING ""FullName""::text;
                    END IF;
                    IF EXISTS (
                        SELECT 1 FROM information_schema.columns 
                        WHERE table_name = 'ExternalUsers' AND column_name = 'Phone' AND data_type = 'bigint'
                    ) THEN
                        ALTER TABLE ""ExternalUsers"" ALTER COLUMN ""Phone"" TYPE text USING ""Phone""::text;
                    END IF;
                END $BODY$;
            ");
        }
        catch (Exception ex)
        {
            Log.Warning("Schema column check: {Message}", ex.Message);
        }
    }

    if (args.Contains("--seed") || args.Contains("-s"))
    {
        Log.Information("Running Database Seeder from CLI arguments...");
        using (var scope = app.Services.CreateScope())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<IDatabaseSeederService>();
            var seedResult = await seeder.SeedAllAsync();
            Log.Information("CLI Seeding Result: {Success} - {Message}", seedResult.Success, seedResult.Message);
        }
    }

    await app.RunAsync();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

    Log.Fatal(ex, ex.Message);
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

//public class Startup
//{
//    public Startup(IConfiguration configuration)
//    {
//        Configuration = configuration;
//    }

//    public IConfiguration Configuration { get; }

//    // This method gets called by the runtime. Use it to add services to the container.
//    public void ConfigureServices(IServiceCollection services)
//    {
//        services.AddControllers();

//        //services.AddCors(options => options.AddPolicy("localhost", e =>
//        //    {
//        //        e.AllowAnyOrigin()
//        //            .AllowAnyHeader()
//        //            .AllowAnyMethod();
//        //    }));

//        // Add Swagger generation
//        services.AddSwaggerGen(c =>
//        {
//            c.SwaggerDoc("v1", new OpenApiInfo
//            {
//                Title = "My API",
//                Version = "v1",
//                Description = "API Description",
//                Contact = new OpenApiContact { Name = "Your Name", Email = "your.email@example.com" }
//            });

//            //c.AddServer(new OpenApiServer { Url = "https://aspireapi-bvdqf3bmbph9g4ep.canadacentral-01.azurewebsites.net" });
//        });

//    }

//    // This method gets called by the runtime. Use it to configure the HTTP request pipeline.
//    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//    {
//        if (env.IsDevelopment())
//        {
//            app.UseDeveloperExceptionPage();
//        }

//        app.UseHttpsRedirection();
//        app.UseRouting();
//        app.UseAuthorization();

//        // Enable middleware to serve generated Swagger as a JSON endpoint.
//        app.UseSwagger();

//        // Enable middleware to serve Swagger-UI (HTML, JS, CSS, etc.)
//        app.UseSwaggerUI(c =>
//        {
//            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
//            c.RoutePrefix = "swagger"; // Optional: Set Swagger UI at the root
//        });

//        app.UseEndpoints(endpoints =>
//        {
//            endpoints.MapControllers();
//        });
//    }
//}
