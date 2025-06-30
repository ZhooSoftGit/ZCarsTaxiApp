using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace ZTaxiApp;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        Window.SetSoftInputMode(SoftInput.AdjustResize);

        AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
        {
            var ex = (Exception)args.ExceptionObject;
            Android.Util.Log.Error("ZhooSoftCrash", ex.ToString());
        };

        TaskScheduler.UnobservedTaskException += (sender, args) =>
        {
            Android.Util.Log.Error("ZhooSoftCrash", args.Exception.ToString());
        };

    }
}
