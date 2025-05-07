using ZhooCars.Common;
using ZhooCars.Model.DTOs;
using ZhooCars.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    #region Interfaces

    public interface IServiceRequestService
    {
        #region Methods

        Task<ApiResponse<bool>> AssignServiceRequestAsync(int ticketId, int providerId);

        Task<ApiResponse<ServiceRequestDto>> CreateServiceRequestAsync(CreateServiceRequestDto requestDto);

        Task<ApiResponse<PagedResponse<ServiceRequestDto>>> GetFilteredServiceRequestsAsync(ServiceRequestFilterDto filter);

        Task<ApiResponse<ServiceRequestDetailsDto>> GetServiceRequestDetailsAsync(int ticketId);

        Task<ApiResponse<bool>> NotifyNearbyProvidersAsync(int ticketId);

        Task<ApiResponse<bool>> UpdateServiceStatusAsync(int ticketId, ServiceRequestStatus status);

        #endregion
    }

    #endregion
}
