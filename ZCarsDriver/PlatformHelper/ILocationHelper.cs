using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCarsDriver.PlatformHelper
{
    public interface ILocationHelper
    {
        bool IsLocationEnabled();
        void OpenLocationSettings();
    }
}
