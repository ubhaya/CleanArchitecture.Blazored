using CleanArchitecture.MudBlazored.WebUi.Client.Components;
using CleanArchitecture.MudBlazored.WebUi.Shared.TodoLists;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using MudBlazor;
using Newtonsoft.Json;

namespace CleanArchitecture.MudBlazored.WebUi.Client.Pages.Todo;

public partial class TodoLists
{
    [Inject] private IDialogService DialogService { get; set; } = default!;
    [CascadingParameter] public TodoState State { get; set; } = default!;

    private bool IsSelected(TodoListDto list)
    {
        return State.SelectedList!.Id == list.Id;
    }

    private void SelectList(TodoListDto list)
    {
        if (IsSelected(list)) return;

        State.SelectedList = list;
    }

    private void ShowDialog()
    {
        var options = new DialogOptions
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Large,
            FullWidth = true,
            CloseButton = true
        };
        var parameter = new DialogParameters<ListCreateDialog>
        {
            { x => x.State, State }
        };

        DialogService.Show<ListCreateDialog>("Lists", parameter, options);
    }
}
