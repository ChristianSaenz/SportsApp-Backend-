using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models;

[Table("basketball_stats")]
public partial class BasketballStat
{
    [Key]
    [Column("basketball_stat_id")]
    public long BasketballStatId { get; set; }

    [Column("player_stat_id")]
    public long PlayerStatId { get; set; }

    [Column("rebounds_offensive")]
    public int? ReboundsOffensive { get; set; }

    [Column("rebounds_defensive")]
    public int? ReboundsDefensive { get; set; }

    [Column("field_goal_percentage", TypeName = "decimal(18, 0)")]
    public decimal? FieldGoalPercentage { get; set; }

    [Column("three_point_percentage", TypeName = "decimal(18, 0)")]
    public decimal? ThreePointPercentage { get; set; }

    [Column("free_throw_percentage", TypeName = "decimal(18, 0)")]
    public decimal? FreeThrowPercentage { get; set; }

    [Column("steals")]
    public int? Steals { get; set; }

    [Column("blocks")]
    public int? Blocks { get; set; }

    [Column("turnovers")]
    public int? Turnovers { get; set; }

    [ForeignKey("PlayerStatId")]
    [InverseProperty("BasketballStats")]
    public virtual PlayerStat PlayerStat { get; set; } = null!;
}
