using CleanArchitecture.Maui.MobileUi.Mobile.ViewModels;
using CleanArchitecture.Maui.MobileUi.Mobile.ViewModels.Authentication;
using CleanArchitecture.Maui.MobileUi.Mobile.Views;
using CleanArchitecture.Maui.MobileUi.Mobile.Views.Authentication;
using CommunityToolkit.Maui;

namespace CleanArchitecture.Maui.MobileUi.Mobile.DependencyInjection;

public static partial class DependencyInjectionHelper
{
    private static IServiceCollection AddViewsAndViewModels(this IServiceCollection services)
    {
        services.AddSingleton<AppShell>();
        services.AddSingleton<AppShellViewModel>();
        services.AddSingleton<LoginPage, LoginPageViewModel>();
        services.AddSingleton<MainPage, MainPageViewModel>();
        services.AddSingleton<WeatherPage, WeatherForecastViewModel>();

        return services;
    }
}