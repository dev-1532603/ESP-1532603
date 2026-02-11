using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCchicLibrary
{
    [Table("employee")]
    public class Employee
    {
        [Key]
        [Column("id_employee")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        [Column("phone")]
        public string Phone {  get; set; } = string.Empty;
        [Column("salary_")]
        public decimal Salary { get; set; }
        [Column("designation")]
        public string Designation { get; set; } = string.Empty;
        [Column("hire_date")]
        public DateTime HireDate { get; set; }
        [Column("username")]
        public string Username {  get; set; } = string.Empty;
        [Column("password")]
        public string Password { get; set; } = string.Empty;
        [Column("id_department")]
        [ForeignKey(nameof(Department))]
        public int IdDepartment { get; set; }
        public Department? Department { get; set; }

        public Employee() { }
        public Employee(int id, string name, string email, string phone, decimal salary, string designation, DateTime hireDate, string username, string password, int idDepartment, Department? department)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Salary = salary;
            Designation = designation;
            HireDate = hireDate;
            Username = username;
            Password = password;
            IdDepartment = idDepartment;
            Department = department;
        }
    }
}
