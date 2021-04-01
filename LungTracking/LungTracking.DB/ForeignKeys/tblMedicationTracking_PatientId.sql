ALTER TABLE [dbo].[tblMedicationTracking]
	ADD CONSTRAINT [tblMedicationTracking_PatientId]
	FOREIGN KEY (PatientId)
	REFERENCES [tblPatient] (Id) ON DELETE CASCADE