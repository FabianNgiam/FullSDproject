using FullSDproject.Server.IRepository;
using FullSDproject.Shared.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullSDproject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GamesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Games
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Game>>> GetGame()
        public async Task<IActionResult> GetGames()
        {
            var Games = await _unitOfWork.Games.GetAll();
            return Ok(Games);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGame(int id)
        {
            var Game = await _unitOfWork.Games.Get(q => q.Id == id);

            if (Game == null)
            {
                return NotFound();
            }

            return Ok(Game);
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game Game)
        {
            if (id != Game.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Game).State = EntityState.Modified;
            _unitOfWork.Games.Update(Game);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
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
        public async Task<ActionResult<Game>> PostGame(Game Game)
        {
            //_context.Game.Add(Game);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Games.Insert(Game);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetGame", new { id = Game.Id }, Game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            //var Game = await _context.Game.FindAsync(id);
            var Game = await _unitOfWork.Games.Get(q => q.Id == id);
            if (Game == null)
            {
                return NotFound();
            }

            //_context.Game.Remove(Game);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Games.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool GameExists(int id)
        private async Task<bool> GameExists(int id)
        {
            //return _context.Game.Any(e => e.Id == id);
            var Game = await _unitOfWork.Games.Get(q => q.Id == id);
            return Game != null;
        }
    }
}
