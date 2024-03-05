using CleanArchitecture.Maui.MobileUi.Mobile.ViewModels.Authentication;
using CommunityToolkit.Maui.Behaviors;

namespace CleanArchitecture.Maui.MobileUi.Mobile.Views.Authentication;

public partial class LoginPage : ContentPage
{

    public LoginPage(LoginPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}