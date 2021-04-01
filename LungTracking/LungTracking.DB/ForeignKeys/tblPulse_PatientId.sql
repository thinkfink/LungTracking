ALTER TABLE [dbo].[tblPulse]
	ADD CONSTRAINT [tblPulse_PatientId]
	FOREIGN KEY (PatientId)
	REFERENCES [tblPatient] (Id) ON DELETE NO ACTION