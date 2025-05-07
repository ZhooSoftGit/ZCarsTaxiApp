using ZCarsDriver.Services.Contracts;
using ZhooCars.Model.DTOs;
using ZhooCars.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Services
{
    public class ServiceProviderService : IServiceProviderService
    {
        #region Fields

        private readonly IApiService _apiService;

        #endregion

        #region Constructors

        public ServiceProviderService(IApiService apiService)
        {
            _apiService = apiService;
        }

        #endregion

        #region Methods

        public async Task<ApiResponse<PagedResponse<ServiceProviderListDto>>> GetByFilterAsync(ServiceProviderFilterDto filter)
        {
            return await _apiService.PostAsync<PagedResponse<ServiceProviderListDto>>($"{ApiConstants.BaseUrl}{ApiConstants.GetServiceProvidersByFilter}", filter);
        }

        public async Task<ApiResponse<ServiceProviderDetailDto>> GetByIdAsync(int id)
        {
            return await _apiService.GetAsync<ServiceProviderDetailDto>($"{ApiConstants.BaseUrl}{ApiConstants.GetServiceProviderById}/{id}");
        }

        public async Task<ApiResponse<bool>> RegisterAsync(int userId, RegisterServiceProviderDto requestDto)
        {
            return await _apiService.PostAsync<bool>($"{ApiConstants.BaseUrl}{ApiConstants.RegisterServiceProvider}", requestDto);
        }

        public async Task<ApiResponse<ServiceProviderDetailDto>> UpsertAsync(int userId, ServiceProviderDetailDto dto)
        {
            return await _apiService.PostAsync<ServiceProviderDetailDto>($"{ApiConstants.BaseUrl}{ApiConstants.UpdateServiceProvider}", dto);
        }

        #endregion
    }
}
