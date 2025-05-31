using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ZTaxi.Model.Response
{
    public class NominatimResponse
    {
        [JsonPropertyName("address")]
        public Address Address { get; set; }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Address
    {
        [JsonPropertyName("road")]
        public string Road { get; set; }

        [JsonPropertyName("suburb")]
        public string Suburb { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("postcode")]
        public string Postcode { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }
    }

}
