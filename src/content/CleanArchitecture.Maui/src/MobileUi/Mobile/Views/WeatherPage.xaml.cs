using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Maui.MobileUi.Mobile.ViewModels;

namespace CleanArchitecture.Maui.MobileUi.Mobile.Views;

public partial class WeatherPage : ContentPage
{
    public WeatherPage(WeatherForecastViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}