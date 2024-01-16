using CleanArchitecture.MudBlazored.Application.AccessControl.Commands;
using CleanArchitecture.MudBlazored.Application.AccessControl.Queries;
using CleanArchitecture.MudBlazored.WebUi.Shared.AccessControl;
using CleanArchitecture.MudBlazored.WebUi.Shared.Authorization;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace CleanArchitecture.MudBlazored.WebUi.Components.Pages.Admin.AccessControl;

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
