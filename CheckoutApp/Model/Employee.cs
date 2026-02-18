using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CheckoutApp.Model
{
    public class Employee
    {
        [JsonPropertyName("Id")]
        public int? Id { get; set; }
        [JsonPropertyName("Username")]
        public string? Username { get; set; } = string.Empty;
        [JsonPropertyName("Name")]
        public string? Name { get; set; } = string.Empty;
    }
}
