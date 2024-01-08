using CleanArchitecture.MudBlazored.WebUi.Shared.TodoItems;
using CleanArchitecture.MudBlazored.WebUi.Shared.TodoLists;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace CleanArchitecture.MudBlazored.WebUi.Client.Pages.Todo;

public partial class TodoItems
{
    [Inject] private IDialogService DialogService { get; set; } = default!;
    [CascadingParameter] public TodoState State { get; set; } = default!;
    
    private TodoItemDto? SelectedItem { get; set; }
    
    private MudTextField<string>? _titleInput;

    private bool IsSelectedItem(TodoItemDto item)
    {
        return SelectedItem == item;
    }

    private async Task AddItem()
    {
        var newItem = new TodoItemDto { ListId = State.SelectedList!.Id };
        State.SelectedList.Items.Add(newItem);

        await EditItem(newItem);
    }

    private async Task ToggleDone(TodoItemDto item, bool value)
    {
        item.Done = value;

        await State.TodoItemsHandler.PutTodoItemAsync(item.Id,
            new UpdateTodoItemRequest { Id = item.Id, ListId = item.ListId, Title = item.Title, Done = item.Done });
    }

    private async Task EditItem(TodoItemDto item)
    {
        SelectedItem = item;

        await Task.Delay(50);

        if (_titleInput != null)
        {
            await _titleInput.FocusAsync();
        }
    }

    private async Task SaveItem()
    {
        if (SelectedItem!.Id == 0)
        {
            if (string.IsNullOrWhiteSpace(SelectedItem.Title))
            {
                State.SelectedList!.Items.Remove(SelectedItem);
            }
            else
            {
                var itemId = await State.TodoItemsHandler.PostTodoItemAsync(new CreateTodoItemRequest
                {
                    ListId = SelectedItem.ListId, Title = SelectedItem.Title
                });

                SelectedItem.Id = itemId;
            }
        }
        else
        {
            if (string.IsNullOrWhiteSpace(SelectedItem.Title))
            {
                await State.TodoItemsHandler.DeleteTodoItemAsync(SelectedItem.Id);
                State.SelectedList!.Items.Remove(SelectedItem);
            }
            else
            {
                // TODO: Check, is anything else being updated here?
                await State.TodoItemsHandler.PutTodoItemAsync(SelectedItem.Id,
                    new UpdateTodoItemRequest
                    {
                        Id = SelectedItem.Id,
                        ListId = SelectedItem.ListId,
                        Title = SelectedItem.Title,
                        Done = SelectedItem.Done
                    });
            }
        }
    }

    private void ShowDialog()
    {
        var options = new DialogOptions
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Large,
            FullWidth = true,
            CloseButton = true,
        };
        var parameter = new DialogParameters<ListOptionDialog>
        {
            { x => x.State, State }
        };
        
        DialogService.Show<ListOptionDialog>("List Options", parameter, options);
    }
}
