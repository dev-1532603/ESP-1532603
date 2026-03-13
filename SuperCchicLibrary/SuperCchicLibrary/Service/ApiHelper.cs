using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Text;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using System.Text.Json;
using Org.BouncyCastle.Bcpg;
using SixLabors.ImageSharp.PixelFormats;
using Newtonsoft.Json;
namespace SuperCchicLibrary.Service
{
    public class ApiHelper
    {
        public static string CONFIG_PATH = "ApiConfig.json";

        private static string apiBaseAddress = string.Empty;
        public static HttpClient ApiClient { get; set; }

        public static bool InitializeClient()
        {
            try
            {
                apiBaseAddress = GetConfig();

                if (string.IsNullOrEmpty(apiBaseAddress))
                {
                    return false;
                }

                ApiClient = new HttpClient();
                ApiClient.BaseAddress = new Uri(apiBaseAddress);
                ApiClient.DefaultRequestHeaders.Accept.Clear();
                ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return true;
            }
            catch (Exception ex) 
            {
                ex.ToString();
            }

            return false;
                
            //string localhost = "\"\"";
            
        }
        public static string GetConfig()
        {
            if (File.Exists(CONFIG_PATH))
            {
                string content = File.ReadAllText(CONFIG_PATH);
                return JsonConvert.DeserializeObject<string>(content) ?? string.Empty;
            }

            return string.Empty;
        }
        public static bool SetupConfig(string url)
        {
            if (GetApiStatus(url))
            {
                var jsonstring = JsonConvert.SerializeObject(url);
                File.WriteAllText(CONFIG_PATH, jsonstring);
                return true;
            }

            return false;
        }
        public static bool GetApiStatus(string url)
        {
            HttpClient client = new HttpClient();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                client.SendAsync(request);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
