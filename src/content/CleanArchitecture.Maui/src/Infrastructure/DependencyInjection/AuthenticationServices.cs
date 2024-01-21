using System.IdentityModel.Tokens.Jwt;
using CleanArchitecture.Maui.Infrastructure.Data;
using CleanArchitecture.Maui.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Maui.Infrastructure.DependencyInjection;

public sealed class AuthenticationServices : IServiceInstaller
{
    public int Order => 2;

    public void InstallerService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultIdentity<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>();

        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
            {
                options.IdentityResources["openid"].UserClaims.Add("role");
                options.ApiResources.Single().UserClaims.Add("role");
                options.IdentityResources["openid"].UserClaims.Add("permissions");
                options.ApiResources.Single().UserClaims.Add("permissions");
            });
        
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");
    }
}