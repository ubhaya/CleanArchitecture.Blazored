using static System.Int32;

namespace CleanArchitecture.Maui.MobileUi.WebApi.DependencyInjection;

public class CustomServices : IServiceInstaller, IMiddlewareInstaller
{
    public int ServiceOrder => MaxValue;

    public void InstallerService(IServiceCollection services, IConfiguration configuration)
    {
    }

    public int MiddleWareOrder => MaxValue;

    public void InstallMiddleWare(WebApplication app)
    {
    }
}