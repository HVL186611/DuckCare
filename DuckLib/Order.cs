using System;
using System.Collections.Generic;
using System.Text;

namespace DuckLib
{
    public class Order
    {
        public Medication Medication { get; set; } = new();
        public double Dose { get; set; } // Amount, true amount set in tandem with unit
        public string DoseUnit { get; set; } = "mg"; // Selected from a dropdown in UI
        public string Route { get; set; } = "";
        public string Timing { get; set; } = "";
        public VitalDeltas DeltaChange { get; set; } = new(); // Values will be *added not replace* to update the deltas after administration
    }
}
