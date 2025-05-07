namespace ZhooCars.Model.DTOs
{
    public class PassengerRideHistoryDto
    {
        #region Properties

        public double? Distance { get; set; }

        public int DriverId { get; set; }

        public string DriverName { get; set; }

        public string DriverPhone { get; set; }

        public double? DropOffLatitude { get; set; }

        public string DropOffLocation { get; set; }

        public double? DropOffLongitude { get; set; }

        public DateTime? EndTime { get; set; }

        public decimal? Fare { get; set; }

        public bool OutstationReturn { get; set; }

        public double? PickUpLatitude { get; set; }

        public string PickUpLocation { get; set; }

        public double? PickUpLongitude { get; set; }

        public int? RentalHours { get; set; }

        public int RideRequestId { get; set; }

        public int RideStatus { get; set; }

        public int RideTripId { get; set; }

        public int RideTypeId { get; set; }

        public DateTime? StartTime { get; set; }

        public int TripStatus { get; set; }

        public int VehicleId { get; set; }

        public string VehicleName { get; set; }

        public string VehicleNumber { get; set; }

        public int VehicleTypeId { get; set; }

        #endregion
    }

}
