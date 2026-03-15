using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperCchicAPI.Data.Context;
using SuperCchicLibrary;

namespace SuperCchicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly SuperCchicContext _context;

        public OrdersController(SuperCchicContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }
        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderDTO dto)
        {
            var order = new Order(dto.TotalPrice, dto.EmployeeId);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var orderDetails = new List<OrderDetails>();
            foreach (var od in dto.OrderDetails)
            {
                var detail = new OrderDetails(od.UnitPrice, od.Quantity, od.ProductId, order.Id);

                orderDetails.Add(detail);
            }

            _context.Order_Details.AddRange(orderDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }
    }
}
