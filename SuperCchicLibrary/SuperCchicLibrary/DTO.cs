
using System.Transactions;

namespace SuperCchicLibrary
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
    public class OrderDTO
    {
        public decimal TotalPrice { get; set; }
        public int EmployeeId { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; } = new();
    }
    public class OrderDetailDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
    public class EmployeeDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
