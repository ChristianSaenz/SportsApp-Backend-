using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models;

[Table("sport")]
public partial class Sport
{
    [Key]
    [Column("sport_id")]
    public long SportId { get; set; }

    [Column("sport_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? SportName { get; set; }

    [InverseProperty("Sport")]
    public virtual ICollection<Course> CourseInfos { get; set; } = new List<Course>();

    [InverseProperty("Sport")]
    public virtual ICollection<League> Leagues { get; set; } = new List<League>();

}