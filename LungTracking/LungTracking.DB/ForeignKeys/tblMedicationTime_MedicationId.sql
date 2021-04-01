ALTER TABLE [dbo].[tblMedicationDetails]
	ADD CONSTRAINT [tblMedicationDetails_MedicationId]
	FOREIGN KEY (MedicationId)
	REFERENCES [tblMedicationDetails] (Id) ON DELETE CASCADE