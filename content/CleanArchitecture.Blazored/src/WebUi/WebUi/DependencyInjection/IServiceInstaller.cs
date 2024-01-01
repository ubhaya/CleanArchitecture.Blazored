namespace CleanArchitecture.Blazored.WebUi.DependencyInjection;

public interface IServiceInstaller
{
    void InstallerService(IServiceCollection services, IConfiguration configuration);
}