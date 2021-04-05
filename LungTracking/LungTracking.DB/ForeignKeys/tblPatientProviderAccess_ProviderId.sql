ALTER TABLE [dbo].[tblPatientProviderAccess]
	ADD CONSTRAINT [tblPatientProviderAccess_ProviderId]
	FOREIGN KEY (ProviderId)
	REFERENCES [tblProvider] (Id) ON DELETE CASCADE