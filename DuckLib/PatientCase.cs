using System;
using System.Collections.Generic;
using System.Text;


/*
 * DENNE KLASSEN BRUKES IKKE
 */



namespace DuckLib
{
    public class PatientCase
    {
        public int Id { get; set; } = 0;

        // patient demographics
        public string Name { get; set; } = "";
        public int Age { get; set; } = 0;  // todo: use date of birth instead?
        public string Sex { get; set; } = "";
        public double Weight { get; set; } = 0.0;

        // history & exam
        public string MedicalHistory { get; set; } = "";
        public string Diagnoses { get; set; } = "";
        public string Medications { get; set; } = "";
        public string Allergies { get; set; } = "";
        public string LabValues { get; set; } = "";

        // cardiovascular 
        public int BPSystolic { get; set; } = 0;  // pressure while heart beats
        public int BPDiastolic { get; set; } = 0;  // pressure while heart rests
        public int HeartRate { get; set; } = 0;
        public int RespiratoryRate { get; set; } = 0;
        public int OxygenSaturation { get; set; } = 0;
        public double Temperature { get; set; } = 0.0;

        // case config
        public string Goals { get; set; } = "";
        public bool StudentEditable { get; set; } = true;
        public bool IsActive { get; set; } = false;
    }
}
