ALTER TABLE [dbo].[tblMedicationDetails]
	ADD CONSTRAINT [tblMedicationDetails_PatientId]
	FOREIGN KEY (PatientId)
	REFERENCES [tblPatient] (Id) ON DELETE CASCADE