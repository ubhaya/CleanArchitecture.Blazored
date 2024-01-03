using CleanArchitecture.MudBlazored.WebUi.Client.Components;
using CleanArchitecture.MudBlazored.WebUi.Shared.TodoLists;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using MudBlazor;
using Newtonsoft.Json;

namespace CleanArchitecture.MudBlazored.WebUi.Client.Pages.Todo;

public partial class ListCreateDialog
{
    [Parameter] public TodoState? State { get; set; }
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
    
    private CustomValidation? _customValidation;
    
    private TodoListDto _newTodoList = new();
    
    private MudTextField<string> _titleInput;
    
    private async Task CreateNewList()
    {
        _customValidation!.ClearErrors();

        try
        {
            var listId = await State!.TodoListHandler.PostTodoListAsync(new CreateTodoListRequest
            {
                Title = _newTodoList.Title
            });

            _newTodoList.Id = listId;

            State.Model!.Lists.Add(_newTodoList);

            SelectList(_newTodoList);

            MudDialog.Close();
        }
        catch (ApiException ex)
        {
            var problemDetails = JsonConvert.DeserializeObject<ValidationProblemDetails>(ex.Response);

            if (problemDetails is not null)
            {
                var errors = new Dictionary<string, string[]>();

                foreach (var error in problemDetails.Errors)
                {
                    var key = error.Key[(error.Key.IndexOf('.') + 1)..];

                    errors[key] = error.Value;
                }

                _customValidation.DisplayErrors(errors);
            }
        }
    }
    
    private bool IsSelected(TodoListDto list)
    {
        return State!.SelectedList!.Id == list.Id;
    }
    
    private void SelectList(TodoListDto list)
    {
        if (IsSelected(list)) return;

        State!.SelectedList = list;
    }

    private void Cancel() => MudDialog.Cancel();
}