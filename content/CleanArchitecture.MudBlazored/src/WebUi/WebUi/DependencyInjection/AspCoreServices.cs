using CleanArchitecture.MudBlazored.WebUi.Client.Pages;
using CleanArchitecture.MudBlazored.WebUi.Components;

namespace CleanArchitecture.MudBlazored.WebUi.DependencyInjection;

public sealed class AspCoreServices : IServiceInstaller, IMiddlewareInstaller
{
    public void InstallerService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();
        
        services.AddDatabaseDeveloperPageExceptionFilter();
        
        services.AddControllers();
    }

    public void InstallMiddleWare(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
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
        
        
        app.MapControllers();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Counter).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
        app.MapAdditionalIdentityEndpoints();
    }
}