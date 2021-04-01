CREATE TABLE [dbo].[tblAppointment]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [AppointmentDateTimeStart] DATETIME NOT NULL, 
    [AppointmentDateTimeEnd] DATETIME NOT NULL, 
    [AppointmentDescription] VARCHAR(MAX) NOT NULL, 
    [AppointmentLocation] VARCHAR(MAX) NOT NULL, 
    [PatientId] UNIQUEIDENTIFIER NOT NULL
)
