namespace CleanArchitecture.Maui.MobileUi.WebApi.DependencyInjection;

public interface IServiceInstaller
{
    int ServiceOrder { get; }
    void InstallerService(IServiceCollection services, IConfiguration configuration);
}