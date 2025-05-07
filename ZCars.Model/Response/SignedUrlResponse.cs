namespace ZhooCars.Model.Response
{
    public class SignedUrlResponse
    {
        #region Properties

        public string? ContentType { get; set; }

        public string DocumentUrl { get; set; } = string.Empty;

        public DateTime Expiration { get; set; }

        public string HttpMethod { get; set; } = string.Empty;// "GET" or "PUT"

        public string Url { get; set; } = string.Empty;// The signed URL

        #endregion
    }

}
