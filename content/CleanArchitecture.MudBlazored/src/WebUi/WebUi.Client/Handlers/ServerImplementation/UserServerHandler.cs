using CleanArchitecture.MudBlazored.Application.Users.Commands;
using CleanArchitecture.MudBlazored.Application.Users.Queries;
using CleanArchitecture.MudBlazored.WebUi.Client.Handlers.Interfaces;
using CleanArchitecture.MudBlazored.WebUi.Shared.AccessControl;
using MediatR;

namespace CleanArchitecture.MudBlazored.WebUi.Client.Handlers.ServerImplementation;

internal class UserServerHandler : IUserHandler
{
    private readonly IMediator _mediator;

    public UserServerHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<UserDetailsVm> GetUserAsync(string userId)
    {
        return _mediator.Send(new GetUserQuery(userId));
    }

    public Task PutUserAsync(string userId, UserDto user)
    {
        return _mediator.Send(new UpdateUserCommand(user));
    }
}
