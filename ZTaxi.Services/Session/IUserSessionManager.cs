using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTaxiApp.Common;
using ZhooSoft.Core.Session;

namespace ZTaxiApp.Services.Session
{
    public interface IUserSessionManager
    {
        Task SaveUserSessionAsync(UserSession session);
        Task<UserSession?> GetUserSessionAsync();
        void ClearSession();

        Task<string?> GetUserPreference(string key);

        Task SetUserPreference(string key, string value);

        string GetUserPhoneNumber();

        string GetUserName();
    }
}
