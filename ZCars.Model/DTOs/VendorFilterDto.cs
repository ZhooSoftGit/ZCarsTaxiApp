using ZhooCars.Common;

namespace ZhooCars.Model.DTOs
{
    public class VendorFilterDto
    {
        #region Properties

        public ApprovalStatus ApprovalStatus { get; set; }

        public string? CompanyName { get; set; } = null;

        public DateTime? ContractEndDate { get; set; } = null;

        public DateTime? ContractStartDate { get; set; } = null;

        public int Page { get; set; } = 1;// Default to page 1

        public int PageSize { get; set; } = 10;// Default to 10 items per page

        public string? PhoneNumber { get; set; } = null;

        public string? Username { get; set; } = null;

        #endregion
    }

}
