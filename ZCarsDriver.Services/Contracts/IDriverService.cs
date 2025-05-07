using ZhooCars.Model.DTOs;
using ZhooCars.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    #region Interfaces

    public interface IDriverService
    {
        #region Methods

        Task<ApiResponse<DriverDetailDto>> GetDriverByIdAsync(int userId);

        Task<ApiResponse<PagedResponse<DriverListDto>>> GetDriversByFilterAsync(DriverFilterDto filter);

        Task<ApiResponse<bool>> RegisterDriverAsync(int userId, RegisterDriverDto driverDto);

        Task<ApiResponse<DriverDetailDto>> UpsertDriverAsync(int userId, DriverDetailDto driverDto);

        #endregion
    }

    #endregion
}
