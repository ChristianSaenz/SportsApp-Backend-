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

    public virtual DbSet<Course> CourseInfos { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<League> Leagues { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Role> Roles { get; set; }


    public virtual DbSet<Sport> Sports { get; set; }

    public virtual DbSet<Team> Teams { get; set; }


    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }
    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK_course_101");

            entity.HasOne(d => d.Sport).WithMany(p => p.CourseInfos).HasConstraintName("FK_course_101__sport_id_sport_sport_id");
        });



        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PK_favorites");

            entity.Property(e => e.FavoriteId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.User)
                .WithMany(p => p.Favorites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_favorites_user_id");

            entity.HasOne(d => d.Player)
                .WithMany(p => p.Favorites)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_favorites_player_id");
        });


        modelBuilder.Entity<League>(entity =>
        {
            entity.HasOne(d => d.Sport).WithMany(p => p.Leagues)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_league_sports_id_sport_sport_id");
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


        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasOne(d => d.League).WithMany(p => p.Teams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_team__league_id_league_league_id");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
