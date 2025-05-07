using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhooCars.Model.Request
{
    public class DistanceRequest
    {
        public double OriginLatitude { get; set; }
        public double OriginLongitude { get; set; }
        public double DestinationLatitude { get; set; }
        public double DestinationLongitude { get; set; }
    }

    public class DistanceResponse
    {
        public double DistanceInMeters { get; set; }
        public string DistanceText { get; set; }
        public double DurationInSeconds { get; set; }
        public string DurationText { get; set; }
    }
}
