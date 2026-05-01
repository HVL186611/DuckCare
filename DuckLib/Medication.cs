 using System;
using System.Collections.Generic;
using System.Text;

namespace DuckLib
{
    public class Medication
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        // for patient medications:
        public string? Route { get; set; }
        public string? Dose { get; set; }
        public string? Frequency { get; set; }
    }
}
