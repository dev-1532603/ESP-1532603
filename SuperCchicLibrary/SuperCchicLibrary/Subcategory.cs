using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCchicLibrary
{
    [Table("subcategory")]
    public class Subcategory
    {
        [Key]
        [Column("id_subcategory")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("id_category")]
        [ForeignKey(nameof(Category))]
        public int IdCategory { get; set; }
        public Category? Category { get; set; }
        public Subcategory() { }
        public Subcategory(int id, string name, int idCategory, Category? category)
        {
            Id = id;
            Name = name;
            IdCategory = idCategory;
            Category = category;
        }
    }
}
