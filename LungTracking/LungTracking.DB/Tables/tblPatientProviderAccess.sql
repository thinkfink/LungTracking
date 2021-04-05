CREATE TABLE [dbo].[tblPatientProviderAccess]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [PatientId] UNIQUEIDENTIFIER NOT NULL, 
    [ProviderId] UNIQUEIDENTIFIER NOT NULL
    PRIMARY KEY ([PatientId], [ProviderId])
)
