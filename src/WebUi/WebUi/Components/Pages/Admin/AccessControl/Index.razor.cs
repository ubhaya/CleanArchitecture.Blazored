using MediatR;
using Microsoft.AspNetCore.Components;
using CleanArchitecture.Blazored.Application.AccessControl.Commands;
using CleanArchitecture.Blazored.Application.AccessControl.Queries;
using CleanArchitecture.Blazored.WebUi.Shared.AccessControl;
using CleanArchitecture.Blazored.WebUi.Shared.Authorization;

namespace CleanArchitecture.Blazored.WebUi.Components.Pages.Admin.AccessControl;

public partial class Index
{
    [Inject] private IMediator Mediator { get; set; } = default!;
    
    private AccessControlVm? Model { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Model = await Mediator.Send(new GetAccessControl());
    }

    private async Task Set(RoleDto role, Permissions permission, bool granted)
    {
        role.Set(permission, granted);

        await Mediator.Send(new UpdateAccessControlCommand(role.Id, role.Permissions));
    }
}
