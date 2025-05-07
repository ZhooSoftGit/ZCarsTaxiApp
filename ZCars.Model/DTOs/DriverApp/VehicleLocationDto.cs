using ZhooCars.Common;

namespace ZhooCars.Model.DTOs.DriverApp
{
    public class VehicleLocationDto
    {
        #region Properties

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DriverStatus? Status { get; set; }

        public int UserId { get; set; }

        public int VehicleId { get; set; }

        #endregion
    }
}
