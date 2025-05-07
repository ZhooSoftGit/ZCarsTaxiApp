namespace ZhooCars.Model.DTOs
{
    public class DriverRideHistoryDto
    {
        #region Properties

        public double Distance { get; set; }

        public string DropOffLocation { get; set; }

        public DateTime? EndTime { get; set; }

        public decimal Fare { get; set; }

        public string PaymentStatus { get; set; }

        public string PickUpLocation { get; set; }

        public int RideRequestId { get; set; }

        public int RideTripId { get; set; }

        public DateTime StartTime { get; set; }

        public string VehicleModel { get; set; }

        public string VehicleNumber { get; set; }

        #endregion
    }

}
