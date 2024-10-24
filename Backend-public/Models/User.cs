using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models;

[Table("user")]
[Index("Username", Name = "UQ_username", IsUnique = true)]
public partial class User
{
    public User()
    {
        UserRoles = new HashSet<UserRole>();
    }

    [Key]
    [Column("user_id")]
    public long UserId { get; set; }

    [Column("username")]
    [StringLength(50)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Column("password")]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column("email")]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("firstname")]
    [StringLength(50)]
    public string Firstname { get; set; } = null!;

    [Column("lastname")]
    [StringLength(50)]
    public string Lastname { get; set; } = null!;

    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<Favorite> Favorites { get; set; }

}


// Need to add auth to user controller and favorites and make sure they work 
// If auth is working how its supposed to be then we can move onto connecting flutter to the backend