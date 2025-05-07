using ZCarsDriver.Services.Contracts;
using ZhooCars.Model.DTOs;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Services
{
    public class RideTripService : IRideTripService
    {
        #region Fields

        private readonly IApiService _apiService;

        #endregion

        #region Constructors

        public RideTripService(IApiService apiService)
        {
            _apiService = apiService;
        }

        #endregion

        #region Methods

        public async Task<ApiResponse<bool>> CancelTripAsync(CancelTripDto request)
        {
            return await _apiService.PostAsync<bool>($"{ApiConstants.BaseUrl}{ApiConstants.CancelTrip}", request);
        }

        public async Task<ApiResponse<RideTripDto>> EndTripAsync(EndTripDto request)
        {
            return await _apiService.PostAsync<RideTripDto>($"{ApiConstants.BaseUrl}{ApiConstants.EndTrip}", request);
        }

        public async Task<ApiResponse<TripPaymentDto>> GetTripPaymentDetailsAsync(int rideTripId)
        {
            return await _apiService.GetAsync<TripPaymentDto>($"{ApiConstants.BaseUrl}{ApiConstants.TripPayment}/{rideTripId}");
        }

        public async Task<ApiResponse<bool>> ReachPickupAsync(UpdateTripStatusDto request)
        {
            return await _apiService.PostAsync<bool>($"{ApiConstants.BaseUrl}{ApiConstants.ReachPickup}", request);
        }

        public async Task<ApiResponse<bool>> StartTripAsync(UpdateTripStatusDto request)
        {
            return await _apiService.PostAsync<bool>($"{ApiConstants.BaseUrl}{ApiConstants.StartTrip}", request);
        }

        public async Task<ApiResponse<bool>> UpdateDistanceAsync(UpdateTripDistanceDto request)
        {
            return await _apiService.PostAsync<bool>($"{ApiConstants.BaseUrl}{ApiConstants.UpdateDistance}", request);
        }

        #endregion
    }
}
