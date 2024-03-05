using CleanArchitecture.Maui.MobileUi.Mobile.Options;
using IdentityModel.OidcClient;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Maui.MobileUi.Mobile.DependencyInjection;

public static partial class DependencyInjectionHelper
{
    private static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(Connectivity.Current);
        services.AddSingleton(SecureStorage.Default);

        services.AddTransient<WebAuthenticatorBrowser>();
        services.AddTransient(sp =>
        {
            var oidcClientSettings =
                configuration.GetRequiredSection(nameof(OidcClientSettings)).Get<OidcClientSettings>();

            if (oidcClientSettings == null) throw new NullReferenceException(nameof(oidcClientSettings));

            return new OidcClient(new OidcClientOptions
            {
                Authority = oidcClientSettings.Authority,
                ClientId = oidcClientSettings.ClientId,
                Scope = string.Join(" ", oidcClientSettings.Scope ?? Enumerable.Empty<string>()),
                RedirectUri = oidcClientSettings.CallBackSchema,
                PostLogoutRedirectUri = oidcClientSettings.CallBackSchema,
                ClientSecret = oidcClientSettings.ClientSecret,
                Browser = sp.GetRequiredService<WebAuthenticatorBrowser>()
            });
        });
    }
}