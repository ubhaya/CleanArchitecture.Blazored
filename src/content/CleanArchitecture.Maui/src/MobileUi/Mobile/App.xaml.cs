using CleanArchitecture.Maui.MobileUi.Mobile.Views;

namespace CleanArchitecture.Maui.MobileUi.Mobile;

public partial class App : Application
{
    public App(AppShell appShell)
    {
        InitializeComponent();

        MainPage = appShell;
    }
}