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
    public class StatsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public StatsController(ApplicationDbContext context)
        public StatsController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Stats
        [HttpGet]
        //Refactored
        //public async Task<ActionResult<IEnumerable<Stat>>> GetStats()
        public async Task<IActionResult> GetStats()
        {
            //Refactored
            //return await _context.Stats.ToListAsync();
            var Stats = await _unitOfWork.Stats.GetAll();
            return Ok(Stats);
        }

        // GET: api/Stats/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Stat>> GetStat(int id)
        public async Task<IActionResult> GetStat(int id)
        {
            //Refactored
            //var Stat = await _context.Stats.FindAsync(id);
            var Stat = await _unitOfWork.Stats.Get(q => q.Id == id);

            if (Stat == null)
            {
                return NotFound();
            }

            return Ok(Stat);
        }

        // PUT: api/Stats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStat(int id, Stats Stat)
        {
            if (id != Stat.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(Stat).State = EntityState.Modified;
            _unitOfWork.Stats.Update(Stat);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!StatExists(id))
                if (!await StatExists(id))
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

        // POST: api/Stats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Stats>> PostStat(Stats Stat)
        {
            //Refactored
            //_context.Stats.Add(Stat);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Stats.Insert(Stat);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetStat", new { id = Stat.Id }, Stat);
        }

        // DELETE: api/Stats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStat(int id)
        {
            //Refactored
            //var Stat = await _context.Stats.FindAsync(id);
            var Stat = await _unitOfWork.Stats.Get(q => q.Id == id);
            if (Stat == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Stats.Remove(Stat);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Stats.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool StatExists(int id)
        private async Task<bool> StatExists(int id)
        {
            //Refactored
            //return _context.Stats.Any(e => e.Id == id);
            var Stat = await _unitOfWork.Stats.Get(q => q.Id == id);
            return Stat != null;
        }
    }
}
