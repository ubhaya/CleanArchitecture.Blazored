using CleanArchitecture.MudBlazored.WebUi.Client.Handlers.Interfaces;
using CleanArchitecture.MudBlazored.WebUi.Client.Handlers.ServerImplementation;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace CleanArchitecture.MudBlazored.WebUi.Client;

public static class AppServices
{
    public static WebAssemblyHostBuilder AddApplicationWebAssemblyServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddHttpClient("CleanArchitecture.Blazored.WebUi", client =>
            client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
        
        builder.Services.AddScoped(sp => 
            sp.GetRequiredService<IHttpClientFactory>().CreateClient("CleanArchitecture.Blazored.WebUi"));

        builder.Services.Scan(scan => scan
            .FromAssemblyOf<ITodoListsClient>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return builder;
    }
    
    public static IServiceCollection AddApplicationServerServices(this IServiceCollection services)
    {
        services.AddScoped<IUserHandler, UserServerHandler>();
        services.AddScoped<ITodoListHandler, TodoListServerHandler>();
        services.AddScoped<ITodoItemsHandler, TodoItemsServerHandler>();
        
        return services;
    }
}
