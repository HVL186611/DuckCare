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
                Id = 0,
                Title = "Test Case",
                Description = "No ducks were harmed during the making of this simulation case.",
                Goals = "Stabilize blood pressure and reduce fever",
                StudentEditable = false,
                IsActive = true,

                Patient = new Patient
                {
                    Id = 1,
                    Name = "John Doe",
                    Age = 72,
                    Sex = "Male",
                    WeightKg = 82.0
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

        // ---
    }
}
