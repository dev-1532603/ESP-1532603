using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCchicLibrary
{
    [Table("category")]
    public class Category
    {
        [Key]
        [Column("id_category")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        public Category() { }
        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
