using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTaxiApp.UIModel
{
    public class DriverLocation
    {
        public string DriverId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class BookingRequest
    {
        public string UserId { get; set; }
        public string DriverId { get; set; }
        public double PickupLat { get; set; }
        public double PickupLng { get; set; }
        public double DropLat { get; set; }
        public double DropLng { get; set; }
    }
}
