using CleanArchitecture.Maui.Infrastructure.Identity;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Maui.Infrastructure.Data;

public class IdentityServerDbContextInitializer
{
    private readonly IdentityServerDbContext _context;
    private readonly PersistedGrantDbContext _persistedGrantDbContext;
    private readonly ConfigurationDbContext _configurationDbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    private const string? AdministratorsRole = "Administrators";
    private const string? AccountsRole = "Accounts";
    private const string? OperationsRole = "Operations";

    private const string DefaultPassword = "Password123!";

    public IdentityServerDbContextInitializer(IdentityServerDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager, PersistedGrantDbContext persistedGrantDbContext, ConfigurationDbContext configurationDbContext)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _persistedGrantDbContext = persistedGrantDbContext;
        _configurationDbContext = configurationDbContext;
    }
    
    public async Task InitializeAsync()
    {
        await InitialiseWithMigrationsAsync();
    }

    public async Task SeedAsync()
    {
        await SeedIdentityAsync();
        await SeedDataAsync();
    }

    private async Task InitialiseWithDropCreateAsync()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();
    }
    
    private async Task InitialiseWithMigrationsAsync()
    {
        if (_context.Database.IsSqlServer())
        {
            await _context.Database.MigrateAsync();
        }
        else
        {
            await _context.Database.EnsureCreatedAsync();
        }
    }

    private async Task SeedIdentityAsync()
    {
        await CreateRole(AdministratorsRole, Permissions.All);
        await CreateRole(AccountsRole, Permissions.ViewUsers | Permissions.Counter);
        await CreateRole(OperationsRole, Permissions.ViewUsers | Permissions.Forecast);

        await CreateUser("admin@localhost", new[] { AdministratorsRole });
        await CreateUser("auditor@localost", new[] { AccountsRole, OperationsRole });

        await _context.SaveChangesAsync();
    }
    
    private async Task CreateRole(string? roleName, Permissions permissions)
    {
        await _roleManager.CreateAsync(
            new ApplicationRole { Name = roleName, NormalizedName = roleName?.ToUpper(), Permissions = permissions });
    }

    private async Task CreateUser(string userName, IEnumerable<string?>? roles = null)
    {
        var user = new ApplicationUser { UserName = userName, Email = userName };

        await _userManager.CreateAsync(user, DefaultPassword);

        user = await _userManager.FindByNameAsync(userName);

        foreach (var role in roles?? Enumerable.Empty<string>())
        {
            if (!string.IsNullOrEmpty(role))
                await _userManager.AddToRoleAsync(user!, role);
        }
            
    }

    private async Task SeedDataAsync()
    {
        await _persistedGrantDbContext.Database.MigrateAsync();

        await _configurationDbContext.Database.MigrateAsync();
        
        if (!_configurationDbContext.Clients.Any())
        {
            foreach (var client in Config.Clients)
            {
                _configurationDbContext.Clients.Add(client.ToEntity());
            }
            await _configurationDbContext.SaveChangesAsync();
        }

        if (!_configurationDbContext.IdentityResources.Any())
        {
            foreach (var resource in Config.IdentityResources)
            {
                _configurationDbContext.IdentityResources.Add(resource.ToEntity());
            }
            await _configurationDbContext.SaveChangesAsync();
        }

        if (!_configurationDbContext.ApiScopes.Any())
        {
            foreach (var resource in Config.ApiScopes)
            {
                _configurationDbContext.ApiScopes.Add(resource.ToEntity());
            }
            await _configurationDbContext.SaveChangesAsync();
        }
    }
}