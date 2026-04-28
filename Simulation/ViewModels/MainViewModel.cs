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
        public VitalDeltas CurrentDeltas { get; set; } = new();

        public MainViewModel()
        {
            _service = new SimulationCaseService();
            ActiveCase = _service.GetActive();

            if (ActiveCase != null)
            {
                CurrentVitals = ActiveCase.StartVitals;
                CurrentDeltas = ActiveCase.StartDeltas ?? new VitalDeltas();
            }

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(10);
            _timer.Tick += OnTick;
            _timer.Start();
        }

        private void OnTick(object? sender, EventArgs e)
        {
            if (CurrentVitals == null) return;

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
        }

        public string Name => ActiveCase?.Patient.Name ?? "No active case";
        public string InitialBloodPressure => $"BP: {ActiveCase?.StartVitals.BPSystolic}/{ActiveCase?.StartVitals.BPDiastolic} mmHg";
        public string BloodPressure => $"BP: {CurrentVitals?.BPSystolic}/{CurrentVitals?.BPDiastolic} mmHg";
        public string InitialHeartRate => $"HR: {ActiveCase?.StartVitals.HeartRate} bpm";
        public string HeartRate => $"HR: {CurrentVitals?.HeartRate} bpm";
        public string InitialSpO2 => $"SpO2: {ActiveCase?.StartVitals.OxygenSaturation}%";
        public string SpO2 => $"SpO2: {CurrentVitals?.OxygenSaturation}%";
        public string InitialRespiratoryRate => $"RR: {ActiveCase?.StartVitals.RespiratoryRate} b/m";
        public string RespiratoryRate => $"RR: {CurrentVitals?.RespiratoryRate} b/m";
        public string InitialTemperature => $"Temp: {ActiveCase?.StartVitals.Temperature}°C";
        public string Temperature => $"Temp: {CurrentVitals?.Temperature}°C";
    }
}
