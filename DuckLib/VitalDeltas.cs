using System;
using System.Collections.Generic;
using System.Text;

namespace DuckLib
{
    public class VitalDeltas
    {
        public int BPSystolicDelta { get; set; }
        public int BPDiastolicDelta { get; set; }
        public int HeartRateDelta { get; set; }
        public int RespiratoryRateDelta { get; set; }
        public int SpO2Delta { get; set; }
        public int TemperatureDelta { get; set; }
    }
}
