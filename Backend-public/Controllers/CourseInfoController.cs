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
    public class CourseInfoController : ControllerBase
    {
        private readonly SportsAppDbContext _context;
        private readonly ILogger<CourseInfoController> _logger;

        public CourseInfoController(SportsAppDbContext context, ILogger<CourseInfoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseInfo>>> GetCourses()
        {
            _logger.LogInformation("Fetching all courses.");
            try
            {
                var courses = await _context.CourseInfos.ToListAsync();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching courses.");
                return StatusCode(500, "Internal server error");
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseInfo>> GetCourseInfo(long id)
        {
            _logger.LogInformation("Fetching course info with ID {CourseId}.", id);
            try
            {
                var courseInfo = await _context.CourseInfos.FindAsync(id);

                if (courseInfo == null)
                {
                    _logger.LogWarning("Course info with ID {CourseId} not found.", id);
                    return NotFound();
                }

                return Ok(courseInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching course info with ID {CourseId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult<CourseInfo>> CreateCourseInfo(CourseInfo courseInfo)
        {
            _logger.LogInformation("Creating a new course info.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid course info data received.");
                return BadRequest(ModelState);
            }

            try
            {
                _context.CourseInfos.Add(courseInfo);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Course info created with ID {CourseId}.", courseInfo.CourseId);

                return CreatedAtAction(nameof(GetCourseInfo), new { id = courseInfo.CourseId }, courseInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new course info.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize(Policy = "AdminOnly")] 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourseInfo(long id, CourseInfo courseInfo)
        {
            _logger.LogInformation("Updating course info with ID {CourseId}.", id);

            if (id != courseInfo.CourseId)
            {
                _logger.LogWarning("ID in route does not match ID in course info object.");
                return BadRequest("ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid course info data received.");
                return BadRequest(ModelState);
            }

            _context.Entry(courseInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Course info with ID {CourseId} updated successfully.", id);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseInfoExists(id))
                {
                    _logger.LogWarning("Course info with ID {CourseId} not found during update.", id);
                    return NotFound();
                }
                else
                {
                    _logger.LogError("Concurrency error occurred while updating course info with ID {CourseId}.", id);
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating course info with ID {CourseId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(Policy = "AdminOnly")] 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseInfo(long id)
        {
            _logger.LogInformation("Deleting course info with ID {CourseId}.", id);
            try
            {
                var courseInfo = await _context.CourseInfos.FindAsync(id);
                if (courseInfo == null)
                {
                    _logger.LogWarning("Course info with ID {CourseId} not found for deletion.", id);
                    return NotFound();
                }

                _context.CourseInfos.Remove(courseInfo);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Course info with ID {CourseId} deleted successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting course info with ID {CourseId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        private bool CourseInfoExists(long id)
        {
            return _context.CourseInfos.Any(e => e.CourseId == id);
        }
    }
}


