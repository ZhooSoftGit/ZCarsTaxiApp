using ZhooCars.Common;

namespace ZhooCars.Model.DTOs
{
    public class UpsertDocumentDto
    {
        #region Properties

        public int? DocumentId { get; set; }

        public string DocumentNo { get; set; } = string.Empty;

        public DocumentTypes DocumentTypeId { get; set; }

        public string DocumentUrl { get; set; } = string.Empty;

        public int? VehicleId { get; set; }

        #endregion
    }
}
