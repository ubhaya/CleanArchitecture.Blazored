using System.Net;
using System.Net.Http.Headers;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Maui.MobileUi.Mobile.Helpers;

public class AccessTokenMessageHandler : DelegatingHandler
{
    private readonly OidcClient _oidcClient;
    private readonly ISecureStorage _secureStorage;
    private readonly ILogger<AccessTokenMessageHandler> _logger;

    public AccessTokenMessageHandler(OidcClient oidcClient,
        ISecureStorage secureStorage, 
        ILogger<AccessTokenMessageHandler> logger)
    {
        _oidcClient = oidcClient;
        _secureStorage = secureStorage;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Todo: AccessTokenMessageHandler is not perfect
        var accessToken = await _secureStorage.GetAsync(OidcConstance.AccessTokenKeyName);

        if (string.IsNullOrWhiteSpace(accessToken))
        {
            var loginResult = await _oidcClient.LoginAsync(new LoginRequest(), cancellationToken);
            if (loginResult.IsError)
            {
                await Shell.Current.DisplayAlert("Error", loginResult.Error, "Ok");
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            accessToken = loginResult.AccessToken;

            await _secureStorage.SetAsync(OidcConstance.AccessTokenKeyName, accessToken);
            await _secureStorage.SetAsync(OidcConstance.RefreshTokenKeyName, loginResult.RefreshToken);
        }
        
        request.SetBearerToken(accessToken);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode != HttpStatusCode.Unauthorized) return response;

        var refreshToken = await _secureStorage.GetAsync(OidcConstance.RefreshTokenKeyName);
        
        var refreshResult = await _oidcClient.RefreshTokenAsync(refreshToken,cancellationToken:cancellationToken);

        if (refreshResult.IsError)
        {
            _logger.LogError("{Error}\n{StackTrace}\n{Response}", 
                refreshResult.Error, refreshResult.ErrorDescription, response.ToString());
            return response;
        }

        refreshToken = refreshResult.RefreshToken;
        accessToken = refreshResult.AccessToken;

        await _secureStorage.SetAsync(OidcConstance.AccessTokenKeyName, accessToken);
        await _secureStorage.SetAsync(OidcConstance.RefreshTokenKeyName, refreshToken);

        request.SetBearerToken(accessToken);
        
        return await base.SendAsync(request, cancellationToken);
    }
}