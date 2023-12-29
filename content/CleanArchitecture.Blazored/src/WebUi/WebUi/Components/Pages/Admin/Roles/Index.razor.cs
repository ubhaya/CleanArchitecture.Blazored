using MediatR;
using Microsoft.AspNetCore.Components;
using CleanArchitecture.Blazored.Application.Roles.Commands;
using CleanArchitecture.Blazored.Application.Roles.Queries;
using CleanArchitecture.Blazored.WebUi.Shared.AccessControl;
using CleanArchitecture.Blazored.WebUi.Shared.Authorization;

namespace CleanArchitecture.Blazored.WebUi.Components.Pages.Admin.Roles;

public partial class Index
{
    [Inject] private IMediator Mediator { get; set; } = default!;
    
    private RolesVm? Model { get; set; }

    private string _newRoleName = string.Empty;

    private RoleDto? _roleToEdit;

    protected override async Task OnInitializedAsync()
    {
        Model = await Mediator.Send(new GetRolesQuery());
    }

    private async Task AddRole()
    {
        if (!string.IsNullOrWhiteSpace(_newRoleName))
        {
            var newRole = new RoleDto(Guid.NewGuid().ToString(), _newRoleName, Permissions.None);

            await Mediator.Send(new CreateRoleCommand(newRole));

            Model!.Roles.Add(newRole);
        }

        _newRoleName = string.Empty;
    }

    private void EditRole(RoleDto role)
    {
        _roleToEdit = role;
    }

    private void CancelEditRole()
    {
        _roleToEdit = null;
    }

    private async Task UpdateRole()
    {
        if (_roleToEdit is not null)
        {
            await Mediator.Send(new UpdateRoleCommand(_roleToEdit));
        }

        _roleToEdit = null;
    }

    private async Task DeleteRole(RoleDto role)
    {
        await Mediator.Send(new DeleteRoleCommand(role.Id));
        Model!.Roles.Remove(role);
    }
}
