using System.Collections.ObjectModel;
using CleanArchitecture.Maui.MobileUi.Mobile.Models.Todo;
using CleanArchitecture.Maui.MobileUi.Shared.TodoLists;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CleanArchitecture.Maui.MobileUi.Mobile.ViewModels.Todo;

public sealed partial class TodoItemsViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty] private string _todoList = string.Empty;

    [ObservableProperty] private ObservableCollection<TodoItemsModel> _todoItems = [];

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var todoList = query[nameof(TodoList)] as TodoListDto;
        TodoList = todoList?.Title ?? string.Empty;
        TodoItems = todoList?.Items
                        .Select(TodoItemsModel.From)
                        .ToObservableCollection()
                    ?? [];
    }
}