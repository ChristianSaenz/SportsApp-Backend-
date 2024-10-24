using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models;

[Table("football_stats")]
public partial class FootballStat
{
    [Key]
    [Column("football_stat_id")]
    public long FootballStatId { get; set; }

    [Column("player_stat_id")]
    public long PlayerStatId { get; set; }

    [Column("passing_yards")]
    public int? PassingYards { get; set; }

    [Column("passing_touchdowns")]
    public int? PassingTouchdowns { get; set; }

    [Column("interceptions_thrown")]
    public int? InterceptionsThrown { get; set; }

    [Column("completion_percentage", TypeName = "decimal(5, 2)")]
    public decimal? CompletionPercentage { get; set; }

    [Column("rushing_yards")]
    public int? RushingYards { get; set; }

    [Column("rushing_touchdowns")]
    public int? RushingTouchdowns { get; set; }

    [Column("receiving_yards")]
    public int? ReceivingYards { get; set; }

    [Column("receptions")]
    public int? Receptions { get; set; }

    [Column("receiving_touchdowns")]
    public int? ReceivingTouchdowns { get; set; }

    [Column("sacks")]
    public int? Sacks { get; set; }

    [Column("field_goals")]
    public int? FieldGoals { get; set; }

    [Column("kick_return_yards")]
    public int? KickReturnYards { get; set; }

    [ForeignKey("PlayerStatId")]
    [InverseProperty("FootballStats")]
    public virtual PlayerStat PlayerStat { get; set; } = null!;
}
