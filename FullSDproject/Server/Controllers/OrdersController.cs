using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FullSDproject.Server.Data;
using FullSDproject.Shared.Domain;
using FullSDproject.Server.IRepository;

namespace FullSDproject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public OrdersController(ApplicationDbContext context)
        public OrdersController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Orders
        [HttpGet]
        //Refactored
        //public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        public async Task<IActionResult> GetOrders()
        {
            //Refactored
            //return await _context.Orders.ToListAsync();
            var Orders = await _unitOfWork.Orders.GetAll();
            return Ok(Orders);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Order>> GetOrder(int id)
        public async Task<IActionResult> GetOrder(int id)
        {
            //Refactored
            //var Order = await _context.Orders.FindAsync(id);
            var Order = await _unitOfWork.Orders.Get(q => q.Id == id);

            if (Order == null)
            {
                return NotFound();
            }

            return Ok(Order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order Order)
        {
            if (id != Order.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(Order).State = EntityState.Modified;
            _unitOfWork.Orders.Update(Order);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!OrderExists(id))
                if (!await OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order Order)
        {
            //Refactored
            //_context.Orders.Add(Order);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Orders.Insert(Order);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetOrder", new { id = Order.Id }, Order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            //Refactored
            //var Order = await _context.Orders.FindAsync(id);
            var Order = await _unitOfWork.Orders.Get(q => q.Id == id);
            if (Order == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Orders.Remove(Order);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Orders.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool OrderExists(int id)
        private async Task<bool> OrderExists(int id)
        {
            //Refactored
            //return _context.Orders.Any(e => e.Id == id);
            var Order = await _unitOfWork.Orders.Get(q => q.Id == id);
            return Order != null;
        }
    }
}
