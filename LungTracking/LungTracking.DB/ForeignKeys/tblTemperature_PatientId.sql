ALTER TABLE [dbo].[tblTemperature]
	ADD CONSTRAINT [tblTemperature_PatientId]
	FOREIGN KEY (PatientId)
	REFERENCES [tblPatient] (Id) ON DELETE CASCADE