using CleanArchitecture.MudBlazored.Application.Users.Queries;
using CleanArchitecture.MudBlazored.WebUi.Shared.AccessControl;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace CleanArchitecture.MudBlazored.WebUi.Components.Pages.Admin.Users;

public partial class Index
{
    [Inject] private IMediator Mediator { get; set; } = default!;
    
    private UsersVm? Model { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Model = await Mediator.Send(new GetUsersQuery());
    }
}
