using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.TempModels;

[Index("ApplicantID", Name = "IDX_Resumes_Applicant")]
public partial class Resumes
{
    [Key]
    public int ResumeID { get; set; }

    public int ApplicantID { get; set; }

    [StringLength(255)]
    public string OriginalFileName { get; set; } = null!;

    [StringLength(255)]
    public string StorageKey { get; set; } = null!;

    [StringLength(100)]
    public string? ContentType { get; set; }

    public long? FileSize { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UploadedAt { get; set; }

    [ForeignKey("ApplicantID")]
    [InverseProperty("Resumes")]
    public virtual Applicants Applicant { get; set; } = null!;
}
