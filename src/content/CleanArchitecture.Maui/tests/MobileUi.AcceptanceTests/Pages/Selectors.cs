namespace CleanArchitecture.Maui.MobileUi.AcceptanceTests.Pages;

public class Selectors
{
    public class LoginPage
    {
        public const string EmailFieldSelector 
            = "//html/body/div[1]/main/article/div/div[1]/section/form/div[1]/input";

        public const string PasswordFieldSelector 
            = "//html/body/div[1]/main/article/div/div[1]/section/form/div[2]/input";

        public const string LoginButtonSelector
            = "//html/body/div[1]/main/article/div/div[1]/section/form/div[4]/button";

        public const string ProfileLinkTextSelector
            = "a[href='Account/Manage']";
        
        public const string InvalidLoginAttemptMessageSelector 
            = "text=Error: Invalid login attempt";
    }
}