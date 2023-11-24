using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Generate_pdf.Models;

public partial class HospitalDbContext : DbContext
{
    public HospitalDbContext()
    {
    }

    public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Disease> Diseases { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=TPAREPAL-L-5467\\SQLEXPRESS;Initial Catalog=HospitalDB;User ID=sa;Password=Welcome2evoke@1234;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.Property(e => e.AppointmentTime).HasColumnType("datetime");
            entity.Property(e => e.DocterToVisit).HasMaxLength(50);
            entity.Property(e => e.DoctorAvaliableTime).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.MedicalIssue).HasMaxLength(50);
            entity.Property(e => e.PatientName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);

            entity.HasOne(d => d.MedicalIssueNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.MedicalIssue)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Diseases");
        });

        modelBuilder.Entity<Disease>(entity =>
        {
            entity.HasKey(e => e.DiseasesName);

            entity.Property(e => e.DiseasesName).HasMaxLength(50);

            entity.HasOne(d => d.Doctor).WithMany(p => p.Diseases)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_Diseases_Doctors");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Experience).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Qualification).HasMaxLength(50);
            entity.Property(e => e.Specialised).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
