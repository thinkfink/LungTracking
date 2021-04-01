ALTER TABLE [dbo].[tblMedicationTracking]
	ADD CONSTRAINT [tblMedicationTracking_MedicationId]
	FOREIGN KEY (MedicationId)
	REFERENCES [tblMedicationDetails] (Id) ON DELETE CASCADE