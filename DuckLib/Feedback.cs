using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DuckLib
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "INT")]
        public int SimulationCaseId { get; set; }

        public string Text { get; set; } = null!;

        public string? CreatedAt { get; set; }  // sql fills this

        [ForeignKey("SimulationCaseId")]
        [InverseProperty("Feedback")]
        public virtual SimulationCase SimulationCase { get; set; } = null!;
    }
}
