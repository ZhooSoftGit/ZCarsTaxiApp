using ZhooCars.Model.DTOs;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    #region Interfaces

    public interface ITaxiBookingService
    {
        #region Methods

        Task<ApiResponse<RideTripDto>> AcceptRideAsync(AcceptRideRequest request);

        Task<ApiResponse<RideRequestDto>> BookRideAsync(RideRequestModel request);

        Task<ApiResponse<FareDetailsModel>> CalculateFareAsync(FareCalculationRequest request);

        Task<ApiResponse<bool>> CancelRideRequestAsync(int rideRequestId);

        Task<ApiResponse<List<FareRequestModel>>> GetFareOptionsAsync(FareRequestModel request);

        Task<ApiResponse<List<AvailableCabModel>>> SearchAvailableCabsAsync(double latitude, double longitude);

        Task<ApiResponse<bool>> SkipBidAsync(UpdateBidRequestModel request);

        #endregion
    }

    #endregion

}
