CREATE TABLE [dbo].[tblBloodPressure]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [BPsystolic] INT NOT NULL, 
    [BPdiastolic] INT NOT NULL,
    [MAP] DECIMAL(7,2),
    [BeginningEnd] BIT NOT NULL, 
    [TimeOfDay] DATETIME NOT NULL, 
    [PatientId] UNIQUEIDENTIFIER NOT NULL
)