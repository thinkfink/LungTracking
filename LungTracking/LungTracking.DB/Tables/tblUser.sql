﻿CREATE TABLE [dbo].[tblUser]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Username] VARCHAR(30) NOT NULL UNIQUE, 
    [Password] VARCHAR(50) NOT NULL, 
    [Role] INT NOT NULL, 
    [Email] VARCHAR(100) NOT NULL UNIQUE, 
    [Created] DATETIME NOT NULL, 
    [LastLogin] DATETIME NOT NULL
)
