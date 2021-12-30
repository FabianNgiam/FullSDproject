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
    public class GamesController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public GamesController(ApplicationDbContext context)
        public GamesController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Games
        [HttpGet]
        //Refactored
        //public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        public async Task<IActionResult> GetGames()
        {
            //Refactored
            //return await _context.Games.ToListAsync();
            var games = await _unitOfWork.Games.GetAll();
            return Ok(games);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Game>> GetGame(int id)
        public async Task<IActionResult> GetGame(int id)
        {
            //Refactored
            //var game = await _context.Games.FindAsync(id);
            var game = await _unitOfWork.Games.Get(q => q.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(game).State = EntityState.Modified;
            _unitOfWork.Games.Update(game);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!GameExists(id))
                if (!await GameExists(id))
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

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            //Refactored
            //_context.Games.Add(game);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Games.Insert(game);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            //Refactored
            //var game = await _context.Games.FindAsync(id);
            var game = await _unitOfWork.Games.Get(q => q.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Games.Remove(game);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Games.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool GameExists(int id)
        private async Task<bool> GameExists(int id)
        {
            //Refactored
            //return _context.Games.Any(e => e.Id == id);
            var game = await _unitOfWork.Games.Get(q => q.Id == id);
            return game != null;
        }
    }
}
