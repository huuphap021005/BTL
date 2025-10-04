using System;
using System.Collections.Generic;
using DAL.TempModels;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Applicants> Applicants { get; set; }

    public virtual DbSet<Applications> Applications { get; set; }

    public virtual DbSet<Companies> Companies { get; set; }

    public virtual DbSet<Interviews> Interviews { get; set; }

    public virtual DbSet<Jobs> Jobs { get; set; }

    public virtual DbSet<Notifications> Notifications { get; set; }

    public virtual DbSet<Resumes> Resumes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MSI\\SQLHUUPHAP;Database=QLJob;Trusted_Connection=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Applicants>(entity =>
        {
            entity.HasKey(e => e.ApplicantID).HasName("PK__Applican__39AE914843806B05");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Applications>(entity =>
        {
            entity.HasKey(e => e.ApplicationID).HasName("PK__Applicat__C93A4F79362C1DBD");

            entity.Property(e => e.AppliedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue("applied");

            entity.HasOne(d => d.Applicant).WithMany(p => p.Applications).HasConstraintName("FK_Applications_Applicants");

            entity.HasOne(d => d.Job).WithMany(p => p.Applications).HasConstraintName("FK_Applications_Jobs");
        });

        modelBuilder.Entity<Companies>(entity =>
        {
            entity.HasKey(e => e.CompanyID).HasName("PK__Companie__2D971C4CEE1BBAE1");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Interviews>(entity =>
        {
            entity.HasKey(e => e.InterviewID).HasName("PK__Intervie__C97C5832D2CE5AA5");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Mode).HasDefaultValue("online");
            entity.Property(e => e.Result).HasDefaultValue("pending");

            entity.HasOne(d => d.Application).WithMany(p => p.Interviews).HasConstraintName("FK_Interviews_Applications");
        });

        modelBuilder.Entity<Jobs>(entity =>
        {
            entity.HasKey(e => e.JobID).HasName("PK__Jobs__056690E296DE1572");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsPublished).HasDefaultValue(false);

            entity.HasOne(d => d.Company).WithMany(p => p.Jobs).HasConstraintName("FK_Jobs_Companies");
        });

        modelBuilder.Entity<Notifications>(entity =>
        {
            entity.HasKey(e => e.NotificationID).HasName("PK__Notifica__20CF2E32C46891C4");

            entity.Property(e => e.Attempts).HasDefaultValue(0);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue("pending");
            entity.Property(e => e.Type).HasDefaultValue("email");
        });

        modelBuilder.Entity<Resumes>(entity =>
        {
            entity.HasKey(e => e.ResumeID).HasName("PK__Resumes__D7D7A317AC3258E2");

            entity.Property(e => e.UploadedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Applicant).WithMany(p => p.Resumes).HasConstraintName("FK_Resumes_Applicants");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
