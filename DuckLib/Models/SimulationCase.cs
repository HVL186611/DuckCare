using System;
using System.Collections.Generic;

namespace DuckLib.Models;

public partial class SimulationCase
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? PatientName { get; set; }

    public int? PatientAge { get; set; }

    public string? PatientSex { get; set; }

    public int? PatientWeightKg { get; set; }

    public double? PatientHeightCm { get; set; }

    public string? PatientAdmittingDiagnosis { get; set; }

    public string? PatientMedicalHistory { get; set; }

    public int? Bpsystolic { get; set; }

    public int? Bpdiastolic { get; set; }

    public int? HeartRate { get; set; }

    public int? RespiratoryRate { get; set; }

    public int? OxygenSaturation { get; set; }

    public int? Temperature { get; set; }

    public int? BpsystolicDelta { get; set; }

    public int? BpdiastolicDelta { get; set; }

    public int? HeartRateDelta { get; set; }

    public int? RespiratoryRateDelta { get; set; }

    public int? SpO2delta { get; set; }

    public int? TemperatureDelta { get; set; }

    public bool? StudentEditable { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedByRole { get; set; }

    public string? GoalDescription { get; set; }

    public int? GoalTimerMinutes { get; set; }

    public int? GoalSystolicBpmin { get; set; }

    public int? GoalSystolicBpmax { get; set; }

    public int? GoalDistolicBpmin { get; set; }

    public int? GoalDistolicBpmax { get; set; }

    public int? GoalHeartRateMin { get; set; }

    public int? GoalHeartRateMax { get; set; }

    public int? GoalRespiratoryRateMin { get; set; }

    public int? GoalRespiratoryRateMax { get; set; }

    public double? GoalSp02Min { get; set; }

    public double? GoalSp02Max { get; set; }

    public double? GoalTemperatureMin { get; set; }

    public double? GoalTemperatureMax { get; set; }

    public virtual ICollection<LabValue> LabValues { get; set; } = new List<LabValue>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
