using ZhooCars.Common;

namespace ZhooCars.Model.DTOs
{
    public class RegisterServiceProviderDto
    {
        #region Properties

        public ServiceProviderDetailDto ServiceProviderDetail { get; set; }

        public UserDetailDto UserDetailDto { get; set; }

        #endregion
    }

    public class ServiceProviderDetailDto
    {
        #region Properties

        public ApprovalStatus ApprovalStatus { get; set; }

        public string? CompanyName { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? Gstin { get; set; }

        public bool? IsActive { get; set; }

        public int? ServiceProviderId { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int UserId { get; set; }

        #endregion
    }

    public class ServiceProviderListDto
    {
        #region Properties

        public string Address { get; set; }

        public ApprovalStatus ApprovalStatus { get; set; }

        public string CompanyName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string PostalCode { get; set; }

        #endregion
    }

}
