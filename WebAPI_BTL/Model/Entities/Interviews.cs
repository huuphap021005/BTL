using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.TempModels;

[Index("ScheduledAt", Name = "IDX_Interviews_ScheduledAt")]
public partial class Interviews
{
    [Key]
    public int InterviewID { get; set; }

    public int ApplicationID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ScheduledAt { get; set; }

    public int? DurationMinutes { get; set; }

    [StringLength(50)]
    public string? Mode { get; set; }

    [StringLength(255)]
    public string? Location { get; set; }

    [StringLength(255)]
    public string? Interviewer { get; set; }

    [StringLength(50)]
    public string? Result { get; set; }

    public string? Notes { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("ApplicationID")]
    [InverseProperty("Interviews")]
    public virtual Applications Application { get; set; } = null!;
}
