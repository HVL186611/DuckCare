using System;
using System.Collections.Generic;

namespace DuckLib.Models;

public partial class LabValue
{
    public int Id { get; set; }

    public int SimulationId { get; set; }

    public string? Value { get; set; }

    public string? Reference { get; set; }

    public string? Interpretation { get; set; }

    public virtual SimulationCase Simulation { get; set; } = null!;
}
