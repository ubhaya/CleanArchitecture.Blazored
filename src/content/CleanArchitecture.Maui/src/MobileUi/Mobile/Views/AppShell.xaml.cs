using CleanArchitecture.Maui.MobileUi.Mobile.Views.Authentication;

namespace CleanArchitecture.Maui.MobileUi.Mobile.Views;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(Routes.LoginPage, typeof(LoginPage));
        Routing.RegisterRoute(Routes.MainPage, typeof(MainPage));
    }
}