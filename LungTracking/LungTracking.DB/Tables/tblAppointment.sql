CREATE TABLE [dbo].[tblAppointment]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    ,
    [StartDateTime] DATETIME NOT NULL, 
    [EndDateTime] DATETIME NOT NULL, 
    [Description] VARCHAR(MAX) NOT NULL, 
    [Location] VARCHAR(MAX) NOT NULL, 
    [PatientId] UNIQUEIDENTIFIER NOT NULL
)
