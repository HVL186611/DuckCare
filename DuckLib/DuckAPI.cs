using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestLib.Temp;

namespace DuckLib
{
    public class DuckAPI
    {
        public static DuckContext dx = new();  // todo: make private when testing is done

        public static void Add(SimulationCase sim) { 
            dx.Add(sim);
            dx.SaveChanges();
        }
        public static void RemoveCase(SimulationCase sim) { 
            dx.Remove(sim); 
            dx.SaveChanges();
        }
        public static void Update(SimulationCase sim) { 
            using DuckContext dc = new();
            dx.Update(sim); 
            dx.SaveChanges(true);
        }

        public static List<SimulationCase> GetSimulationCases()
        {
            return dx.SimulationCases
                .Include(s => s.Patient)
                .Include(s => s.StartVitals)
                .Include(s => s.StartDeltas)
                .Include(s => s.Orders)
                    .ThenInclude(o => o.Medication)
                .Include(s => s.Allergies)
                    .ThenInclude(a => a.Medications)
                .Include(s => s.Goals)
                .ToList();
        }

        public static SimulationCase? GetSimulationCase(int Id)
        {
            return dx.SimulationCases
                .Include(s => s.Patient)
                    .ThenInclude(p => p.Medications)
                        .ThenInclude(o => o.Medication)
                .Include(s => s.StartVitals)
                .Include(s => s.StartDeltas)
                .Include(s => s.Orders)
                    .ThenInclude(o => o.Medication)
                .Include(s => s.Orders)
                    .ThenInclude(o => o.DeltaChange)
                .Include(s => s.Allergies)
                    .ThenInclude(a => a.Medications)
                .Include(s => s.LabValues)
                .Include(s => s.Goals)
                .Where(s => s.Id == Id)
                .FirstOrDefault();
        }

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

        public static List<Feedback> GetFeedback(int simulationCaseId)
        {
            return dx.Feedback.Where(c => c.SimulationCaseId == simulationCaseId).ToList();
        }
        public static void AddFeedback(int simulationCaseId, string text)
        {
            Feedback log = new Feedback
            {
                SimulationCaseId = simulationCaseId,
                Text = text
            };

            dx.Feedback.Add(log);
            dx.SaveChanges();
        }
    }
}
