using Microsoft.AspNetCore.Components;
using CleanArchitecture.Blazored.WebUi.Client.Handlers;
using CleanArchitecture.Blazored.WebUi.Client.Handlers.Interfaces;
using CleanArchitecture.Blazored.WebUi.Shared.AccessControl;

namespace CleanArchitecture.Blazored.WebUi.Client.Pages.Admin.Users;

public partial class Edit
{
    [Parameter] public string UserId { get; set; } = string.Empty;

    [Inject] private IUserHandler UsersClient { get; set; } = default!;

    [Inject] private NavigationManager Navigation { get; set; } = default!;

    private UserDetailsVm? Model { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        Model = await UsersClient.GetUserAsync(UserId);
    }

    private void ToggleSelectedRole(string roleName)
    {
        if (Model!.User.Roles.Contains(roleName))
        {
            Model.User.Roles.Remove(roleName);
            StateHasChanged();
            return;
        }
        
        Model.User.Roles.Add(roleName);
        StateHasChanged();
    }

    private async Task UpdateUser()
    {
        await UsersClient.PutUserAsync(Model!.User.Id, Model.User);
        Navigation.NavigateTo("/admin/users");
    }
}
