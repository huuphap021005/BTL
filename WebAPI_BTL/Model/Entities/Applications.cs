using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.TempModels;

[Index("ApplicantID", Name = "IDX_Applications_Applicant")]
[Index("JobID", Name = "IDX_Applications_Job")]
public partial class Applications
{
    [Key]
    public int ApplicationID { get; set; }

    public int JobID { get; set; }

    public int ApplicantID { get; set; }

    public int? ResumeID { get; set; }

    public string? CoverLetter { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AppliedAt { get; set; }

    [ForeignKey("ApplicantID")]
    [InverseProperty("Applications")]
    public virtual Applicants Applicant { get; set; } = null!;

    [InverseProperty("Application")]
    public virtual ICollection<Interviews> Interviews { get; set; } = new List<Interviews>();

    [ForeignKey("JobID")]
    [InverseProperty("Applications")]
    public virtual Jobs Job { get; set; } = null!;
}
