using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models;

[Table("ufc_stats")]
public partial class UfcStat
{
    [Key]
    [Column("ufc_stat_id")]
    public long UfcStatId { get; set; }

    [Column("player_stat_id")]
    public long PlayerStatId { get; set; }

    [Column("losses")]
    public int? Losses { get; set; }

    [Column("ko_tko_wins")]
    public int? KoTkoWins { get; set; }

    [Column("submission_wins")]
    public int? SubmissionWins { get; set; }

    [Column("decision_wins")]
    public int? DecisionWins { get; set; }

    [Column("significant_strikes_landed")]
    public int? SignificantStrikesLanded { get; set; }

    [Column("strikes_absorbed_per_minute", TypeName = "decimal(5, 2)")]
    public decimal? StrikesAbsorbedPerMinute { get; set; }

    [Column("takedown_accuracy", TypeName = "decimal(5, 2)")]
    public decimal? TakedownAccuracy { get; set; }

    [Column("takedown_defense", TypeName = "decimal(5, 2)")]
    public decimal? TakedownDefense { get; set; }

    [Column("submissions_attempted")]
    public int? SubmissionsAttempted { get; set; }

    [Column("average_fight_time", TypeName = "decimal(5, 2)")]
    public decimal? AverageFightTime { get; set; }

    [Column("reach", TypeName = "decimal(5, 2)")]
    public decimal? Reach { get; set; }

    [ForeignKey("PlayerStatId")]
    [InverseProperty("UfcStats")]
    public virtual PlayerStat PlayerStat { get; set; } = null!;
}
