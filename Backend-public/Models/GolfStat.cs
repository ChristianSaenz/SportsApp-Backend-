using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models;

[Table("golf_stats")]
public partial class GolfStat
{
    [Key]
    [Column("golf_stat_id")]
    public long GolfStatId { get; set; }

    [Column("player_stat_id")]
    public long PlayerStatId { get; set; }

    [Column("stroke_average", TypeName = "decimal(18, 0)")]
    public decimal? StrokeAverage { get; set; }

    [Column("driving_distance", TypeName = "decimal(18, 0)")]
    public decimal? DrivingDistance { get; set; }

    [Column("fairway_accuracy", TypeName = "decimal(18, 0)")]
    public decimal? FairwayAccuracy { get; set; }

    [Column("greens_in_reg", TypeName = "decimal(18, 0)")]
    public decimal? GreensInReg { get; set; }

    [Column("putting_average", TypeName = "decimal(18, 0)")]
    public decimal? PuttingAverage { get; set; }

    [Column("scrambling_percentage", TypeName = "decimal(18, 0)")]
    public decimal? ScramblingPercentage { get; set; }

    [Column("sand_save_percentage", TypeName = "decimal(18, 0)")]
    public decimal? SandSavePercentage { get; set; }

    [Column("top_10_finishes")]
    public int? Top10Finishes { get; set; }

    [Column("earnings", TypeName = "decimal(18, 0)")]
    public decimal? Earnings { get; set; }

    [ForeignKey("PlayerStatId")]
    [InverseProperty("GolfStats")]
    public virtual PlayerStat PlayerStat { get; set; } = null!;
}
