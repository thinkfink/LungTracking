ALTER TABLE [dbo].[tblProvider]
	ADD CONSTRAINT [tblProvider_UserId]
	FOREIGN KEY (UserId)
	REFERENCES [tblUser] (Id) ON DELETE CASCADE