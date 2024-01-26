using CommunityToolkit.Mvvm.Input;

namespace CleanArchitecture.Maui.MobileUi.Mobile.ViewModels;

public sealed partial class AppShellViewModel
{
    [RelayCommand]
    private Task OnNavigating(ShellNavigatingEventArgs args)
    {
        // Todo: Implement authorization logic before navigation
        return Task.CompletedTask;
    }
}