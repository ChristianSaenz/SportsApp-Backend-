using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsApp.Data;
using SportsApp.Models;
using SportsApp.Helpers;
using System.Threading.Tasks;

namespace SportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SportsAppDbContext _context;
        private readonly TokenHelper _tokenHelper;

        public AuthController(SportsAppDbContext context, TokenHelper tokenService)
        {
            _context = context;
            _tokenHelper = tokenService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDto registrationDto)
        {
            // Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == registrationDto.Email))
            {
                return BadRequest("Email is already in use.");
            }

            var hashedPassword = PasswordHelper.HashPassword(registrationDto.Password);

            var newUser = new User
            {
                Username = registrationDto.Username,
                Email = registrationDto.Email,
                Password = hashedPassword,
                Firstname = registrationDto.Firstname,
                Lastname = registrationDto.Lastname
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var userRole = new UserRole
            {
                UserId = newUser.UserId,
                RoleId = await _context.Roles
                    .Where(r => r.RoleName == "User")
                    .Select(r => r.RoleId)
                    .FirstOrDefaultAsync()
            };

            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            var passwordIsValid = PasswordHelper.VerifyPassword(user.Password, loginDto.Password);
            if (!passwordIsValid)
            {
                return Unauthorized("Invalid Credentials");
            }

            var roles = user.UserRoles.Select(ur => ur.Role.RoleName).ToList();

            var token = _tokenHelper.GenerateToken(user.Email, roles, user.UserId.ToString());

            return Ok(new { token });
        }
    }
}
