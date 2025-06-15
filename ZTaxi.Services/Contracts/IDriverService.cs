using ZTaxiApp.Model.DTOs;
using ZTaxiApp.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZTaxiApp.Services.Contracts
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
