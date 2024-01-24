using CleanArchitecture.Maui.Application.Common.Services.Identity;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using CleanArchitecture.Maui.MobileUi.WebApi.Options;
using CleanArchitecture.Maui.MobileUi.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Maui.MobileUi.WebApi.DependencyInjection;

public sealed class AspCoreServices : IServiceInstaller, IMiddlewareInstaller
{
    public void InstallerService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddAntiforgery();
        
        var oidcSettings = new OidcSettings();
        configuration.GetRequiredSection(nameof(OidcSettings)).Bind(oidcSettings);
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = oidcSettings.Authority;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateLifetime = false,
                };
            });

        services.AddAuthorizationBuilder()
            .AddPolicy("api_scope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", oidcSettings.RequiredScope??Enumerable.Empty<string>());
            });
     
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, FlexibleAuthorizationPolicyProvider>();
        
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();
    }

    public void InstallMiddleWare(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();
        
        app.UseAuthentication();
        app.UseAuthorization();
    }
}