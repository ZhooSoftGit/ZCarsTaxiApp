using ZCarsDriver.Services.Contracts;
using ZhooCars.Model.DTOs.DriverApp;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Services
{
    public class VehicleLocationService : IVehicleLocationService
    {
        #region Fields

        private readonly IApiService _apiService;

        #endregion

        #region Constructors

        public VehicleLocationService(IApiService apiService)
        {
            _apiService = apiService;
        }

        #endregion

        #region Methods

        public async Task<ApiResponse<VehicleLocationDto>> GetVehicleLocationAsync(int vehicleId)
        {
            var url = $"{ApiConstants.GetVehicleLocation}/{vehicleId}";
            return await _apiService.GetAsync<VehicleLocationDto>(url);
        }

        public async Task<ApiResponse<bool>> UpdateVehicleLocationAsync(VehicleLocationDto location)
        {
            return await _apiService.PostAsync<bool>(ApiConstants.UpdateVehicleLocation, location);
        }

        public async Task<ApiResponse<bool>> UpsertVehicleLocation(VehicleLocationDto locationDto)
        {
            return await _apiService.PostAsync<bool>(ApiConstants.UpdateVehicleStatus, locationDto);
        }

        #endregion
    }
}
