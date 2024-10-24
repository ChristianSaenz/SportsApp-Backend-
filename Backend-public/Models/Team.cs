using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models;

[Table("team")]
public partial class Team
{
    [Key]
    [Column("team_id")]
    public long TeamId { get; set; }

    [Column("league_id")]
    public long LeagueId { get; set; }

    [Column("team_name")]
    [StringLength(50)]
    public string? TeamName { get; set; }

    [ForeignKey("LeagueId")]
    [InverseProperty("Teams")]
    public virtual League League { get; set; } = null!;

    [InverseProperty("AwayTeam")]
    public virtual ICollection<Match> MatchAwayTeams { get; set; } = new List<Match>();

    [InverseProperty("HomeTeam")]
    public virtual ICollection<Match> MatchHomeTeams { get; set; } = new List<Match>();

    [InverseProperty("Team")]
    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
