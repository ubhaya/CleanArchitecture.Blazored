using IdentityModel.Client;
using IdentityModel.OidcClient.Browser;
using Microsoft.Extensions.Logging;
using OidcClientIBrowser = IdentityModel.OidcClient.Browser.IBrowser;

namespace CleanArchitecture.Maui.MobileUi.Mobile;

public class WebAuthenticatorBrowser : OidcClientIBrowser
{
    private readonly ILogger<WebAuthenticatorBrowser> _logger;

    public WebAuthenticatorBrowser(ILogger<WebAuthenticatorBrowser> logger)
    {
        _logger = logger;
    }

    public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            var authenticationResult =
                await WebAuthenticator
                    .Default.AuthenticateAsync(
                        new Uri(options.StartUrl), 
                        new Uri(options.EndUrl));

            //var authorizeResponse = ToRawIdentityUrl(options.EndUrl, authenticationResult);
            var authorizeResponse = new RequestUrl(options.EndUrl)
                .Create(new Parameters(authenticationResult.Properties));

            return new BrowserResult()
            {
                Response = authorizeResponse,
                ResultType = BrowserResultType.Success,
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Login");
            return new BrowserResult
            {
                ResultType = BrowserResultType.UnknownError,
                Error = ex.Message,
                ErrorDescription = ex.StackTrace
            };
        }
    }

    private string ToRawIdentityUrl(string redirectUrl, WebAuthenticatorResult result)
    {
        var parameters = result.Properties.Select(pair => $"{pair.Key}={pair.Value}");
        var values = string.Join("&", parameters);
        return $"{redirectUrl}#{values}";
    }
}