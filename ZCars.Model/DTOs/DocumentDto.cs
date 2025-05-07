using ZhooCars.Common;

namespace ZhooCars.Model.DTOs
{
    public class DocumentDto
    {
        #region Properties

        public DateTime? ApprovalDate { get; set; }

        public ApprovalStatus ApprovalStatus { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int DocumentId { get; set; }

        public string DocumentNo { get; set; } = string.Empty;

        public DocumentTypes DocumentTypeId { get; set; }

        public string DocumentUrl { get; set; } = string.Empty;

        public bool? IsActive { get; set; }

        public string? RejectionReason { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? VehicleId { get; set; }

        #endregion
    }

}
