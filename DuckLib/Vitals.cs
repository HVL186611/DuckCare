using System;
using System.Collections.Generic;
using System.Text;

namespace DuckLib
{
    public class Vitals
    {
        public int BPSystolic { get; set; } = 0;  // pressure while heart beats
        public int BPDiastolic { get; set; } = 0;  // pressure while heart rests
        public int HeartRate { get; set; } = 0;
        public int RespiratoryRate { get; set; } = 0;
        public int OxygenSaturation { get; set; } = 0;
        public double Temperature { get; set; } = 0.0;

        // could be useful for log
        public DateTime RecordedAt { get; set; } = DateTime.Now;
    }
}
