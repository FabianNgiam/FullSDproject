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
    public class CopiesController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public CopiesController(ApplicationDbContext context)
        public CopiesController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Copies
        [HttpGet]
        //Refactored
        //public async Task<ActionResult<IEnumerable<Copies>>> GetCopies()
        public async Task<IActionResult> GetCopies()
        {
            //Refactored
            //return await _context.Copies.ToListAsync();
            var Copies = await _unitOfWork.Copies.GetAll(includes: q => q.Include(x => x.Game).Include(x => x.User).Include(x => x.Stats).Include(x => x.Order));
            return Ok(Copies);
        }

        // GET: api/Copies/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Copies>> GetCopies(int id)
        public async Task<IActionResult> GetCopies(int id)
        {
            //Refactored
            //var Copies = await _context.Copies.FindAsync(id);
            var Copies = await _unitOfWork.Copies.Get(q => q.Id == id);

            if (Copies == null)
            {
                return NotFound();
            }

            return Ok(Copies);
        }

        // PUT: api/Copies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCopies(int id, Copy Copies)
        {
            if (id != Copies.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(Copies).State = EntityState.Modified;
            _unitOfWork.Copies.Update(Copies);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!CopiesExists(id))
                if (!await CopiesExists(id))
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

        // POST: api/Copies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Copy>> PostCopies(Copy Copies)
        {
            //Refactored
            //_context.Copies.Add(Copies);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Copies.Insert(Copies);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetCopies", new { id = Copies.Id }, Copies);
        }

        // DELETE: api/Copies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCopies(int id)
        {
            //Refactored
            //var Copies = await _context.Copies.FindAsync(id);
            var Copies = await _unitOfWork.Copies.Get(q => q.Id == id);
            if (Copies == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Copies.Remove(Copies);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Copies.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool CopiesExists(int id)
        private async Task<bool> CopiesExists(int id)
        {
            //Refactored
            //return _context.Copies.Any(e => e.Id == id);
            var Copies = await _unitOfWork.Copies.Get(q => q.Id == id);
            return Copies != null;
        }
    }
}
