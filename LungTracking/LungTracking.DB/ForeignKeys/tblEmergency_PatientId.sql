ALTER TABLE [dbo].[tblEmergency]
	ADD CONSTRAINT [tblEmergency_PatientId]
	FOREIGN KEY (PatientId)
	REFERENCES [tblPatient] (Id) ON DELETE CASCADE