ALTER TABLE [dbo].[tblPatientCaregiverAccess]
	ADD CONSTRAINT [tblPatientCaregiverAccess_ProviderId]
	FOREIGN KEY (CaregiverId)
	REFERENCES [tblCaregiver] (Id) ON DELETE CASCADE