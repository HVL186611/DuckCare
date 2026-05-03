using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DuckLib
{
    public class Vitals
    {
        [Key]
        public int Id { get; set; }

        [Column("BPSystolic", TypeName = "INT")]

        public int BPSystolic { get; set; }

        [Column("BPDiastolic", TypeName = "INT")]
        public int BPDiastolic { get; set; }

        [Column(TypeName = "INT")]
        public int HeartRate { get; set; }

        [Column(TypeName = "INT")]
        public int RespiratoryRate { get; set; }

        [Column(TypeName = "INT")]
        public int OxygenSaturation { get; set; }

        public double Temperature { get; set; }

        public string? RecordedAt { get; set; } = null!;

        [InverseProperty("StartVitals")]
        public virtual ICollection<SimulationCase> SimulationCases { get; set; } = new List<SimulationCase>();
    }

}
