using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models;

[Table("tennis_stats")]
public partial class TennisStat
{
    [Key]
    [Column("tennis_stat_id")]
    public long TennisStatId { get; set; }

    [Column("player_stat_id")]
    public long PlayerStatId { get; set; }

    [Column("aces")]
    public int? Aces { get; set; }

    [Column("double_faults")]
    public int? DoubleFaults { get; set; }

    [Column("first_serve_percentage", TypeName = "decimal(5, 2)")]
    public decimal? FirstServePercentage { get; set; }

    [Column("break_points_saved")]
    public int? BreakPointsSaved { get; set; }

    [Column("break_points_converted")]
    public int? BreakPointsConverted { get; set; }

    [Column("unforced_errors")]
    public int? UnforcedErrors { get; set; }

    [Column("sets_won")]
    public int? SetsWon { get; set; }

    [Column("sets_lost")]
    public int? SetsLost { get; set; }

    [Column("tiebreaks_won")]
    public int? TiebreaksWon { get; set; }

    [ForeignKey("PlayerStatId")]
    [InverseProperty("TennisStats")]
    public virtual PlayerStat PlayerStat { get; set; } = null!;
}
