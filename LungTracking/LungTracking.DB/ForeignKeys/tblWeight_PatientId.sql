ALTER TABLE [dbo].[tblWeight]
	ADD CONSTRAINT [tblWeight_PatientId]
	FOREIGN KEY (PatientId)
	REFERENCES [tblPatient] (Id) ON DELETE NO ACTION