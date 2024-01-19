using CleanArchitecture.Maui.Application.Common.Services.Identity;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using CleanArchitecture.Maui.MobileUi.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.Maui.MobileUi.WebApi.DependencyInjection;

public class AuthenticationServices : IServiceInstaller, IMiddlewareInstaller
{
    public void InstallerService(IServiceCollection services, IConfiguration configuration)
    {

        services.AddCors();
        
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, FlexibleAuthorizationPolicyProvider>();

        services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();
        services.AddAuthorization();
        
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();
    }

    public void InstallMiddleWare(WebApplication app)
    {
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
    }
}