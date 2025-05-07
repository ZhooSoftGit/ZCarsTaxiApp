using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhooSoft.ServiceBase
{
    public class AuthInfo
    {
        public string Token { get; set; }

        public string UserId { get; set; }

        public string RefreshToken { get; set; }
    }
}
