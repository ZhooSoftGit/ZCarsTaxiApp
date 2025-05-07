using ZhooCars.Common;

namespace ZhooCars.Model.DTOs
{
    public class DriverFilterDto
    {
        #region Properties

        public ApprovalStatus ApprovalStatus { get; set; }

        public string? City { get; set; }

        public string? FirstName { get; set; }

        public bool? IsActive { get; set; }

        public string? KnownLanguage { get; set; }

        public string? LastName { get; set; }

        public string? LicenseNumber { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? PhoneNumber { get; set; }

        #endregion
    }

}
