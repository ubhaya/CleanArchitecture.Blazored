namespace CleanArchitecture.MudBlazored.WebUi.DependencyInjection;

public interface IMiddlewareInstaller
{
    void InstallMiddleWare(WebApplication app);
}