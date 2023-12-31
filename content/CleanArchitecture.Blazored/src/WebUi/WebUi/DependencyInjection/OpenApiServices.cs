using NSwag;
using NSwag.Generation.Processors.Security;

namespace CleanArchitecture.Blazored.WebUi.DependencyInjection;

public class OpenApiServices : IServiceInstaller, IMiddlewareInstaller
{
    public void InstallerService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApiDocument(configure =>
        {
            configure.Title = "CleanArchitecture Api";
            configure.AddSecurity("basic", Enumerable.Empty<string>(),
                new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.Basic,
                    Name = ".AspNetCore.Identity.Application",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });
            configure.OperationProcessors.Add(
                new AspNetCoreOperationSecurityScopeProcessor("basic"));
        });
    }

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