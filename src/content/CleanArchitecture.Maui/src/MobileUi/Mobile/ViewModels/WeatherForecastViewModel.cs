﻿using CleanArchitecture.Maui.MobileUi.Client;
using CleanArchitecture.Maui.MobileUi.Shared.WeatherForecasts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanArchitecture.Maui.MobileUi.Mobile.ViewModels;

public sealed partial class WeatherForecastViewModel : BaseViewModel
{
    private readonly IWeatherForecastClient _client;

    [ObservableProperty] private IEnumerable<WeatherForecast>? _weatherForecasts;

    public WeatherForecastViewModel(IWeatherForecastClient client)
    {
        _client = client;
    }

    [RelayCommand]
    private async Task GetWeatherForecastAsync()
    {
        WeatherForecasts = await _client.GetWeatherForecastAsync();
    }
}