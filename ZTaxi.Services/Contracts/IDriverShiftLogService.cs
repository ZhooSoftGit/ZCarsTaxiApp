using ZTaxiApp.Model.DTOs;
using ZTaxiApp.Model.Request;
using ZTaxiApp.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZTaxiApp.Services.Contracts
{
    #region Interfaces

    public interface IDriverShiftLogService
    {
        #region Methods

        Task<ApiResponse<bool>> CheckInAsync(CheckInRequest request);

        Task<ApiResponse<bool>> CheckOutAsync(CheckOutRequest request);

        Task<ApiResponse<PagedResponse<DriverShiftLogDto>>> GetShiftLogsAsync(DateTime fromDate, DateTime toDate, int page, int pageSize);

        #endregion
    }

    #endregion
}
