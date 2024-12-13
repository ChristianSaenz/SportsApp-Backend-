using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsApp.Models
{
    [Table("user_role")]
    public class UserRole
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }


        public User User { get; set; }
        public Role Role { get; set; }
    }
}
