using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Blazored.WebUi.AcceptanceTests;

public static class ConfigurationHelper
{
    private readonly static IConfiguration _configuration;

    static ConfigurationHelper()
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
    }

    private static string? _baseUrl;
    private static string? _dockerComposeFileName;

    public static string GetDockerComposeFileName()
    {
        if (string.IsNullOrWhiteSpace(_dockerComposeFileName))
        {
            _dockerComposeFileName = _configuration["DockerComposeFileName"];
            Guard.Against.NullOrWhiteSpace(_dockerComposeFileName);
        }

        return _dockerComposeFileName;
    }
    
    public static string GetBaseUrl()
    {
        if (string.IsNullOrWhiteSpace(_baseUrl))
        {
            _baseUrl = _configuration["BaseUrl"];
            _baseUrl = _baseUrl!.TrimEnd('/');
        }

        return _baseUrl;
    }
}
