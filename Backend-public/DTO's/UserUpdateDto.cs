using System.ComponentModel.DataAnnotations;

namespace SportsApp.DTO_s
{
    public class UserUpdateDto
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Lastname { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }
    }
}