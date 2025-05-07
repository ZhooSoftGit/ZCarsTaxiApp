using ZCars.Model.Response;
using ZhooCars.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZhooCars.Services
{
    #region Interfaces

    public interface IAccountService
    {
        #region Methods

        Task<ApiResponse<TokenResponse?>> RefreshTokenAsync(string refreshToken);

        Task<ApiResponse<bool>> ReSendOtpAsync(string phoneNumber);

        Task<ApiResponse<bool>> SendOtpAsync(string phoneNumber);

        Task<ApiResponse<OTPResponse?>> VerifyOtpAsync(string phoneNumber, string otpCode);

        #endregion
    }

    #endregion

}
