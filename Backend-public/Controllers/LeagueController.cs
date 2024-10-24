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
    public class LeagueController : ControllerBase
    {
        private readonly SportsAppDbContext _context;

        public LeagueController(SportsAppDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<League>>> GetLeagues()
        {
            return await _context.Leagues.ToListAsync();
        }


        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<League>> GetLeague(int id)
        {
            var league = await _context.Leagues.FindAsync(id);

            if (league == null)
            {
                return NotFound();
            }

            return league;
        }

        [AllowAnonymous]
        [HttpGet("Sport/{sportId}/leagues")]
        public async Task<ActionResult<IEnumerable<League>>> GetLeaguesBySportId(int sportId)
        {
            var leagues = await _context.Leagues
                                        .Where(l => l.SportId == sportId)
                                        .ToListAsync();

            if (leagues == null || leagues.Count == 0)
            {
                return NotFound($"No leagues found for sport ID: {sportId}");
            }

            return Ok(leagues);
        }





        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult<League>> CreateLeague(League league)
        {
            _context.Leagues.Add(league);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLeague), new { id = league.LeagueId }, league);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeague(int id, League league)
        {
            if (id != league.LeagueId)
            {
                return BadRequest();
            }

            _context.Entry(league).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeagueExists(id))
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
        public async Task<IActionResult> DeleteLeague(int id)
        {
            var league = await _context.Leagues.FindAsync(id);
            if (league == null)
            {
                return NotFound();
            }

            _context.Leagues.Remove(league);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeagueExists(int id)
        {
            return _context.Leagues.Any(e => e.LeagueId == id);
        }
    }
}


