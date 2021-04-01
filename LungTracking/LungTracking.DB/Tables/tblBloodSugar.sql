CREATE TABLE [dbo].[tblBloodSugar]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [BloodSugarNumber] INT NOT NULL, 
    [TimeOfDay] DATETIME NOT NULL,
    [UnitsOfInsulinGiven] INT NOT NULL,
    [TypeOfInsulinGiven] CHAR(1) NOT NULL,
    [Notes] VARCHAR(MAX) NULL,
    [PatientId] UNIQUEIDENTIFIER NOT NULL
)
