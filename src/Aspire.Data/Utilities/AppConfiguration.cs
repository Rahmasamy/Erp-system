using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Aspire.Utilities;

public static class SecretManager
{
    public static IConfiguration StaticConfig { get; set; }
    public static void ConfigureAppSecrets()
    {
        AppConfiguration.GOOGLE_CLIENTID = Environment.GetEnvironmentVariable("GOOGLE_CLIENTID");
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower();
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json",
                             optional: true);

        bool isDevelopment = environment == Environments.Development.ToLower();

        if (isDevelopment)
        {
            _ = configurationBuilder.AddUserSecrets(Assembly.GetCallingAssembly());
        }

        StaticConfig = configurationBuilder.Build();

        if (isDevelopment)
        {
            EnvironmentVariableConfiguration.SetEnvironmentVariables(StaticConfig);
        }
    }
}

public static class AppConfiguration
{
    //public const string TenantId = "tenant_id";
    //public const string UserId = "user_id";
    //public const string RoleId = "role_id";

    public static string API_BASE_URL = "";

    public const string FRONTENDURLValue = "FRONTENDURL";
    public const string TOKENEXPIRATIONValue = "TOKENEXPIRATION";
    public const string OTPEXPIRATIONValue = "OTPEXPIRATION";
    public const string INVITEUSEREXPIRATIONValue = "INVITEUSEREXPIRATION";
    public const string DefaultPassword = "DefaultPassword@123456";

    public static string? FRONTENDURL { get; set; }
    public static string TOKENEXPIRATION { get; set; }
    public static string OTPEXPIRATION { get; set; }
    public static string INVITEUSEREXPIRATION { get; set; }
    public static string ConnectionString { get; set; }

    public static string BlobStorageKey { get; set; }
    public static string BlobStorageSecret { get; set; }

    public static string DefaultTenant { get; set; }
    #region JWT
    public static string JWT_KEY { get; set; }
    public static string JWT_ISSUER { get; set; }
    public static string JWT_AUDIENCE { get; set; }
    #endregion

    #region Google
    public static string GOOGLE_CLIENTID { get; set; }
    public static string GOOGLE_CLIENTSECRET { get; set; }
    #endregion

    #region Apple
    public static string APPLE_CLIENTID { get; set; }
    public static string APPLE_CLIENTSECRET { get; set; }
    public static string APPLE_ISSUER { get; set; }
    public static string APPLE_PUBLICKEYSURL { get; set; }
    #endregion

    #region Mandrill
    public static string MANDRILL_APIKEY { get; set; }
    public static string MANDRILL_FROMEMAIL { get; set; }
    public static string MANDRILL_FROMNAME { get; set; }
    public static string MANDRILL_SIGNINDOMAIN { get; set; }
    #endregion


    public static void SetAppConfigurationValues()
    {
        AppConfiguration.API_BASE_URL = "http://localhost:5000/";
        //AppConfiguration.API_BASE_URL = "https://api.drixaldocs.com/";

     

        AppConfiguration.JWT_ISSUER = "https://api.aspire-gateway.ws"; //"https://api.tradify.ws";
        AppConfiguration.JWT_AUDIENCE = "https://web.aspire-gateway.ws"; //"https://beta.tradify.ws";

        AppConfiguration.BlobStorageKey = (Environment.GetEnvironmentVariable("BlobStorageKey") ?? String.Empty).Trim();
        AppConfiguration.BlobStorageSecret = (Environment.GetEnvironmentVariable("BlobStorageSecret") ?? String.Empty).Trim();

        AppConfiguration.DefaultTenant = "1";

        AppConfiguration.FRONTENDURL = Environment.GetEnvironmentVariable(AppConfiguration.FRONTENDURLValue);
        AppConfiguration.OTPEXPIRATION = "5";
        AppConfiguration.INVITEUSEREXPIRATION = "1";
        AppConfiguration.TOKENEXPIRATION = "1"; // Set token expiration to 1 day
    }
}

public static class EnvironmentVariableConfiguration
{
    public static void SetEnvironmentVariables(IConfiguration StaticConfig)
    {
        Environment.SetEnvironmentVariable("JWT_ISSUER", StaticConfig["JWT_ISSUER"]);
        Environment.SetEnvironmentVariable("JWT_AUDIENCE", StaticConfig["JWT_AUDIENCE"]);
        Environment.SetEnvironmentVariable("JWT_KEY", StaticConfig["JWT_KEY"]);
        Environment.SetEnvironmentVariable("ASPIRECONSTR", StaticConfig["ASPIRECONSTR"]);
        Environment.SetEnvironmentVariable("MANDRILL_APIKEY", StaticConfig["MANDRILL_APIKEY"]);
        Environment.SetEnvironmentVariable("MANDRILL_FROMEMAIL", StaticConfig["MANDRILL_FROMEMAIL"]);
        Environment.SetEnvironmentVariable("MANDRILL_FROMNAME", StaticConfig["MANDRILL_FROMNAME"]);
        Environment.SetEnvironmentVariable("MANDRILL_SIGNINDOMAIN", StaticConfig["MANDRILL_SIGNINDOMAIN"]);
        Environment.SetEnvironmentVariable("GOOGLE_CLIENTID", StaticConfig["GOOGLE_CLIENTID"]);
        Environment.SetEnvironmentVariable("GOOGLE_CLIENTSECRET", StaticConfig["GOOGLE_CLIENTSECRET"]);
        Environment.SetEnvironmentVariable("APPLE_ISSUER", StaticConfig["APPLE_ISSUER"]);
        Environment.SetEnvironmentVariable("APPLE_PUBLICKEYSURL", StaticConfig["APPLE_PUBLICKEYSURL"]);
        Environment.SetEnvironmentVariable("APPLE_CLIENTID", StaticConfig["APPLE_CLIENTID"]);
        Environment.SetEnvironmentVariable("APPLE_CLIENTSECRET", StaticConfig["APPLE_CLIENTSECRET"]);
        Environment.SetEnvironmentVariable("FRONTENDURL", StaticConfig["FRONTENDURL"]);
        Environment.SetEnvironmentVariable("TOKENEXPIRATION", StaticConfig["TOKENEXPIRATION"]);
        Environment.SetEnvironmentVariable("OTPEXPIRATION", StaticConfig["OTPEXPIRATION"]);
        Environment.SetEnvironmentVariable("INVITEUSEREXPIRATION", StaticConfig["INVITEUSEREXPIRATION"]);
        Environment.SetEnvironmentVariable("BlobStorageKey", StaticConfig["BlobStorageKey"]);
        Environment.SetEnvironmentVariable("BlobStorageSecret", StaticConfig["BlobStorageSecret"]);
    }
}
