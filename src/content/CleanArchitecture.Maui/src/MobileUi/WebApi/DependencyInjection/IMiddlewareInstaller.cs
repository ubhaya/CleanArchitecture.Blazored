namespace CleanArchitecture.Maui.MobileUi.WebApi.DependencyInjection;

public interface IMiddlewareInstaller
{
    int MiddleWareOrder { get; }
    void InstallMiddleWare(WebApplication app);
}