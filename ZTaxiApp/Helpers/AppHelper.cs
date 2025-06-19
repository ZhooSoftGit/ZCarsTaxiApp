using ZTaxiApp.UIModel;
using ZTaxiApp.Common;
using ZTaxi.Model.Response;
using ZTaxiApp.Model;
using ZTaxi.Core.Storage;

namespace ZTaxiApp.Helpers
{
    public static class AppHelper
    {
        public static CurrentRide? CurrentRide
        {
            get
            {
                if (_currentRide == null)
                {
                    _currentRide = RideStorageService.Load();
                }

                return _currentRide;
            }
            set
            {
                if (value == null)
                {
                    ChatMessages?.Clear();
                    RideStorageService.Clear();
                }
                _currentRide = value;
            }
        }

        public static MobileModule CurrentModule;
        private static CurrentRide? _currentRide;

        public static LocationInfo? SelectedLocation { get; set; }
        public static List<DriverLocation> AvailableDrivers { get; internal set; }
        public static VehicleOption SelectedVehicleType { get; internal set; }
        public static ActionEnum SelectedAction { get; internal set; }

        public static List<ChatMessage>? ChatMessages { get; set; } = new();

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

        internal static void SaveRideInfo(CurrentRide ride)
        {
            CurrentRide = null;
            RideStorageService.Save(ride);
        }
    }
}
