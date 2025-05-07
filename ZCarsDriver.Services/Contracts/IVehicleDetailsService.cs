using ZhooCars.Model.DTOs;
using ZhooCars.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    #region Interfaces

    public interface IVehicleDetailsService
    {
        #region Methods

        Task<ApiResponse<VehicleDto>> GetVehicleByIdAsync(int id);

        Task<ApiResponse<PagedResponse<VehicleDto>>> GetVehiclesByFilterAsync(VehicleFilterDto filterDto);

        Task<ApiResponse<VehicleDto>> RegisterVehicleDetailsAsync(RegisterVehicleDto driverDto, string? phoneNumber = null);

        Task<ApiResponse<bool>> UpdateInsuranceAsync(int id, InsuranceUpdateDto insuranceDto);

        Task<ApiResponse<VehicleDto>> UpsertVehicleAsync(VehicleDto vehicleDto, string? phoneNumber = null);

        #endregion
    }

    #endregion
}
