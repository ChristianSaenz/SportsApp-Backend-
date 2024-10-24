using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsApp.Data;
using SportsApp.Models;

namespace SportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerStatController : ControllerBase
    {
        private readonly SportsAppDbContext _context;

        public PlayerStatController(SportsAppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerStat>>> GetPlayerStats()
        {
            return await _context.PlayerStats.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerStat>> GetPlayerStat(int id)
        {
            var playerStat = await _context.PlayerStats.FindAsync(id);

            if (playerStat == null)
            {
                return NotFound();
            }

            return playerStat;
        }

        [HttpPost]
        public async Task<ActionResult<PlayerStat>> CreatePlayerStat(PlayerStat playerStat)
        {
            _context.PlayerStats.Add(playerStat);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlayerStat), new { id = playerStat.PlayerStatId }, playerStat);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayerStat(int id, PlayerStat playerStat)
        {
            if (id != playerStat.PlayerStatId)
            {
                return BadRequest();
            }

            _context.Entry(playerStat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerStatExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayerStat(int id)
        {
            var playerStat = await _context.PlayerStats.FindAsync(id);
            if (playerStat == null)
            {
                return NotFound();
            }

            _context.PlayerStats.Remove(playerStat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerStatExists(int id)
        {
            return _context.PlayerStats.Any(e => e.PlayerStatId == id);
        }
    }
}


