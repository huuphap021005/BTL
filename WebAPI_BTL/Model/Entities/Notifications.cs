using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.TempModels;

[Index("Status", Name = "IDX_Notifications_Status")]
public partial class Notifications
{
    [Key]
    public int NotificationID { get; set; }

    [StringLength(255)]
    public string? UserEmail { get; set; }

    [StringLength(20)]
    public string? Type { get; set; }

    [StringLength(255)]
    public string? Subject { get; set; }

    public string? Body { get; set; }

    [StringLength(20)]
    public string? Status { get; set; }

    public int? Attempts { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastAttemptAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }
}
