using ZhooCars.Model.DTOs;
using ZhooCars.Model.Request;
using ZhooCars.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    #region Interfaces

    public interface IVendorService
    {
        #region Methods

        Task<ApiResponse<VendorDetailDto>> GetVendorByIdAsync(string? phoneNumber = null);

        Task<ApiResponse<PagedResponse<VendorListDto>>> GetVendorsByFilterAsync(VendorFilterDto filter);

        Task<ApiResponse<bool>> Register(string? phoneNumber, VendorRegisterRequest vendorRegisterRequest);

        Task<ApiResponse<VendorDetailDto>> UpsertVendorAsync(string? phoneNumber, VendorDetailDto dto);

        #endregion
    }

    #endregion

}
