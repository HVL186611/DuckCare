using System;
using System.Collections.Generic;
using System.Text;
using TestLib.Temp;

namespace DuckLib
{
    public class DuckAPI
    {
        public static readonly DuckContext dx = new();  // todo: make private when testing is done

        public static void Add(SimulationCase sim) { 
            dx.Add(sim);
            dx.SaveChanges();
        }
        public static void RemoveCase(SimulationCase sim) { 
            dx.Remove(sim); 
            dx.SaveChanges();
        }
        public static void Update(SimulationCase sim) { 
            dx.Update(sim); 
            dx.SaveChanges(true);
        }
        public static List<SimulationCase> GetSimulationCases() { return dx.SimulationCases.ToList(); }
        public static SimulationCase? GetSimulationCase(int Id) { return dx.SimulationCases.Where(s => s.Id == Id).FirstOrDefault(); }
        public static List<Medication> GetMedications() { return dx.Medications.ToList(); }
        public static Medication? GetMedication(int Id) { return dx.Medications.Where(s => s.Id == Id).FirstOrDefault(); }

        public static void AddCaseLog(int simulationCaseId, string text)
        {
            CaseLog log = new CaseLog
            {
                SimulationCaseId = simulationCaseId,
                Text = text
            };

            dx.CaseLogs.Add(log);
            dx.SaveChanges();
        }

        public static List<CaseLog> GetCaseLogs(int simulationCaseId)
        {
            return dx.CaseLogs.Where(c => c.SimulationCaseId == simulationCaseId).ToList();
        }
    }
}
