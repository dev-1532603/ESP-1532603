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
    public class Order_Details
    {
        [Key]
        [Column("id_order_details")]
        public int Id { get; set; }
        [Column("unit_price")]
        public decimal UnitPrice { get; set; }
        [Column("id_product")]
        [ForeignKey(nameof(Product))]
        public int IdProduct { get; set; }
        public Product? Product {  get; set; }
        [Column("id_order")]
        [ForeignKey(nameof(Order))]
        public int IdOrder { get; set; }
        public Order? Order { get; set; }

        public Order_Details() { }

        public Order_Details(int id, decimal unitPrice, int idProduct, Product? product, int idOrder, Order? order)
        {
            Id = id;
            UnitPrice = unitPrice;
            IdProduct = idProduct;
            Product = product;
            IdOrder = idOrder;
            Order = order;
        }
    }
}
