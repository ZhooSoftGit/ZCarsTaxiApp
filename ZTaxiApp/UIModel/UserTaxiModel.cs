using ZTaxiApp.UIHelper;

namespace ZTaxiApp.UIModel
{
    public class LocationInfo
    {
        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public LocationType LocationType { get; set; }
    }

    public class RideOption
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Icon { get; set; }
    }
}
