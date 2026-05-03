using DuckLib;

/*
DuckAPI.dx.Add(new DuckLib.Models.Medication
{
    Id = 1,
    Name = "Hah"
});
DuckAPI.dx.SaveChanges(); // */

//SimulationCase sim = SimulationCase.FromEntity(DuckAPI.dx.SimulationCases.Where(s => s.Id == 3).ToList()[0]);


SimulationCase testCase = new SimulationCase
{
    Id = 3,
    Title = "Hypertensive Emergency",
    Description = "Patient presents with dangerously elevated blood pressure and severe headache.",
    StudentEditable = 1,
    IsActive = 0,
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
                        }
                    }
                },
    Orders = new List<Order>
                {
                    new Order
                    {
                        Id = 0,
                        Medication = new Medication { Id = 5, Name = "Labetalol" },
                        Dose = 20,
                        DoseUnit = "mg",
                        Route = "IV",
                        Timing = "STAT",
                        DeltaChange = new VitalDeltas { BPSystolicDelta = -25, BPDiastolicDelta = -10, HeartRateDelta = -10 }
                    }
                },
    Goals = new Goal
    {
        Description = "Reduce systolic BP to safe range within 60 minutes",
        TimerMinutes = 60,
        SystolicBP = (140, 160)
    }
};

DuckAPI.dx.Add(testCase.ToEntity());
DuckAPI.dx.SaveChanges();

/*
Console.WriteLine(DuckAPI.GetMedication(1).Name);

Medication med = new Medication
{
    Id = 2,
    Name = "Test"
};

DuckAPI.dx.Add(med.ToEntity());
DuckAPI.dx.SaveChanges();

Console.WriteLine(DuckAPI.GetMedication(2).Name);

// */