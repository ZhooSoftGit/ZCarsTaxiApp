using ZhooCars.Model.DTOs;
using ZhooCars.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    #region Interfaces

    public interface ISparePartsProviderService
    {
        #region Methods

        Task<ApiResponse<PagedResponse<SparPartsListDto>>> GetByFilterAsync(SparePartsProviderFilterDto filter);

        Task<ApiResponse<SparePartsProviderDetailDto>> GetByIdAsync(int id);

        Task<ApiResponse<bool>> RegisterAsync(int userId, RegisterSparePartsProviderDto requestDto);

        Task<ApiResponse<SparePartsProviderDetailDto>> UpsertAsync(int userId, SparePartsProviderDetailDto dto);

        #endregion
    }

    #endregion
}
