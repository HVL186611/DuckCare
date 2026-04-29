using CaseSetup.Services;
using DuckLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Threading;

namespace Simulation.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private readonly SimulationCaseService _service;
        private DispatcherTimer _timer;

        public SimulationCase? ActiveCase {  get; set; }
        public Vitals? CurrentVitals { get; set; }
        public Vitals? LastVitals { get; set; }
        public VitalDeltas CurrentDeltas { get; set; } = new();

        public MainViewModel()
        {
            _service = new SimulationCaseService();
            ActiveCase = _service.GetActive();

            if (ActiveCase != null)
            {
                CurrentVitals = ActiveCase.StartVitals;
                LastVitals = CurrentVitals;
                CurrentDeltas = ActiveCase.StartDeltas ?? new VitalDeltas();
            }

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTick;
            _timer.Start();
        }

        private void OnTick(object? sender, EventArgs e)
        {
            if (CurrentVitals == null) return;

            LastVitals = new Vitals
            {
                BPSystolic = CurrentVitals.BPSystolic,
                BPDiastolic = CurrentVitals.BPDiastolic,
                HeartRate = CurrentVitals.HeartRate,
                RespiratoryRate = CurrentVitals.RespiratoryRate,
                OxygenSaturation = CurrentVitals.OxygenSaturation,
                Temperature = CurrentVitals.Temperature
            };

            CurrentVitals.BPSystolic += CurrentDeltas.BPSystolicDelta;
            CurrentVitals.BPDiastolic += CurrentDeltas.BPDiastolicDelta;
            CurrentVitals.HeartRate += CurrentDeltas.HeartRateDelta;
            CurrentVitals.RespiratoryRate += CurrentDeltas.RespiratoryRateDelta;
            CurrentVitals.OxygenSaturation += CurrentDeltas.SpO2Delta;
            CurrentVitals.Temperature += CurrentDeltas.TemperatureDelta;

            OnPropertyChanged(nameof(BloodPressure));
            OnPropertyChanged(nameof(HeartRate));
            OnPropertyChanged(nameof(SpO2));
            OnPropertyChanged(nameof(RespiratoryRate));
            OnPropertyChanged(nameof(Temperature));
            OnPropertyChanged(nameof(LastBloodPressure));
            OnPropertyChanged(nameof(LastHeartRate));
            OnPropertyChanged(nameof(LastSpO2));
            OnPropertyChanged(nameof(LastRespiratoryRate));
            OnPropertyChanged(nameof(LastTemperature));
        } 

        public string Name => ActiveCase?.Patient.Name ?? "No active case";
        public string InitialBloodPressure => $"{ActiveCase?.StartVitals.BPSystolic}/{ActiveCase?.StartVitals.BPDiastolic} mmHg";
        public string BloodPressure => $"{CurrentVitals?.BPSystolic}/{CurrentVitals?.BPDiastolic} mmHg";
        public string LastBloodPressure => $"{LastVitals?.BPSystolic}/{LastVitals?.BPDiastolic} mmHg";
        public string InitialHeartRate => $"{ActiveCase?.StartVitals.HeartRate} bpm";
        public string HeartRate => $"{CurrentVitals?.HeartRate} bpm";
        public string LastHeartRate => $"{LastVitals?.HeartRate} bpm";
        public string InitialSpO2 => $"{ActiveCase?.StartVitals.OxygenSaturation}%";
        public string SpO2 => $"{CurrentVitals?.OxygenSaturation}%";
        public string LastSpO2 => $"{LastVitals?.OxygenSaturation}%";
        public string InitialRespiratoryRate => $"{ActiveCase?.StartVitals.RespiratoryRate} b/m";
        public string RespiratoryRate => $"{CurrentVitals?.RespiratoryRate} b/m";
        public string LastRespiratoryRate => $"{LastVitals?.RespiratoryRate} b/m";
        public string InitialTemperature => $"{ActiveCase?.StartVitals.Temperature}°C";
        public string Temperature => $"{CurrentVitals?.Temperature}°C";
        public string LastTemperature => $"{LastVitals?.Temperature}°C";
    }
}
