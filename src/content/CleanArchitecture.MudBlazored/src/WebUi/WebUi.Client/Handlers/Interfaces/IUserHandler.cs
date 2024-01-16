using CleanArchitecture.MudBlazored.WebUi.Shared.AccessControl;

namespace CleanArchitecture.MudBlazored.WebUi.Client.Handlers.Interfaces;

public interface IUserHandler
{
    Task<UserDetailsVm> GetUserAsync(string userId);
    Task PutUserAsync(string userId, UserDto user);
}
