using ZCarsDriver.Services.Contracts;
using ZhooCars.Model.DTOs;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Services
{
    public class VehicleMetadataService : IVehicleMetadataService
    {
        #region Fields

        private readonly IApiService _apiService;

        #endregion

        #region Constructors

        public VehicleMetadataService(IApiService apiService)
        {
            _apiService = apiService;
        }

        #endregion

        #region Methods

        public async Task<ApiResponse<IEnumerable<VehicleModelDto>>> GetVehicleModelsAsync()
        {
            return await _apiService.GetAsync<IEnumerable<VehicleModelDto>>($"{ApiConstants.BaseUrl}{ApiConstants.VehicleModels}");
        }

        public async Task<ApiResponse<IEnumerable<VehicleModelDto>>> GetVehicleModelsByTypeAsync(int typeId)
        {
            return await _apiService.GetAsync<IEnumerable<VehicleModelDto>>($"{ApiConstants.BaseUrl}{string.Format(ApiConstants.VehicleModelsByType, typeId)}");
        }

        public async Task<ApiResponse<IEnumerable<VehicleTypeDto>>> GetVehicleTypesAsync()
        {
            return await _apiService.GetAsync<IEnumerable<VehicleTypeDto>>($"{ApiConstants.BaseUrl}{ApiConstants.VehicleTypes}");
        }

        #endregion
    }
}
