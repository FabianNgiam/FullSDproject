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
    public class UsersController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public UsersController(ApplicationDbContext context)
        public UsersController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Users
        [HttpGet]
        //Refactored
        //public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        public async Task<IActionResult> GetUsers()
        {
            //Refactored
            //return await _context.Users.ToListAsync();
            var Users = await _unitOfWork.Users.GetAll();
            return Ok(Users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<User>> GetUser(int id)
        public async Task<IActionResult> GetUser(int id)
        {
            //Refactored
            //var User = await _context.Users.FindAsync(id);
            var User = await _unitOfWork.Users.Get(q => q.Id == id);

            if (User == null)
            {
                return NotFound();
            }

            return Ok(User);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User User)
        {
            if (id != User.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(User).State = EntityState.Modified;
            _unitOfWork.Users.Update(User);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!UserExists(id))
                if (!await UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User User)
        {
            //Refactored
            //_context.Users.Add(User);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Users.Insert(User);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetUser", new { id = User.Id }, User);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            //Refactored
            //var User = await _context.Users.FindAsync(id);
            var User = await _unitOfWork.Users.Get(q => q.Id == id);
            if (User == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Users.Remove(User);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Users.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool UserExists(int id)
        private async Task<bool> UserExists(int id)
        {
            //Refactored
            //return _context.Users.Any(e => e.Id == id);
            var User = await _unitOfWork.Users.Get(q => q.Id == id);
            return User != null;
        }
    }
}
