using CleanArchitecture.Maui.MobileUi.Mobile.Helpers;
using CommunityToolkit.Mvvm.Input;
using IdentityModel.OidcClient;

namespace CleanArchitecture.Maui.MobileUi.Mobile.ViewModels;

public sealed partial class AppShellViewModel
{
    private readonly ISecureStorage _storage;
    private readonly OidcClient _oidcClient;

    public AppShellViewModel(ISecureStorage storage, OidcClient oidcClient)
    {
        _storage = storage;
        _oidcClient = oidcClient;
    }

    [RelayCommand]
    private Task OnNavigating(ShellNavigatingEventArgs args)
    {
        // Todo: Implement authorization logic before navigation
        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        _storage.Remove(OidcConstance.AccessTokenKeyName);
        _storage.Remove(OidcConstance.RefreshTokenKeyName);
        await _oidcClient.LogoutAsync(new LogoutRequest());
        await Shell.Current.GoToAsync($"//{Routes.LoginPage}");
    }
}