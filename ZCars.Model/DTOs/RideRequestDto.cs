using ZhooCars.Common;

namespace ZhooCars.Model.DTOs
{
    public class RideRequestDto
    {
        #region Properties

        public DateTime? CreatedAt { get; set; }

        public double? DropOffLatitude { get; set; }

        public string DropOffLocation { get; set; } = null!;

        public double? DropOffLongitude { get; set; }

        public bool? IsActive { get; set; }

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

        public int VehicleTypeId { get; set; }

        #endregion
    }

}
