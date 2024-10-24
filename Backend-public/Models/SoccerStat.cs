using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models;

[Table("soccer_stats")]
public partial class SoccerStat
{
    [Key]
    [Column("soccer_stat_id")]
    public long SoccerStatId { get; set; }

    [Column("player_stat_id")]
    public long PlayerStatId { get; set; }

    [Column("passes_completed")]
    public int? PassesCompleted { get; set; }

    [Column("pass_completion_percentage", TypeName = "decimal(18, 0)")]
    public decimal? PassCompletionPercentage { get; set; }

    [Column("key_passes")]
    public int? KeyPasses { get; set; }

    [Column("dribbles_completed")]
    public int? DribblesCompleted { get; set; }

    [Column("yellow_cards")]
    public int? YellowCards { get; set; }

    [Column("red_cards")]
    public int? RedCards { get; set; }

    [ForeignKey("PlayerStatId")]
    [InverseProperty("SoccerStats")]
    public virtual PlayerStat PlayerStat { get; set; } = null!;
}
