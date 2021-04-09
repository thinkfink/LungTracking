CREATE TABLE [dbo].[tblAppointment]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Date] DATE NOT NULL,
    [TimeStart] TIME NOT NULL, 
    [TimeEnd] TIME NOT NULL, 
    [Description] VARCHAR(MAX) NOT NULL, 
    [Location] VARCHAR(MAX) NOT NULL, 
    [PatientId] UNIQUEIDENTIFIER NOT NULL
)
