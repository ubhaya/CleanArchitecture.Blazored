namespace CleanArchitecture.Maui.MobileUi.WebApi.DependencyInjection;

public interface IServiceInstaller
{
    void InstallerService(IServiceCollection services, IConfiguration configuration);
}