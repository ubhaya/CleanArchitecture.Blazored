using CleanArchitecture.MudBlazored.WebUi.Client.Handlers.Interfaces;
using CleanArchitecture.MudBlazored.WebUi.Shared.TodoItems;

namespace CleanArchitecture.MudBlazored.WebUi.Client.Handlers.WasmImplementation;

internal class TodoItemsApiHandler : ITodoItemsHandler
{
    private ITodoItemsClient _client;

    public TodoItemsApiHandler(ITodoItemsClient client)
    {
        _client = client;
    }

    public Task PutTodoItemAsync(int id, UpdateTodoItemRequest request)
    {
        return _client.PutTodoItemAsync(id, request);
    }

    public Task<int> PostTodoItemAsync(CreateTodoItemRequest request)
    {
        return _client.PostTodoItemAsync(request);
    }

    public Task DeleteTodoItemAsync(int id)
    {
        return _client.DeleteTodoItemAsync(id);
    }
}
