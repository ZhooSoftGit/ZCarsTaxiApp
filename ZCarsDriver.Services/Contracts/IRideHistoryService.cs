﻿using ZTaxiApp.Model.DTOs;
using ZhooSoft.ServiceBase;

namespace ZTaxiApp.Services.Contracts
{
    #region Interfaces

    public interface IRideHistoryService
    {
        #region Methods

        Task<ApiResponse<IEnumerable<DriverRideHistoryDto>>> GetDriverRideHistoryAsync(int userId, RideHistoryFilterDto filterDto);

        Task<ApiResponse<IEnumerable<PassengerRideHistoryDto>>> GetPassengerRideHistoryAsync(int userId, RideHistoryFilterDto filterDto);

        #endregion
    }

    #endregion
}
