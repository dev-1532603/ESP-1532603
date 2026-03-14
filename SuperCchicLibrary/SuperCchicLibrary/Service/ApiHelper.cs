
using System.Net.Http.Headers;
using Newtonsoft.Json;
namespace SuperCchicLibrary.Service
{
    public class ApiHelper
    {
        public static string CONFIG_PATH = "ApiConfig.json";

        private static string apiBaseAddress = string.Empty;
        public static HttpClient ApiClient { get; set; }

        public static async Task<bool> InitializeClient()
        {
            try
            {
                string? apiBaseAddress = await GetConfig();

                if (string.IsNullOrEmpty(apiBaseAddress))
                {
                    return false;
                }

                ApiClient?.Dispose();  
                ApiClient = new HttpClient();
                ApiClient.BaseAddress = new Uri(apiBaseAddress);
                ApiClient.DefaultRequestHeaders.Accept.Clear();
                ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ApiHelper init fail: {ex.Message}");
                return false;
            }

        }
        public static async Task<string?> GetConfig()
        {
            if (!File.Exists(CONFIG_PATH))
            {
                return null;
            }

            string content = await File.ReadAllTextAsync(CONFIG_PATH);
            try
            {
                return JsonConvert.DeserializeObject<string>(content) ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
        public static async Task<bool> SetupConfig(string url)
        {
            if (await GetApiStatus(url))
            {
                await File.WriteAllTextAsync(CONFIG_PATH, JsonConvert.SerializeObject(url));

                return true;
            }

            return false;
        }
        public static async Task<bool> GetApiStatus(string url)
        {
            try
            {
                HttpClient client = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Get, url);

                client.SendAsync(request);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
