using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models;

[Table("hockey_stats")]
public partial class HockeyStat
{
    [Key]
    [Column("hockey_stat_id")]
    public long HockeyStatId { get; set; }

    [Column("player_stat_id")]
    public long PlayerStatId { get; set; }

    [Column("penalty_minutes")]
    public int? PenaltyMinutes { get; set; }

    [Column("power_play_goals")]
    public int? PowerPlayGoals { get; set; }

    [Column("short_handed_goals")]
    public int? ShortHandedGoals { get; set; }

    [Column("hits")]
    public int? Hits { get; set; }

    [Column("faceoff_wins")]
    public int? FaceoffWins { get; set; }

    [ForeignKey("PlayerStatId")]
    [InverseProperty("HockeyStats")]
    public virtual PlayerStat PlayerStat { get; set; } = null!;
}
