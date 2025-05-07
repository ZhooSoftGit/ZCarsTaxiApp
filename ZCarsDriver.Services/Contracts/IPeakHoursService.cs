using ZhooCars.Model.DTOs;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    #region Interfaces

    public interface IPeakHoursService
    {
        #region Methods

        Task<ApiResponse<PeakHourDto>> AddPeakHourAsync(PeakHourDto peakHour);

        Task<ApiResponse<List<PeakHourDto>>> GetAllPeakHoursAsync();

        Task<ApiResponse<PeakHourDto>> GetPeakHourByIdAsync(int id);

        #endregion
    }

    #endregion
}
