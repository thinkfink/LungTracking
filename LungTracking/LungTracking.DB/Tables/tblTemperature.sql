CREATE TABLE [dbo].[tblTemperature]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [TempNumber] DECIMAL(18, 2) NOT NULL, 
    [BeginningEnd] BIT NOT NULL, 
    [TimeOfDay] DATETIME NOT NULL, 
    [PatientId] UNIQUEIDENTIFIER NOT NULL
)
