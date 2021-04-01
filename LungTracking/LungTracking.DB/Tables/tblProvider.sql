﻿CREATE TABLE [dbo].[tblProvider]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [City] VARCHAR(50) NOT NULL, 
    [State] VARCHAR(20) NOT NULL, 
    [PhoneNumber] VARCHAR(22) NOT NULL, 
    [JobDescription] VARCHAR(50) NOT NULL,
    [ImagePath] VARCHAR(MAX) NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL
)
