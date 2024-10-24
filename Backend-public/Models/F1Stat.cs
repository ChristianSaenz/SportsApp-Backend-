using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models;

[Table("f1_stats")]
public partial class F1Stat
{
    [Key]
    [Column("f1_stat_id")]
    public long F1StatId { get; set; }

    [Column("player_stat_id")]
    public long PlayerStatId { get; set; }

    [Column("races_started")]
    public int? RacesStarted { get; set; }

    [Column("pole_positions")]
    public int? PolePositions { get; set; }

    [Column("podium_finishes")]
    public int? PodiumFinishes { get; set; }

    [Column("fastest_laps")]
    public int? FastestLaps { get; set; }

    [Column("average_finish_position", TypeName = "decimal(18, 0)")]
    public decimal? AverageFinishPosition { get; set; }

    [Column("dnfs")]
    public int? Dnfs { get; set; }

    [Column("laps_led")]
    public int? LapsLed { get; set; }

    [Column("pit_stops")]
    public int? PitStops { get; set; }

    [Column("pit_stop_time", TypeName = "decimal(5, 2)")]
    public decimal? PitStopTime { get; set; }

    [Column("championships_won")]
    public int? ChampionshipsWon { get; set; }

    [ForeignKey("PlayerStatId")]
    [InverseProperty("F1Stats")]
    public virtual PlayerStat PlayerStat { get; set; } = null!;
}
