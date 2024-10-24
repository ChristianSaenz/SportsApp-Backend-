using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsApp.Models
{
    [Table("role_permission")]
    public class RolePermission
    {
        [Key]
        public long RoleId { get; set; }

        [Key]
        public long PermissionId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; } = null!;

        [ForeignKey("PermissionId")]
        public virtual Permission Permission { get; set; } = null!;
    }
}



