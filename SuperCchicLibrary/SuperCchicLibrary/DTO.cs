
using System.Transactions;

namespace SuperCchicLibrary
{
    public class LoginRequestDTO
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
    public class ProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public bool Taxable { get; set; }
        public int IdSubcategory { get; set; }
    }
    public class OrderDTO
    {
        public decimal TotalPrice { get; set; }
        public int EmployeeId { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; } = new();
    }
    public class MonthlyReportDTO
    {
        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }
        public decimal AverageOrderValue { get; set; }
        public List<DailyReportDTO> DailyReports { get; set; } = new();
    }
    public class DailyReportDTO
    {
        public DayOfWeek DayOfWeek { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }
        public decimal AverageOrderValue { get; set; }
    }
    public class OrderDetailDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice {  get; set; }
    }
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

    }
}
