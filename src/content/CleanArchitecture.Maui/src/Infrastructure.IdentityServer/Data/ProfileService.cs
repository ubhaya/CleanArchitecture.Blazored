using System.Security.Claims;
using CleanArchitecture.Maui.Infrastructure.Identity;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Maui.Infrastructure.Data;

public class ProfileService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public ProfileService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var subjectId = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(subjectId);
        if (user is null)
            return;
        var userRolesName = await _userManager.GetRolesAsync(user);

        var userRoles = await _roleManager.Roles.Where(r =>
            userRolesName.Contains(r.Name ?? string.Empty)).ToListAsync();

        var userPermissions = Permissions.None;

        foreach (var role in userRoles)
            userPermissions |= role.Permissions;

        var permissionValue = (int)userPermissions;
        
        context.IssuedClaims.Add(new Claim(CustomClaimTypes.Permissions,permissionValue.ToString()));
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
       return Task.CompletedTask;
    }
}