using CaseSetup.Services;
using DuckLib;
using Simulation.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
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

        public ObservableCollection<string> AvailableMedications { get; set; } = new() { "Labetalol", "Penicillin", "Ramipril", "Enalapril" };
        public ObservableCollection<string> AvailableUnits { get; set; } = new() { "mg", "µg", "ml", "g" };
        public ObservableCollection<string> AvailableRoutes { get; set; } = new() { "IV", "Sublingual", "Oral", "IM" };
        public ObservableCollection<string> EventLog { get; set; } = new();

        private string? _selectedMedication;
        public string? SelectedMedication
        {
            get => _selectedMedication;
            set { _selectedMedication = value; OnPropertyChanged(nameof(SelectedMedication)); }
        }

        private string? _selectedUnit;
        public string? SelectedUnit
        {
            get => _selectedUnit;
            set { _selectedUnit = value; OnPropertyChanged(nameof(SelectedUnit)); }
        }

        private string? _selectedRoute;
        public string? SelectedRoute
        {
            get => _selectedRoute;
            set { _selectedRoute = value; OnPropertyChanged(nameof(SelectedRoute)); }
        }
        public string DoseInput { get; set; } = "";

        public List<Order> Orders { get; } = new();
        public Goal Goal { get; } = new();
        public List<Allergy> Allergies { get; } = new();

        public ICommand ConfirmCommand { get; }

        public MainViewModel()
        {
            _service = new SimulationCaseService();
            ActiveCase = _service.GetActive();

            if (ActiveCase != null)
            {
                CurrentVitals = ActiveCase.StartVitals;
                LastVitals = ActiveCase.StartVitals;
                CurrentDeltas = ActiveCase.StartDeltas ?? new VitalDeltas();

                Orders = ActiveCase.Orders;
                Goal = ActiveCase.Goals;
                Allergies = ActiveCase.Allergies;
            }

            ConfirmCommand = new RelayCommand(Confirm);

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(60);
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

            Log("Vitals Updated!");
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

        private void Confirm()
        {
            if (string.IsNullOrEmpty(SelectedMedication) || string.IsNullOrEmpty(SelectedUnit) || string.IsNullOrEmpty(SelectedRoute))
            {
                Log("Please fill all fields");
                return;
            }

            if (string.IsNullOrEmpty(DoseInput) || !double.TryParse(DoseInput, out _))
            {
                Log("Dose has to be a valid number");
                return;
            }

            Allergy? triggered = Allergies.FirstOrDefault(a => a.AffectedMedications.Any(m => m.Name == SelectedMedication));
            if (triggered != null)
            {
                Log($"CRITICAL: {SelectedMedication} is contraindicted - {triggered.Allergen}: {triggered.Reaction}");
                return;
            }

            // Because of a somewhat shortsighted design decision I need to rely on the order itself to fetch medicine info.
            // Once the database is hooked up I should be able to do a more elegant lookup
            bool validOrder = false;
            VitalDeltas effect = new();
            foreach (var order in Orders)
            {
                if (SelectedMedication == order.Medication.Name && SelectedRoute == order.Route && SelectedUnit == order.DoseUnit && double.Parse(DoseInput) == order.Dose)
                {
                    validOrder = true;
                    effect = order.DeltaChange;
                    break;
                }
            }

            if (!validOrder)
            {
                Log($"WARNING: Selected administration does not match an order, denied");
                return;
            }
            else
            {
                CurrentDeltas.BPSystolicDelta += effect.BPSystolicDelta;
                CurrentDeltas.BPDiastolicDelta += effect.BPDiastolicDelta;
                CurrentDeltas.HeartRateDelta += effect.HeartRateDelta;
                CurrentDeltas.SpO2Delta += effect.SpO2Delta;
                CurrentDeltas.RespiratoryRateDelta += effect.RespiratoryRateDelta;
                CurrentDeltas.TemperatureDelta += effect.TemperatureDelta;

                Log($"Administered {DoseInput} {SelectedUnit} of {SelectedMedication} through {SelectedRoute}");
            }
        }
        
        private void Log(string message) // In the future this will persist to database via the API, for now it is purely on simulation
        {
            EventLog.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
        }
    }
}
