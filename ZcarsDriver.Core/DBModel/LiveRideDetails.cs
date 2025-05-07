using SQLite;
using ZhooCars.Common;

namespace ZCarsDriver.Core.DBModel
{
    public class LiveRideDetails
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public double? DropOffLatitude { get; set; }

        public string DropOffLocation { get; set; } = null!;

        public double? DropOffLongitude { get; set; }

        public bool? OutstationReturn { get; set; }

        public double? PickUpLatitude { get; set; }

        public string PickUpLocation { get; set; } = null!;

        public double? PickUpLongitude { get; set; }

        public int? RentalHours { get; set; }

        public int RideRequestId { get; set; }

        public int RideTypeId { get; set; }

        public RideStatus Status { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int UserId { get; set; }

        public double? Distance { get; set; }

        public double? Fare { get; set; }

        public int RideTripId { get; set; }

        public string CustomerName { get; set; } = null!;

        public string? CustomerPhoneNoLink { get; set; }
    }
}
