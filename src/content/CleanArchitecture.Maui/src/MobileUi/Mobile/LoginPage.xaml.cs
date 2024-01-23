using CleanArchitecture.Maui.MobileUi.Mobile.Helpers;
using IdentityModel.OidcClient;

namespace CleanArchitecture.Maui.MobileUi.Mobile;

public partial class LoginPage : ContentPage
{
    private readonly ISecureStorage _storage;
    private readonly IConnectivity _connectivity;
    private readonly OidcClient _oidcClient;

    public LoginPage(ISecureStorage storage, IConnectivity connectivity, OidcClient oidcClient)
    {
        _storage = storage;
        _connectivity = connectivity;
        _oidcClient = oidcClient;
        InitializeComponent();
    }

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
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

            var refreshToken = loginResult.RefreshToken;
            if (string.IsNullOrEmpty(refreshToken))
                refreshToken = "FakeToken";
            
            await _storage.SetAsync(OidcConstance.AccessTokenKeyName, loginResult.AccessToken);
            await _storage.SetAsync(OidcConstance.RefreshTokenKeyName, refreshToken);

            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.ToString(), "Ok");
        }
    }
}