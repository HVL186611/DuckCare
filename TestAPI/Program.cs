
using DuckLib;


DuckAPI.AddFeedback(1, "yo");
Console.WriteLine(DuckAPI.GetCaseLogs(1)[0].Text);

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