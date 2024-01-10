using CleanArchitecture.MudBlazored.WebUi.Shared.TodoLists;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CleanArchitecture.MudBlazored.WebUi.Client.Pages.Todo;

public partial class ListOptionDialog
{
    private MudTextField<string>? _titleInput;
    [Parameter] public TodoState? State { get; set; }
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await _titleInput!.FocusAsync();
    }

    private async Task SaveList()
    {
        await State!.TodoListHandler.PutTodoListAsync(State.SelectedList!.Id, new UpdateTodoListRequest
        {
            Id = State.SelectedList.Id,
            Title = State.SelectedList.Title
        });

        State.SyncList();

        MudDialog.Close();
    }

    private async Task DeleteList()
    {
        await State!.TodoListHandler.DeleteTodoListAsync(State.SelectedList!.Id);
        
        State.DeleteList();
        
        MudDialog.Close();
    }

    private void Cancel()
    {
        MudDialog.Cancel();
    }
}