using CleanArchitecture.Blazored.Infrastructure.Identity;
using CleanArchitecture.Blazored.WebUi.Client;
using CleanArchitecture.Blazored.WebUi.Components.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Blazored.WebUi.DependencyInjection;

public class CustomServices : IServiceInstaller
{
    public void InstallerService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();


        services.AddApplicationServerServices();
    }
}