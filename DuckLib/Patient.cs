using System;
using System.Collections.Generic;
using System.Text;

namespace DuckLib
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Sex {  get; set; } = "";
        public double WeightKg { get; set; }
        public double HeightCm  { get; set; }
        public string Bed { get; set; } = "";
        public DateTime AdmissionDate {  get; set; }
        public string AdmittingDiagnosis { get; set; } = "";
        public string MedicalHistory { get; set; } = "";
        public string SurgicalHistory { get; set; } = "";
        public string SocialHistory { get; set; } = "";
        public string FamilyHistory { get; set; } = "";
        public List<Allergy> Allergies { get; set; } = new();
        public List<Medication> Medications { get; set; } = new();
    }
}
