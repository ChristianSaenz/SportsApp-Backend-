using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsApp.Data;
using SportsApp.DTO_s;
using SportsApp.Models;
using System.Linq;
using System.Threading.Tasks;
using SportsApp.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SportsAppDbContext _context;
        private readonly ILogger<TeamController> _logger;

        public UserController(SportsAppDbContext context, ILogger<TeamController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Authorize(Policy = "UserOnly")] 
        [HttpGet("profile")]
        public async Task<ActionResult<User>> GetUserProfile()
        {
            foreach (var claim in User.Claims)
            {
                _logger.LogInformation($"Type: {claim.Type}, Value: {claim.Value}");
            }

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value
                ?? User.FindFirst(JwtRegisteredClaimNames.Email)?.Value
                ?? User.FindFirst("email")?.Value; 

            if (string.IsNullOrEmpty(userEmail))
            {
                _logger.LogWarning("Email not found.");
                return Unauthorized("Email claim not found.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        
        [Authorize(Policy = "UserOnly")]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateUserProfile(UserUpdateDto userUpdateDto)
        {
            foreach (var claim in User.Claims)
            {
                _logger.LogInformation($"Type: {claim.Type}, Value: {claim.Value}");
            }

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value
                ?? User.FindFirst(JwtRegisteredClaimNames.Email)?.Value
                ?? User.FindFirst("email")?.Value;
            
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("Email claim not found.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return NotFound("User not found.");
            }

        var hashedPassword = PasswordHelper.HashPassword(userUpdateDto.Password);

       
            user.Username = userUpdateDto.Username;
            user.Password = hashedPassword; 
            user.Email = userUpdateDto.Email;
            user.Firstname = userUpdateDto.Firstname;
            user.Lastname = userUpdateDto.Lastname;


            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Account updated successfully.");
        }

      
        [Authorize(Policy = "AdminOnly")] 
        [HttpGet("{userId}/roles")]
        public async Task<ActionResult<string>> GetUserRoles(long userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

           
            var roles = user.UserRoles.Select(ur => ur.Role.RoleName).ToList();
            return Ok(roles);
        }
    }
}

