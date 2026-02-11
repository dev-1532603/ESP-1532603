using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCchicLibrary
{
    [Table("order_")]
    public class Order
    {
        [Key]
        [Column("id_order")]
        public int Id { get; set; }
        [Column("date_")]
        public DateTime Date { get; set; }
        [Column("total_price")]
        public double Total_Price { get; set; }
        [Column("id_employee")]
        [ForeignKey(nameof(Employee))]
        public int IdEmployee {  get; set; }
        public Employee? Employee { get; set; }

        public Order() { }

        public Order(int id, DateTime date, double total_Price, int idEmployee, Employee? employee)
        {
            Id = id;
            Date = date;
            Total_Price = total_Price;
            IdEmployee = idEmployee;
            Employee = employee;
        }
    }
}
