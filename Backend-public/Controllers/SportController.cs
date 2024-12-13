using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsApp.Data;
using SportsApp.Models;
using SportsApp.Helpers;
using Microsoft.AspNetCore.Authorization;


namespace SportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportController : ControllerBase
    {
        private readonly SportsAppDbContext _context;
        private readonly ILogger<SportController> _logger;

        public SportController(SportsAppDbContext context, ILogger<SportController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sport>>> GetSports()
        {
            _logger.LogInformation("Fetching all sports");
            try
            {
                var sports = await _context.Sports.ToListAsync();
                return Ok(sports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while fetching data.");
                return StatusCode(500, "Interal server error");
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Sport>> GetSport(long id)
        {
            _logger.LogInformation("Fetching sport with ID {SportId}.", id);

            try
            {
                var sport = await _context.Sports.FindAsync(id);

                if (sport == null)
                {
                    _logger.LogWarning("Sport with ID {SportId} not found.", id);
                    return NotFound();
                }

                return Ok(sport);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching sport with ID {SportId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult<Sport>> CreateSport(Sport sport)
        {
            _logger.LogInformation("Creating a new sport.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid Sport data recived.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.Sports.Add(sport);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Sport created with ID {SportId}.", sport.SportId);

                return CreatedAtAction(nameof(GetType), new { id = sport.SportId }, sport);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occuered while creating a new sport.");
                return StatusCode(500, "Internal server error.");
            }


        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSport(int id, Sport sport)
        {
            _logger.LogInformation("Updating sport with ID {SportId}.", id);

            if (id != sport.SportId)
            {
                _logger.LogWarning("ID in route does not match ID in sport object.");
                return BadRequest("ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid sport data received.");
                return BadRequest(ModelState);
            }

            _context.Entry(sport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Sport with ID {SportId} updated successfully.", id);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SportExists(id))
                {
                    _logger.LogWarning("Sport with ID {SportId} not found during update.", id);
                    return NotFound();
                }
                else
                {
                    _logger.LogError("Concurrency error occurred while updating sport with ID {SportId}.", id);
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating sport with ID {SportId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSport(int id)
        {
            _logger.LogInformation("Deleting sport with ID {SportId}.", id);
            try
            {
                var sport = await _context.Sports.FindAsync(id);
                if (sport == null)
                {
                    _logger.LogWarning("Sport with ID {SportId} not found for deletion.", id);
                    return NotFound();
                }
                _context.Sports.Remove(sport);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Sport with ID {SportId} deleted successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting sport with ID {SportId}.", id);
                return StatusCode(500, "Internal server error");
            }

        }
        private bool SportExists(long id)
        {
            return _context.Sports.Any(e => e.SportId == id);
        }
    }
}







