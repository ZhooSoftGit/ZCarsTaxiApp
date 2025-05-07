using ZhooCars.Common;

namespace ZhooCars.Model.DTOs
{
    public class DriverInfo
    {
        #region Properties

        public ApprovalStatus ApprovalStatus { get; set; }

        public int DriverId { get; set; }

        public string KnownLanguage { get; set; }

        public DateTime? LicenseExpiryDate { get; set; }

        public string LicenseNumber { get; set; }

        #endregion
    }

    public class UserDashboardDto
    {
        #region Properties

        public DriverInfo? DriverDetails { get; set; }

        public UserInfo? User { get; set; }

        public VendorInfo? VendorDetails { get; set; }

        #endregion
    }

    public class UserInfo
    {
        #region Properties

        public string City { get; set; }

        public string Country { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public int RoleId { get; set; }

        public string Status { get; set; }

        public int UserId { get; set; }

        #endregion
    }

    public class VendorInfo
    {
        #region Properties

        public string CompanyName { get; set; }

        public DateTime? ContractEndDate { get; set; }

        public DateTime? ContractStartDate { get; set; }

        public string DocumentStatus { get; set; }

        public string Gstin { get; set; }

        public int VendorId { get; set; }

        #endregion
    }

}
