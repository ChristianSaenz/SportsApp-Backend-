using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models;

[Table("favorite")]
public partial class Favorite
{
    [Key]
    [Column("favorite_id")]
    public long FavoriteId { get; set; }

    [Column("user_id")]
    public long UserId { get; set; }

    [Column("player_id")]
    public long PlayerId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Favorites")]
    public virtual User User { get; set; } = null!;

    [ForeignKey("PlayerId")]
    [InverseProperty("Favorites")]
    public virtual Player Player { get; set; } = null!;
}
