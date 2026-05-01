using System;
using System.Collections.Generic;

namespace DuckLib.Models;

public partial class Medication
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();
}
