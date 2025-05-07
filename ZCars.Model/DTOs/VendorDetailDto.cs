using ZhooCars.Common;

namespace ZhooCars.Model.DTOs
{
    public class VendorDetailDto
    {
        #region Properties

        public ApprovalStatus ApprovalStatus { get; set; }

        public string? CompanyName { get; set; }

        public DateOnly? ContractEndDate { get; set; }

        public DateOnly? ContractStartDate { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? Description { get; set; }

        public string? Gstin { get; set; }

        public bool? IsActive { get; set; }

        public bool IsManualVerification { get; set; }

        public DateTime? UpdatedAt { get; set; }

        #endregion
    }

    public class VendorListDto
    {
        #region Properties

        public string CompanyName { get; set; }

        public DateTime? ContractEndDate { get; set; }

        public DateTime? ContractStartDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public string DocumentStatus { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string Gstin { get; set; }

        public bool IsActive { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Status { get; set; }

        public int UserId { get; set; }

        #endregion
    }
}
