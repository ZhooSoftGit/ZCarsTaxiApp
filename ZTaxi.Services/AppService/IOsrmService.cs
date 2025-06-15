using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTaxiApp.Services.AppService
{
    public interface IOsrmService
    {
        Task<(double? Distance, List<Location> Locations, double? duration)> GetRoutePoints(double startLat, double startLng, double endLat, double endLng);
    }
}
