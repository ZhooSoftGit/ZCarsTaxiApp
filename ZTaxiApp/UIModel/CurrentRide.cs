﻿using ZTaxiApp.Common;
using ZTaxiApp.Model.DTOs;
using ZTaxi.Model.DTOs.UserApp;

namespace ZTaxiApp.UIModel
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
