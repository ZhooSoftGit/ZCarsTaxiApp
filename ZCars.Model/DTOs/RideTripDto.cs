namespace ZhooCars.Model.DTOs
{
    public class CancelTripDto
    {
        #region Properties

        public int RideTripId { get; set; }

        #endregion
    }

    public class ConfirmTripDto
    {
        #region Properties

        public int DriverId { get; set; }

        public int RideRequestId { get; set; }

        public int VehicleId { get; set; }

        #endregion
    }

    public class EndTripDto
    {
        #region Properties

        public double Distance { get; set; }

        public double EndLatitude { get; set; }

        public double EndLongitude { get; set; }

        public int RideTripId { get; set; }

        public double StartLatitude { get; set; }

        public double StartLongitude { get; set; }

        #endregion
    }

    public class RideTripDto
    {
        #region Properties

        public DateTime? CreatedAt { get; set; }

        public double? Distance { get; set; }

        public int DriverId { get; set; }

        public DateTime? EndTime { get; set; }

        public double? Fare { get; set; }

        public bool? IsActive { get; set; }

        public int RideRequestId { get; set; }

        public int RideTripId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int VehicleId { get; set; }

        #endregion
    }

    public class TripFareDto
    {
        #region Properties

        public double Distance { get; set; }

        public double Fare { get; set; }

        #endregion
    }

    public class TripPaymentDto
    {
        #region Properties

        public double Distance { get; set; }

        public double? Fare { get; set; }

        public int RideTripId { get; set; }

        public string Status { get; set; } = string.Empty;

        #endregion
    }

    public class UpdateTripDistanceDto
    {
        #region Properties

        public double Distance { get; set; }

        public double Fare { get; set; }

        public int RideTripId { get; set; }

        #endregion
    }

    public class UpdateTripStatusDto
    {
        #region Properties

        public int RideTripId { get; set; }

        public string? OTP { get; set; }

        #endregion
    }

}
