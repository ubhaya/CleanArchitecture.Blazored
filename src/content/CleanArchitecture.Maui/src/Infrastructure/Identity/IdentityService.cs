using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Maui.Application.Common.Services.Identity;
using CleanArchitecture.Maui.MobileUi.Shared.AccessControl;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using CleanArchitecture.Maui.MobileUi.Shared.Common;

namespace CleanArchitecture.Maui.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    public Task<string> GetUserNameAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<string>> CreateUserAsync(string userName, string password)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteUserAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<IList<RoleDto>> GetRolesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRolePermissionsAsync(string roleId, Permissions permissions)
    {
        throw new NotImplementedException();
    }

    public Task<IList<UserDto>> GetUsersAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> GetUserAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAsync(UserDto updatedUser)
    {
        throw new NotImplementedException();
    }

    public Task CreateRoleAsync(RoleDto newRole)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRoleAsync(RoleDto updatedRole)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRoleAsync(string roleId)
    {
        throw new NotImplementedException();
    }
}
