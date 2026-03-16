using Newtonsoft.Json;
using System.Text;

namespace SuperCchicLibrary.Service
{
    public class ApiProcessor
    {
        public static async Task<EmployeeDTO> Login(string username, string password)
        {
            string url = "Employees/Login";
            string json = JsonConvert.SerializeObject(new { username, password });
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<EmployeeDTO>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.ReasonPhrase}: {error}");
                }
            };
        }
        public static async Task<EmployeeDTO> LoginAdmin(string username, string password)
        {
            string url = "Employees/Login/Admin";
            string json = JsonConvert.SerializeObject(new { username, password });
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<EmployeeDTO>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.ReasonPhrase}: {error}");
                }
            };
        }
        public static async Task<List<Product>> GetProducts()
        {
            string url = "Products";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<Product>>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.ReasonPhrase}: {error}");
                }
            };
        }
        public static async Task<Product> PostProduct(Product product)
        {
            string url = $"Products";
            string json = JsonConvert.SerializeObject(product);
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Product>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.ReasonPhrase}: {error}");
                }
            }
        }
        public static async Task<Product> PutProduct(Product product)
        {
            string url = $"Products/{product.Id}";
            string json = JsonConvert.SerializeObject(product);
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Product>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.ReasonPhrase}: {error}");
                }
            }
            ;
        }
        public static async Task<Product> DeleteProduct(int id)
        {
            string url = $"Products/{id}";
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Product>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.ReasonPhrase}: {error}");
                }
            }
            ;
        }
        public static async Task<List<EmployeeDTO>> GetEmployees()
        {
            string url = "Employees";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<EmployeeDTO>>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.ReasonPhrase}: {error}");
                }
            };
        }
        public static async Task<List<Subcategory>> GetSubcategories()
        {
            string url = "Subcategories";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<Subcategory>>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.ReasonPhrase}: {error}");
                }
            };
        }
        public static async Task<Order> PostOrder(OrderDTO dto)
        {
            string url = "Orders";
            string json = JsonConvert.SerializeObject(dto);

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Order>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.ReasonPhrase}: {error}");
                }
            };
        }
        public static async Task<MonthlyReportDTO> GetReport()
        {
            string url = "Orders/Report";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<MonthlyReportDTO>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.ReasonPhrase}: {error}");
                }
            };
        }
    }
}
