using ZCarsDriver.Services.Contracts;
using ZhooCars.Model.DTOs;
using ZhooCars.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Services
{
    public class SparePartsProviderService : ISparePartsProviderService
    {
        #region Fields

        private readonly IApiService _apiService;

        #endregion

        #region Constructors

        public SparePartsProviderService(IApiService apiService)
        {
            _apiService = apiService;
        }

        #endregion

        #region Methods

        public async Task<ApiResponse<PagedResponse<SparPartsListDto>>> GetByFilterAsync(SparePartsProviderFilterDto filter)
        {
            return await _apiService.PostAsync<PagedResponse<SparPartsListDto>>($"{ApiConstants.BaseUrl}{ApiConstants.SparePartsProvidersFilter}", filter);
        }

        public async Task<ApiResponse<SparePartsProviderDetailDto>> GetByIdAsync(int id)
        {
            return await _apiService.GetAsync<SparePartsProviderDetailDto>($"{ApiConstants.BaseUrl}{ApiConstants.SparePartsProvidersBase}/{id}");
        }

        public async Task<ApiResponse<bool>> RegisterAsync(int userId, RegisterSparePartsProviderDto requestDto)
        {
            return await _apiService.PostAsync<bool>($"{ApiConstants.BaseUrl}{ApiConstants.SparePartsProvidersRegister}?userId={userId}", requestDto);
        }

        public async Task<ApiResponse<SparePartsProviderDetailDto>> UpsertAsync(int userId, SparePartsProviderDetailDto dto)
        {
            return await _apiService.PostAsync<SparePartsProviderDetailDto>($"{ApiConstants.BaseUrl}{ApiConstants.SparePartsProvidersUpdate}?userId={userId}", dto);
        }

        #endregion
    }
}
