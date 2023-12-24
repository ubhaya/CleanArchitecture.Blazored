using MediatR;
using CleanArchitecture.Blazored.Application.Users.Commands;
using CleanArchitecture.Blazored.Application.Users.Queries;
using CleanArchitecture.Blazored.WebUi.Client.Handlers.Interfaces;
using CleanArchitecture.Blazored.WebUi.Shared.AccessControl;

namespace CleanArchitecture.Blazored.WebUi.Client.Handlers.ServerImplementation;

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
