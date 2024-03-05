using Microsoft.Extensions.Configuration;
using CleanArchitecture.Blazored.Infrastructure.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var installerType = typeof(IServiceInstaller);

        var installers = installerType
            .Assembly.ExportedTypes
            .Where(t =>
            {
                var isAssignable = installerType.IsAssignableFrom(t);
                var concreteType = t is { IsInterface: false, IsAbstract: false };
                return isAssignable && concreteType;
            })
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>()
            .OrderBy(x=>x.Order);
        foreach (var installer in installers)
        {
            installer.InstallerService(services, configuration);
        }

        return services;
    }
}