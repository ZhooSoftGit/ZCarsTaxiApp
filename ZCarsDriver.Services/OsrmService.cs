using System.Text.Json;
using ZTaxi.Model.Response;
using ZTaxiApp.Services.AppService;

namespace ZTaxiApp.Services
{
    public class OsrmService : IOsrmService
    {
        private readonly HttpClient _httpClient;

        public OsrmService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<(double? Distance, List<Location> Locations, double? duration)> GetRoutePoints(double startLat, double startLng, double endLat, double endLng)
        {
            try
            {
                string accessToken = "pk.eyJ1Ijoiemhvb3NvZnQiLCJhIjoiY21iYWdmdDFyMTYyaDJrczl0OW04OTloMyJ9.NgqcQ7l55QagG0OpknCL-A";

                string url = $"https://api.mapbox.com/directions/v5/mapbox/driving/" +
                $"{startLng},{startLat};{endLng},{endLat}" +
                             $"?geometries=geojson&overview=full&access_token={accessToken}";

                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    return (null, null, null);

                var content = await response.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(content);

                var route = json.RootElement.GetProperty("routes")[0];

                double distance = route.GetProperty("distance").GetDouble();
                double duration = route.GetProperty("duration").GetDouble();

                var coordinates = route.GetProperty("geometry").GetProperty("coordinates");
                var locations = new List<Location>();

                foreach (var point in coordinates.EnumerateArray())
                {
                    double lng = point[0].GetDouble();
                    double lat = point[1].GetDouble();
                    locations.Add(new Location { Latitude = lat, Longitude = lng });
                }

                return (distance, locations, duration);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching OSRM route: {ex.Message}");
                return (null, null, null);
            }
        }

        private void osrm(double startLat, double startLng, double endLat, double endLng)
        {
            //var url = $"http://router.project-osrm.org/route/v1/driving/{startLng},{startLat};{endLng},{endLat}?overview=full&geometries=polyline";

            //var response = await _httpClient.GetStringAsync(url);

            //// Parse using System.Text.Json
            //using var doc = JsonDocument.Parse(response);
            //var root = doc.RootElement;

            //// Extract the polyline
            //if (root.TryGetProperty("routes", out var routes) && routes.GetArrayLength() > 0)
            //{
            //    var geometry = routes[0].GetProperty("geometry").GetString();
            //    var distance = routes[0].GetProperty("distance").GetDouble() / 1000;
            //    var duration = routes[0].GetProperty("duration").GetDouble() / 3600;
            //    var locations = PolylineDecoder.DecodePolyline(geometry);
            //    return (distance, locations, duration);
            //}

            //Console.WriteLine("No route found.");
            //return (null, null, null);
        }

        public async Task<PhotonResult?> SearchPlacesAsync(string query)
        {
            var url = $"https://photon.komoot.io/api/?q={Uri.EscapeDataString(query)}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<PhotonResult>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
