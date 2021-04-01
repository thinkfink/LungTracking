CREATE TABLE [dbo].[tblMedication]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [MedicationName] VARCHAR(MAX) NOT NULL, 
    [MedicationDosageTotal] VARCHAR(50) NOT NULL, 
    [MedicationDosagePerPill] VARCHAR(50) NOT NULL, 
    [NumberOfPills] INT NOT NULL, 
    [MedicationTime] DATETIME NOT NULL, 
    [ConfirmTaken] BIT NOT NULL, 
    [DateFilled] DATE NOT NULL, 
    [QuantityOfFill] INT NOT NULL, 
    [RefillDate] DATE NOT NULL, 
    [PatientId] UNIQUEIDENTIFIER NOT NULL
)
