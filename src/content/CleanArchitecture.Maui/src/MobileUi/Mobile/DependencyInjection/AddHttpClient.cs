using CleanArchitecture.Maui.MobileUi.Client;
using CleanArchitecture.Maui.MobileUi.Mobile.Helpers;
using CleanArchitecture.Maui.MobileUi.Mobile.Options;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Maui.MobileUi.Mobile.DependencyInjection;

public static partial class DependencyInjectionHelper
{
    private static void AddHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        var baseUrl = configuration.GetValue<string>("BaseUrl");
        var basePort = new BasePort();
        configuration.GetRequiredSection(nameof(BasePort)).Bind(basePort);

        var baseAddress = GetBaseAddress(baseUrl, basePort);

        services.AddSingleton<HttpsClientHandlerService>();
        services.AddSingleton<AccessTokenMessageHandler>();
        
        services.AddHttpClient("CleanArchitecture.Maui.MobileUi",
                configureClient: client => { client.BaseAddress = new Uri(baseAddress); })
            .ConfigurePrimaryHttpMessageHandler(sp =>
            {
                var handlerService = sp.GetRequiredService<HttpsClientHandlerService>();
                return handlerService.GetPlatformMessageHandler();
            })
            .AddHttpMessageHandler<AccessTokenMessageHandler>();

        services.AddScoped(sp =>
            sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("CleanArchitecture.Maui.MobileUi"));

        services.Scan(scan => scan
            .FromAssemblyOf<IWeatherForecastClient>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    private static string GetBaseAddress(string? baseUrl, BasePort basePort)
    {
        return string.IsNullOrWhiteSpace(baseUrl)
            ? DeviceInfo.Platform == DevicePlatform.Android
                ? $"https://10.0.2.2:{basePort.Https}"
                : $"https://localhost:{basePort.Https}"
            : baseUrl;
    }
}