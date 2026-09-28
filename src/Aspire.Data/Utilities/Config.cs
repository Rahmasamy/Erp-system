// generic
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Aspire.Utilities;
/// <summary>


/// Version : 1.0.0
/// </summary>
public class Config
{
    public static IEnumerable<Client> Clients =>
        [
            new Client()
            {
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientId = "client",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "secretApi" },
            },
            new Client()
            {
                ClientId = "postman",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                RedirectUris = { "https://localhost:4200" },
                AllowedScopes =
                {
                    "secretApi",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },
                AllowAccessTokensViaBrowser = true,
                AllowOfflineAccess = true,
                RequireConsent = false
            },
            new Client()
            {
                ClientId = "angular",

                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,

                RedirectUris = { "http://localhost:4200" },
                PostLogoutRedirectUris = { "http://localhost:4200" },
                AllowedCorsOrigins = { "http://localhost:4200" },

                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    "secretApi",
                },

                AllowAccessTokensViaBrowser = true,
                RequireConsent = false,
            },
        ];

    public static IEnumerable<ApiScope> ApiScopes =>
        [
            new ApiScope(){Name = "secretApi"},
        ];

    public static IEnumerable<ApiResource> ApiResources =>
        [];
    public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        ];
}
