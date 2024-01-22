using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new("clean_architecture_maui_api", "CleanArchitecture.Maui.Api"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new()
            {
                ClientId = "CleanArchitecture.Maui.MobileUi",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "clean_architecture_maui_api" }
            }
        };
}