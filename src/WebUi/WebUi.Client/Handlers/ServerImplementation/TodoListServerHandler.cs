using MediatR;
using CleanArchitecture.Blazored.Application.TodoLists.Commands;
using CleanArchitecture.Blazored.Application.TodoLists.Queries;
using CleanArchitecture.Blazored.WebUi.Client.Handlers.Interfaces;
using CleanArchitecture.Blazored.WebUi.Shared.TodoLists;

namespace CleanArchitecture.Blazored.WebUi.Client.Handlers.ServerImplementation;

internal class TodoListServerHandler : ITodoListHandler
{
    private IMediator _mediator;

    public TodoListServerHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<TodosVm> GetTodoListsAsync()
    {
        return _mediator.Send(new GetTodoListsQuery());
    }

    public Task PutTodoListAsync(int id, UpdateTodoListRequest request)
    {
        return _mediator.Send(new UpdateTodoListCommand(request));
    }

    public Task DeleteTodoListAsync(int id)
    {
        return _mediator.Send(new DeleteTodoListCommand(id));
    }

    public Task<int> PostTodoListAsync(CreateTodoListRequest request)
    {
        return _mediator.Send(new CreateTodoListCommand(request));
    }
}
