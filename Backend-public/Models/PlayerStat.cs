using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models;

[Table("player_stat")]
public partial class PlayerStat
{
    [Key]
    [Column("player_stat_id")]
    public long PlayerStatId { get; set; }

    [Column("player_id")]
    public long PlayerId { get; set; }

    [Column("match_id")]
    public long MatchId { get; set; }

    [Column("sport_id")]
    [StringLength(50)]
    public string? SportId { get; set; }

    [Column("goals")]
    public int? Goals { get; set; }

    [Column("assists")]
    public int? Assists { get; set; }

    [Column("points")]
    public int? Points { get; set; }

    [Column("tackles")]
    public int? Tackles { get; set; }

    [Column("rounds_played")]
    public int? RoundsPlayed { get; set; }

    [Column("wins")]
    public int? Wins { get; set; }

    [Column("shots_on_target")]
    public int? ShotsOnTarget { get; set; }

    [Column("minutes_played")]
    public int? MinutesPlayed { get; set; }

    [Column("interceptions")]
    public int? Interceptions { get; set; }

    [Column("fouls")]
    public int? Fouls { get; set; }

    [Column("saves")]
    public int? Saves { get; set; }

    [Column("clean_sheet")]
    public int? CleanSheet { get; set; }

    [Column("total_points")]
    public int? TotalPoints { get; set; }

    [Column("matches_played")]
    public int? MatchesPlayed { get; set; }

    [Column("goals_against_average")]
    [StringLength(10)]
    public string? GoalsAgainstAverage { get; set; }

    [Column("save_percentage")]
    [StringLength(10)]
    public string? SavePercentage { get; set; }

    [Column("goals_against")]
    public int? GoalsAgainst { get; set; }

    [InverseProperty("PlayerStat")]
    public virtual ICollection<BasketballStat> BasketballStats { get; set; } = new List<BasketballStat>();

    [InverseProperty("PlayerStat")]
    public virtual ICollection<F1Stat> F1Stats { get; set; } = new List<F1Stat>();

    [InverseProperty("PlayerStat")]
    public virtual ICollection<FootballStat> FootballStats { get; set; } = new List<FootballStat>();

    [InverseProperty("PlayerStat")]
    public virtual ICollection<GolfStat> GolfStats { get; set; } = new List<GolfStat>();

    [InverseProperty("PlayerStat")]
    public virtual ICollection<HockeyStat> HockeyStats { get; set; } = new List<HockeyStat>();

    [ForeignKey("MatchId")]
    [InverseProperty("PlayerStats")]
    public virtual Match Match { get; set; } = null!;

    [ForeignKey("PlayerId")]
    [InverseProperty("PlayerStats")]
    public virtual Player Player { get; set; } = null!;

    [ForeignKey("PlayerId")]
    [InverseProperty("PlayerStats")]
    public virtual Sport PlayerNavigation { get; set; } = null!;

    [InverseProperty("PlayerStat")]
    public virtual ICollection<SoccerStat> SoccerStats { get; set; } = new List<SoccerStat>();

    [InverseProperty("PlayerStat")]
    public virtual ICollection<TennisStat> TennisStats { get; set; } = new List<TennisStat>();

    [InverseProperty("PlayerStat")]
    public virtual ICollection<UfcStat> UfcStats { get; set; } = new List<UfcStat>();
}
