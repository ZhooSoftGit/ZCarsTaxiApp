using ZCarsDriver.Services.Contracts;
using ZhooCars.Model.DTOs;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Services
{
    public class DriverVehicleLinkService : IDriverVehicleLinkService
    {
        #region Fields

        private readonly IApiService _apiService;

        #endregion

        #region Constructors

        public DriverVehicleLinkService(IApiService apiService)
        {
            _apiService = apiService;
        }

        #endregion

        #region Methods

        public async Task<ApiResponse<bool>> DeleteVehicleLinkAsync(int linkId)
        {
            return await _apiService.PostAsync<bool>($"{ApiConstants.BaseUrl}{ApiConstants.DriverVehicleDeleteLink}", new { LinkId = linkId });
        }

        public async Task<ApiResponse<List<DriverVehicleLinkDto>>> GetLinkedVehicleAsync(int userId)
        {
            return await _apiService.GetAsync<List<DriverVehicleLinkDto>>($"{ApiConstants.BaseUrl}{ApiConstants.DriverVehicleGetLink}?userId={userId}");
        }

        public async Task<ApiResponse<List<VehicleDto>>> GetVehiclesByVendorPhoneNumberAsync(string vendorPhoneNumber)
        {
            return await _apiService.GetAsync<List<VehicleDto>>($"{ApiConstants.BaseUrl}{ApiConstants.DriverVehicleByVendor}?vendorPhoneNumber={vendorPhoneNumber}");
        }

        public async Task<ApiResponse<bool>> RequestVehicleLinkAsync(RequestVehicleLinkDto request)
        {
            return await _apiService.PostAsync<bool>($"{ApiConstants.BaseUrl}{ApiConstants.DriverVehicleRequestLink}", request);
        }

        public async Task<ApiResponse<bool>> VerifyVehicleLinkAsync(VerifyVehicleLinkDto request)
        {
            return await _apiService.PostAsync<bool>($"{ApiConstants.BaseUrl}{ApiConstants.DriverVehicleVerifyLink}", request);
        }

        #endregion
    }
}
