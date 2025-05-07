using ZhooCars.Common;

namespace ZhooCars.Model.Request
{
    public class SignedUrlRequest
    {
        #region Properties

        public string BucketName { get; set; } = string.Empty;// The target bucket

        public string? ContentType { get; set; }

        public int ExpirationMinutes { get; set; } = 15;// Expiration time

        public DocumentHttpMethod HttpMethod { get; set; } // "GET" for download, "PUT" for upload

        public string ObjectKey { get; set; } = string.Empty;// File name (e.g., "documents/invoice.pdf")

        #endregion
    }

}
