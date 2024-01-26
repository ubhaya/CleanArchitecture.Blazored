using CleanArchitecture.Maui.MobileUi.Mobile.ViewModels;

namespace CleanArchitecture.Maui.MobileUi.Mobile.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}