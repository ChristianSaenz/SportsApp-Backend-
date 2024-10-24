using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsApp.Data;
using SportsApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly SportsAppDbContext _context;

        public FavoriteController(SportsAppDbContext context)
        {
            _context = context;
        }

       
        [Authorize(Policy = "UserOnly")] 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetFavorites()
        {
            var userEmail = User.Identity.Name;
            var user = await _context.Users
                .Include(u => u.Favorites)
                .FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user.Favorites);
        }

       
        [Authorize(Policy = "UserOnly")] 
        [HttpPost]
        public async Task<IActionResult> AddFavorite(Favorite favorite)
        {
            var userEmail = User.Identity.Name; 
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Add favorite for the user
            user.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return Ok("Favorite added.");
        }

        
        [Authorize(Policy = "UserOnly")] 
        [HttpDelete("{favoriteId}")]
        public async Task<IActionResult> RemoveFavorite(int favoriteId)
        {
            var userEmail = User.Identity.Name;
            var user = await _context.Users
                .Include(u => u.Favorites)
                .FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var favorite = user.Favorites.FirstOrDefault(f => f.FavoriteId == favoriteId);
            if (favorite == null)
            {
                return NotFound("Favorite not found.");
            }

            user.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return Ok("Favorite removed.");
        }
    }
}


