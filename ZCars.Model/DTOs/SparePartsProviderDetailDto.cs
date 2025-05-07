using ZhooCars.Common;

namespace ZhooCars.Model.DTOs
{
    public class RegisterSparePartsProviderDto
    {
        #region Properties

        public SparePartsProviderDetailDto SparePartsProvider { get; set; }

        public UserDetailDto UserDetail { get; set; }

        #endregion
    }

    public class SparePartsProviderDetailDto
    {
        #region Properties

        public ApprovalStatus ApprovalStatus { get; set; }

        public string? CompanyName { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? Gstin { get; set; }

        public bool? IsActive { get; set; }

        public int? SparePartsProviderId { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int UserId { get; set; }

        #endregion
    }

    public class SparPartsListDto
    {
        #region Properties

        public string? Address { get; set; }

        public ApprovalStatus? ApprovalStatus { get; set; }

        public string? CompanyName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? PostalCode { get; set; }

        #endregion
    }

}
