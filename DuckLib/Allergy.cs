using System;
using System.Collections.Generic;
using System.Text;

namespace DuckLib
{
    public class Allergy
    {
        public int Id { get; set; }
        public string Allergen {  get; set; }
        public string Reaction { get; set; }
        public List<String> AffectedMedications { get; set; } = new();
    }
}
