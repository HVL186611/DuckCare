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

        public int Id = -1; // for conversion. if Id==-1, assign Id before saving to database
        internal static List<Order> FromEntity(ICollection<Models.Order> orders)
        {
            throw new NotImplementedException();
        }

        internal DuckLib.Models.Order ToEntity(int SimulationId)
        {
            return new DuckLib.Models.Order
            {
                Id = Id,
                SimulationId = SimulationId,
                MedicationId = Medication.Id,
                Dose = (double)Dose,
                DoseUnit = DoseUnit,
                Route = Route,
                Timing = Timing,
            };
        }
    }
}
