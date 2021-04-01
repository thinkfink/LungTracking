ALTER TABLE [dbo].[tblPatient]
	ADD CONSTRAINT [tblPatient_UserId]
	FOREIGN KEY (UserId)
	REFERENCES [tblUser] (Id) ON DELETE CASCADE