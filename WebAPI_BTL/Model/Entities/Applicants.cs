using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.TempModels;

[Index("Email", Name = "UQ_Applicants_Email", IsUnique = true)]
public partial class Applicants
{
    [Key]
    public int ApplicantID { get; set; }

    [StringLength(255)]
    public string FullName { get; set; } = null!;

    [StringLength(255)]
    public string Email { get; set; } = null!;

    [StringLength(50)]
    public string? Phone { get; set; }

    public string? Skills { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Applicant")]
    public virtual ICollection<Applications> Applications { get; set; } = new List<Applications>();

    [InverseProperty("Applicant")]
    public virtual ICollection<Resumes> Resumes { get; set; } = new List<Resumes>();
}
