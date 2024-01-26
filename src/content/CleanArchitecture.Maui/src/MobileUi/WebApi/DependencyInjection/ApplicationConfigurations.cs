namespace CleanArchitecture.Maui.MobileUi.WebApi.DependencyInjection;

public static class ApplicationConfigurations
{
    public static void RegisterCleanArchitectureServices(this WebApplicationBuilder builder)
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
            .OrderBy(installer => installer.ServiceOrder);
        
        foreach (var installer in installers)
        {
            installer.InstallerService(builder.Services,builder.Configuration);
        }
    }

    public static void UseCleanArchitectureMiddleware(this WebApplication app)
    {
        var installerType = typeof(IMiddlewareInstaller);

        var installers = installerType
            .Assembly.ExportedTypes
            .Where(t =>
            {
                var isAssignable = installerType.IsAssignableFrom(t);
                var concreteType = t is { IsInterface: false, IsAbstract: false };
                return isAssignable && concreteType;
            })
            .Select(Activator.CreateInstance)
            .Cast<IMiddlewareInstaller>()
            .OrderBy(installer => installer.MiddleWareOrder);
        
        foreach (var installer in installers)
        {
            installer.InstallMiddleWare(app);
        }
    }
}