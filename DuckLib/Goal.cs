using System;
using System.Collections.Generic;
using System.Text;

namespace DuckLib
{
    public class Goal
    {
        public string Description { get; set; } = "";
        public int TimerMinutes { get; set; }

        public (int Min, int Max)? SystolicBP { get; set; }
        public (int Min, int Max)? DiastolicBP { get; set; }
        public (int Min, int Max)? HeartRate {  get; set; }
        public (int Min, int Max)? RespiratoryRate {  get; set; }
        public (double Min, double Max)? SpO2 { get; set; }
        public (double Min, double Max)? Temperature { get; set; }
    }
}
