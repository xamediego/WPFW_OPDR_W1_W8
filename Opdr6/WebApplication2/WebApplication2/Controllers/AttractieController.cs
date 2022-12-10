using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttractieController : ControllerBase
    {
        private readonly DatabaseContext _context;


        public AttractieController(DatabaseContext context)
        {
            _context = context;
        }


        [Authorize]
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Attractie>>> GetAttracties()
        {
            return await _context.Attracties.ToListAsync();
        }

        [Authorize(Roles = "Employee")]
        [HttpGet("GetAllEager")]
        public async Task<ActionResult<IEnumerable<Attractie>>> GetAttractieAndLikes()
        {
            return await _context.Attracties.Include(a => a.Likes).ToListAsync();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Attractie>> GetAttractie(int id)
        {
            var attractie = await _context.Attracties.FindAsync(id);

            if (attractie == null)
            {
                return NotFound();
            }

            return attractie;
        }

        [Authorize(Roles = "Employee")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutAttractie(int id, Attractie attractie)
        {
            if (id != attractie.Id)
            {
                return BadRequest();
            }

            _context.Entry(attractie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttractieExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        [Authorize(Roles = "Employee")]
        [HttpPost("Add")]
        public async Task<ActionResult<Attractie>> PostAttractie(Attractie attractie)
        {
            _context.Attracties.Add(attractie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttractie", new { id = attractie.Id }, attractie);
        }

        [Authorize(Roles = "Employee")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAttractie(int id)
        {
            var attractie = await _context.Attracties.FindAsync(id);
            if (attractie == null)
            {
                return NotFound();
            }

            _context.Attracties.Remove(attractie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize]
        [HttpPut("LikeById/Att/{attractieId}")]
        public async Task<IActionResult> LikeAttractie(int attractieId)
        {
            var attractie = await _context.Attracties.Include(a => a.Likes).Where(a => a.Id == attractieId)
                .FirstAsync();

            if (attractie == null)
            {
                Console.WriteLine("No Attractie");
                return NotFound();
            }
            
            Console.WriteLine("ID: " + _context.Users
                .FirstOrDefault(u => u.UserName == User.Identity.Name).Id);
            
            //couldnt find a way to get id from jwt bearer
            var user = await _context.Users.FindAsync(_context.Users
                .FirstOrDefault(u => u.UserName == User.Identity.Name).Id);

            if (user == null)
            {
                Console.WriteLine("No User");
                return NotFound();
            }
            
            if (attractie.Likes.FirstOrDefault(l => l.UserId == attractieId) != null)
            {
                Console.WriteLine("Already Liked");
                return BadRequest();
            }

            var like = new UserLikedAttraction();
            like.User = user;
            like.Attractie = attractie;

            _context.Likes.Add(like);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpDelete("LikeById/Att/{attractieId}")]
        public async Task<IActionResult> Unlike(int attractieId)
        {
            var attractie = await _context.Attracties.Include(a => a.Likes).Where(a => a.Id == attractieId)
                .FirstAsync();

            if (attractie.Likes.FirstOrDefault(l => l.UserId == attractieId) == null)
            {
                Console.WriteLine("No like found");
                return NotFound();
            }
            
            //couldnt find a way to get id from jwt bearer
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            
            var like = attractie.Likes
                .FirstOrDefault(l => l.AttractieId == user.Id);
            
            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();

            return Ok();
        }
        
        [HttpGet("GetLikes/{id}")]
        public async Task<ActionResult<int>> GetAttracties(int id)
        {
            var attractie = await _context.Attracties.Include(a => a.Likes).Where(a => a.Id == id)
                .FirstAsync();
            
            return attractie.aantalLikes();
        }
        
        [HttpGet("FindByName")]
        public async Task<ActionResult<Attractie>> GetAttracties(string name)
        {
            var attractie = await _context.Attracties.Where(a => a.Name.Contains(name))
                .FirstAsync();
            
            return attractie;
        }



        private bool AttractieExists(int id)
        {
            return _context.Attracties.Any(e => e.Id == id);
        }
    }
}