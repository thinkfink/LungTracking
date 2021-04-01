ALTER TABLE [dbo].[tblBloodPressure]
	ADD CONSTRAINT [tblBloodPressure_PatientId]
	FOREIGN KEY (PatientId)
	REFERENCES [tblPatient] (Id) ON DELETE CASCADE