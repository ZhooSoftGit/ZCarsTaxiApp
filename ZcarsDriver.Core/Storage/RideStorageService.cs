using System.Text.Json;
using ZTaxiApp.Model;

namespace ZTaxi.Core.Storage
{
    public static class RideStorageService
    {
        private const string RideKey = "ongoing_ride_info";

        public static void Save(CurrentRide ride)
        {
            var json = JsonSerializer.Serialize(ride);
            Preferences.Set(RideKey, json);
        }

        public static CurrentRide? Load()
        {
            if (!Preferences.ContainsKey(RideKey))
                return null;

            var json = Preferences.Get(RideKey, string.Empty);
            return string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<CurrentRide>(json);
        }

        public static void Clear()
        {
            Preferences.Remove(RideKey);
        }
    }
}
