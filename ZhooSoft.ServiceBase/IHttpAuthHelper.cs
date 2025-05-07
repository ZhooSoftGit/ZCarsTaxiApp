using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhooSoft.ServiceBase
{
    public interface IHttpAuthHelper
    {
        Task<AuthInfo> GetUserAuthInfo();
    }
}
