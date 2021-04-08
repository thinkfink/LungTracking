/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
DROP TABLE IF EXISTS dbo.tblBloodSugar
DROP TABLE IF EXISTS dbo.tblPulse
DROP TABLE IF EXISTS dbo.tblBloodPressure
DROP TABLE IF EXISTS dbo.tblTemperature
DROP TABLE IF EXISTS dbo.tblFEV1
DROP TABLE IF EXISTS dbo.tblPEF
DROP TABLE IF EXISTS dbo.tblWeight
DROP TABLE IF EXISTS dbo.tblSleepWake
DROP TABLE IF EXISTS dbo.tblMedicationTime
DROP TABLE IF EXISTS dbo.tblMedicationTracking
DROP TABLE IF EXISTS dbo.tblMedicationDetails
DROP TABLE IF EXISTS dbo.tblAppointment
DROP TABLE IF EXISTS dbo.tblPatientCaregiverAccess
DROP TABLE IF EXISTS dbo.tblPatientProviderAccess
DROP TABLE IF EXISTS dbo.tblCaregiver
DROP TABLE IF EXISTS dbo.tblPatient
DROP TABLE IF EXISTS dbo.tblProvider
DROP TABLE IF EXISTS dbo.tblUser