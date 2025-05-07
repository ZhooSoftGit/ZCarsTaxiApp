using ZhooCars.Common;

namespace ZhooCars.Model
{
    public class PhoneNumberDto
    {
        #region Properties
        public string? PhoneNumber { get; set; }

        #endregion
    }

    public class RefreshTokenDto
    {
        #region Properties

        public string? DeviceKey { get; set; }

        public string? IPAddress { get; set; }

        public required string RefreshToken { get; set; }

        public required int UserId { get; set; }

        #endregion
    }

    public class VerifyOtpDto
    {
        #region Properties

        public required string Code { get; set; }

        public string? DeviceKey { get; set; }

        public string? IPAddress { get; set; }

        public required string PhoneNumber { get; set; }

        #endregion
    }
}
