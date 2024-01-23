using CleanArchitecture.Maui.Infrastructure.Data;

namespace CleanArchitecture.Maui.MobileUi.WebApi.DependencyInjection;

public class HealthServices : IServiceInstaller, IMiddlewareInstaller
{
    public void InstallerService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();
    }

    public void InstallMiddleWare(WebApplication app)
    {
        app.MapHealthChecks("/health");
    }
}