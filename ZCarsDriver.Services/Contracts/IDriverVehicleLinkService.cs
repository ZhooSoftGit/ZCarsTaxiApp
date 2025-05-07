using ZhooCars.Model.DTOs;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    #region Interfaces

    public interface IDriverVehicleLinkService
    {
        #region Methods

        Task<ApiResponse<bool>> DeleteVehicleLinkAsync(int linkId);

        Task<ApiResponse<List<DriverVehicleLinkDto>>> GetLinkedVehicleAsync(int userId);

        Task<ApiResponse<List<VehicleDto>>> GetVehiclesByVendorPhoneNumberAsync(string vendorPhoneNumber);

        Task<ApiResponse<bool>> RequestVehicleLinkAsync(RequestVehicleLinkDto request);

        Task<ApiResponse<bool>> VerifyVehicleLinkAsync(VerifyVehicleLinkDto request);

        #endregion
    }

    #endregion
}
