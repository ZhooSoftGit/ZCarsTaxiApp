using ZCarsDriver.Services.Contracts;
using ZhooCars.Model.DTOs;
using ZhooCars.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Services
{
    public class VehicleDetailsService : IVehicleDetailsService
    {
        #region Fields

        private readonly IApiService _apiService;

        #endregion

        #region Constructors

        public VehicleDetailsService(IApiService apiService)
        {
            _apiService = apiService;
        }

        #endregion

        #region Methods

        public async Task<ApiResponse<VehicleDto>> GetVehicleByIdAsync(int id)
        {
            return await _apiService.GetAsync<VehicleDto>(ApiConstants.GetVehicleById.Replace("{id}", id.ToString()));
        }

        public async Task<ApiResponse<PagedResponse<VehicleDto>>> GetVehiclesByFilterAsync(VehicleFilterDto filterDto)
        {
            return await _apiService.PostAsync<PagedResponse<VehicleDto>>(ApiConstants.GetVehiclesByFilter, filterDto);
        }

        public async Task<ApiResponse<VehicleDto>> RegisterVehicleDetailsAsync(RegisterVehicleDto driverDto, string? phoneNumber = null)
        {
            var url = string.IsNullOrEmpty(phoneNumber) ? ApiConstants.RegisterVehicleDetails : $"{ApiConstants.RegisterVehicleDetails}?phoneNumber={phoneNumber}";
            return await _apiService.PostAsync<VehicleDto>(url, driverDto);
        }

        public async Task<ApiResponse<bool>> UpdateInsuranceAsync(int id, InsuranceUpdateDto insuranceDto)
        {
            var url = ApiConstants.UpdateInsurance.Replace("{id}", id.ToString());
            return await _apiService.PostAsync<bool>(url, insuranceDto);
        }

        public async Task<ApiResponse<VehicleDto>> UpsertVehicleAsync(VehicleDto vehicleDto, string? phoneNumber = null)
        {
            var url = string.IsNullOrEmpty(phoneNumber) ? ApiConstants.UpsertVehicle : $"{ApiConstants.UpsertVehicle}?phoneNumber={phoneNumber}";
            return await _apiService.PostAsync<VehicleDto>(url, vehicleDto);
        }

        #endregion
    }
}
