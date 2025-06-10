using ZTaxiApp.Common;
using ZTaxiApp.Model.DTOs;
using ZTaxi.Model.DTOs.UserApp;
using ZTaxi.Model.Common;

namespace ZTaxiApp.Model
{
    public class CurrentRide
    {
        private RideStatus _currentStatus = RideStatus.Assigned;

        public BookingRequestModel BookingRequest { get; set; }

        public RideTripDto RideDetails { get; set; }

        public RideStatus CurrentStatus
        {
            get
            {
                return _currentStatus;
            }
            set
            {
                _currentStatus = value;
                UpdateNextStatus();
            }
        }

        public TripOtpInfo TripOtpInfo { get; set; }

        private void UpdateNextStatus()
        {
            if (CurrentStatus == RideStatus.Assigned)
            {
                NextStatus = RideStatus.Reached;
            }
            else if (CurrentStatus == RideStatus.Reached)
            {
                NextStatus = RideStatus.Started;
            }
            else if (CurrentStatus == RideStatus.Started)
            {
                NextStatus = RideStatus.Completed;
            }
            else
            {
                NextStatus = CurrentStatus;
            }
        }

        public RideStatus NextStatus { get; set; } = RideStatus.Reached;
    }
}
