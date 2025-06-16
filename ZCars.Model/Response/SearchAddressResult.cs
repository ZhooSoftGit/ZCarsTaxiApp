using System.Text.Json.Serialization;

namespace ZTaxi.Model.Response
{
    public partial class SearchAddressResult
    {
        [JsonPropertyName("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("place_id")]
        public string PlaceId { get; set; }

        [JsonPropertyName("types")]
        public List<string> Types { get; set; }
    }

    public partial class Geometry
    {
        [JsonPropertyName("location")]
        public RespLocation Location { get; set; }

        [JsonPropertyName("viewport")]
        public Viewport Viewport { get; set; }
    }

    public partial class RespLocation
    {
        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lng")]
        public double Lng { get; set; }
    }

    public partial class Viewport
    {
        [JsonPropertyName("northeast")]
        public RespLocation Northeast { get; set; }

        [JsonPropertyName("southwest")]
        public RespLocation Southwest { get; set; }
    }

    public class GeocodeResponse
    {
        [JsonPropertyName("results")]
        public List<GeocodeResult> Results { get; set; }
    }

    public class GeocodeResult
    {
        [JsonPropertyName("formatted_address")]
        public string FormattedAddress { get; set; }
    }



    public class PhotonResult
    {
        [JsonPropertyName("features")]
        public List<PhotonFeature> Features { get; set; }
    }

    public class PhotonFeature
    {
        [JsonPropertyName("properties")]
        public PhotonProperties Properties { get; set; }

        [JsonPropertyName("geometry")]
        public PhotonGeometry Geometry { get; set; }
    }

    public class PhotonProperties
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("postcode")]
        public string Postcode { get; set; }
    }

    public class PhotonGeometry
    {
        [JsonPropertyName("coordinates")]
        public List<double> Coordinates { get; set; } // [lon, lat]
    }

    public class MapboxSearchResult
    {
        public string Name { get; set; }
        public string FormattedAddress { get; set; }
        public string PlaceId { get; set; }
        public List<string> Types { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
