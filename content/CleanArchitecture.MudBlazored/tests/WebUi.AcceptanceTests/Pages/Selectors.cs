namespace CleanArchitecture.MudBlazored.WebUi.AcceptanceTests.Pages;

public class Selectors
{
    public class LoginPage
    {
        public const string EmailFieldSelector
            = "//html/body/div[3]/div/div/div/div[1]/section/form/div[1]/input";

        public const string PasswordFieldSelector
            = "//html/body/div[3]/div/div/div/div[1]/section/form/div[2]/input";

        public const string LoginButtonSelector
            = "//html/body/div[3]/div/div/div/div[1]/section/form/div[4]/button";

        public const string ProfileLinkTextSelector
            = "a[href='Account/Manage']";
        public const string InvalidLoginAttemptMessageSelector 
            = "//html/body/div[3]/div/div/div/div[1]/section/div";
    }
}