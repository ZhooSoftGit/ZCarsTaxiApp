using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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
