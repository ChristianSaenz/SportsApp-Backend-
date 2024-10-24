using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportsApp.Data;
using SportsApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly SportsAppDbContext _context;
        private readonly ILogger<TeamController> _logger;


        public TeamController(SportsAppDbContext context, ILogger<TeamController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeam()
        {
            _logger.LogInformation("Fetching all Teams");
            try
            {
                var teams = await _context.Teams.ToListAsync();
                return Ok(teams);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching data.");
                return StatusCode(500, "Internal server error");
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(long id)
        {
            _logger.LogInformation("Fetching team with ID {TeamId}.", id);

            try
            {
                var team = await _context.Teams.FindAsync(id);

                if (team == null)
                {
                    _logger.LogWarning("Team with ID {TeamId} not found.", id);
                    return NotFound();
                }

                return Ok(team);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching team with ID {TeamId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }


        [AllowAnonymous]
        [HttpGet("{leagueId}/teams")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeamsByLeagueId(int leagueId)
        {
            var teams = await _context.Teams
                                      .Where(t => t.LeagueId == leagueId)
                                      .ToListAsync();

            if (teams == null || teams.Count == 0)
            {
                return NotFound($"No teams found for league ID: {leagueId}");
            }

            return Ok(teams);
        }



        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult<Team>> CreateTeam(Team team)
        {
            _logger.LogInformation("Creating a new team.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid team data received.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.Teams.Add(team);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Team created with ID {TeamId}.", team.TeamId);

                return CreatedAtAction(nameof(GetTeam), new { id = team.TeamId }, team);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new team.");
                return StatusCode(500, "Internal server error.");
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(long id, Team team)
        {
            _logger.LogInformation("Updating team with ID {TeamId}.", id);

            if (id != team.TeamId)
            {
                _logger.LogWarning("ID in route does not match ID in team object.");
                return BadRequest("ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid team data received.");
                return BadRequest(ModelState);
            }

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Team with ID {TeamId} updated successfully.", id);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    _logger.LogWarning("Team with ID {TeamId} not found during update.", id);
                    return NotFound();
                }
                else
                {
                    _logger.LogError("Concurrency error occurred while updating team with ID {TeamId}.", id);
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating team with ID {TeamId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(long id)
        {
            _logger.LogInformation("Deleting team with ID {TeamId}.", id);
            try
            {
                var team = await _context.Teams.FindAsync(id);
                if (team == null)
                {
                    _logger.LogWarning("Team with ID {TeamId} not found for deletion.", id);
                    return NotFound();
                }
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Team with ID {TeamId} deleted successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting team with ID {TeamId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }
        private bool TeamExists(long id)
        {
            return _context.Teams.Any(e => e.TeamId == id);
        }

    }
}

