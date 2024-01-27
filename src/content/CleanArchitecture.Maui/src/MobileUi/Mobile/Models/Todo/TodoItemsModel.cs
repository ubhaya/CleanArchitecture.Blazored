using CleanArchitecture.Maui.MobileUi.Client;
using CleanArchitecture.Maui.MobileUi.Shared.TodoItems;
using CleanArchitecture.Maui.MobileUi.Shared.TodoLists;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanArchitecture.Maui.MobileUi.Mobile.Models.Todo;

public sealed partial class TodoItemsModel : ObservableObject
{
    private readonly ITodoItemsClient _todoItemsClient;
    [ObservableProperty] private bool _done;

    private TodoItemsModel(ITodoItemsClient todoItemsClient)
    {
        _todoItemsClient = todoItemsClient;
    }

    public int Id { get; set; }

    public int ListId { get; set; }

    public string Title { get; set; } = string.Empty;

    public int Priority { get; set; }

    public string Note { get; set; } = string.Empty;

    public static TodoItemsModel From(TodoItemDto source, ITodoItemsClient todoItemsClient)
    {
        return new(todoItemsClient)
        {
            Id = source.Id,
            ListId = source.ListId,
            Title = source.Title,
#pragma warning disable MVVMTK0034
            _done = source.Done,
#pragma warning restore MVVMTK0034
            Priority = source.Priority,
            Note = source.Note,
        };
    }

    partial void OnDoneChanged(bool value)
    {
        Task.Run(async () => await ToggleDoneCommand.ExecuteAsync(value));
    }

    [RelayCommand]
    private async Task ToggleDoneAsync(bool value)
    {
        await _todoItemsClient.PutTodoItemAsync(Id,
            new UpdateTodoItemRequest
            {
                Id = Id,
                ListId = ListId,
                Title = Title,
                Done = Done,
                Note = Note,
                Priority = Priority
            });
    }
}