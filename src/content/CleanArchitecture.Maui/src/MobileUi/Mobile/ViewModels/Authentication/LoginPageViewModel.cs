using CleanArchitecture.Maui.MobileUi.Mobile.Helpers;
using CommunityToolkit.Mvvm.Input;
using IdentityModel.OidcClient;

namespace CleanArchitecture.Maui.MobileUi.Mobile.ViewModels.Authentication;

public sealed partial class LoginPageViewModel : BaseViewModel
{
    private readonly OidcClient _oidcClient;
    private readonly IConnectivity _connectivity;
    private readonly ISecureStorage _storage;

    public LoginPageViewModel(
        OidcClient oidcClient, 
        IConnectivity connectivity, 
        ISecureStorage secureStorage)
    {
        _oidcClient = oidcClient;
        _connectivity = connectivity;
        _storage = secureStorage;
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (IsBusy)
            return;
        
        IsBusy = true;

        try
        {
            if (_connectivity.NetworkAccess is not NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Internet Offline", "Check you internet connection!", "Ok");
                return;
            }
            
            var loginResult = await _oidcClient.LoginAsync(new LoginRequest());
            if (loginResult.IsError)
            {
                await Shell.Current.DisplayAlert("Error", loginResult.Error, "Ok");
                return;
            }
            
            await _storage.SetAsync(OidcConstance.AccessTokenKeyName, loginResult.AccessToken);
            await _storage.SetAsync(OidcConstance.RefreshTokenKeyName, loginResult.RefreshToken);

            await ApplicationHelper.AddFlyoutMenuDetails();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.ToString(), "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task CheckUserLoginDetailsAsync()
    {
        IsBusy = true;
        try
        {
            var tokenResult = await _storage.GetAsync(OidcConstance.AccessTokenKeyName);
            if (string.IsNullOrWhiteSpace(tokenResult))
            {
                return;
            }

            await ApplicationHelper.AddFlyoutMenuDetails();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.ToString(), "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }
}