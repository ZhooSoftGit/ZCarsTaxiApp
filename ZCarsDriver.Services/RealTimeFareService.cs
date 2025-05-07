using System.Text.Json;

namespace ZCarsDriver.Services
{
    public class RealTimeFareService
    {
        private Location _lastLocation;
        private double _totalDistanceKm = 0;
        private double _totalFare = 0;
        private HttpClient _httpClient = new HttpClient();

        public async Task TrackAndCalculateFareAsync(Action<double, double> onFareUpdated)
        {
            try
            {
                while (true)
                {
                    var location = await Geolocation.GetLastKnownLocationAsync();
                    if (location == null)
                    {
                        Console.WriteLine("Unable to fetch location.");
                        continue;
                    }

                    if (_lastLocation != null)
                    {
                        // Get actual road distance using OSRM API
                        double distance = await GetRoadDistanceFromOSRMAsync(_lastLocation, location);

                        if (distance > 0)
                        {
                            _totalDistanceKm += distance;
                            _totalFare = CalculateFare(_totalDistanceKm);
                        }
                    }

                    _lastLocation = location;

                    onFareUpdated?.Invoke(_totalDistanceKm, _totalFare);
                    Console.WriteLine($"Distance: {_totalDistanceKm:F2} km, Fare: ₹{_totalFare:F2}");

                    await Task.Delay(5000); // Update every 5 seconds
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in tracking: {ex.Message}");
            }
        }

        private async Task<double> GetRoadDistanceFromOSRMAsync(Location start, Location end)
        {
            try
            {
                string url = $"https://router.project-osrm.org/route/v1/driving/{start.Longitude},{start.Latitude};{end.Longitude},{end.Latitude}?overview=false";

                var response = await _httpClient.GetStringAsync(url);
                using var doc = JsonDocument.Parse(response);
                var root = doc.RootElement;

                if (root.TryGetProperty("routes", out var routes) && routes.GetArrayLength() > 0)
                {
                    return routes[0].GetProperty("distance").GetDouble() / 1000.0; // Convert to KM
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching road distance from OSRM: {ex.Message}");
            }
            return 0;
        }

        private double CalculateFare(double distanceKm)
        {
            double baseFare = 50; // Base fare for first 3 KM
            double perKmRate = 10; // Rate per KM

            if (distanceKm <= 3)
            {
                return baseFare;
            }
            else
            {
                double extraDistance = distanceKm - 3;
                return baseFare + (extraDistance * perKmRate);
            }
        }
    }
}
