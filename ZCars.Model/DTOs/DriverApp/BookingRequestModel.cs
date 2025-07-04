﻿using System.Text.Json.Serialization;
using ZTaxiApp.Common;

namespace ZTaxi.Model.DTOs.UserApp
{
    public class BookingRequestModel
    {
        public RideTypeEnum BookingType { get; set; }
        public string Fare { get; set; }
        public string DistanceAndPayment { get; set; }
        public double PickupLatitude { get; set; }
        public double PickupLongitude { get; set; }
        public string PickupAddress { get; set; }
        public string PickupTime { get; set; }
        public double DropLatitude { get; set; }
        public double DropLongitude { get; set; }
        public int RemainingBids { get; set; }
        public int BoookingRequestId { get; set; }
        public string? UserName { get; set; }

        public string DriverId { get; set; }

        public string UserId { get; set; }
        public string DropAddress { get; set; }
    }

    public class BookingResponseModel
    {
        [JsonPropertyName("driverId")]
        public string DriverId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
