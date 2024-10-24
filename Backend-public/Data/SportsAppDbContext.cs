using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SportsApp.Helpers;
using SportsApp.Models;

namespace SportsApp.Data;

public partial class SportsAppDbContext : DbContext
{
    public SportsAppDbContext(DbContextOptions<SportsAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BasketballStat> BasketballStats { get; set; }

    public virtual DbSet<CourseInfo> CourseInfos { get; set; }

    public virtual DbSet<F1Stat> F1Stats { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<FootballStat> FootballStats { get; set; }

    public virtual DbSet<GolfStat> GolfStats { get; set; }

    public virtual DbSet<HockeyStat> HockeyStats { get; set; }

    public virtual DbSet<League> Leagues { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerStat> PlayerStats { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SoccerStat> SoccerStats { get; set; }

    public virtual DbSet<Sport> Sports { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TennisStat> TennisStats { get; set; }

    public virtual DbSet<UfcStat> UfcStats { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }
    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BasketballStat>(entity =>
        {
            entity.HasKey(e => e.BasketballStatId).HasName("PK__basketba__9128F05F37664597");

            entity.Property(e => e.Blocks).HasDefaultValue(0);
            entity.Property(e => e.ReboundsDefensive).HasDefaultValue(0);
            entity.Property(e => e.ReboundsOffensive).HasDefaultValue(0);
            entity.Property(e => e.Steals).HasDefaultValue(0);
            entity.Property(e => e.Turnovers).HasDefaultValue(0);

            entity.HasOne(d => d.PlayerStat).WithMany(p => p.BasketballStats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__basketbal__playe__55F4C372");
        });

        modelBuilder.Entity<CourseInfo>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK_course_101");

            entity.HasOne(d => d.Sport).WithMany(p => p.CourseInfos).HasConstraintName("FK_course_101__sport_id_sport_sport_id");
        });

        modelBuilder.Entity<F1Stat>(entity =>
        {
            entity.HasKey(e => e.F1StatId).HasName("PK__f1_stats__479811C491B03B79");

            entity.Property(e => e.ChampionshipsWon).HasDefaultValue(0);
            entity.Property(e => e.Dnfs).HasDefaultValue(0);
            entity.Property(e => e.FastestLaps).HasDefaultValue(0);
            entity.Property(e => e.LapsLed).HasDefaultValue(0);
            entity.Property(e => e.PitStops).HasDefaultValue(0);
            entity.Property(e => e.PodiumFinishes).HasDefaultValue(0);
            entity.Property(e => e.PolePositions).HasDefaultValue(0);
            entity.Property(e => e.RacesStarted).HasDefaultValue(0);

            entity.HasOne(d => d.PlayerStat).WithMany(p => p.F1Stats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__f1_stats__player__6FB49575");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PK_favorites");

            entity.Property(e => e.FavoriteId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.User)
                .WithMany(p => p.Favorites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_favorites_user_id_user_user_id");

            entity.HasOne(d => d.Player)
                .WithMany(p => p.Favorites)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_favorites_player_id_player_player_id");
        });

        modelBuilder.Entity<FootballStat>(entity =>
        {
            entity.HasKey(e => e.FootballStatId).HasName("PK__football__D23B9FB24B526B81");

            entity.Property(e => e.FieldGoals).HasDefaultValue(0);
            entity.Property(e => e.InterceptionsThrown).HasDefaultValue(0);
            entity.Property(e => e.KickReturnYards).HasDefaultValue(0);
            entity.Property(e => e.PassingTouchdowns).HasDefaultValue(0);
            entity.Property(e => e.PassingYards).HasDefaultValue(0);
            entity.Property(e => e.ReceivingTouchdowns).HasDefaultValue(0);
            entity.Property(e => e.ReceivingYards).HasDefaultValue(0);
            entity.Property(e => e.Receptions).HasDefaultValue(0);
            entity.Property(e => e.RushingTouchdowns).HasDefaultValue(0);
            entity.Property(e => e.RushingYards).HasDefaultValue(0);
            entity.Property(e => e.Sacks).HasDefaultValue(0);

            entity.HasOne(d => d.PlayerStat).WithMany(p => p.FootballStats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__football___playe__0880433F");
        });

        modelBuilder.Entity<GolfStat>(entity =>
        {
            entity.Property(e => e.Top10Finishes).HasDefaultValue(0);

            entity.HasOne(d => d.PlayerStat).WithMany(p => p.GolfStats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_golf_stats__player_stat_id_player_stat_player_stat_id");
        });

        modelBuilder.Entity<HockeyStat>(entity =>
        {
            entity.HasKey(e => e.HockeyStatId).HasName("PK__hockey_s__72F3125C8FFA8A8F");

            entity.Property(e => e.FaceoffWins).HasDefaultValue(0);
            entity.Property(e => e.Hits).HasDefaultValue(0);
            entity.Property(e => e.PenaltyMinutes).HasDefaultValue(0);
            entity.Property(e => e.PowerPlayGoals).HasDefaultValue(0);
            entity.Property(e => e.ShortHandedGoals).HasDefaultValue(0);

            entity.HasOne(d => d.PlayerStat).WithMany(p => p.HockeyStats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__hockey_st__playe__14E61A24");
        });

        modelBuilder.Entity<League>(entity =>
        {
            entity.HasOne(d => d.Sport).WithMany(p => p.Leagues)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_league_sports_id_sport_sport_id");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasOne(d => d.AwayTeam).WithMany(p => p.MatchAwayTeams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_match_away_team_id_team_team_id");

            entity.HasOne(d => d.HomeTeam).WithMany(p => p.MatchHomeTeams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_match__home_team_id_team_team_id");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK_permissions");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.Property(e => e.Lastname).IsFixedLength();

            entity.HasOne(d => d.Team).WithMany(p => p.Players)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_player__team_id_team_team_id");
        });

        modelBuilder.Entity<PlayerStat>(entity =>
        {
            entity.Property(e => e.GoalsAgainstAverage).IsFixedLength();
            entity.Property(e => e.SavePercentage).IsFixedLength();

            entity.HasOne(d => d.Match).WithMany(p => p.PlayerStats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_player_stat_match_id_match_match_id");

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerStats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_player_stat_player_id_player_player_id");

            entity.HasOne(d => d.PlayerNavigation).WithMany(p => p.PlayerStats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_player_stat_sport_id_sport_sport_id");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(ur => new { ur.UserId, ur.RoleId });

            entity.ToTable("user_role");

            entity.Property(ur => ur.UserId).HasColumnName("user_id");
            entity.Property(ur => ur.RoleId).HasColumnName("role_id");

            entity.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_role_user_id_user_user_id");

            entity.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_role_role_id_role_role_id");
        });


        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(rp => new { rp.RoleId, rp.PermissionId })
                .HasName("PK__role_permission");

            entity.ToTable("role_permission");

            entity.Property(rp => rp.RoleId).HasColumnName("role_id");
            entity.Property(rp => rp.PermissionId).HasColumnName("permission_id");

            entity.HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_role_permission_role_id_role_role_id");

            entity.HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_role_permission_permission_id_permission_permission_id");
        });


        modelBuilder.Entity<SoccerStat>(entity =>
        {
            entity.HasKey(e => e.SoccerStatId).HasName("PK__soccer_s__FE6B4975DE9A3C92");

            entity.Property(e => e.DribblesCompleted).HasDefaultValue(0);
            entity.Property(e => e.KeyPasses).HasDefaultValue(0);
            entity.Property(e => e.PassesCompleted).HasDefaultValue(0);
            entity.Property(e => e.RedCards).HasDefaultValue(0);
            entity.Property(e => e.YellowCards).HasDefaultValue(0);

            entity.HasOne(d => d.PlayerStat).WithMany(p => p.SoccerStats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__soccer_st__playe__4A8310C6");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasOne(d => d.League).WithMany(p => p.Teams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_team__league_id_league_league_id");
        });

        modelBuilder.Entity<TennisStat>(entity =>
        {
            entity.HasKey(e => e.TennisStatId).HasName("PK__tennis_s__235560E32D03A3E3");

            entity.Property(e => e.Aces).HasDefaultValue(0);
            entity.Property(e => e.BreakPointsConverted).HasDefaultValue(0);
            entity.Property(e => e.BreakPointsSaved).HasDefaultValue(0);
            entity.Property(e => e.DoubleFaults).HasDefaultValue(0);
            entity.Property(e => e.SetsLost).HasDefaultValue(0);
            entity.Property(e => e.SetsWon).HasDefaultValue(0);
            entity.Property(e => e.TiebreaksWon).HasDefaultValue(0);
            entity.Property(e => e.UnforcedErrors).HasDefaultValue(0);

            entity.HasOne(d => d.PlayerStat).WithMany(p => p.TennisStats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tennis_st__playe__6442E2C9");
        });

        modelBuilder.Entity<UfcStat>(entity =>
        {
            entity.HasKey(e => e.UfcStatId).HasName("PK__ufc_stat__774303D181BEDA74");

            entity.Property(e => e.DecisionWins).HasDefaultValue(0);
            entity.Property(e => e.KoTkoWins).HasDefaultValue(0);
            entity.Property(e => e.Losses).HasDefaultValue(0);
            entity.Property(e => e.SignificantStrikesLanded).HasDefaultValue(0);
            entity.Property(e => e.SubmissionWins).HasDefaultValue(0);
            entity.Property(e => e.SubmissionsAttempted).HasDefaultValue(0);

            entity.HasOne(d => d.PlayerStat).WithMany(p => p.UfcStats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ufc_stats_player_stat_id_player_stat_player_stat_id");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
