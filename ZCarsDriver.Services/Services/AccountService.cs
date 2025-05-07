using ZCars.Model.Response;
using ZCarsDriver.Services;
using ZhooCars.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZhooCars.Services
{
    public class AccountService : BaseService, IAccountService
    {
        #region Fields

        private readonly IApiService _apiService;

        #endregion

        #region Constructors

        public AccountService(IApiService apiService)
        {
            _apiService = apiService;
        }

        #endregion

        #region Methods

        public async Task<ApiResponse<TokenResponse?>> RefreshTokenAsync(string refreshToken)
        {
            return await _apiService.PostAsync<TokenResponse?>($"{ApiConstants.BaseUrl}{ApiConstants.AccountRefreshToken}", new { RefreshToken = refreshToken });
        }

        public async Task<ApiResponse<bool>> ReSendOtpAsync(string phoneNumber)
        {
            return await _apiService.PostAsync<bool>($"{ApiConstants.BaseUrl}{ApiConstants.AccountReSendOtp}", new { PhoneNumber = phoneNumber });
        }

        public async Task<ApiResponse<bool>> SendOtpAsync(string phoneNumber)
        {
            return await _apiService.PostAsync<bool>($"{ApiConstants.BaseUrl}{ApiConstants.AccountSendOtp}", new { PhoneNumber = phoneNumber });
        }

        public async Task<ApiResponse<OTPResponse?>> VerifyOtpAsync(string phoneNumber, string otpCode)
        {
            var result = await _apiService.PostAsync<OTPResponse?>($"{ApiConstants.BaseUrl}{ApiConstants.AccountVerifyOtp}", new { PhoneNumber = phoneNumber, Code = otpCode });

            result.Data = new OTPResponse { TokenResponse = new TokenResponse { RefreshToken = "new Guid()", Token = "" } };

            return result;
        }

        #endregion
    }
}
