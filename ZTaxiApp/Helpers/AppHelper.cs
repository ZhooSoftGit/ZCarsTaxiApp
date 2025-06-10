using ZTaxiApp.UIModel;
using ZTaxiApp.Common;
using ZTaxi.Model.Response;
using ZTaxiApp.Model;
using ZTaxi.Core.Storage;

namespace ZTaxiApp.Helpers
{
    public static class AppHelper
    {
        public static CurrentRide? CurrentRide => RideStorageService.Load();

        public static MobileModule CurrentModule;

        public static LocationInfo? SelectedLocation { get; set; }
        public static List<DriverLocation> AvailableDrivers { get; internal set; }

        public static async Task<Location> GetUserLocation()
        {
            try
            {
                var location = await Geolocation.GetLocationAsync() ?? await Geolocation.GetLastKnownLocationAsync();
                return location;
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
    }
}
