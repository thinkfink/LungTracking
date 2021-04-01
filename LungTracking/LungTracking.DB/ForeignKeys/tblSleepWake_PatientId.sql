ALTER TABLE [dbo].[tblSleepWake]
	ADD CONSTRAINT [tblSleepWake_PatientId]
	FOREIGN KEY (PatientId)
	REFERENCES [tblPatient] (Id) ON DELETE NO ACTION