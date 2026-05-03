using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DuckLib
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "INT")]
        public int? SimulationCaseId { get; set; }

        [Column(TypeName = "INT")]
        public int? PatientId { get; set; }

        [Column(TypeName = "INT")]
        public int MedicationId { get; set; }

        [Column(TypeName = "INT")]
        public int? DeltaChangeId { get; set; }

        public double Dose { get; set; }

        public string? DoseUnit { get; set; } = null!;

        public string Route { get; set; } = null!;

        public string Timing { get; set; } = null!;

        [ForeignKey("DeltaChangeId")]
        [InverseProperty("Orders")]
        public virtual VitalDeltas? DeltaChange { get; set; }

        [ForeignKey("MedicationId")]
        //[InverseProperty("Medications")]
        public virtual Medication Medication { get; set; } = null!;

        [ForeignKey("PatientId")]
        [InverseProperty("Medications")]
        public virtual Patient? Patient { get; set; }

        [ForeignKey("SimulationCaseId")]
        [InverseProperty("Orders")]
        public virtual SimulationCase? SimulationCase { get; set; }
    }

}
