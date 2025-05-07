using System.Collections;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace ZhooSoft.ServiceBase
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpAuthHelper _authHelper;
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };
        private AuthInfo _authInfo { get; set; }

        public ApiService(HttpClient httpClient, IHttpAuthHelper authHelper)
        {
            _httpClient = httpClient;
            _authHelper = authHelper;
        }

        public async Task<ApiResponse<T>> GetAsync<T>(string url)
        {
            try
            {
                await Task.Delay(500); // Simulating API delay

                var dummyData = DummyDataGenerator.CreateDummy<T>();

                return new ApiResponse<T?>
                {
                    IsSuccess = true,
                    Data = dummyData
                };
                await SetAuthData();
                var response = await _httpClient.GetAsync(url);
                return await HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                return HandleException<T>(ex);
            }
        }

        public async Task<ApiResponse<T>> PostAsync<T>(string url, object data)
        {
            try
            {
                await Task.Delay(500); // Simulating API delay

                var dummyData = DummyDataGenerator.CreateDummy<T>();

                return new ApiResponse<T?>
                {
                    IsSuccess = true,
                    Data = dummyData
                };

                await SetAuthData();
                var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);
                return await HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                return HandleException<T>(ex);
            }
        }

        public async Task<ApiResponse<T>> PutAsync<T>(string url, object data)
        {
            try
            {
                await SetAuthData();
                var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(url, content);
                return await HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                return HandleException<T>(ex);
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(string url)
        {
            try
            {
                await SetAuthData();
                var response = await _httpClient.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse<bool> { IsSuccess = true, Data = true, Message = "Deleted successfully" };
                }
                else
                {
                    return new ApiResponse<bool> { IsSuccess = false, Data = false, Message = $"Error: {response.StatusCode}" };
                }
            }
            catch (Exception ex)
            {
                return HandleException<bool>(ex);
            }
        }

        private async Task<ApiResponse<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
                return new ApiResponse<T> { IsSuccess = true, Data = data, Message = "Success" };
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return new ApiResponse<T> { IsSuccess = false, Message = $"Error: {response.StatusCode}, Message: {errorMessage}" };
            }
        }

        private ApiResponse<T> HandleException<T>(Exception ex)
        {
            return new ApiResponse<T> { IsSuccess = false, Message = $"Exception: {ex.Message}" };
        }

        private async Task SetAuthData()
        {
            if (_authInfo == null)
            {
                _authInfo = await _authHelper.GetUserAuthInfo();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authInfo.Token);
            }
        }



        #region DUMMY DATA
        /// <summary>
        /// Generates a dummy instance of type T with default values.
        /// </summary>
        private T? GenerateDummyData<T>()
        {
            // If T is a primitive type, return a default value
            if (typeof(T) == typeof(string)) return (T?)(object)"dummy_string";
            if (typeof(T) == typeof(int)) return (T?)(object)123;
            if (typeof(T) == typeof(bool)) return (T?)(object)true;
            if (typeof(T) == typeof(double)) return (T?)(object)123.45;
            if (typeof(T) == typeof(Guid)) return (T?)(object)Guid.NewGuid();

            // If T is a class, create an instance and populate properties
            if (typeof(T).IsClass)
            {
                var instance = Activator.CreateInstance<T>();
                foreach (var prop in typeof(T).GetProperties())
                {
                    if (prop.CanWrite)
                    {
                        object? value = GenerateDummyData(prop.PropertyType);
                        prop.SetValue(instance, value);
                    }
                }
                return instance;
            }

            return default; // Return default if type is unknown
        }

        /// <summary>
        /// Generates a dummy value based on property type.
        /// </summary>
        private object? GenerateDummyData(Type type)
        {
            if (type == typeof(string)) return "dummy_string";
            if (type == typeof(int)) return 123;
            if (type == typeof(bool)) return true;
            if (type == typeof(double)) return 123.45;
            if (type == typeof(Guid)) return Guid.NewGuid();

            if (type.IsClass)
            {
                var instance = Activator.CreateInstance(type);
                foreach (var prop in type.GetProperties())
                {
                    if (prop.CanWrite)
                    {
                        object? value = GenerateDummyData(prop.PropertyType);
                        prop.SetValue(instance, value);
                    }
                }
                return instance;
            }

            return null;
        }

        #endregion


    }


public static class DummyDataGenerator
    {
        private static readonly Random _rand = new();

        public static T CreateDummy<T>()
        {
            return (T)CreateObject(typeof(T));
        }

        private static object CreateObject(Type type)
        {
            if (type == typeof(string))
                return $"Dummy_{Guid.NewGuid().ToString()[..8]}";

            if (type == typeof(int) || type == typeof(int?))
                return _rand.Next(1, 100);

            if (type == typeof(long) || type == typeof(long?))
                return (long)_rand.Next(1, 100000);

            if (type == typeof(double) || type == typeof(double?))
                return _rand.NextDouble() * 100;

            if (type == typeof(bool) || type == typeof(bool?))
                return _rand.Next(0, 2) == 1;

            if (type == typeof(DateTime) || type == typeof(DateTime?))
                return DateTime.Now.AddDays(-_rand.Next(0, 1000));

            if (type == typeof(Guid) || type == typeof(Guid?))
                return Guid.NewGuid();

            if (type.IsEnum)
            {
                var values = Enum.GetValues(type);
                return values.GetValue(_rand.Next(values.Length))!;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return CreateObject(Nullable.GetUnderlyingType(type)!);
            }

            if (typeof(IEnumerable).IsAssignableFrom(type) && type.IsGenericType)
            {
                var itemType = type.GetGenericArguments()[0];
                var listType = typeof(List<>).MakeGenericType(itemType);
                var list = (IList)Activator.CreateInstance(listType)!;
                for (int i = 0; i < 3; i++)
                    list.Add(CreateObject(itemType));
                return list;
            }

            if (type.IsClass || (type.IsValueType && !type.IsPrimitive))
            {
                var obj = Activator.CreateInstance(type)!;
                foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!prop.CanWrite) continue;
                    try
                    {
                        var value = CreateObject(prop.PropertyType);
                        prop.SetValue(obj, value);
                    }
                    catch
                    {
                        // Ignore if unsupported
                    }
                }
                return obj;
            }

            return Activator.CreateInstance(type)!;
        }
    }



}
