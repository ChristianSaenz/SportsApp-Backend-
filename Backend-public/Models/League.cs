using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models;

[Table("league")]
public partial class League
{
    [Key]
    [Column("league_id")]
    public long LeagueId { get; set; }

    [Column("sport_id")]
    public long SportId { get; set; }

    [Column("league_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? LeagueName { get; set; }

    [ForeignKey("SportId")]
    [InverseProperty("Leagues")]
    public virtual Sport Sport { get; set; } = null!;

    [InverseProperty("League")]
    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
