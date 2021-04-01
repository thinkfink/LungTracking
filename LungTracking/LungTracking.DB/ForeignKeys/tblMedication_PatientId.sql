ALTER TABLE [dbo].[tblMedication]
	ADD CONSTRAINT [tblMedication_PatientId]
	FOREIGN KEY (PatientId)
	REFERENCES [tblPatient] (Id) ON DELETE NO ACTION