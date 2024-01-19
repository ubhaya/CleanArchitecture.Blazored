using CleanArchitecture.Maui.MobileUi.Shared.WeatherForecasts;
using FastEndpoints;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints;

public class WeatherForecastEndpoint : EndpointWithoutRequest<IEnumerable<WeatherForecast>>
{
    private readonly string[] _summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/weatherforecast");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var forecasts = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = _summaries[Random.Shared.Next(_summaries.Length)]
                })
            .ToArray();
        await SendAsync(forecasts, cancellation: cancellationToken);
    }
}