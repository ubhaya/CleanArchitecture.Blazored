using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Blazored.Infrastructure.DependencyInjection;

public interface IServiceInstaller
{
    int Order { get; }
    void InstallerService(IServiceCollection services, IConfiguration configuration);
}