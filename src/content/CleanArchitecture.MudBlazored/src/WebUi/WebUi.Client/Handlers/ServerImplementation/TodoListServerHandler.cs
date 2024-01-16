using CleanArchitecture.MudBlazored.Application.TodoLists.Commands;
using CleanArchitecture.MudBlazored.Application.TodoLists.Queries;
using CleanArchitecture.MudBlazored.WebUi.Client.Handlers.Interfaces;
using CleanArchitecture.MudBlazored.WebUi.Shared.TodoLists;
using MediatR;

namespace CleanArchitecture.MudBlazored.WebUi.Client.Handlers.ServerImplementation;

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
