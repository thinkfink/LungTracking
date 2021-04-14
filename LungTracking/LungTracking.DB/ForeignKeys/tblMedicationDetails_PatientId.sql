ALTER TABLE [dbo].[tblMedicationDetail]
	ADD CONSTRAINT [tblMedicationDetail_PatientId]
	FOREIGN KEY (PatientId)
	REFERENCES [tblPatient] (Id) ON DELETE CASCADE