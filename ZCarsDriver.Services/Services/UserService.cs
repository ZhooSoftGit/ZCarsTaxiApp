using ZCarsDriver.Services.Contracts;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Services
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly IApiService _apiService;

        #endregion

        #region Constructors

        public UserService(IApiService apiService)
        {
            _apiService = apiService;
        }

        #endregion

        #region Methods

        public async Task<ApiResponse<UserDetailDto>> GetUserDashBoardDetailsAsync(string? phoneNumber = null)
        {
            var url = string.IsNullOrEmpty(phoneNumber) ? ApiConstants.GetUserDashBoardDetails : $"{ApiConstants.GetUserDashBoardDetails}?phoneNumber={phoneNumber}";
            return await _apiService.GetAsync<UserDetailDto>(url);
        }

        public async Task<ApiResponse<UserDetailDto>> GetUserDetailsAsync(string? phoneNumber = null)
        {
            var url = string.IsNullOrEmpty(phoneNumber) ? ApiConstants.GetUserDetails : $"{ApiConstants.GetUserDetails}?phoneNumber={phoneNumber}";
            return await _apiService.GetAsync<UserDetailDto>(url);
        }

        public async Task<ApiResponse<bool>> UpsertUserDetailsAsync(UserDetailDto userDetails, string? phoneNumber = null)
        {
            var url = string.IsNullOrEmpty(phoneNumber) ? ApiConstants.UpsertUserDetails : $"{ApiConstants.UpsertUserDetails}?phoneNumber={phoneNumber}";
            return await _apiService.PostAsync<bool>(url, userDetails);
        }

        #endregion
    }
}
