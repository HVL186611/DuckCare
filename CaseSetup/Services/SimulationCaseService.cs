using DuckLib;

/*
 * temporarily stores cases in memory
 * todo: link with database
 */

namespace CaseSetup.Services
{
    public class SimulationCaseService
    {
        private static readonly List<SimulationCase> Cases = new()
        {
            new SimulationCase
            {
                Id = 1,
                Title = "Someone Save Donald!",
                Description = "No ducks were harmed during the making of this simulation case.",
                // Goals = "Stabilize bread pressure and reduce feathver.",
                StudentEditable = false,
                Patient = new Patient
                {
                    Id = 0,
                    Name = "Donald Duck",
                    Age = 25,
                    Sex = "Male",
                    WeightKg = 35.0
                },

                StartVitals = new Vitals
                {
                    BPSystolic = 90,
                    BPDiastolic = 60,
                    HeartRate = 118,
                    RespiratoryRate = 24,
                    OxygenSaturation = 92,
                    Temperature = 39.1
                },
                StartDeltas = new VitalDeltas
                {
                    BPSystolicDelta = -3,
                    HeartRateDelta = 2,
                    SpO2Delta = -1,

                }
            },
            new SimulationCase
            {
                Id = 2,
                Title = "Don't Try This at Home",
                Description = "A goose was harmed during the making of this simulation case.",
                // Goals = "Reduce heart rate to 0.",
                StudentEditable = true,

                Patient = new Patient
                {
                    Id = 1,
                    Name = "Gus Goose",
                    Age = 29,
                    Sex = "Male",
                    WeightKg = 55.0
                },

                StartVitals = new Vitals
                {
                    BPSystolic = 90,
                    BPDiastolic = 60,
                    HeartRate = 118,
                    RespiratoryRate = 24,
                    OxygenSaturation = 92,
                    Temperature = 39.1
                },
                StartDeltas = new VitalDeltas
                {
                    HeartRateDelta = 3
                }
            },
            new SimulationCase
            {
                Id = 2,
                Title = "Hypertensive Emergency",
                Description = "Patient presents with dangerously elevated blood pressure and severe headache.",
                StudentEditable = false,
                IsActive = true,
                Patient = new Patient
                {
                    Id = 2,
                    Name = "Kari Olsen",
                    Age = 58,
                    Sex = "Female",
                    WeightKg = 78.0
                },
                StartVitals = new Vitals
                {
                    BPSystolic = 210,
                    BPDiastolic = 120,
                    HeartRate = 102,
                    RespiratoryRate = 18,
                    OxygenSaturation = 97,
                    Temperature = 37.2
                },
                StartDeltas = new VitalDeltas
                {
                    BPSystolicDelta = 5,
                    HeartRateDelta = 2
                },
                Allergies = new List<Allergy>
                {
                    new Allergy
                    {
                        Id = 0,
                        Allergen = "ACE Inhibitors",
                        Reaction = "Angioedema - severe swelling of face/throat, can block airways",
                        AffectedMedications = new List<Medication>
                        {
                            new Medication { Id = 0, Name = "Ramipril" },
                            new Medication { Id = 1, Name = "Enalapril" }
                        }
                    },
                    new Allergy
                    {
                        Id = 1,
                        Allergen = "Penicillin",
                        Reaction = "Skin rash",
                        AffectedMedications = new List<Medication>
                        {
                            new Medication { Id = 2, Name = "Penicillin" },
                        }
                    }
                },
                Orders = new List<Order>
                {
                    new Order
                    {
                        Medication = new Medication { Id = 3, Name = "Labetalol" },
                        Dose = 20,
                        DoseUnit = "mg",
                        Route = "IV",
                        Timing = "STAT",
                        DeltaChange = new VitalDeltas { BPSystolicDelta = -25, BPDiastolicDelta = -10, HeartRateDelta = -10 }
                    },
                    new Order
                    {
                        Medication = new Medication { Id = 3, Name = "Labetalol" },
                        Dose = 40,
                        DoseUnit = "mg",
                        Route = "IV",
                        Timing = "PRN",
                        DeltaChange = new VitalDeltas { BPSystolicDelta = -35, BPDiastolicDelta = -15, HeartRateDelta = -12 }
                    },
                    new Order
                    {
                        Medication = new Medication { Id = 4, Name = "GTN" },
                        Dose = 0.4,
                        DoseUnit = "mg",
                        Route = "Sublingual",
                        Timing = "PRN",
                        DeltaChange = new VitalDeltas { BPSystolicDelta = -15, BPDiastolicDelta = -8 }
                    }
                },
                Goals = new List<Goal>
                {
                    new Goal
                    {
                        Description = "Reduce systolic BP to safe range within 60 minutes",
                        TimerMinutes = 60,
                        SystolicBP = (140, 160)
                    }
                }
            }
        };

        public List<SimulationCase> GetAll() { return Cases; }

        public SimulationCase? GetById(int Id) { return Cases.FirstOrDefault(c => c.Id == Id); }

        public void Add(SimulationCase simulationCase)
        {
            // assign id
            simulationCase.Id =
                Cases.Count == 0 ?
                0  // 0 if there are no cases
                : Cases.Max(c => c.Id) + 1; 

            // new cases should not be active
            simulationCase.IsActive = false; 

            Cases.Add(simulationCase);
        }

        public void Update(SimulationCase updatedCase)
        {
            SimulationCase? existing = GetById(updatedCase.Id);

            if (existing != null) return;

            // todo: update case
        }

        public void Delete(int id)
        {
            SimulationCase? simulationCase = GetById(id);

            if (simulationCase != null)
            {
                Cases.Remove(simulationCase);
            }
        }

        public void Activate(int id)
        {
            foreach (SimulationCase simulationCase in Cases)
            {
                simulationCase.IsActive = simulationCase.Id == id;
            }
        }
        public void Deactivate(int id)
        {
            foreach (SimulationCase simulationCase in Cases)
            {
                simulationCase.IsActive = simulationCase.Id == id;
            }
        }

        public SimulationCase? GetActive()
        {
            return Cases.FirstOrDefault(c => c.IsActive);
        }


        // ---
    }
}
