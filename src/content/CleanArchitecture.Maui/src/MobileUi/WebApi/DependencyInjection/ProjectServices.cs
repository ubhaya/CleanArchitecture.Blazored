namespace CleanArchitecture.Maui.MobileUi.WebApi.DependencyInjection;

public class ProjectServices : IServiceInstaller
{
    public int ServiceOrder => 3;

    public void InstallerService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationService();
        services.AddInfrastructureServices(configuration);
    }
}