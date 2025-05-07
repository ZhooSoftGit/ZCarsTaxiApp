using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhooSoft.ServiceBase
{
    public interface IApiService
    {
        Task<ApiResponse<T>> GetAsync<T>(string url);
        Task<ApiResponse<T>> PostAsync<T>(string url, object data);
        Task<ApiResponse<T>> PutAsync<T>(string url, object data);
        Task<ApiResponse<bool>> DeleteAsync(string url);
    }

}
