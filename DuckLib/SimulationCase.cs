using System;
using System.Collections.Generic;
using System.Text;

namespace DuckLib
{
    public class SimulationCase
    {
        public int Id { get; set; } = 0;

        // case info
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";

        // simulation content
        public Patient Patient { get; set; } = new();
        public Vitals StartVitals { get; set; } = new();
        public string LabValues { get; set; } = "";
        public VitalDeltas StartDeltas { get; set; } = new();
        public List<Allergy> Allergies { get; set; } = new();

        // simulation goals
        public List<Goal> Goals { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
        public int GoalTimeMinutes { get; set; } = 15; // This should be cut

        // simulation config
        public bool StudentEditable { get; set; } = true;
        public bool IsActive { get; set; } = false;

        // meta?
        public string CreatedByRole { get; set; } = "Student";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
