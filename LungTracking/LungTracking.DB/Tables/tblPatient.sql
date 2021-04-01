﻿CREATE TABLE [dbo].[tblPatient]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [DateOfBirth] DATE NOT NULL,
    [StreetAddress] VARCHAR(100) NOT NULL,
    [City] VARCHAR(50) NOT NULL, 
    [State] VARCHAR(20) NOT NULL, 
    [PhoneNumber] VARCHAR(22) NOT NULL, 
    [Sex] VARCHAR(6) NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL
)