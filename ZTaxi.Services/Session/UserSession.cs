using ZTaxiApp.Common;

namespace ZhooSoft.Core.Session
{
    public class UserSession
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public List<UserRoles> Roles { get; set; }
        public bool HasRole(UserRoles role) => Roles.Contains(role);
    }
}
