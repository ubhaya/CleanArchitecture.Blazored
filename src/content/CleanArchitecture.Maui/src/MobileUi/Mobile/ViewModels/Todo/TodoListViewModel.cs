using System.Collections.ObjectModel;
using CleanArchitecture.Maui.MobileUi.Client;
using CleanArchitecture.Maui.MobileUi.Shared.TodoLists;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanArchitecture.Maui.MobileUi.Mobile.ViewModels.Todo;

public sealed partial class TodoListViewModel : BaseViewModel
{
    private readonly ITodoListsClient _todoListsClient;

    [ObservableProperty, NotifyPropertyChangedFor(nameof(TodoLists))] private TodosVm _model = new();
    [ObservableProperty]
    private TodoListDto? _selectedList;

    public TodoListViewModel(ITodoListsClient todoListsClient)
    {
        _todoListsClient = todoListsClient;
    }

    public ObservableCollection<TodoListDto> TodoLists => Model.Lists.ToObservableCollection();

    [RelayCommand]
    private async Task GetTodoListsAsync()
    {
        IsBusy = true;
        try
        {
            Model = await _todoListsClient.GetTodoListsAsync();
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task SelectedTodoListAsync()
    {
        var navigationParameter = new Dictionary<string, object?>
        {
            { nameof(TodoItemsViewModel.TodoListTitle), SelectedList}
        };
        await Shell.Current.GoToAsync($"//{Routes.TodoItemsPage}", navigationParameter);
    }

    [RelayCommand]
    private async Task CreateNewListAsync()
    {
        var title = await Shell.Current.DisplayPromptAsync("Title", "Enter your title");
        var newTodoList = new TodoListDto()
        {
            Title = title
        };

        var listId = await _todoListsClient.PostTodoListAsync(new CreateTodoListRequest()
        {
            Title = newTodoList.Title
        });

        newTodoList.Id = listId;
        TodoLists.Add(newTodoList);
        SelectList(newTodoList);
    }

    private void SelectList(TodoListDto list)
    {
        if (!IsSelected(list)) return;

        SelectedList = list;
    }

    private bool IsSelected(TodoListDto list)
    {
        return SelectedList?.Id == list.Id;
    }
}