using CleanArchitecture.Maui.MobileUi.WebApi.Options;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace CleanArchitecture.Maui.MobileUi.WebApi.DependencyInjection;

public class OpenApiServices : IServiceInstaller, IMiddlewareInstaller
{
    public int ServiceOrder => 3;

    public void InstallerService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApiDocument(configure =>
        {
            configure.Title = "CleanArchitecture API";
            configure.AddSecurity("JWT", Enumerable.Empty<string>(),
                new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });
        
            configure.OperationProcessors.Add(
                new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });
    }

    public int MiddleWareOrder => 3;

    public void InstallMiddleWare(WebApplication app)
    {
        app.UseOpenApi();
        app.UseSwaggerUi();

        app.UseReDoc(configure =>
        {
            configure.Path = "/redoc";
            configure.DocumentPath = "/api/v1/openapi.json";
        });
    }
}