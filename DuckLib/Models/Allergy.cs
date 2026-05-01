using System;
using System.Collections.Generic;

namespace DuckLib.Models;

public partial class Allergy
{
    public int Id { get; set; }

    public string Allergen { get; set; } = null!;

    public string Reaction { get; set; } = null!;

    public virtual ICollection<Medication> Medications { get; set; } = new List<Medication>();
}
