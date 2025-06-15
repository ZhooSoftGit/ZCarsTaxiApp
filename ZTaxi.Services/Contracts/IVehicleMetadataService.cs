using ZTaxiApp.Model.DTOs;
using ZhooSoft.ServiceBase;

namespace ZTaxiApp.Services.Contracts
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
