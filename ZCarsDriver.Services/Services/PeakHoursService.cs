using ZCarsDriver.Services.Contracts;
using ZhooCars.Model.DTOs;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Services
{
    public class PeakHoursService : IPeakHoursService
    {
        #region Fields

        private readonly IApiService _apiService;

        #endregion

        #region Constructors

        public PeakHoursService(IApiService apiService)
        {
            _apiService = apiService;
        }

        #endregion

        #region Methods

        public async Task<ApiResponse<PeakHourDto>> AddPeakHourAsync(PeakHourDto peakHour)
        {
            return await _apiService.PostAsync<PeakHourDto>($"{ApiConstants.BaseUrl}{ApiConstants.AddPeakHour}", peakHour);
        }

        public async Task<ApiResponse<List<PeakHourDto>>> GetAllPeakHoursAsync()
        {
            return await _apiService.GetAsync<List<PeakHourDto>>($"{ApiConstants.BaseUrl}{ApiConstants.PeakHours}");
        }

        public async Task<ApiResponse<PeakHourDto>> GetPeakHourByIdAsync(int id)
        {
            return await _apiService.GetAsync<PeakHourDto>($"{ApiConstants.BaseUrl}{string.Format(ApiConstants.PeakHourById, id)}");
        }

        #endregion
    }
}
