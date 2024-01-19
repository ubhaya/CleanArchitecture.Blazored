using CleanArchitecture.Maui.Infrastructure.Data;
using CleanArchitecture.Maui.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Maui.Infrastructure.DependencyInjection;

public sealed class AuthenticationServices : IServiceInstaller
{
    public void InstallerService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityApiEndpoints<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>();
    }
}