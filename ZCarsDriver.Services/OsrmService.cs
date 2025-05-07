using System.Text.Json;
using ZCarsDriver.Services.AppService;

namespace ZCarsDriver.Services
{
    public class OsrmService : IOsrmService
    {
        private readonly HttpClient _httpClient;

        public OsrmService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<(double? Distance, List<Location> Locations)> GetRoutePoints(double startLat, double startLng, double endLat, double endLng)
        {
            try
            {
                var url = $"http://router.project-osrm.org/route/v1/driving/{startLng},{startLat};{endLng},{endLat}?overview=full&geometries=polyline";
                var response = await _httpClient.GetStringAsync(url);

                // Parse using System.Text.Json
                using var doc = JsonDocument.Parse(response);
                var root = doc.RootElement;

                // Extract the polyline
                if (root.TryGetProperty("routes", out var routes) && routes.GetArrayLength() > 0)
                {
                    var geometry = routes[0].GetProperty("geometry").GetString();
                    var distance = routes[0].GetProperty("distance").GetDouble() / 1000;
                    var locations = PolylineDecoder.DecodePolyline(geometry);
                    return (distance, locations);
                }

                Console.WriteLine("No route found.");
                return (null, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching OSRM route: {ex.Message}");
                return (null, null);
            }
        }
    }
}
