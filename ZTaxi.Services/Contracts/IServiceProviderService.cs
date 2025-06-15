using ZTaxiApp.Model.DTOs;
using ZTaxiApp.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZTaxiApp.Services.Contracts
{
    #region Interfaces

    public interface IServiceProviderService
    {
        #region Methods

        Task<ApiResponse<PagedResponse<ServiceProviderListDto>>> GetByFilterAsync(ServiceProviderFilterDto filter);

        Task<ApiResponse<ServiceProviderDetailDto>> GetByIdAsync(int id);

        Task<ApiResponse<bool>> RegisterAsync(int userId, RegisterServiceProviderDto requestDto);

        Task<ApiResponse<ServiceProviderDetailDto>> UpsertAsync(int userId, ServiceProviderDetailDto dto);

        #endregion
    }

    #endregion
}
