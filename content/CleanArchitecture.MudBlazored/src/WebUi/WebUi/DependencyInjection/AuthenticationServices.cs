using CleanArchitecture.MudBlazored.Application.Common.Services.Identity;
using CleanArchitecture.MudBlazored.WebUi.Components.Account;
using CleanArchitecture.MudBlazored.WebUi.Services;
using CleanArchitecture.MudBlazored.WebUi.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.MudBlazored.WebUi.DependencyInjection;

public class AuthenticationServices : IServiceInstaller
{
    public void InstallerService(IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddCascadingAuthenticationState();
        services.AddScoped<IdentityUserAccessor>();
        services.AddScoped<IdentityRedirectManager>();
        services.AddScoped<AuthenticationStateProvider, PersistingServerAuthenticationStateProvider>();
        
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, FlexibleAuthorizationPolicyProvider>();

        services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();
        
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();
    }
}