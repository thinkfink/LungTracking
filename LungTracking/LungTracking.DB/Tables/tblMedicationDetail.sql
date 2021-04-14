CREATE TABLE [dbo].[tblMedicationDetail]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [MedicationName] VARCHAR(MAX) NOT NULL, 
    [MedicationDosageTotal] VARCHAR(50) NOT NULL, 
    [MedicationDosagePerPill] VARCHAR(50) NOT NULL, 
    [MedicationInstructions] VARCHAR(MAX) NOT NULL,
    [NumberOfPills] INT NOT NULL, 
    [DateFilled] DATE NOT NULL,
    [QuantityOfFill] INT NOT NULL, 
    [RefillDate] DATE NOT NULL, 
    [PatientId] UNIQUEIDENTIFIER NOT NULL
)
