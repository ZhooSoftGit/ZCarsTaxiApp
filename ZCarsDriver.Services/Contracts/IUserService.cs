using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Contracts
{
    #region Interfaces

    public interface IUserService
    {
        #region Methods

        Task<ApiResponse<UserDetailDto>> GetUserDashBoardDetailsAsync(string? phoneNumber = null);

        Task<ApiResponse<UserDetailDto>> GetUserDetailsAsync(string? phoneNumber = null);

        Task<ApiResponse<bool>> UpsertUserDetailsAsync(UserDetailDto userDetails, string? phoneNumber = null);

        #endregion
    }

    #endregion
}
