using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DuckLib
{
    public class Allergy
    {
        [Key]
        public int Id { get; set; }

        public string Allergen { get; set; } = null!;

        public string Reaction { get; set; } = null!;

        [ForeignKey("AllergyId")]
        [InverseProperty("Allergies")]
        public virtual ICollection<Medication> Medications { get; set; } = new List<Medication>();

        [ForeignKey("AllergyId")]
        [InverseProperty("Allergies")]
        public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

        //[ForeignKey("AllergyId")]
        //[InverseProperty("Allergies")]
        public virtual ICollection<SimulationCase> SimulationCases { get; set; } = new List<SimulationCase>();
    }
}
