using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace ZTaxiApp;

public static class MauiProgram
{
    public static string GoogleMapsKey = "AIzaSyDz-0CVEdMMiWHfuXm_zXZp2t69963hCkY";
    public static MauiApp CreateMauiApp()
	{

		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
