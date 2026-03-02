using SuperCchicLibrary.Service;
using Newtonsoft.Json;
using SuperCchicLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCchicLibrary.Service
{
    public class ApiProcessor
    {
        public static Task<LoginResponseDTO> Login(string username, string password)
        {
            string url = "Employees/Login";
            string json = JsonConvert.SerializeObject(new { username, password });
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            using (Task<HttpResponseMessage> task = ApiHelper.ApiClient.SendAsync(request))
            {
                if (task.Result.IsSuccessStatusCode)
                {
                    return task.Result.Content.ReadAsAsync<LoginResponseDTO>();
                }
                else
                {
                    throw new Exception(task.Result.ReasonPhrase);
                }
            }
        }
        public static Task<List<Product>> GetProducts()
        {
            string url = "Products";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            using (Task<HttpResponseMessage> task = ApiHelper.ApiClient.SendAsync(request))
            {
                if (task.Result.IsSuccessStatusCode)
                {
                    return task.Result.Content.ReadAsAsync<List<Product>>();
                }
                else
                {
                    throw new Exception(task.Result.ReasonPhrase);
                }
            }
        }
        public static Task<Product> PostProduct(Product product)
        {
            string url = $"Products";
            string json = JsonConvert.SerializeObject(product);
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            using (Task<HttpResponseMessage> task = ApiHelper.ApiClient.SendAsync(request))
            {
                if (task.Result.IsSuccessStatusCode)
                {
                    return task.Result.Content.ReadAsAsync<Product>();
                }
                else
                {
                    throw new Exception(task.Result.ReasonPhrase);
                }
            }
        }
        public static Task<Product> PutProduct(Product product)
        {
            string url = $"Products/{product.Id}";
            string json = JsonConvert.SerializeObject(product);
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            using (Task<HttpResponseMessage> task = ApiHelper.ApiClient.SendAsync(request))
            {
                if (task.Result.IsSuccessStatusCode)
                {
                    return task.Result.Content.ReadAsAsync<Product>();
                }
                else
                {
                    throw new Exception(task.Result.ReasonPhrase);
                }
            }
        }
        public static Task DeleteProduct(int id)
        {
            string url = $"Products/{id}";
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            using (Task<HttpResponseMessage> task = ApiHelper.ApiClient.SendAsync(request))
            {
                if (task.Result.IsSuccessStatusCode)
                {
                    return Task.CompletedTask;
                }
                else
                {
                    throw new Exception(task.Result.ReasonPhrase);
                }
            }
        }

        public static Task<List<Product>> SearchProducts(string searchText)
        {
            string url = $"Products/Search/{searchText}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            using (Task<HttpResponseMessage> task = ApiHelper.ApiClient.SendAsync(request))
            {
                if (task.Result.IsSuccessStatusCode)
                {
                    return task.Result.Content.ReadAsAsync<List<Product>>();
                }
                else
                {
                    throw new Exception(task.Result.ReasonPhrase);
                }
            }
        }
        public static Task<List<EmployeeDTO>> GetEmployees()
        {
            string url = "Employees";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            using (Task<HttpResponseMessage> task = ApiHelper.ApiClient.SendAsync(request))
            {
                if (task.Result.IsSuccessStatusCode)
                {
                    return task.Result.Content.ReadAsAsync<List<EmployeeDTO>>();
                }
                else
                {
                    throw new Exception(task.Result.ReasonPhrase);
                }
            }
        }
        public static Task<List<Subcategory>> GetSubcategories()
        {
            string url = "Subcategories";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            using (Task<HttpResponseMessage> task = ApiHelper.ApiClient.SendAsync(request))
            {
                if (task.Result.IsSuccessStatusCode)
                {
                    return task.Result.Content.ReadAsAsync<List<Subcategory>>();
                }
                else
                {
                    throw new Exception(task.Result.ReasonPhrase);
                }
            }
        }
        public static Task<List<Category>> GetCategories()
        {
            string url = "Categories";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            using (Task<HttpResponseMessage> task = ApiHelper.ApiClient.SendAsync(request))
            {
                if (task.Result.IsSuccessStatusCode)
                {
                    return task.Result.Content.ReadAsAsync<List<Category>>();
                }
                else
                {
                    throw new Exception(task.Result.ReasonPhrase);
                }
            }
        }
        public static async Task<Order> PostOrder(OrderDTO dto)
        {
            string url = "Orders";
            string json = JsonConvert.SerializeObject(dto);

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await ApiHelper.ApiClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Order>();
            }

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"{response.ReasonPhrase}: {error}");
        }
    }
}
