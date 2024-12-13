using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsApp.Models;

[Table("course")]
public partial class Course
{
    [Key]
    [Column("course_id")]
    public long CourseId { get; set; }

    [Column("sport_id")]
    public long? SportId { get; set; }

    [Column("title")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Title { get; set; }

    [Column("content")]
    [StringLength(50)]
    public string? Description { get; set; }

    [Column("createdAt", TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [ForeignKey("SportId")]
    [InverseProperty("CourseInfos")]
    public virtual Sport? Sport { get; set; }
}
