ALTER TABLE [dbo].[tblBloodSugar]
	ADD CONSTRAINT [tblBloodSugar_PatientId]
	FOREIGN KEY (PatientId)
	REFERENCES [tblPatient] (Id) ON DELETE CASCADE