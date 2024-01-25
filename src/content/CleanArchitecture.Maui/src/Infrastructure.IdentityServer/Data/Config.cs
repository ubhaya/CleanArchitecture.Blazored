using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace CleanArchitecture.Maui.Infrastructure.Data;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
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
                ClientName = "Clean Architecture Maui app",
                AllowedGrantTypes = GrantTypes.Code,
                
                RedirectUris = {"mobile://"},
                PostLogoutRedirectUris = {"mobile://"},
                
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = [ 
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "clean_architecture_maui_api" 
                ],
                
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                RefreshTokenExpiration = TokenExpiration.Sliding
            },
            new()
            {
                ClientId = "CleanArchitecture.Maui.MobileUi.Postman",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                AllowedScopes =
                [
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "clean_architecture_maui_api",
                ],
            },
        };
}
