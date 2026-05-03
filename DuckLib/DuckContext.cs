using DuckLib;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System;
using System.Collections.Generic;

namespace TestLib.Temp;

public partial class DuckContext : DbContext
{
    public DuckContext()
    {
    }

    public DuckContext(DbContextOptions<DuckContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CaseLog> CaseLogs { get; set; }
    public virtual DbSet<Feedback> Feedback { get; set; }
    public virtual DbSet<Allergy> Allergies { get; set; }

    public virtual DbSet<Goal> Goals { get; set; }

    public virtual DbSet<LabValue> LabValues { get; set; }

    public virtual DbSet<Medication> Medications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<SimulationCase> SimulationCases { get; set; }

    public virtual DbSet<Vitals> Vitals { get; set; }

    public virtual DbSet<VitalDeltas> VitalDeltas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\GitHub\\DuckCare\\SQL\\DuckCare.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Allergy>(entity =>
        {
            entity.Property(e => e.Allergen).HasDefaultValue("");
            entity.Property(e => e.Reaction).HasDefaultValue("");

            entity.HasMany(d => d.Medications).WithMany(p => p.Allergies)
                .UsingEntity<Dictionary<string, object>>(
                    "AllergyAffectedMedication",
                    r => r.HasOne<Medication>().WithMany().HasForeignKey("MedicationId"),
                    l => l.HasOne<Allergy>().WithMany().HasForeignKey("AllergyId"),
                    j =>
                    {
                        j.HasKey("AllergyId", "MedicationId");
                        j.ToTable("AllergyAffectedMedications");
                        j.IndexerProperty<int>("AllergyId").HasColumnType("INT");
                        j.IndexerProperty<int>("MedicationId").HasColumnType("INT");
                    });
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.Property(e => e.Description).HasDefaultValue("");
        });

        modelBuilder.Entity<LabValue>(entity =>
        {
            entity.Property(e => e.Interpretation).HasDefaultValue("");
            entity.Property(e => e.Name).HasDefaultValue("");
            entity.Property(e => e.Reference).HasDefaultValue("");
            entity.Property(e => e.Value).HasDefaultValue("");
        });

        modelBuilder.Entity<Medication>(entity =>
        {
            entity.Property(e => e.Name).HasDefaultValue("");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.DoseUnit).HasDefaultValue("mg");
            entity.Property(e => e.Route).HasDefaultValue("");
            entity.Property(e => e.Timing).HasDefaultValue("");

            entity.HasOne(d => d.Medication).WithMany(p => p.Orders).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SimulationCase).WithMany(p => p.Orders).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.Property(e => e.AdmittingDiagnosis).HasDefaultValue("");
            entity.Property(e => e.Bed).HasDefaultValue("");
            entity.Property(e => e.FamilyHistory).HasDefaultValue("");
            entity.Property(e => e.MedicalHistory).HasDefaultValue("");
            entity.Property(e => e.Name).HasDefaultValue("");
            entity.Property(e => e.Sex).HasDefaultValue("");
            entity.Property(e => e.SocialHistory).HasDefaultValue("");
            entity.Property(e => e.SurgicalHistory).HasDefaultValue("");

            entity.HasMany(d => d.Allergies).WithMany(p => p.Patients)
                .UsingEntity<Dictionary<string, object>>(
                    "PatientAllergy",
                    r => r.HasOne<Allergy>().WithMany().HasForeignKey("AllergyId"),
                    l => l.HasOne<Patient>().WithMany().HasForeignKey("PatientId"),
                    j =>
                    {
                        j.HasKey("PatientId", "AllergyId");
                        j.ToTable("PatientAllergies");
                        j.IndexerProperty<int>("PatientId").HasColumnType("INT");
                        j.IndexerProperty<int>("AllergyId").HasColumnType("INT");
                    });
        });

        modelBuilder.Entity<SimulationCase>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.CreatedByRole).HasDefaultValue("Student");
            entity.Property(e => e.Description).HasDefaultValue("");
            entity.Property(e => e.GoalTimeMinutes).HasDefaultValue(15);
            entity.Property(e => e.StudentEditable).HasDefaultValue(1);
            entity.Property(e => e.Title).HasDefaultValue("");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasMany(d => d.Allergies).WithMany(p => p.SimulationCases)
                .UsingEntity<Dictionary<string, object>>(
                    "SimulationCaseAllergy",
                    r => r.HasOne<Allergy>().WithMany().HasForeignKey("AllergyId"),
                    l => l.HasOne<SimulationCase>().WithMany().HasForeignKey("SimulationCaseId"),
                    j =>
                    {
                        j.HasKey("SimulationCaseId", "AllergyId");
                        j.ToTable("SimulationCaseAllergies");
                        j.IndexerProperty<int>("SimulationCaseId").HasColumnType("INT");
                        j.IndexerProperty<int>("AllergyId").HasColumnType("INT");
                    });
        });

        modelBuilder.Entity<Vitals>(entity =>
        {
            entity.Property(e => e.RecordedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<CaseLog>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S', 'now', 'localtime')");

            entity.HasOne(d => d.SimulationCase)
                .WithMany(p => p.CaseLogs)
                .HasForeignKey(d => d.SimulationCaseId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S', 'now', 'localtime')");

            entity.HasOne(d => d.SimulationCase)
                .WithMany(p => p.Feedback)
                .HasForeignKey(d => d.SimulationCaseId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
