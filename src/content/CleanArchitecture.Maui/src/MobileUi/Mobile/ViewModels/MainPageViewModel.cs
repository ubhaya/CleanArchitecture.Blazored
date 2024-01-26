using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanArchitecture.Maui.MobileUi.Mobile.ViewModels;

public sealed partial class MainPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _buttonText = "Click me";
    
    private int _count;
    
    [RelayCommand]
    private void OnCounterClicked()
    {
        _count++;
        
        if (_count ==1)
            ButtonText = $"Clicked {_count} time";
        else
            ButtonText = $"Clicked {_count} times";
    }
}