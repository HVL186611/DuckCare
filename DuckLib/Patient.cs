using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DuckLib
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        [Column(TypeName = "INT")]
        public int Age { get; set; }

        public string Sex { get; set; } = null!;

        public double WeightKg { get; set; }

        public double HeightCm { get; set; }

        public string? Bed { get; set; } = null!;

        public string? AdmissionDate { get; set; }

        public string? AdmittingDiagnosis { get; set; } = null!;

        public string? MedicalHistory { get; set; } = null!;

        public string? SurgicalHistory { get; set; } = null!;

        public string? SocialHistory { get; set; } = null!;

        public string? FamilyHistory { get; set; } = null!;

        [InverseProperty("Patient")]
        public virtual ICollection<Order> Medications { get; set; } = new List<Order>();

        [InverseProperty("Patient")]
        public virtual ICollection<SimulationCase> SimulationCases { get; set; } = new List<SimulationCase>();

        [ForeignKey("PatientId")]
        [InverseProperty("Patients")]
        public virtual ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();
    }

}
