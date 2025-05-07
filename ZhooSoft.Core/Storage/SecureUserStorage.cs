namespace ZhooSoft.Core.Storage
{
    public static class SecureUserStorage
    {
        private const string UserIdKey = "user_id";
        private const string UserTokenKey = "user_token";
        private const string UserRefreshTokenKey = "user_refresh_token";
        private const string UserNameKey = "user_name";

        public static void SaveUserDetails(string userId, string token, string name)
        {
            SecureStorage.SetAsync(UserIdKey, userId);
            SecureStorage.SetAsync(UserTokenKey, token);
            SecureStorage.SetAsync(UserRefreshTokenKey, token);
            SecureStorage.SetAsync(UserNameKey, name);
        }

        public static async Task<string> GetUserId() => await SecureStorage.GetAsync(UserIdKey);
        public static async Task<string> GetUserToken() => await SecureStorage.GetAsync(UserTokenKey);

        public static async Task<string> GetUserRefreshToken() => await SecureStorage.GetAsync(UserRefreshTokenKey);
        public static async Task<string> GetUserName() => await SecureStorage.GetAsync(UserNameKey);



        public static async Task SetUserId(string userId) => await SecureStorage.SetAsync(UserIdKey, userId);
        public static async Task SetUserToken(string token) => await SecureStorage.SetAsync(UserTokenKey, token);

        public static async Task SetUserRefreshToken(string refreshtoken) => await SecureStorage.SetAsync(UserRefreshTokenKey, refreshtoken);
        public static async Task SetUserName(string name) => await SecureStorage.SetAsync(UserNameKey, name);



        public static void ClearUserData()
        {
            SecureStorage.Remove(UserIdKey);
            SecureStorage.Remove(UserTokenKey);
            SecureStorage.Remove(UserNameKey);
            SecureStorage.Remove(UserRefreshTokenKey);
        }
    }
}
