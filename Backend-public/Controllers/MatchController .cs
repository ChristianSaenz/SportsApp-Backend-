using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using SportsApp.Data;
using SportsApp.Models;

namespace SportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly SportsAppDbContext _context;
        private readonly ILogger<SportController> _logger;

        public MatchController(SportsAppDbContext context, ILogger<SportController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        {
            return await _context.Matches.ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("today")]
        public async Task<IActionResult> GetTodayMatches()
        {
            var today = DateTime.UtcNow.Date;

            var matches = await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Where(m => m.MatchDate.Date == today)
                .Select(m => new
                {
                    m.MatchId,
                    HomeTeam = m.HomeTeam.TeamName,
                    AwayTeam = m.AwayTeam.TeamName,
                    m.MatchTime,
                    m.HomeScore,
                    m.AwayScore,
                    m.MatchStatus
                })
                .ToListAsync();


            if (matches == null )
            {
                _logger.LogWarning("No matches found for today");
                return Ok(new { message = "There are no matches today, Come back tommrrow." });
            }

            return Ok(matches);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);

            if (match == null)
            {
                return NotFound();
            }

            return match;
        }

        [AllowAnonymous]
        [HttpGet("league/{leagueId}")]
        public async Task<IActionResult> GetMatchByLeague(long leagueId)
        {
            var teamInLeague = await _context.Teams
                .Where(t => t.LeagueId == leagueId)
                .Select(t => t.TeamId)
                .ToListAsync();

            if (teamInLeague == null || teamInLeague.Count == 0)
            {
                return NotFound("No teams were found for this league");
            }

            var matches = await _context.Matches
                .Where(m => teamInLeague.Contains(m.HomeTeamId) || teamInLeague.Contains(m.AwayTeamId))
                .Select(m => new
                {
                    m.MatchId,
                    m.MatchDate,
                    m.MatchTime,
                    HomeTeam = m.HomeTeam.TeamName,
                    AwayTeam = m.AwayTeam.TeamName,
                    m.HomeScore,
                    m.AwayScore,
                    m.MatchStatus,
                })
                .ToListAsync();

            if (matches == null || matches.Count == 0)
            {
                return NotFound("No matches were found for this league");
            }

            return Ok(matches);

        }



        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult<Match>> CreateMatch(Match match)
        {
            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMatch), new { id = match.MatchId }, match);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatch(int id, Match match)
        {
            if (id != match.MatchId)
            {
                return BadRequest();
            }

            _context.Entry(match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
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

        [Authorize(Policy = "AdminOnly")] // Only admins can view user roles
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.MatchId == id);
        }
    }

}

