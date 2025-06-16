using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZhooSoft.Core;
using ZTaxi.Model.Response;
using ZTaxi.Services.Contracts;

namespace ZTaxi.Services
{
    public partial class AddressService : IAddressService
    {
        private readonly HttpClient _httpClient;

        public string GoogleMapKey { get; set; } = "AIzaSyDz-0CVEdMMiWHfuXm_zXZp2t69963hCkY";
        public AddressService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(5);
        }

        public async Task<List<SearchAddressResult>> GetAddressFromSearchAsync(string searchTerm)
        {
            string url = $"https://maps.googleapis.com/maps/api/place/textsearch/json?query=areas+in+{Uri.EscapeDataString(searchTerm)}&key={GoogleMapKey}";

            try
            {
                var response = await _httpClient.GetStringAsync(url);

                using JsonDocument document = JsonDocument.Parse(response);
                JsonElement root = document.RootElement;

                // Navigate the JSON structure to find the data you need.
                if (root.TryGetProperty("results", out JsonElement results) && results.GetArrayLength() > 0)
                {
                    var tt = results.ToString();
                    var hh = JsonSerializer.Serialize(results);
                    List<SearchAddressResult> res = JsonSerializer.Deserialize<List<SearchAddressResult>>(results);
                    return res;
                }
            }
            catch (Exception ex)
            {

            }

            return new List<SearchAddressResult>();
        }

        public async Task<List<MapboxSearchResult>> SearchAsync(string query, double? latitude = null, double? longitude = null)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<MapboxSearchResult>();

            string baseUrl = $"https://api.mapbox.com/search/searchbox/v1/suggest?q={Uri.EscapeDataString(query)}&access_token={GlobalConstants.MapBoxKey}";

            if (latitude.HasValue && longitude.HasValue)
                baseUrl += $"&proximity={longitude.Value},{latitude.Value}";

            var response = await _httpClient.GetAsync(baseUrl);

            if (!response.IsSuccessStatusCode)
                return new List<MapboxSearchResult>();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var results = new List<MapboxSearchResult>();

            if (!doc.RootElement.TryGetProperty("features", out var features)) return results;

            foreach (var feature in features.EnumerateArray())
            {
                var props = feature.GetProperty("properties");
                var coords = feature.GetProperty("geometry").GetProperty("coordinates");

                results.Add(new MapboxSearchResult
                {
                    Name = props.GetProperty("name").GetString(),
                    FormattedAddress = props.GetProperty("full_address").GetString(),
                    PlaceId = props.GetProperty("mapbox_id").GetString(),
                    Types = props.TryGetProperty("poi_category", out var types)
                            ? types.EnumerateArray().Select(t => t.GetString()).Where(t => t != null).ToList()
                            : new List<string>(),
                    Longitude = coords[0].GetDouble(),
                    Latitude = coords[1].GetDouble()
                });
            }

            return results;
        }

        public async Task<string> GetPlaceNameAsync(double latitude, double longitude)
        {
            //var apiKey = GoogleMapKey;
            //var url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key={apiKey}";

            //using var client = new HttpClient();
            //var response = await client.GetStringAsync(url);
            //var geocodeResponse = JsonSerializer.Deserialize<GeocodeResponse>(response);

            //return geocodeResponse?.Results?.FirstOrDefault()?.FormattedAddress ?? null;
            try
            {                
                var url = $"https://nominatim.openstreetmap.org/reverse?format=jsonv2&lat={latitude}&lon={longitude}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<NominatimResponse>(content);
                    return result?.DisplayName;
                }
            }
            catch (Exception ex)
            {
                //log error
            }

            return "Unnamed Location";
        }
    }
}
