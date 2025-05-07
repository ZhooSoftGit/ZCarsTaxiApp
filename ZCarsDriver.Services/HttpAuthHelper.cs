using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ZCarsDriver.Services;
using ZCarsDriver.Services.Session;
using ZhooSoft.ServiceBase;

namespace ZhooSoft.Core
{
    public class HttpAuthHelper : IHttpAuthHelper
    {
        private readonly IUserSessionManager _userSessionManager;

        public HttpAuthHelper(IUserSessionManager userSessionManager)
        {
            _userSessionManager = userSessionManager;
        }
        public async Task<AuthInfo> GetUserAuthInfo()
        {
            var session = await _userSessionManager.GetUserSessionAsync();

            if (session == null) return new AuthInfo();

            return new AuthInfo
            {
                RefreshToken = session.RefreshToken,
                Token = session.Token,
                UserId = session.PhoneNumber
            };
        }
    }
}
