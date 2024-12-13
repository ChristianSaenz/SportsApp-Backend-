using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsApp.Data;
using SportsApp.Models;

namespace SportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly SportsAppDbContext _context;

        public PlayerController(SportsAppDbContext context)
        {
            _context = context;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }


        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        [AllowAnonymous]
        [HttpGet("{teamId}/players")]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayersByTeamId(int teamId)  // Correct method name and parameter
        {
            var players = await _context.Players
                                         .Where(p => p.TeamId == teamId)
                                         .ToListAsync();

            if (players == null || players.Count == 0)
            {
                return NotFound($"No players found for team ID: {teamId}");
            }

            return Ok(players);
        }


        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult<Player>> CreatePlayer(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlayer), new { id = player.PlayerId }, player);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(int id, Player player)
        {
            if (id != player.PlayerId)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.PlayerId == id);
        }
    }


}
