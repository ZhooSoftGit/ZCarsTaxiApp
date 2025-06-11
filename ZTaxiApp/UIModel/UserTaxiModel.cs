using CommunityToolkit.Mvvm.ComponentModel;
using ZTaxiApp.UIHelper;

namespace ZTaxiApp.UIModel
{
    public partial class LocationInfo
    {
        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public LocationType LocationType { get; set; }

        public Location GetLocation() => new Location(Latitude, Longitude);
    }

    public class RideOption
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Icon { get; set; }
    }
}
