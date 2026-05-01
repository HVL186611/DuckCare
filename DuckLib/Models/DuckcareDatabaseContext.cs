using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DuckLib.Models;

public partial class DuckcareDatabaseContext : DbContext
{
    public DuckcareDatabaseContext()
    {
    }

    public DuckcareDatabaseContext(DbContextOptions<DuckcareDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Allergy> Allergies { get; set; }

    public virtual DbSet<LabValue> LabValues { get; set; }

    public virtual DbSet<Medication> Medications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<SimulationCase> SimulationCases { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=duckcareserver.database.windows.net;Database=duckcareDatabase;User ID=johannes;Password=DuckCare!;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Allergy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Allergy__3214EC073A85BE27");

            entity.ToTable("Allergy");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Allergen)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Reaction)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LabValue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LabValue__3214EC0781859356");

            entity.ToTable("LabValue");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Interpretation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Value)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Simulation).WithMany(p => p.LabValues)
                .HasForeignKey(d => d.SimulationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LabValue__Simula__0D7A0286");
        });

        modelBuilder.Entity<Medication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Medicati__3214EC07A52C66D2");

            entity.ToTable("Medication");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.Allergies).WithMany(p => p.Medications)
                .UsingEntity<Dictionary<string, object>>(
                    "AffectedMedication",
                    r => r.HasOne<Allergy>().WithMany()
                        .HasForeignKey("AllergyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__AffectedM__Aller__7B5B524B"),
                    l => l.HasOne<Medication>().WithMany()
                        .HasForeignKey("MedicationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__AffectedM__Medic__7A672E12"),
                    j =>
                    {
                        j.HasKey("MedicationId", "AllergyId").HasName("PK__Affected__58A5F11EC89B0374");
                        j.ToTable("AffectedMedication");
                    });
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC07B577932F");

            entity.ToTable("Order");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DoseUnit)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Route)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Timing)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Medication).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MedicationId)
                .HasConstraintName("FK__Order__Medicatio__0A9D95DB");

            entity.HasOne(d => d.Simulation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SimulationId)
                .HasConstraintName("FK__Order__Timing__09A971A2");
        });

        modelBuilder.Entity<SimulationCase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Simulati__3214EC0747414170");

            entity.ToTable("SimulationCase");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Bpdiastolic).HasColumnName("BPDiastolic");
            entity.Property(e => e.BpdiastolicDelta).HasColumnName("BPDiastolicDelta");
            entity.Property(e => e.Bpsystolic).HasColumnName("BPSystolic");
            entity.Property(e => e.BpsystolicDelta).HasColumnName("BPSystolicDelta");
            entity.Property(e => e.CreatedByRole)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.GoalDescription)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GoalDistolicBpmax).HasColumnName("GoalDistolicBPMax");
            entity.Property(e => e.GoalDistolicBpmin).HasColumnName("GoalDistolicBPMin");
            entity.Property(e => e.GoalSystolicBpmax).HasColumnName("GoalSystolicBPMax");
            entity.Property(e => e.GoalSystolicBpmin).HasColumnName("GoalSystolicBPMin");
            entity.Property(e => e.PatientAdmittingDiagnosis)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PatientMedicalHistory)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PatientName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientSex)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SpO2delta).HasColumnName("SpO2Delta");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
