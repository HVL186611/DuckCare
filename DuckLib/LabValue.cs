using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DuckLib
{
    public class LabValue
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "INT")]
        public int SimulationCaseId { get; set; }

        public string Name { get; set; } = null!;

        public string Value { get; set; } = null!;

        public string Reference { get; set; } = null!;

        public string Interpretation { get; set; } = null!;

        [ForeignKey("SimulationCaseId")]
        [InverseProperty("LabValues")]
        public virtual SimulationCase SimulationCase { get; set; } = null!;

    }
}
