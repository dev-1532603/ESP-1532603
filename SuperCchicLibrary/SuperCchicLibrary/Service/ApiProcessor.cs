using CheckoutApp.Service;
using Newtonsoft.Json;
using SuperCchicAPI.Models;
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
        public static Task<List<ProductDTO>> GetProducts()
        {
            string url = "Products";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            using (Task<HttpResponseMessage> task = ApiHelper.ApiClient.SendAsync(request))
            {
                if (task.Result.IsSuccessStatusCode)
                {
                    return task.Result.Content.ReadAsAsync<List<ProductDTO>>();
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
    }
}
