using CleanArchitecture.Maui.Infrastructure.Data;
using CleanArchitecture.Maui.MobileUi.WebApi.DependencyInjection;
using CleanArchitecture.Maui.MobileUi.WebApi.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var oidcSettings = new OidcSettings();
builder.Configuration.GetRequiredSection(nameof(OidcSettings)).Bind(oidcSettings);
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = oidcSettings.Authority;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("api_scope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", oidcSettings.RequiredScope??Enumerable.Empty<string>());
    });

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

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.UseAuthentication();

app.UseAuthorization();


await app.RunAsync();