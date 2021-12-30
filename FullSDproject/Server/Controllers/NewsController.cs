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
    public class NewsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public NewsController(ApplicationDbContext context)
        public NewsController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/News
        [HttpGet]
        //Refactored
        //public async Task<ActionResult<IEnumerable<New>>> GetNews()
        public async Task<IActionResult> GetNews()
        {
            //Refactored
            //return await _context.News.ToListAsync();
            var News = await _unitOfWork.News.GetAll();
            return Ok(News);
        }

        // GET: api/News/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<New>> GetNew(int id)
        public async Task<IActionResult> GetNew(int id)
        {
            //Refactored
            //var New = await _context.News.FindAsync(id);
            var New = await _unitOfWork.News.Get(q => q.Id == id);

            if (New == null)
            {
                return NotFound();
            }

            return Ok(New);
        }

        // PUT: api/News/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNew(int id, News New)
        {
            if (id != New.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(New).State = EntityState.Modified;
            _unitOfWork.News.Update(New);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!NewExists(id))
                if (!await NewExists(id))
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

        // POST: api/News
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<News>> PostNew(News New)
        {
            //Refactored
            //_context.News.Add(New);
            //await _context.SaveChangesAsync();
            await _unitOfWork.News.Insert(New);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetNew", new { id = New.Id }, New);
        }

        // DELETE: api/News/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNew(int id)
        {
            //Refactored
            //var New = await _context.News.FindAsync(id);
            var New = await _unitOfWork.News.Get(q => q.Id == id);
            if (New == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.News.Remove(New);
            //await _context.SaveChangesAsync();
            await _unitOfWork.News.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool NewExists(int id)
        private async Task<bool> NewExists(int id)
        {
            //Refactored
            //return _context.News.Any(e => e.Id == id);
            var New = await _unitOfWork.News.Get(q => q.Id == id);
            return New != null;
        }
    }
}
