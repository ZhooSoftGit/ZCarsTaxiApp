using ZhooCars.Model.DTOs;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    #region Interfaces

    public interface IRideTripService
    {
        #region Methods

        Task<ApiResponse<bool>> CancelTripAsync(CancelTripDto request);

        Task<ApiResponse<RideTripDto>> EndTripAsync(EndTripDto request);

        Task<ApiResponse<TripPaymentDto>> GetTripPaymentDetailsAsync(int rideTripId);

        Task<ApiResponse<bool>> ReachPickupAsync(UpdateTripStatusDto request);

        Task<ApiResponse<bool>> StartTripAsync(UpdateTripStatusDto request);

        Task<ApiResponse<bool>> UpdateDistanceAsync(UpdateTripDistanceDto request);

        #endregion
    }

    #endregion
}
