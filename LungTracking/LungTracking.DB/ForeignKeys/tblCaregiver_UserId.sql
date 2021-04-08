ALTER TABLE [dbo].[tblCaregiver]
	ADD CONSTRAINT [tblCaregiver_UserId]
	FOREIGN KEY (UserId)
	REFERENCES [tblUser] (Id) ON DELETE CASCADE