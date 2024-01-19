using CleanArchitecture.Blazored.Infrastructure.Data;

namespace CleanArchitecture.Blazored.WebUi.DependencyInjection;

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