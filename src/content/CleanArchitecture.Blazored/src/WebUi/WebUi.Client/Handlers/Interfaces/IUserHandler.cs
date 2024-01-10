using CleanArchitecture.Blazored.WebUi.Shared.AccessControl;

namespace CleanArchitecture.Blazored.WebUi.Client.Handlers.Interfaces;

public interface IUserHandler
{
    Task<UserDetailsVm> GetUserAsync(string userId);
    Task PutUserAsync(string userId, UserDto user);
}
