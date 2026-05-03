 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DuckLib
{
    public class Medication
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Route { get; set; }

        public string? Dose { get; set; }

        public string? Frequency { get; set; }

        //[InverseProperty("Medication")]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        //[ForeignKey("MedicationId")]
        //[InverseProperty("Medication")]
        public virtual ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();

    }

}
