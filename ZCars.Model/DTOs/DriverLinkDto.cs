using ZhooCars.Common;

namespace ZhooCars.Model.DTOs
{
    public class DriverVehicleLinkDto
    {
        #region Properties

        public ApprovalStatus ApprovalStatusId { get; set; }

        public int? ApprovedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsActive { get; set; }

        public int LinkId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int VehicleId { get; set; }

        #endregion
    }

    public class RequestVehicleLinkDto
    {
        #region Properties

        public int? UserId { get; set; }

        public int VehicleId { get; set; }

        public required string VendorPhoneNumber { get; set; }

        #endregion
    }

    public class VerifyVehicleLinkDto
    {
        #region Properties

        public int? DriverId { get; set; }

        public required string OTP { get; set; }

        public int VehicleId { get; set; }

        public required string VendorPhoneNumber { get; set; }

        #endregion
    }

}
