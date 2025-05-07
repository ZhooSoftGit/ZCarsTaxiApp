using ZhooCars.Common;

namespace ZhooSoft.Core.Session
{
    public class UserSession
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public List<UserRoles> Roles { get; set; } = new();
        public bool HasRole(UserRoles role) => Roles.Contains(role);
    }
}
