﻿CREATE TABLE [dbo].[tblPulse]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [PulseNumber] INT NOT NULL, 
    [BeginningEnd] BIT NOT NULL, 
    [TimeOfDay] DATETIME NOT NULL, 
    [PatientId] UNIQUEIDENTIFIER NOT NULL
)
