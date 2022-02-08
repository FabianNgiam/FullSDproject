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
    public class PaymentsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public PaymentsController(ApplicationDbContext context)
        public PaymentsController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Payments
        [HttpGet]
        //Refactored
        //public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        public async Task<IActionResult> GetPayments()
        {
            //Refactored
            //return await _context.Payments.ToListAsync();
            var Payments = await _unitOfWork.Payments.GetAll(includes: q => q.Include(x => x.User));
            return Ok(Payments);
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Payment>> GetPayment(int id)
        public async Task<IActionResult> GetPayment(int id)
        {
            //Refactored
            //var Payment = await _context.Payments.FindAsync(id);
            var Payment = await _unitOfWork.Payments.Get(q => q.Id == id);

            if (Payment == null)
            {
                return NotFound();
            }

            return Ok(Payment);
        }

        // PUT: api/Payments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, Payment Payment)
        {
            if (id != Payment.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(Payment).State = EntityState.Modified;
            _unitOfWork.Payments.Update(Payment);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!PaymentExists(id))
                if (!await PaymentExists(id))
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

        // POST: api/Payments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(Payment Payment)
        {
            //Refactored
            //_context.Payments.Add(Payment);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Payments.Insert(Payment);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetPayment", new { id = Payment.Id }, Payment);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            //Refactored
            //var Payment = await _context.Payments.FindAsync(id);
            var Payment = await _unitOfWork.Payments.Get(q => q.Id == id);
            if (Payment == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Payments.Remove(Payment);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Payments.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool PaymentExists(int id)
        private async Task<bool> PaymentExists(int id)
        {
            //Refactored
            //return _context.Payments.Any(e => e.Id == id);
            var Payment = await _unitOfWork.Payments.Get(q => q.Id == id);
            return Payment != null;
        }
    }
}
