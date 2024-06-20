using CleanArchitecture.Maui.MobileUi.Client;
using CleanArchitecture.Maui.MobileUi.Shared.TodoItems;
using CleanArchitecture.Maui.MobileUi.Shared.TodoLists;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanArchitecture.Maui.MobileUi.Mobile.Models.Todo;

public sealed partial class TodoItemsModel : ObservableObject
{
    private readonly ITodoItemsClient _client;
    [ObservableProperty] private bool _done;
    [ObservableProperty] private string _title = string.Empty;
    [ObservableProperty, NotifyPropertyChangedFor(nameof(IsNotEditable))] private bool _isEditable;

    private TodoItemsModel(ITodoItemsClient client)
    {
        _client = client;
    }
    
    public int Id { get; set; }

    public int ListId { get; set; }

    public int Priority { get; set; }

    public string Note { get; set; } = string.Empty;
    public bool IsNotEditable => !IsEditable;

    public static TodoItemsModel From(TodoItemDto source, ITodoItemsClient client)
    {
        return new TodoItemsModel(client)
        {
            Id = source.Id,
            ListId = source.ListId,
            Title = source.Title,
            Done = source.Done,
            Priority = source.Priority,
            Note = source.Note,
        };
    }

    public static TodoItemsModel From(int listId, ITodoItemsClient client)
    {
        return new TodoItemsModel(client)
        {
            ListId = listId
        };
    }

    [RelayCommand]
    private async Task CheckChanged()
    {
        await _client.PutTodoItemAsync(Id,
            new UpdateTodoItemRequest()
            {
                Id = Id,
                ListId = ListId,
                Title = Title,
                Done = Done,
                Note = Note,
                Priority = Priority
            });
    }

    [RelayCommand]
    private async Task SaveItem()
    {
        if (Id == 0)
        {
            var itemId = await _client.PostTodoItemAsync(new CreateTodoItemRequest
            {
                ListId = ListId,
                Title = Title
            });

            Id = itemId;
        }
        else
        {
            await _client.PutTodoItemAsync(Id,
                new UpdateTodoItemRequest
                {
                    Id = Id,
                    ListId = ListId,
                    Title = Title,
                    Done = Done,
                });
        }

        IsEditable = false;
    }
}