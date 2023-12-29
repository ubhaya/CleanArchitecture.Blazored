using CleanArchitecture.Blazored.WebUi.Client.Handlers.Interfaces;
using CleanArchitecture.Blazored.WebUi.Shared.TodoLists;

namespace CleanArchitecture.Blazored.WebUi.Client.Handlers.WasmImplementation;

internal class TodoApiHandler:ITodoListHandler
{
    private TodoListsClient _todoListsClient;

    public TodoApiHandler(TodoListsClient todoListsClient)
    {
        _todoListsClient = todoListsClient;
    }

    public Task<TodosVm> GetTodoListsAsync()
    {
        return _todoListsClient.GetTodoListsAsync();
    }

    public Task PutTodoListAsync(int id, UpdateTodoListRequest request)
    {
        return _todoListsClient.PutTodoListAsync(id, request);
    }

    public Task DeleteTodoListAsync(int id)
    {
        return _todoListsClient.DeleteTodoListAsync(id);
    }

    public Task<int> PostTodoListAsync(CreateTodoListRequest request)
    {
        return _todoListsClient.PostTodoListAsync(request);
    }
}
