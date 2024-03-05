using System.Collections.ObjectModel;
using CleanArchitecture.Maui.MobileUi.Client;
using CleanArchitecture.Maui.MobileUi.Mobile.Models.Todo;
using CleanArchitecture.Maui.MobileUi.Shared.TodoLists;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanArchitecture.Maui.MobileUi.Mobile.ViewModels.Todo;

public sealed partial class TodoItemsViewModel : BaseViewModel, IQueryAttributable
{
    private readonly ITodoItemsClient _itemsClient;
    private TodoListDto _todoList = new();
    [ObservableProperty] private TodoItemsModel? _selectedItem;
    [ObservableProperty] private string _todoListTitle = string.Empty;

    [ObservableProperty] private ObservableCollection<TodoItemsModel> _todoItems = [];

    public TodoItemsViewModel(ITodoItemsClient itemsClient)
    {
        _itemsClient = itemsClient;
    }
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _todoList = query[nameof(TodoListTitle)] as TodoListDto ?? new TodoListDto();
        TodoListTitle = _todoList.Title;
        TodoItems = _todoList.Items
            .Select(x => TodoItemsModel.From(x, _itemsClient))
            .ToObservableCollection();
    }

    [RelayCommand]
    private void AddItem()
    {
        var newItem = TodoItemsModel.From(_todoList.Id, _itemsClient);
        TodoItems.Add(newItem);

        EditItem(newItem);
    }

    [RelayCommand]
    private async Task RemoveItem(TodoItemsModel item)
    {
        await _itemsClient.DeleteTodoItemAsync(item.Id);
        TodoItems.Remove(item);
    }

    private void EditItem(TodoItemsModel? item)
    {
        if (IsSelectedItem(item))
            item!.IsEditable = true;
    }

    private bool IsSelectedItem(TodoItemsModel? item)
    {
        return SelectedItem == item;
    }

    partial void OnSelectedItemChanged(TodoItemsModel? value)
    {
        EditItem(value);
    }
}