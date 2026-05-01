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
        public List<LabValue> LabValues { get; set; } = new();
        public VitalDeltas StartDeltas { get; set; } = new();
        public List<Allergy> Allergies { get; set; } = new();

        // simulation goals
        //public List<Goal> Goals { get; set; } = new();
        public Goal Goals { get; set; } = new(); 
        /* Goal
        {
            TimerMinutes = 60,
            SystolicBP = (90, 120),
            DiastolicBP = (80, 90),
            HeartRate = (60, 100),
            RespiratoryRate = (12, 20),
            SpO2 = (95, 100),
            Temperature = (36.1, 37.2)
        }*/
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
