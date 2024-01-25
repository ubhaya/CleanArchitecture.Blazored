using CleanArchitecture.Maui.MobileUi.Client;
using CleanArchitecture.Maui.MobileUi.Shared.WeatherForecasts;
using Newtonsoft.Json;

namespace CleanArchitecture.Maui.MobileUi.Mobile;

public partial class MainPage : ContentPage
{
    private readonly IClient _client;
    int count = 0;

    public MainPage(IClient client)
    {
        _client = client;
        InitializeComponent();
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);

        try
        {
            var result = await _client.GetWeatherForecastAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}