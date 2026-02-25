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
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int QuantityInStock { get; set; }
        public bool Taxable { get; set; }
        public int IdSubcategory { get; set; }
    }
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
