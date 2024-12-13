using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models;

[Table("role")]
public partial class Role
{
    public Role()
    {
        UserRoles = new HashSet<UserRole>();
        RolePermissions = new HashSet<RolePermission>();
    }

    [Key]
    [Column("role_id")]
    public long RoleId { get; set; }

    [Column("role_name")]
    [StringLength(50)]
    public string RoleName { get; set; } = null!;

    [Column("role_description")]
    public string RoleDescription { get; set; } = null!;
    public virtual ICollection<RolePermission> RolePermissions { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; }
}