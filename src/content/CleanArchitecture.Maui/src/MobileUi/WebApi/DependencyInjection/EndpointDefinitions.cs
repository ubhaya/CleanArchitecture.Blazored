using CleanArchitecture.Maui.MobileUi.WebApi.Endpoints;

namespace CleanArchitecture.Maui.MobileUi.WebApi.DependencyInjection;

public class EndpointDefinitions : IServiceInstaller, IMiddlewareInstaller
{
    public void InstallerService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointDefinitions(typeof(WeatherForecastEndpoint));
    }

    public void InstallMiddleWare(WebApplication app)
    {
        app.UseEndpointDefinition();
    }
}

internal static class EndpointDefinitionsExtension
{
    public static void AddEndpointDefinitions(this IServiceCollection services, params Type[] scanMakers)
    {
        var endpointDefinitions = new List<IEndpointsDefinition>();

        foreach (var maker in scanMakers)
        {
            endpointDefinitions.AddRange(
                maker.Assembly.ExportedTypes
                    .Where(x=>typeof(IEndpointsDefinition).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                    .Select(Activator.CreateInstance).Cast<IEndpointsDefinition>()
                );
        }

        services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IEndpointsDefinition>);
    }

    public static void UseEndpointDefinition(this WebApplication app)
    {
        var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointsDefinition>>();

        foreach (var endpointsDefinition in definitions)
        {
            endpointsDefinition.DefineEndpoints(app);
        }
    }
}