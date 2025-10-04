using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.TempModels;

[Index("CompanyID", Name = "IDX_Jobs_Company")]
[Index("Industry", Name = "IDX_Jobs_Industry")]
[Index("Title", Name = "IDX_Jobs_Title")]
public partial class Jobs
{
    [Key]
    public int JobID { get; set; }

    public int CompanyID { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Requirements { get; set; }

    [StringLength(255)]
    public string? Location { get; set; }

    [StringLength(100)]
    public string? Industry { get; set; }

    public int? SalaryMin { get; set; }

    public int? SalaryMax { get; set; }

    public bool? IsPublished { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PublishDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpireDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Job")]
    public virtual ICollection<Applications> Applications { get; set; } = new List<Applications>();

    [ForeignKey("CompanyID")]
    [InverseProperty("Jobs")]
    public virtual Companies Company { get; set; } = null!;
}
