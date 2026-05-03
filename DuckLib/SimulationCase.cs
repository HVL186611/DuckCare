using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DuckLib
{
    public class SimulationCase
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Column(TypeName = "INT")]
        public int? PatientId { get; set; }

        [Column(TypeName = "INT")]
        public int? StartVitalsId { get; set; }

        [Column(TypeName = "INT")]
        public int? StartDeltasId { get; set; }

        [Column(TypeName = "INT")]
        public int? GoalsId { get; set; }

        [Column(TypeName = "INT")]
        public int GoalTimeMinutes { get; set; }

        public int StudentEditable { get; set; }

        public int IsActive { get; set; }

        public string? CreatedByRole { get; set; } = null!;

        public string? CreatedAt { get; set; } = null!;

        public string? UpdatedAt { get; set; } = null!;

        [ForeignKey("GoalsId")]
        [InverseProperty("SimulationCases")]
        public virtual Goal? Goals { get; set; } = new();

        [InverseProperty("SimulationCase")]
        public virtual ICollection<LabValue> LabValues { get; set; } = new List<LabValue>();

        [InverseProperty("SimulationCase")]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        [ForeignKey("PatientId")]
        [InverseProperty("SimulationCases")]
        public virtual Patient? Patient { get; set; } = new();

        [ForeignKey("StartDeltasId")]
        [InverseProperty("SimulationCases")]
        public virtual VitalDeltas? StartDeltas { get; set; } = new();

        [ForeignKey("StartVitalsId")]
        [InverseProperty("SimulationCases")]
        public virtual Vitals? StartVitals { get; set; } = new();

        [ForeignKey("SimulationCaseId")]
        [InverseProperty("SimulationCases")]
        public virtual ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();

        [InverseProperty("SimulationCase")]
        public virtual ICollection<CaseLog> CaseLogs { get; set; } = new List<CaseLog>();

    }
}
