using System.Collections.ObjectModel;
using CleanArchitecture.Maui.MobileUi.Client;
using CleanArchitecture.Maui.MobileUi.Mobile.Models.Todo;
using CleanArchitecture.Maui.MobileUi.Shared.TodoLists;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CleanArchitecture.Maui.MobileUi.Mobile.ViewModels.Todo;

[QueryProperty(nameof(TodoList), nameof(TodoList))]
public sealed partial class TodoItemsViewModel : BaseViewModel
{
    private readonly ITodoItemsClient _todoItemsClient;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TodoItems))]
    private TodoListDto _todoList = new();

    public TodoItemsViewModel(ITodoItemsClient todoItemsClient)
    {
        _todoItemsClient = todoItemsClient;
    }

    public ObservableCollection<TodoItemsModel> TodoItems => TodoList?.Items
                                                              .Select(x=> TodoItemsModel.From(x, _todoItemsClient))
                                                              .ToObservableCollection() ?? 
                                                          [];

    
}