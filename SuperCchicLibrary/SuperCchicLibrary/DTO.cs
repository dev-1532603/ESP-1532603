using SuperCchicLibrary;

namespace SuperCchicAPI.Models
{
    public class LoginRequestDTO
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
    public class LoginResponseDTO
    {
        public int? Id { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
    }

    public class ProductDTO
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public Subcategory? Subcategory { get; set; }
    }
    public class EmployeeDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
