using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsApp.Data;
using SportsApp.DTO_s;
using SportsApp.Models;
using System.Linq;
using System.Security.Claims;
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
        public async Task<ActionResult<IEnumerable<FavoriteDTO>>> GetFavorites()
        {
            var userEmail = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _context.Users
                .Include(u => u.Favorites)
                    .ThenInclude(f => f.Player)
                        .ThenInclude(p => p.Team)
                .FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return NotFound("User not found.");
            }


            var favoriteDTOs = user.Favorites.Select(f => new FavoriteDTO
            {
                FavoriteId = f.FavoriteId,
                PlayerId = f.PlayerId,
                FirstName = f.Player.Firstname,
                LastName = f.Player.Lastname,
                Age = f.Player.Age,
                Height = f.Player.Height,
                Weight = f.Player.Weight,
                Position = f.Player.Postion,
                TeamName = f.Player.Team.TeamName
            }).ToList();

            return Ok(favoriteDTOs);
        }


        [Authorize(Policy = "UserOnly")]
        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] AddFavoriteDTO favoriteDto)
        {
            if (favoriteDto == null || favoriteDto.PlayerId <= 0)
            {
                return BadRequest("Invalid favorite data.");
            }

            var player = await _context.Players.FindAsync(favoriteDto.PlayerId);
            if (player == null)
            {
                return NotFound("Player not found.");
            }

            var userEmail = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _context.Users.Include(u => u.Favorites).FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (user.Favorites.Any(f => f.PlayerId == player.PlayerId))
            {
                return BadRequest("Player is already a favorite.");
            }

            var favorite = new Favorite
            {
                PlayerId = player.PlayerId,
                UserId = user.UserId,
            };

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return Ok(new { favoriteId = favorite.FavoriteId });
        }




        [Authorize(Policy = "UserOnly")]
        [HttpDelete("{favoriteId}")]
        public async Task<IActionResult> RemoveFavorite(int favoriteId)
        {
            var userEmail = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
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



