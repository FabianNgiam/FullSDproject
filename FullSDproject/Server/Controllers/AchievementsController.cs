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
    public class AchievementsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public AchievementsController(ApplicationDbContext context)
        public AchievementsController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Achievements
        [HttpGet]
        //Refactored
        //public async Task<ActionResult<IEnumerable<Achievement>>> GetAchievements()
        public async Task<IActionResult> GetAchievements()
        {
            //Refactored
            //return await _context.Achievements.ToListAsync();
            var Achievements = await _unitOfWork.Achievements.GetAll();
            return Ok(Achievements);
        }

        // GET: api/Achievements/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Achievement>> GetAchievement(int id)
        public async Task<IActionResult> GetAchievement(int id)
        {
            //Refactored
            //var Achievement = await _context.Achievements.FindAsync(id);
            var Achievement = await _unitOfWork.Achievements.Get(q => q.Id == id);

            if (Achievement == null)
            {
                return NotFound();
            }

            return Ok(Achievement);
        }

        // PUT: api/Achievements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAchievement(int id, Achievement Achievement)
        {
            if (id != Achievement.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(Achievement).State = EntityState.Modified;
            _unitOfWork.Achievements.Update(Achievement);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!AchievementExists(id))
                if (!await AchievementExists(id))
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

        // POST: api/Achievements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Achievement>> PostAchievement(Achievement Achievement)
        {
            //Refactored
            //_context.Achievements.Add(Achievement);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Achievements.Insert(Achievement);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetAchievement", new { id = Achievement.Id }, Achievement);
        }

        // DELETE: api/Achievements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAchievement(int id)
        {
            //Refactored
            //var Achievement = await _context.Achievements.FindAsync(id);
            var Achievement = await _unitOfWork.Achievements.Get(q => q.Id == id);
            if (Achievement == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Achievements.Remove(Achievement);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Achievements.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool AchievementExists(int id)
        private async Task<bool> AchievementExists(int id)
        {
            //Refactored
            //return _context.Achievements.Any(e => e.Id == id);
            var Achievement = await _unitOfWork.Achievements.Get(q => q.Id == id);
            return Achievement != null;
        }
    }
}
