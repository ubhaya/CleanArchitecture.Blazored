using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using CleanArchitecture.Maui.MobileUi.Shared.WeatherForecasts;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints;

public class WeatherForecastEndpoint : IEndpointsDefinition
{
    private readonly string[] _summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/weatherforecast", GetWeatherForecast)
            .WithName("GetWeatherForecast")
            .WithOpenApi()
            .RequireAuthorization(Permissions.Forecast);
    }

    private WeatherForecast[] GetWeatherForecast()
    {
        var forecasts = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = _summaries[Random.Shared.Next(_summaries.Length)]
                })
            .ToArray();
        return forecasts;
    }
}