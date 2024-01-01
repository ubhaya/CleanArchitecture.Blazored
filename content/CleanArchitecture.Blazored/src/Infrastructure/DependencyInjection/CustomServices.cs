using CleanArchitecture.Blazored.Application.Common.Services.DateTime;
using CleanArchitecture.Blazored.Application.Common.Services.Identity;
using CleanArchitecture.Blazored.Infrastructure.DateTime;
using CleanArchitecture.Blazored.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Blazored.Infrastructure.DependencyInjection;

public sealed class CustomServices : IServiceInstaller
{
    public void InstallerService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
    }
}