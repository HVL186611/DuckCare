using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DuckLib
{
    public class Goal
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; } = null!;

        [Column(TypeName = "INT")]
        public int TimerMinutes { get; set; }

        [Column("SystolicBPMin", TypeName = "INT")]
        public int? SystolicBpmin { get; set; }

        [Column("SystolicBPMax", TypeName = "INT")]
        public int? SystolicBpmax { get; set; }

        [Column("DiastolicBPMin", TypeName = "INT")]
        public int? DiastolicBpmin { get; set; }

        [Column("DiastolicBPMax", TypeName = "INT")]
        public int? DiastolicBpmax { get; set; }

        [Column(TypeName = "INT")]
        public int? HeartRateMin { get; set; }

        [Column(TypeName = "INT")]
        public int? HeartRateMax { get; set; }

        [Column(TypeName = "INT")]
        public int? RespiratoryRateMin { get; set; }

        [Column(TypeName = "INT")]
        public int? RespiratoryRateMax { get; set; }

        [Column("SpO2Min")]
        public double? SpO2min { get; set; }

        [Column("SpO2Max")]
        public double? SpO2max { get; set; }

        public double? TemperatureMin { get; set; }

        public double? TemperatureMax { get; set; }

        [InverseProperty("Goals")]
        public virtual ICollection<SimulationCase> SimulationCases { get; set; } = new List<SimulationCase>();

        [NotMapped]
        public (int Min, int Max)? SystolicBP
        {
            get => SystolicBpmin.HasValue && SystolicBpmax.HasValue
                ? (SystolicBpmin.Value, SystolicBpmax.Value)
                : null;
            set
            {
                SystolicBpmin = value?.Min;
                SystolicBpmax = value?.Max;
            }
        }
        [NotMapped]
        public (int Min, int Max)? DiastolicBP
        {
            get => DiastolicBpmin.HasValue && DiastolicBpmax.HasValue
                ? (DiastolicBpmin.Value, DiastolicBpmax.Value)
                : null;
            set
            {
                DiastolicBpmin = value?.Min;
                DiastolicBpmax = value?.Max;
            }
        }

        [NotMapped]
        public (int Min, int Max)? HeartRate
        {
            get => HeartRateMin.HasValue && HeartRateMax.HasValue
                ? (HeartRateMin.Value, HeartRateMax.Value)
                : null;
            set
            {
                HeartRateMin = value?.Min;
                HeartRateMax = value?.Max;
            }
        }

        [NotMapped]
        public (int Min, int Max)? RespiratoryRate
        {
            get => RespiratoryRateMin.HasValue && RespiratoryRateMax.HasValue
                ? (RespiratoryRateMin.Value, RespiratoryRateMax.Value)
                : null;
            set
            {
                RespiratoryRateMin = value?.Min;
                RespiratoryRateMax = value?.Max;
            }
        }

        [NotMapped]
        public (double Min, double Max)? SpO2
        {
            get => SpO2min.HasValue && SpO2max.HasValue
                ? (SpO2min.Value, SpO2max.Value)
                : null;
            set
            {
                SpO2min = value?.Min;
                SpO2max = value?.Max;
            }
        }

        [NotMapped]
        public (double Min, double Max)? Temperature
        {
            get => TemperatureMin.HasValue && TemperatureMax.HasValue
                ? (TemperatureMin.Value, TemperatureMax.Value)
                : null;
            set
            {
                TemperatureMin = value?.Min;
                TemperatureMax = value?.Max;
            }
        }
    }
}
