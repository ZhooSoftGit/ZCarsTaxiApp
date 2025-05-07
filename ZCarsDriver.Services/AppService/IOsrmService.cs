using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCarsDriver.Services.AppService
{
    public interface IOsrmService
    {
        Task<(double? Distance, List<Location> Locations)> GetRoutePoints(double startLat, double startLng, double endLat, double endLng);
    }
}
