namespace ZhooCars.Model.Request
{
    public class CheckInRequest
    {
        #region Properties

        public double Latitude { get; set; }

        public required string Location { get; set; }

        public double Longitude { get; set; }

        public int? UserId { get; set; }

        public int VehicleId { get; set; }

        #endregion
    }

    public class CheckOutRequest
    {
        #region Properties

        public double Latitude { get; set; }

        public required string Location { get; set; }

        public double Longitude { get; set; }

        public int? UserId { get; set; }

        public int VehicleId { get; set; }

        #endregion
    }
}
