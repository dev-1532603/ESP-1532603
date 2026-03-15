using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperCchicAPI.Data.Context;
using SuperCchicLibrary;

namespace SuperCchicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly SuperCchicContext _context;

        public EmployeesController(SuperCchicContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<EmployeeDTO>> LoginEmployee([FromBody] LoginRequestDTO request)
        {
            if(string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Le username et le mot de passe sont requis.");
            }

            var existingEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.Username == request.Username && e.Password == request.Password);

            if (existingEmployee == null)
            {
                return Unauthorized("Les identifiants sont invalides.");
            }

            EmployeeDTO dto = new EmployeeDTO{ Id = existingEmployee.Id, Name = existingEmployee.Name, Username = existingEmployee.Username };

            return Ok(dto);

        }
        [HttpPost]
        [Route("Login/Admin")]
        public async Task<ActionResult<EmployeeDTO>> LoginAdmin([FromBody] LoginRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Le username et le mot de passe sont requis.");
            }

            var existingEmployee = await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Username == request.Username && e.Password == request.Password && e.IdDepartment == 8);

            if (existingEmployee == null)
            {
                return Unauthorized("Identifiants invalides ou l'utilisateur n'est pas administrateur.");
            }

            EmployeeDTO dto = new EmployeeDTO { Id = existingEmployee.Id, Name = existingEmployee.Name, Username = existingEmployee.Username };

            return Ok(dto);
        }
    }
}
