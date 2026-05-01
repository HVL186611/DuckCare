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

        public static SimulationCase FromEntity(DuckLib.Models.SimulationCase entity)
        {
            return new SimulationCase
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Patient = new Patient
                {
                    Name = entity.PatientName,
                    Sex = entity.PatientSex,
                    WeightKg = (double) entity.PatientWeightKg,
                    HeightCm = (double) entity.PatientHeightCm,
                    AdmittingDiagnosis = entity.PatientAdmittingDiagnosis,
                    MedicalHistory = entity.PatientMedicalHistory,
                },
                StartVitals = new Vitals
                {
                    BPDiastolic = entity.Bpdiastolic ?? 0,
                    BPSystolic = entity.Bpsystolic ?? 0,
                    HeartRate = entity.HeartRate ?? 0,
                    RespiratoryRate = entity.RespiratoryRate ?? 0,
                    OxygenSaturation = entity.OxygenSaturation ?? 0,
                    Temperature = entity.Temperature ?? 0,
                },
                StartDeltas = new VitalDeltas
                {
                    BPDiastolicDelta = entity.BpdiastolicDelta ?? 0,
                    BPSystolicDelta = entity.BpsystolicDelta ?? 0,
                    HeartRateDelta = entity.HeartRateDelta ?? 0,
                    RespiratoryRateDelta = entity.RespiratoryRateDelta ?? 0,
                    SpO2Delta = entity.SpO2delta ?? 0,
                    TemperatureDelta = entity.TemperatureDelta ?? 0
                },
                StudentEditable = entity.StudentEditable ?? true,
                IsActive = entity.IsActive ?? false,
                CreatedByRole = entity.CreatedByRole ?? "",
                Goals = new Goal
                {
                    Description = entity.GoalDescription,
                    TimerMinutes = entity.GoalTimerMinutes ?? 0,
                    SystolicBP = (entity.GoalSystolicBpmin ?? 0, entity.GoalSystolicBpmax ?? 0),
                    DiastolicBP = (entity.GoalDistolicBpmin ?? 0, entity.GoalDistolicBpmax ?? 0),
                    HeartRate = (entity.GoalHeartRateMin ?? 0, entity.GoalHeartRateMax ?? 0),
                    RespiratoryRate = (entity.GoalRespiratoryRateMin ?? 0, entity.GoalRespiratoryRateMax ?? 0),
                    SpO2 = (entity.GoalSp02Min ?? 0, entity.GoalSp02Max ?? 0),
                    Temperature = (entity.GoalTemperatureMin ?? 0, entity.GoalTemperatureMax ?? 0),
                },
                LabValues = LabValue.FromEntity(entity.LabValues),
                Orders = Order.FromEntity(entity.Orders)
            };
        }

        public DuckLib.Models.SimulationCase ToEntity()
        {
            return new DuckLib.Models.SimulationCase
            {
                Id = Id,
                Title = Title,
                Description = Description,

                PatientName = Patient.Name,
                PatientSex = Patient.Sex,
                PatientWeightKg = (int)Patient.WeightKg,
                PatientHeightCm = (int)Patient.HeightCm,
                PatientAdmittingDiagnosis = Patient.AdmittingDiagnosis,
                PatientMedicalHistory = Patient.MedicalHistory,

                Bpdiastolic = StartVitals.BPDiastolic,
                Bpsystolic = StartVitals.BPSystolic,
                HeartRate = StartVitals.HeartRate,
                RespiratoryRate = StartVitals.RespiratoryRate,
                OxygenSaturation = StartVitals.OxygenSaturation,
                Temperature = (int)StartVitals.Temperature,

                BpdiastolicDelta = StartDeltas.BPDiastolicDelta,
                BpsystolicDelta = StartDeltas.BPSystolicDelta,
                HeartRateDelta = StartDeltas.HeartRateDelta,
                RespiratoryRateDelta = StartDeltas.RespiratoryRateDelta,
                SpO2delta = StartDeltas.SpO2Delta,
                TemperatureDelta = StartDeltas.TemperatureDelta,

                StudentEditable = StudentEditable,
                IsActive = IsActive,
                CreatedByRole = CreatedByRole,

                GoalDescription = Goals?.Description,
                GoalTimerMinutes = Goals?.TimerMinutes,

                GoalSystolicBpmin = Goals?.SystolicBP?.Min,
                GoalSystolicBpmax = Goals?.SystolicBP?.Max,

                GoalDistolicBpmin = Goals?.DiastolicBP?.Min,
                GoalDistolicBpmax = Goals?.DiastolicBP?.Max,

                GoalHeartRateMin = Goals?.HeartRate?.Min,
                GoalHeartRateMax = Goals?.HeartRate?.Max,

                GoalRespiratoryRateMin = Goals?.RespiratoryRate?.Min,
                GoalRespiratoryRateMax = Goals?.RespiratoryRate?.Max,

                GoalSp02Min = Goals?.SpO2?.Min,
                GoalSp02Max = Goals?.SpO2?.Max,

                GoalTemperatureMin = Goals?.Temperature?.Min,
                GoalTemperatureMax = Goals?.Temperature?.Max,

                LabValues = LabValues.Select(labValue => labValue.ToEntity(Id)).ToList(),
                Orders = Orders.Select(order => order.ToEntity(Id)).ToList()
            };
        }
    }
}
