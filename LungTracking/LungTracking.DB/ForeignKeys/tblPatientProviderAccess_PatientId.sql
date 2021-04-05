ALTER TABLE [dbo].[tblPatientProviderAccess]
	ADD CONSTRAINT [tblPatientProviderAccess_PatientId]
	FOREIGN KEY (PatientId)
	REFERENCES [tblPatient] (Id) ON DELETE CASCADE