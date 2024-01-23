using CleanArchitecture.Maui.Application.Common.Services.Identity;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using CleanArchitecture.Maui.MobileUi.WebApi.Options;
using CleanArchitecture.Maui.MobileUi.WebApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Maui.MobileUi.WebApi.DependencyInjection;

public class AuthenticationServices : IServiceInstaller, IMiddlewareInstaller
{
    public void InstallerService(IServiceCollection services, IConfiguration configuration)
    {

        var oidcSettings = new OidcSettings();
        configuration.GetRequiredSection(nameof(OidcSettings)).Bind(oidcSettings);
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = oidcSettings.Authority;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

        services.AddAuthorizationBuilder()
            .AddPolicy("api_scope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", oidcSettings.RequiredScope??Enumerable.Empty<string>());
            });
        
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();
    }

    public void InstallMiddleWare(WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}