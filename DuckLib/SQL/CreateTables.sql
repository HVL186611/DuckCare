/* 
    Drop tables in dependency order
*/

DROP TABLE IF EXISTS AllergyAffectedMedications;
DROP TABLE IF EXISTS PatientAllergies;
DROP TABLE IF EXISTS SimulationCaseAllergies;
DROP TABLE IF EXISTS LabValues;
DROP TABLE IF EXISTS Orders;
DROP TABLE IF EXISTS Allergies;
DROP TABLE IF EXISTS Medications;
DROP TABLE IF EXISTS Goals;
DROP TABLE IF EXISTS VitalDeltas;
DROP TABLE IF EXISTS Vitals;
DROP TABLE IF EXISTS Patients;
DROP TABLE IF EXISTS SimulationCases;


/*
    Root table
*/

CREATE TABLE SimulationCases (
    Id INT IDENTITY(1,1) PRIMARY KEY,

    Title NVARCHAR(200) NOT NULL DEFAULT '',
    Description NVARCHAR(MAX) NOT NULL DEFAULT '',

    PatientId INT NULL,
    StartVitalsId INT NULL,
    StartDeltasId INT NULL,
    GoalsId INT NULL,

    GoalTimeMinutes INT NOT NULL DEFAULT 15,

    StudentEditable BIT NOT NULL DEFAULT 1,
    IsActive BIT NOT NULL DEFAULT 0,

    CreatedByRole NVARCHAR(100) NOT NULL DEFAULT 'Student',
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);


/*
    Patient
*/

CREATE TABLE Patients (
    Id INT IDENTITY(1,1) PRIMARY KEY,

    Name NVARCHAR(200) NOT NULL DEFAULT '',
    Age INT NOT NULL DEFAULT 0,
    Sex NVARCHAR(50) NOT NULL DEFAULT '',

    WeightKg FLOAT NOT NULL DEFAULT 0,
    HeightCm FLOAT NOT NULL DEFAULT 0,

    Bed NVARCHAR(100) NOT NULL DEFAULT '',
    AdmissionDate DATETIME2 NULL,

    AdmittingDiagnosis NVARCHAR(MAX) NOT NULL DEFAULT '',
    MedicalHistory NVARCHAR(MAX) NOT NULL DEFAULT '',
    SurgicalHistory NVARCHAR(MAX) NOT NULL DEFAULT '',
    SocialHistory NVARCHAR(MAX) NOT NULL DEFAULT '',
    FamilyHistory NVARCHAR(MAX) NOT NULL DEFAULT ''
);


/*
    Vitals
    Add Id to the C# Vitals class.
*/

CREATE TABLE Vitals (
    Id INT IDENTITY(1,1) PRIMARY KEY,

    BPSystolic INT NOT NULL DEFAULT 0,
    BPDiastolic INT NOT NULL DEFAULT 0,
    HeartRate INT NOT NULL DEFAULT 0,
    RespiratoryRate INT NOT NULL DEFAULT 0,
    OxygenSaturation INT NOT NULL DEFAULT 0,
    Temperature FLOAT NOT NULL DEFAULT 0,

    RecordedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);


/*
    VitalDeltas
    Add Id to the C# VitalDeltas class.
*/

CREATE TABLE VitalDeltas (
    Id INT IDENTITY(1,1) PRIMARY KEY,

    BPSystolicDelta INT NOT NULL DEFAULT 0,
    BPDiastolicDelta INT NOT NULL DEFAULT 0,
    HeartRateDelta INT NOT NULL DEFAULT 0,
    RespiratoryRateDelta INT NOT NULL DEFAULT 0,
    SpO2Delta INT NOT NULL DEFAULT 0,
    TemperatureDelta INT NOT NULL DEFAULT 0
);


/*
    Goals
    Add Id to the C# Goal class.
*/

CREATE TABLE Goals (
    Id INT IDENTITY(1,1) PRIMARY KEY,

    Description NVARCHAR(MAX) NOT NULL DEFAULT '',
    TimerMinutes INT NOT NULL DEFAULT 0,

    SystolicBPMin INT NULL,
    SystolicBPMax INT NULL,

    DiastolicBPMin INT NULL,
    DiastolicBPMax INT NULL,

    HeartRateMin INT NULL,
    HeartRateMax INT NULL,

    RespiratoryRateMin INT NULL,
    RespiratoryRateMax INT NULL,

    SpO2Min FLOAT NULL,
    SpO2Max FLOAT NULL,

    TemperatureMin FLOAT NULL,
    TemperatureMax FLOAT NULL
);


/*
    Medications
    Matches Medication.cs
*/

CREATE TABLE Medications (
    Id INT IDENTITY(1,1) PRIMARY KEY,

    Name NVARCHAR(200) NOT NULL DEFAULT '',

    -- Optional fields used when medication is attached to a patient
    Route NVARCHAR(100) NULL,
    Dose NVARCHAR(100) NULL,
    Frequency NVARCHAR(100) NULL
);


/*
    Orders
    Used by:
    - SimulationCase.Orders
    - Patient.Medications

    DeltaChange is stored as a VitalDeltas row.
*/

CREATE TABLE Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,

    SimulationCaseId INT NULL,
    PatientId INT NULL,

    MedicationId INT NOT NULL,
    DeltaChangeId INT NULL,

    Dose FLOAT NOT NULL DEFAULT 0,
    DoseUnit NVARCHAR(50) NOT NULL DEFAULT 'mg',
    Route NVARCHAR(100) NOT NULL DEFAULT '',
    Timing NVARCHAR(200) NOT NULL DEFAULT '',

    CONSTRAINT FK_Orders_SimulationCases
        FOREIGN KEY (SimulationCaseId)
        REFERENCES SimulationCases(Id)
        ON DELETE CASCADE,

    CONSTRAINT FK_Orders_Patients
        FOREIGN KEY (PatientId)
        REFERENCES Patients(Id)
        ON DELETE NO ACTION,

    CONSTRAINT FK_Orders_Medications
        FOREIGN KEY (MedicationId)
        REFERENCES Medications(Id),

    CONSTRAINT FK_Orders_DeltaChange
        FOREIGN KEY (DeltaChangeId)
        REFERENCES VitalDeltas(Id)
);


/*
    Allergies
*/

CREATE TABLE Allergies (
    Id INT IDENTITY(1,1) PRIMARY KEY,

    Allergen NVARCHAR(200) NOT NULL DEFAULT '',
    Reaction NVARCHAR(MAX) NOT NULL DEFAULT ''
);


/*
    SimulationCase.Allergies
*/

CREATE TABLE SimulationCaseAllergies (
    SimulationCaseId INT NOT NULL,
    AllergyId INT NOT NULL,

    PRIMARY KEY (SimulationCaseId, AllergyId),

    CONSTRAINT FK_SimulationCaseAllergies_SimulationCases
        FOREIGN KEY (SimulationCaseId)
        REFERENCES SimulationCases(Id)
        ON DELETE CASCADE,

    CONSTRAINT FK_SimulationCaseAllergies_Allergies
        FOREIGN KEY (AllergyId)
        REFERENCES Allergies(Id)
        ON DELETE CASCADE
);


/*
    Patient.Allergies
*/

CREATE TABLE PatientAllergies (
    PatientId INT NOT NULL,
    AllergyId INT NOT NULL,

    PRIMARY KEY (PatientId, AllergyId),

    CONSTRAINT FK_PatientAllergies_Patients
        FOREIGN KEY (PatientId)
        REFERENCES Patients(Id)
        ON DELETE CASCADE,

    CONSTRAINT FK_PatientAllergies_Allergies
        FOREIGN KEY (AllergyId)
        REFERENCES Allergies(Id)
        ON DELETE CASCADE
);


/*
    Allergy.AffectedMedications
*/

CREATE TABLE AllergyAffectedMedications (
    AllergyId INT NOT NULL,
    MedicationId INT NOT NULL,

    PRIMARY KEY (AllergyId, MedicationId),

    CONSTRAINT FK_AllergyAffectedMedications_Allergies
        FOREIGN KEY (AllergyId)
        REFERENCES Allergies(Id)
        ON DELETE CASCADE,

    CONSTRAINT FK_AllergyAffectedMedications_Medications
        FOREIGN KEY (MedicationId)
        REFERENCES Medications(Id)
        ON DELETE CASCADE
);


/*
    LabValues
    Matches LabValue.cs

    LabValue currently has:
        public int Id = -1;

    I would change that to:
        public int Id { get; set; } = -1;
*/

CREATE TABLE LabValues (
    Id INT IDENTITY(1,1) PRIMARY KEY,

    SimulationCaseId INT NOT NULL,

    Name NVARCHAR(200) NOT NULL DEFAULT '',
    Value NVARCHAR(200) NOT NULL DEFAULT '',
    Reference NVARCHAR(200) NOT NULL DEFAULT '',
    Interpretation NVARCHAR(MAX) NOT NULL DEFAULT '',

    CONSTRAINT FK_LabValues_SimulationCases
        FOREIGN KEY (SimulationCaseId)
        REFERENCES SimulationCases(Id)
        ON DELETE CASCADE
);


/*
    Add one-to-one references from SimulationCases
*/

ALTER TABLE SimulationCases
ADD CONSTRAINT FK_SimulationCases_Patients
    FOREIGN KEY (PatientId)
    REFERENCES Patients(Id);

ALTER TABLE SimulationCases
ADD CONSTRAINT FK_SimulationCases_StartVitals
    FOREIGN KEY (StartVitalsId)
    REFERENCES Vitals(Id);

ALTER TABLE SimulationCases
ADD CONSTRAINT FK_SimulationCases_StartDeltas
    FOREIGN KEY (StartDeltasId)
    REFERENCES VitalDeltas(Id);

ALTER TABLE SimulationCases
ADD CONSTRAINT FK_SimulationCases_Goals
    FOREIGN KEY (GoalsId)
    REFERENCES Goals(Id);