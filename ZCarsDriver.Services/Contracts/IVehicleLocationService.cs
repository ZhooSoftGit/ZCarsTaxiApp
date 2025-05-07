using ZhooCars.Model.DTOs.DriverApp;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    #region Interfaces

    public interface IVehicleLocationService
    {
        #region Methods

        Task<ApiResponse<VehicleLocationDto>> GetVehicleLocationAsync(int vehicleId);

        Task<ApiResponse<bool>> UpdateVehicleLocationAsync(VehicleLocationDto location);

        Task<ApiResponse<bool>> UpsertVehicleLocation(VehicleLocationDto locationDto);

        #endregion
    }

    #endregion
}
