using Microsoft.Maui.Platform;
using System.Reflection;
using System.Text.Json;
using ZCarsDriver.ViewModel;
using ZhooSoft.Core;

namespace ZCarsDriver.Views;

public partial class CustomMapWebView : BaseContentPage<CustomMapWebViewModel>
{
    private bool isTracking;
    public CustomMapWebView()
	{
		InitializeComponent();
        LoadMap();
    }

    private async void LoadMap()
    {
        try
        {
            var location = await Geolocation.GetLastKnownLocationAsync() ?? await Geolocation.GetLocationAsync();

            if (location != null)
            {
                double userLat = location.Latitude;
                double userLng = location.Longitude;

                double destinationLat = 12.9716; // Example: Bangalore
                double destinationLng = 77.5946;

                string html = await LoadHtmlFile("google_map.html");

                html = html.Replace("{{START_LAT}}", userLat.ToString())
                           .Replace("{{START_LNG}}", userLng.ToString())
                           .Replace("{{END_LAT}}", destinationLat.ToString())
                           .Replace("{{END_LNG}}", destinationLng.ToString());

                MapWebView.Source = new HtmlWebViewSource { Html = html };

                StartTracking();
            }
            else
            {
                Console.WriteLine("Location not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting location: {ex.Message}");
        }
    }

    private async void StartTracking()
    {
        isTracking = true;

        while (isTracking)
        {
            try
            {
                var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best));

                if (location != null)
                {
                    double userLat = location.Latitude;
                    double userLng = location.Longitude;

                    string jsCode = $"window.postMessage({JsonSerializer.Serialize(new { lat = userLat, lng = userLng })});";
                    await MapWebView.EvaluateJavaScriptAsync(jsCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Location tracking error: {ex.Message}");
            }

            await Task.Delay(2000); // Update every 2 seconds
        }
    }

    private async Task<string> LoadHtmlFile(string fileName)
    {

        string filePath = fileName;
        var stream = await FileSystem.OpenAppPackageFileAsync(filePath);

        //var assembly = Assembly.GetExecutingAssembly();
        //var stream = assembly.GetManifestResourceStream($"ZCarsDriver.Resources.Raw.{fileName}");

        if (stream == null)
            throw new Exception($"HTML file '{fileName}' not found in Resources/Raw.");

        using (var reader = new StreamReader(stream))
        {
            return reader.ReadToEnd();
        }
    }
}