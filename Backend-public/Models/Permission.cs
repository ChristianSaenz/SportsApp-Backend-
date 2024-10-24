using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models;

[Table("permission")]
public partial class Permission
{
    public Permission()
    {
        RolePermissions = new HashSet<RolePermission>();
    }

    [Key]
    [Column("permission_id")]
    public long PermissionId { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string? Name { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    public ICollection<RolePermission> RolePermissions { get; set; }

}
