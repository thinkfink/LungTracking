/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
:r .\DefaultData\Users.sql
:r .\DefaultData\Providers.sql
:r .\DefaultData\Patients.sql
:r .\DefaultData\Caregivers.sql
:r .\DefaultData\PatientProviderAccesses.sql
:r .\DefaultData\PatientCaregiverAccesses.sql
:r .\DefaultData\MedicationDetails.sql
:r .\DefaultData\MedicationTimes.sql
:r .\DefaultData\MedicationTrackings.sql
:r .\DefaultData\PEFs.sql
:r .\DefaultData\FEV1s.sql
:r .\DefaultData\Temperatures.sql
:r .\DefaultData\BloodPressures.sql
:r .\DefaultData\Pulses.sql
:r .\DefaultData\BloodSugars.sql
:r .\DefaultData\Weights.sql
:r .\DefaultData\SleepWakes.sql
:r .\DefaultData\Appointments.sql