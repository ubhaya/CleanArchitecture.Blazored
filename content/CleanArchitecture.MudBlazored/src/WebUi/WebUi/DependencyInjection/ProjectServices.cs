namespace CleanArchitecture.MudBlazored.WebUi.DependencyInjection;

public class ProjectServices : IServiceInstaller
{
    public void InstallerService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationService();
        services.AddInfrastructureServices(configuration);
    }
}