ALTER TABLE [dbo].[tblMedicationTime]
	ADD CONSTRAINT [tblMedicationTime_PatientId]
	FOREIGN KEY (PatientId)
	REFERENCES [tblPatient] (Id) ON DELETE CASCADE