using System.Reflection;
using Ardalis.GuardClauses;
using CleanArchitecture.Maui.MobileUi.Mobile.DependencyInjection;
using CleanArchitecture.Maui.MobileUi.Mobile.Helpers.FontAwesome;
using CommunityToolkit.Maui;
using epj.RouteGenerator;
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
                fonts.AddFont("Font Awesome 6 Brands-Regular-400.otf", nameof(FaBrands));
                fonts.AddFont("Font Awesome 6 Free-Regular-400.otf", nameof(FaRegular));
                fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", nameof(FaSolid));
            });

        builder.Configuration.AddConfiguration(config);
        DependencyInjectionHelper.InstallDependencyServices(builder);
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}