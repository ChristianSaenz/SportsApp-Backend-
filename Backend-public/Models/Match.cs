using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models;

[Table("match")]
public partial class Match
{
    [Key]
    [Column("match_id")]
    public long MatchId { get; set; }

    [Column("home_team_id")]
    public long HomeTeamId { get; set; }

    [Column("away_team_id")]
    public long AwayTeamId { get; set; }

    [Column("match_date", TypeName = "datetime")]
    public DateTime MatchDate { get; set; }

    [Column("match_time", TypeName = "datetime")]
    public DateTime? MatchTime { get; set; }

    [Column("home_score")]
    public int? HomeScore { get; set; }

    [Column("away_score")]
    public int? AwayScore { get; set; }

    [Column("match_status")]
    [StringLength(50)]
    public string? MatchStatus { get; set; }

    [ForeignKey("AwayTeamId")]
    [InverseProperty("MatchAwayTeams")]
    public virtual Team AwayTeam { get; set; } = null!;

    [ForeignKey("HomeTeamId")]
    [InverseProperty("MatchHomeTeams")]
    public virtual Team HomeTeam { get; set; } = null!;

    [InverseProperty("Match")]
    public virtual ICollection<PlayerStat> PlayerStats { get; set; } = new List<PlayerStat>();
}
