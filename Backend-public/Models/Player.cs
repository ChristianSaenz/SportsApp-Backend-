using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models;

[Table("player")]
public partial class Player
{
    [Key]
    [Column("player_id")]
    public long PlayerId { get; set; }

    [Column("team_id")]
    public long TeamId { get; set; }

    [Column("firstname")]
    [StringLength(50)]
    public string? Firstname { get; set; }

    [Column("lastname")]
    [StringLength(10)]
    public string? Lastname { get; set; }

    [Column("position")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Postion { get; set; }

    [Column("age")]
    public int? Age { get; set; }

    [Column("nationality")]
    [StringLength(50)]
    public string? Nationality { get; set; }

    [Column("weight")]
    public int? Weight { get; set; }

    [Column("height")]
    public int? Height { get; set; }

    [InverseProperty("Player")]
    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    [ForeignKey("TeamId")]
    [InverseProperty("Players")]
    public virtual Team Team { get; set; } = null!;
}