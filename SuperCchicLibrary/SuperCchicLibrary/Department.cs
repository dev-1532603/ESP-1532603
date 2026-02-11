using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCchicLibrary
{
    [Table("department")]
    public class Department
    {
        [Key]
        [Column("id_department")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        public Department() { }
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
