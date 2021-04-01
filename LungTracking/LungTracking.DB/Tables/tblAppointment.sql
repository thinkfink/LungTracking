CREATE TABLE [dbo].[tblAppointment]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [AppointmentDate] DATE NOT NULL,
    [AppointmentTimeStart] TIME NOT NULL, 
    [AppointmentTimeEnd] TIME NOT NULL, 
    [AppointmentDescription] VARCHAR(MAX) NOT NULL, 
    [AppointmentLocation] VARCHAR(MAX) NOT NULL, 
    [PatientId] UNIQUEIDENTIFIER NOT NULL
)
