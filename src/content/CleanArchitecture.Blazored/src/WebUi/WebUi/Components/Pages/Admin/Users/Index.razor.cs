using MediatR;
using Microsoft.AspNetCore.Components;
using CleanArchitecture.Blazored.Application.Users.Queries;
using CleanArchitecture.Blazored.WebUi.Shared.AccessControl;

namespace CleanArchitecture.Blazored.WebUi.Components.Pages.Admin.Users;

public partial class Index
{
    [Inject] private IMediator Mediator { get; set; } = default!;
    
    private UsersVm? Model { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Model = await Mediator.Send(new GetUsersQuery());
    }
}
