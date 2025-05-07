using System.Text.Json;
using System.Text.Json.Serialization;
using ZhooCars.Common;
using ZhooSoft.Core.Session;

namespace ZCarsDriver.Services.Session
{
    public class UserSessionManager : IUserSessionManager
    {
        private const string UserSessionKey = "UserSession";
        private const string RefreshTokenKey = "RefreshToken";

        private string _phoneNumber { get; set; }
        private string _name { get; set; }

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            Converters = { new JsonStringEnumConverter() }, // Serialize enums as strings
            WriteIndented = true
        };

        public async Task SaveUserSessionAsync(UserSession session)
        {
            // Save non-sensitive data in Preferences
            var json = JsonSerializer.Serialize(session, JsonOptions);
            Preferences.Set(UserSessionKey, json);

            // Save refresh token in SecureStorage
            if (!string.IsNullOrEmpty(session.RefreshToken))
            {
                await SecureStorage.SetAsync(RefreshTokenKey, session.RefreshToken);
            }
        }

        public async Task<string?> GetUserPreference(string key)
        {
            return await SecureStorage.GetAsync(key);
        }

        public async Task SetUserPreference(string key, string value)
        {
            await SecureStorage.SetAsync(key, value);
        }

        public async Task<UserSession?> GetUserSessionAsync()
        {
            var json = Preferences.Get(UserSessionKey, string.Empty);
            if (string.IsNullOrEmpty(json)) return null;

            var session = JsonSerializer.Deserialize<UserSession>(json, JsonOptions);
            if (session != null)
            {
                session.RefreshToken = await SecureStorage.GetAsync(RefreshTokenKey) ?? string.Empty;
                _phoneNumber = session.PhoneNumber;
                _name = session.Name;
            }
            return session;
        }

        public void ClearSession()
        {
            Preferences.Remove(UserSessionKey);
            SecureStorage.Remove(RefreshTokenKey);
        }

        public string GetUserPhoneNumber()
        {
            return _phoneNumber;
        }

        public string GetUserName()
        {
            throw new NotImplementedException();
        }

        public List<UserRoles> GetUserRoles()
        {
            throw new NotImplementedException();
        }
    }

}
