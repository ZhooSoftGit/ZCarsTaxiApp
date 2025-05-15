using ZTaxiApp.UIModel;
using ZTaxiApp.Common;
using ZTaxi.Model.Response;

namespace ZTaxiApp.Helpers
{
    public static class AppHelper
    {
        public static CurrentRide? CurrentRide = null;

        public static MobileModule CurrentModule;

        public static LocationInfo? SelectedLocation { get; set; }


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
