using ZhooCars.Common;

namespace ZhooCars.Model.DTOs
{
    public class VehicleFilterDto
    {
        #region Properties

        public ApprovalStatus? ApprovalStatus { get; set; }

        // Pagination Fields
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public VehicleStatus? Status { get; set; }

        public string? VehicleRegistrationNumber { get; set; }

        public int? VehicleTypeId { get; set; }

        #endregion
    }
}
