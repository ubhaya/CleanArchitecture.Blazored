using CleanArchitecture.Maui.MobileUi.Mobile.ViewModels;
using CleanArchitecture.Maui.MobileUi.Mobile.Views.Authentication;

namespace CleanArchitecture.Maui.MobileUi.Mobile.Views;

public partial class AppShell : Shell
{
    public AppShell(AppShellViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}