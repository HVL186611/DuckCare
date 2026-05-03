using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DuckLib
{
    public class VitalDeltas
    {
        [Key]
        public int Id { get; set; }

        [Column("BPSystolicDelta", TypeName = "INT")]
        public int BPSystolicDelta { get; set; }

        [Column("BPDiastolicDelta", TypeName = "INT")]
        public int BPDiastolicDelta { get; set; }

        [Column(TypeName = "INT")]
        public int HeartRateDelta { get; set; }

        [Column(TypeName = "INT")]
        public int RespiratoryRateDelta { get; set; }

        [Column("SpO2Delta", TypeName = "INT")]
        public int SpO2delta { get; set; }

        [Column(TypeName = "INT")]
        public int TemperatureDelta { get; set; }

        [InverseProperty("DeltaChange")]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        [InverseProperty("StartDeltas")]
        public virtual ICollection<SimulationCase> SimulationCases { get; set; } = new List<SimulationCase>();
    }

}
