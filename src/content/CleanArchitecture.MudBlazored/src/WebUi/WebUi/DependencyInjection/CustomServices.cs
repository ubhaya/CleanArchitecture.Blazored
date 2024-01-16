using CleanArchitecture.MudBlazored.Infrastructure.Identity;
using CleanArchitecture.MudBlazored.WebUi.Client;
using CleanArchitecture.MudBlazored.WebUi.Components.Account;
using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;

namespace CleanArchitecture.MudBlazored.WebUi.DependencyInjection;

public class CustomServices : IServiceInstaller
{
    public void InstallerService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
        services.AddMudServices();

        services.AddApplicationServerServices();
    }
}