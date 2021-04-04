CREATE TABLE [dbo].[tblEmergency]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [EmergencyType] VARCHAR(50) NOT NULL, 
    [EmergencyMessage] VARCHAR(MAX) NOT NULL, 
    [TimeOfDay] DATETIME NOT NULL, 
    [PatientId] UNIQUEIDENTIFIER NOT NULL
)
