using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SuperCchicLibrary
{
    [Table("order_details")]
    public class OrderDetails
    {
        [Key]
        [Column("id_order_details")]
        public int Id { get; set; }
        [Column("unit_price")]
        public decimal UnitPrice { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("id_product")]
        [ForeignKey(nameof(Product))]
        public int IdProduct { get; set; }
        public Product? Product {  get; set; }
        [Column("id_order")]
        [ForeignKey(nameof(Order))]
        public int IdOrder { get; set; }
        public Order? Order { get; set; }
        public OrderDetails() { }

        public OrderDetails(decimal unitPrice, int quantity, int productId, int orderId)
        {
            UnitPrice = unitPrice;
            Quantity = quantity;
            IdProduct = productId;
            IdOrder = orderId;
        }
    }
}
