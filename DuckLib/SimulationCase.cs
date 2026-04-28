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

        // simulation goals
        public string Goals { get; set; } = "";
        public List<Order> Orders { get; set; } = new();
        public int GoalTimeMinutes { get; set; } = 15;

        // simulation config
        public bool StudentEditable { get; set; } = true;
        public bool IsActive { get; set; } = false;

        // meta?
        public string CreatedByRole { get; set; } = "Student";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
