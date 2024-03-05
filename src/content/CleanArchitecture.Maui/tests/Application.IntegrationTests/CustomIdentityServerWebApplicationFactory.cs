using CleanArchitecture.Maui.Application.Common.Services.Identity;
using CleanArchitecture.Maui.Infrastructure.Data;
using CleanArchitecture.Maui.MobileUi.IdentityServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Maui.Application.IntegrationTests;

using static Testing;

internal class CustomIdentityServerWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.IdentityServer.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });

        builder.ConfigureServices((webBuilder, services) =>
        {
            services
                .Remove<ICurrentUser>()
                .AddTransient(_ => Mock.Of<ICurrentUser>(s =>
                    s.UserId == GetCurrentUserId()));

            services
                .Remove<DbContextOptions<IdentityServerDbContext>>()
                .AddDbContext<IdentityServerDbContext>(options =>
                {
                    options.UseSqlServer(webBuilder.Configuration.GetConnectionString("IdentityServer"),
                        sqlBuilder => sqlBuilder.MigrationsAssembly(typeof(IdentityServerDbContext).Assembly.FullName));
                });
        });
    }
}