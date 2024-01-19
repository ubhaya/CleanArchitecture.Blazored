using FastEndpoints;

namespace CleanArchitecture.Maui.MobileUi.WebApi.DependencyInjection;

public class CustomServices : IServiceInstaller, IMiddlewareInstaller
{
    public void InstallerService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddFastEndpoints();
    }

    public void InstallMiddleWare(WebApplication app)
    {
        app.UseFastEndpoints();
    }
}