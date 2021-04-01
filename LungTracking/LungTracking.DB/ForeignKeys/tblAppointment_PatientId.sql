ALTER TABLE [dbo].[tblAppointment]
	ADD CONSTRAINT [tblAppointment_PatientId]
	FOREIGN KEY (PatientId)
	REFERENCES [tblPatient] (Id) ON DELETE NO ACTION