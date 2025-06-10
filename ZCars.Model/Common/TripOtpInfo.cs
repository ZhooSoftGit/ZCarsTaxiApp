using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTaxi.Model.Common
{
    public class TripOtpInfo
    {
        public string BookingRequestId { get; set; }
        public string StartOtp { get; set; }
        public string EndOtp { get; set; }
    }
}
