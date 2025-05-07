using ZhooCars.Common;

namespace ZhooCars.Model.DTOs
{
    public class DriverDetailDto
    {
        #region Properties

        public ApprovalStatus ApprovalStatus { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? DriverId { get; set; }

        public bool? IsActive { get; set; }

        public string? KnownLanguage { get; set; }

        public DateOnly? LicenseExpiryDate { get; set; }

        public string LicenseNumber { get; set; } = null!;

        public string? Status { get; set; }

        public DateTime? UpdatedAt { get; set; }

        #endregion
    }

    public class DriverListDto
    {
        #region Properties

        public ApprovalStatus? ApprovalStatus { get; set; }

        public string? City { get; set; }

        public int? DriverId { get; set; }

        public string? FirstName { get; set; }

        public string? Gender { get; set; }

        public string? KnownLanguage { get; set; }

        public string? LastName { get; set; }

        public string? LicenseNumber { get; set; }

        public string? PhoneNumber { get; set; }

        #endregion
    }

    public class RegisterDriverDto
    {
        #region Properties

        public required DriverDetailDto DriverDetailDto { get; set; }

        public required UserDetailDto UserDetailDto { get; set; }

        #endregion
    }
}
