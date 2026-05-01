using System;
using System.Collections.Generic;

namespace DuckLib.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? SimulationId { get; set; }

    public int? MedicationId { get; set; }

    public double? Dose { get; set; }

    public string? DoseUnit { get; set; }

    public string? Route { get; set; }

    public string? Timing { get; set; }

    public virtual Medication? Medication { get; set; }

    public virtual SimulationCase? Simulation { get; set; }
}
