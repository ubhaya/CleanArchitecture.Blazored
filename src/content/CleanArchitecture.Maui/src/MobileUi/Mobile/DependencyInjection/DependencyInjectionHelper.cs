using CleanArchitecture.Maui.MobileUi.Client;
using CleanArchitecture.Maui.MobileUi.Mobile.Helpers;
using CleanArchitecture.Maui.MobileUi.Mobile.Options;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Maui.MobileUi.Mobile.DependencyInjection;

public static partial class DependencyInjectionHelper
{
    public static void InstallDependencyServices(MauiAppBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddViewsAndViewModels();

        services.AddServices(configuration);

        services.AddHttpClient(configuration);
    }
}