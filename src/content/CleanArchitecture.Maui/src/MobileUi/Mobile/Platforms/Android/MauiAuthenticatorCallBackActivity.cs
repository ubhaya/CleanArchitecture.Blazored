using Android.App;
using Android.Content;
using Android.Content.PM;

namespace CleanArchitecture.Maui.MobileUi.Mobile;

[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
[IntentFilter([Intent.ActionView],
    Categories = [Intent.CategoryDefault, Intent.CategoryBrowsable], DataScheme = CallbackScheme)]
public class MauiAuthenticatorCallBackActivity : WebAuthenticatorCallbackActivity
{
    private const string CallbackScheme = "mobile";
}