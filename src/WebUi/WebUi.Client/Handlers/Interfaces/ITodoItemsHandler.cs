using CleanArchitecture.Blazored.WebUi.Shared.TodoItems;

namespace CleanArchitecture.Blazored.WebUi.Client.Handlers.Interfaces;

public interface ITodoItemsHandler
{
    Task PutTodoItemAsync(int id, UpdateTodoItemRequest request);
    Task<int> PostTodoItemAsync(CreateTodoItemRequest request);
    Task DeleteTodoItemAsync(int id);
}
