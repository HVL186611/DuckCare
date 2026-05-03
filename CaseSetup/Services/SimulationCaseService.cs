using DuckLib;


/*
 * temporarily stores cases in memory
 * todo: link with database
 */

namespace CaseSetup.Services
{
    public class SimulationCaseService
    {
        private static readonly List<string> Medications = new List<string> {
            "Breadicillin",
            "Featherol",
            "Ducaine",
            "Duckamine",
            "Mallardol",
            "Waddlemycin",
            "Billatonin",
            "Floatamine",
            "Paddlex",
            "Plumagrel",
            "Plumadryl",
            "Quackzac",
            "Billagra",
            "Nestapro",
            "Nestrogen"
        };

        public List<SimulationCase> GetAllCases() {
            return DuckAPI.GetSimulationCases();
        }

        public SimulationCase? GetById(int Id) { return GetAllCases().FirstOrDefault(c => c.Id == Id); }

        public void Add(SimulationCase simulationCase) { DuckAPI.Add(simulationCase);  }

        public void Update(SimulationCase updatedCase)
        {
            SimulationCase? existing = GetById(updatedCase.Id);

            if (existing == null) return;

            // todo: update case
            DuckAPI.Update(updatedCase);
        }

        public void Delete(int id)
        {
            SimulationCase? simulationCase = GetById(id);

            if (simulationCase != null)
            {
                DuckAPI.RemoveCase(simulationCase);
            }
        }

        public void Activate(int id)
        {
            foreach (SimulationCase simulationCase in GetAllCases())
            {
                if (simulationCase.Id == id)
                {
                    simulationCase.IsActive = 1;
                    DuckAPI.Update(simulationCase);
                }
                else
                {
                    if (simulationCase.IsActive == 1)
                    {
                        simulationCase.IsActive = 0; DuckAPI.Update(simulationCase);
                    }
                }
            }
        }
        [Obsolete]
        public void Deactivate(int id)
        {
            foreach (SimulationCase simulationCase in GetAllCases())
            {
                if (simulationCase.Id == id)
                {
                    simulationCase.IsActive = 0;
                    DuckAPI.Update(simulationCase);
                }
            }
        }

        public SimulationCase? GetActive()
        {
            return GetAllCases().FirstOrDefault(c => c.IsActive == 1);
        }


        // ---

        public List<string> GetMedicationNames()
        {
            return DuckAPI.GetMedications().Select(m => m.Name).ToList();
        }

        public List<Medication> GetMedications()
        {
            return DuckAPI.GetMedications().ToList();
        }
    }
}
