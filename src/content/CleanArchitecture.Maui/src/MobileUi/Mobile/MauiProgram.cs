using System.Reflection;
using Ardalis.GuardClauses;
using CleanArchitecture.Maui.MobileUi.Client;
using CleanArchitecture.Maui.MobileUi.Mobile.Helpers;
using CleanArchitecture.Maui.MobileUi.Mobile.Options;
using CleanArchitecture.Maui.MobileUi.Mobile.ViewModels;
using CleanArchitecture.Maui.MobileUi.Mobile.ViewModels.Authentication;
using CleanArchitecture.Maui.MobileUi.Mobile.Views;
using CleanArchitecture.Maui.MobileUi.Mobile.Views.Authentication;
using CommunityToolkit.Maui;
using epj.RouteGenerator;
using IdentityModel.OidcClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Maui.MobileUi.Mobile;

[AutoRoutes("Page")]
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("CleanArchitecture.Maui.MobileUi.Mobile.appsettings.json");
#if DEBUG
        using var developmentStream = assembly.GetManifestResourceStream("CleanArchitecture.Maui.MobileUi.Mobile.appsettings.Development.json");
#endif

        Guard.Against.Null(stream);
#if DEBUG
        Guard.Against.Null(developmentStream);
#endif

        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
#if DEBUG
            .AddJsonStream(developmentStream)
#endif
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
        services.AddSingleton<AppShellViewModel>();
        services.AddSingleton<LoginPage, LoginPageViewModel>();
        services.AddSingleton<MainPage, MainPageViewModel>();
        services.AddSingleton<WeatherPage, WeatherForecastViewModel>();
        
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
        var baseUrl = builder.Configuration.GetValue<string>("BaseUrl");
        var basePort = new BasePort();
        builder.Configuration.GetRequiredSection(nameof(BasePort)).Bind(basePort);

        var baseAddress = string.IsNullOrWhiteSpace(baseUrl)
            ? DeviceInfo.Platform == DevicePlatform.Android
                ? $"https://10.0.2.2:{basePort.Https}"
                : $"https://localhost:{basePort.Https}"
            : baseUrl;

        builder.Services.AddSingleton<HttpsClientHandlerService>();
        builder.Services.AddSingleton<AccessTokenMessageHandler>();
        builder.Services.AddHttpClient("CleanArchitecture.Maui.MobileUi", configureClient: client =>
        {
            client.BaseAddress = new Uri(baseAddress);
        })
        .ConfigurePrimaryHttpMessageHandler(sp =>
        {
            var handlerService = sp.GetRequiredService<HttpsClientHandlerService>();
            return handlerService.GetPlatformMessageHandler();
        })
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