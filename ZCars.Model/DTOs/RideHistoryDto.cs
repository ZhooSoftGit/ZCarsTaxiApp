namespace ZhooCars.Model.DTOs
{
    public class CustomerDto
    {
        #region Properties

        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        #endregion
    }

    public class DriverDto
    {
        #region Properties

        public int DriverId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        #endregion
    }

    public class RideHistoryDto
    {
        #region Properties

        public CustomerDto CustomerDetails { get; set; }

        public double? Distance { get; set; }

        public DriverDto DriverDetails { get; set; }

        public string DropOffLocation { get; set; }

        public double? Fare { get; set; }

        public string PaymentStatus { get; set; }

        public string PickUpLocation { get; set; }

        public DateTime? RideDate { get; set; }

        public int RideTripId { get; set; }

        #endregion
    }
}
