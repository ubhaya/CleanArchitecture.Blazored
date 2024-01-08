using Microsoft.Playwright;

namespace CleanArchitecture.Blazored.WebUi.AcceptanceTests.Pages;

public sealed class LoginPage : BasePage
{
    public LoginPage(IBrowser browser, IPage page)
    {
        Browser = browser;
        Page = page;
    }

    public override string PagePath => $"{BaseUrl}/Account/Login";
    public override IBrowser Browser { get; }
    public override IPage Page { get; protected set; }

    public Task SetEmail(string email)
        => Page.FillAsync(Selectors.LoginPage.EmailFieldSelector, email);

    public Task SetPassword(string password)
        => Page.FillAsync(Selectors.LoginPage.PasswordFieldSelector, password);

    public Task ClickLogin()
        => Page.ClickAsync(Selectors.LoginPage.LoginButtonSelector);

    public Task<string?> ProfileLinkText()
        => Page.Locator(Selectors.LoginPage.ProfileLinkTextSelector).TextContentAsync();

    public Task<bool> InvalidLoginAttemptMessageVisible()
        => Page.Locator(Selectors.LoginPage.InvalidLoginAttemptMessageSelector).IsVisibleAsync();
}
