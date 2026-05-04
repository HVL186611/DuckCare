
using DuckLib;


SimulationCase sim = DuckAPI.GetSimulationCase(5);
Console.WriteLine(sim.Patient.Medications.ToList()[0]);

/*
 * 
List<string> medications = new List<string> {
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

foreach (string medication in medications)
{
    DuckAPI.dx.Medications.Add(new Medication { Name = medication });
}
DuckAPI.dx.SaveChanges();
//*/