using CleanArchitecture.Maui.Infrastructure.Data;
using CleanArchitecture.Maui.MobileUi.WebApi.DependencyInjection;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.RegisterCleanArchitectureServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
        await initializer.InitializeAsync();
        await initializer.SeedAsync();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex,"An error occurred during database initialisation.");
    }
}

// Configure the HTTP request pipeline.
app.UseCleanArchitectureMiddleware();

await app.RunAsync();