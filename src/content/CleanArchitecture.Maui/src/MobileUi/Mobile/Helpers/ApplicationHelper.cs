using CleanArchitecture.Maui.MobileUi.Mobile.Views;

namespace CleanArchitecture.Maui.MobileUi.Mobile.Helpers;

public static class ApplicationHelper
{
    public static async Task AddFlyoutMenuDetails()
    {
        var mainPage = Shell.Current.Items
            .FirstOrDefault(f => f.Route == Routes.MainPage);
        if (mainPage is not null) Shell.Current.Items.Remove(mainPage);

        var flyOutItem = new FlyoutItem()
        {
            Title = "Main Page",
            Route = Routes.MainPage,
            FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
            Items =
            {
                new ShellContent
                {
                    Title = "Main Page",
                    ContentTemplate = new DataTemplate(typeof(MainPage))
                }
            }
        };

        if (!Shell.Current.Items.Contains(flyOutItem))
        {
            Shell.Current.Items.Add(flyOutItem);

            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                Shell.Current.Dispatcher.Dispatch(async () =>
                    await Shell.Current.GoToAsync($"//{Routes.MainPage}"));
            }
            else
            {
                await Shell.Current.GoToAsync($"//{Routes.MainPage}");
            }
        }
    }
}