using System.Reflection;
using CleanArchitecture.Maui.MobileUi.Client;
using CleanArchitecture.Maui.MobileUi.Mobile.Helpers;
using CleanArchitecture.Maui.MobileUi.Mobile.Options;
using CommunityToolkit.Maui;
using IdentityModel.OidcClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Maui.MobileUi.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("CleanArchitecture.Maui.MobileUi.Mobile.appsettings.json");

        if (stream is null) throw new NullReferenceException(nameof(stream));

        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();
        
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Configuration.AddConfiguration(config);
        InstallDependencyServices(builder);
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static void InstallDependencyServices(MauiAppBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddSingleton<AppShell>();
        services.AddSingleton<LoginPage>();
        services.AddSingleton<MainPage>();
        
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
        
        AddHttpClient(builder);
    }

    private static void AddHttpClient(MauiAppBuilder builder)
    {
        var baseAddress = builder.Configuration.GetValue<string>("BaseAddress") ??
                          throw new NullReferenceException("Base Address Cannot be null");
        builder.Services.AddSingleton<AccessTokenMessageHandler>();
        builder.Services.AddHttpClient("CleanArchitecture.Maui.MobileUi", configureClient: client =>
                client.BaseAddress = new Uri(baseAddress))
            .AddHttpMessageHandler<AccessTokenMessageHandler>();

        builder.Services.AddScoped(sp =>
            sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("CleanArchitecture.Maui.MobileUi"));

        builder.Services.Scan(scan => scan
            .FromAssemblyOf<IClient>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}