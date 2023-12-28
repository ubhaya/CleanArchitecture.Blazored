using BoDi;
using CleanArchitecture.Blazored.WebUi.AcceptanceTests.Pages;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace CleanArchitecture.Blazored.WebUi.AcceptanceTests.Hooks;

[Binding]
public class LoginHooks
{
    [BeforeFeature("Login")]
    public static async Task BeforeLoginFeature(IObjectContainer container)
    {
        var playwright = await Playwright.CreateAsync();

        var options = new BrowserTypeLaunchOptions();
        
#if DEBUG
        options.Headless = false;
        options.SlowMo = 500;
#endif

        var browser = await playwright.Chromium.LaunchAsync(options);

        var page = await browser.NewPageAsync();

        var loginPage = new LoginPage(browser, page);

        container.RegisterInstanceAs(playwright);
        container.RegisterInstanceAs(browser);
        container.RegisterInstanceAs(loginPage);
    }
}