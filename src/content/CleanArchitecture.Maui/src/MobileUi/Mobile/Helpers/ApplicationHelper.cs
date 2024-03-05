using CleanArchitecture.Maui.MobileUi.Mobile.Views;
using CleanArchitecture.Maui.MobileUi.Mobile.Views.Todo;

namespace CleanArchitecture.Maui.MobileUi.Mobile.Helpers;

public static class ApplicationHelper
{
    public static async Task AddFlyoutMenuDetails()
    {
        var mainPage = Shell.Current.Items
            .FirstOrDefault(f => f.Route == Routes.MainPage);
        if (mainPage is not null) Shell.Current.Items.Remove(mainPage);

        var mainPageFlyoutItem = new FlyoutItem()
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

        var weatherForecastFlyout = new FlyoutItem
        {
            Title = "Weather Forecasts",
            Route = Routes.WeatherPage,
            FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
            Items =
            {
                new ShellContent
                {
                    Title = "Weather Forecasts",
                    ContentTemplate = new DataTemplate(typeof(WeatherPage))
                }
            }
        };
        
        Shell.Current.Items.Add(weatherForecastFlyout);

        var todoListFlyout = new FlyoutItem
        {
            Title = "Todo Lists",
            Route = Routes.TodoListPage,
            FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
            Items =
            {
                new ShellContent
                {
                    Title = "Todo Lists",
                    ContentTemplate = new DataTemplate(typeof(TodoListPage))
                }
            }
        };

        Shell.Current.Items.Add(todoListFlyout);
        
        var todoItemFlyout = new FlyoutItem
        {
            Title = "Todo Items",
            Route = Routes.TodoItemsPage,
            FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
            FlyoutItemIsVisible = false,
            Items =
            {
                new ShellContent
                {
                    Title = "Todo Items",
                    ContentTemplate = new DataTemplate(typeof(TodoItemsPage))
                }
            }
        };

        Shell.Current.Items.Add(todoItemFlyout);

        if (!Shell.Current.Items.Contains(mainPageFlyoutItem))
        {
            Shell.Current.Items.Add(mainPageFlyoutItem);

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