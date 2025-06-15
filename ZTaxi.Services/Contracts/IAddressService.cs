using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTaxi.Model.Response;

namespace ZTaxi.Services.Contracts
{
    public interface IAddressService
    {
        Task<List<SearchAddressResult>> GetAddressFromSearchAsync(string searchTerm);

        Task<string> GetPlaceNameAsync(double latitude, double longitude);
    }
}
