﻿using ZTaxiApp.Common;

namespace ZTaxiApp.Model.Request
{
    public class DocumentApprovalRequest
    {
        #region Properties

        public ApprovalStatus ApprovalStatus { get; set; }

        public int DocumentId { get; set; }

        public string? RejectionReason { get; set; }

        #endregion
    }

}
