using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsApp.Data;
using SportsApp.Models;

namespace SportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseInfoController : ControllerBase
    {
        private readonly SportsAppDbContext _context;

        public CourseInfoController(SportsAppDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _context.CourseInfos.ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourseInfo(int id)
        {
            var courseInfo = await _context.CourseInfos.FindAsync(id);

            if (courseInfo == null)
            {
                return NotFound();
            }

            return courseInfo;
        }

        [AllowAnonymous]
        [HttpGet("{sportId}/course")]
        public async Task<ActionResult<List<Course>>> GetCourseBySportId(int sportId)
        {
            var courseInfo = await _context.CourseInfos
                                           .Where(c => c.SportId == sportId)
                                           .ToListAsync();

            if (courseInfo == null || courseInfo.Count == 0)
            {
                return NotFound($"No courses found for sport ID {sportId}");
            }

            return Ok(courseInfo);
        }


        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult<Course>> CreateCourseInfo(Course courseInfo)
        {
            _context.CourseInfos.Add(courseInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourseInfo), new { id = courseInfo.CourseId }, courseInfo);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourseInfo(int id, Course courseInfo)
        {
            if (id != courseInfo.CourseId)
            {
                return BadRequest();
            }

            _context.Entry(courseInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseInfoExists(id))
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
        public async Task<IActionResult> DeleteCourseInfo(int id)
        {
            var courseInfo = await _context.CourseInfos.FindAsync(id);
            if (courseInfo == null)
            {
                return NotFound();
            }

            _context.CourseInfos.Remove(courseInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseInfoExists(int id)
        {
            return _context.CourseInfos.Any(e => e.CourseId == id);
        }
    }
}




