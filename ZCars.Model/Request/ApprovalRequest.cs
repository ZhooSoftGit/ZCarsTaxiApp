using ZhooCars.Common;

namespace ZhooCars.Model.Request
{
    public class ApprovalRequest
    {
        #region Properties

        public int EntityId { get; set; }

        public required string PhoneNumber { get; set; }

        public required UserRoles RoleType { get; set; }

        #endregion
    }

    public class RejectionRequest
    {
        #region Properties

        public int EntityId { get; set; }

        public required string PhoneNumber { get; set; }

        public required string RejectionReason { get; set; }

        public UserRoles RoleType { get; set; }

        #endregion
    }

}
