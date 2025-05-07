using ZhooCars.Model.DTOs;
using ZhooCars.Model.Request;
using ZhooCars.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
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
