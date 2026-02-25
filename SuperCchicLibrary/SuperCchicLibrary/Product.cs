using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCchicLibrary
{
    [Table("product")]
    public class Product
    {
        [Key]
        [Column("id_product")]
        public int Id { get; set; }
        [Column("code")]
        public string Code { get; set; } = string.Empty;
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("price")]
        public decimal Price { get; set; }
        [Column("quantity_in_stock")]
        public int QuantityInStock { get; set; }
        [Column("taxable")]
        public bool Taxable { get; set; }
        [Column("id_subcategory")]
        [ForeignKey(nameof(Subcategory))]
        public int IdSubcategory { get; set; }
        public Subcategory? Subcategory { get; set; }

        public Product () { }
        public Product(int id, string code, string name, decimal price, int quantityInStock, bool taxable, int idSubcategory, Subcategory? subcategory)
        {
            Id = id;
            Code = code;
            Name = name;
            Price = price;
            QuantityInStock = quantityInStock;
            Taxable = taxable;
            IdSubcategory = idSubcategory;
            Subcategory = subcategory;
        }
    }
}
