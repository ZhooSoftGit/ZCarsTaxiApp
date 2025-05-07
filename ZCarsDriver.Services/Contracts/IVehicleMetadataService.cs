using ZhooCars.Model.DTOs;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    #region Interfaces

    public interface IVehicleMetadataService
    {
        #region Methods

        Task<ApiResponse<IEnumerable<VehicleModelDto>>> GetVehicleModelsAsync();

        Task<ApiResponse<IEnumerable<VehicleModelDto>>> GetVehicleModelsByTypeAsync(int typeId);

        Task<ApiResponse<IEnumerable<VehicleTypeDto>>> GetVehicleTypesAsync();

        #endregion
    }

    #endregion
}
