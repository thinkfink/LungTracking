ALTER TABLE [dbo].[tblPatientCaregiverAccess]
	ADD CONSTRAINT [tblPatientCaregiverAccess_PatientId]
	FOREIGN KEY (PatientId)
	REFERENCES [tblPatient] (Id) ON DELETE CASCADE