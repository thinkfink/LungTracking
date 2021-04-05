CREATE PROCEDURE [dbo].[spFindAllPatientData]
	@Id uniqueidentifier
AS
BEGIN
	SELECT 
		FirstName, LastName, Sex, DateOfBirth, StreetAddress, City, State, PhoneNumber,
		tblAppointment.AppointmentDate, tblAppointment.AppointmentTimeStart, tblAppointment.AppointmentTimeEnd, tblAppointment.AppointmentDescription, tblAppointment.AppointmentLocation,
		tblBloodSugar.BloodSugarNumber, tblBloodSugar.TimeOfDay, tblBloodSugar. UnitsOfInsulinGiven,
		tblFEV1.FEV1Number, tblFEV1.BeginningEnd, tblFEV1.TimeOfDay,
		tblMedicationDetails.MedicationName, tblMedicationDetails.MedicationDosageTotal, tblMedicationDetails.MedicationDosagePerPill, tblMedicationDetails.MedicationInstructions, tblMedicationDetails.NumberOfPills, tblMedicationDetails.DateFilled, tblMedicationDetails.QuantityOfFill, tblMedicationDetails.RefillDate,
		tblMedicationTime.PillTime, tblMedicationTime.MedicationId,
		tblMedicationTracking.PillTakenTime, tblMedicationTracking.MedicationId,
		tblPEF.PEFNumber, tblPEF.BeginningEnd, tblPEF.TimeOfDay,
		tblPulse.PulseNumber, tblPulse.BeginningEnd, tblPulse.TimeOfDay,
		tblSleepWake.SleepOrWake, tblSleepWake.TimeOfDay,
		tblTemperature.TempNumber, tblTemperature.BeginningEnd, tblTemperature.TimeOfDay,
		tblWeight.WeightNumberInPounds, tblWeight.TimeOfDay
	FROM tblPatient	
	JOIN tblAppointment ON tblAppointment.PatientId = tblPatient.Id
	JOIN tblBloodPressure ON tblBloodPressure.PatientId = tblPatient.Id
	JOIN tblBloodSugar ON tblBloodSugar.PatientId = tblPatient.Id
	JOIN tblFEV1 ON tblFEV1.PatientId = tblPatient.Id
	JOIN tblMedicationDetails ON tblMedicationDetails.PatientId = tblPatient.Id
	JOIN tblMedicationTime ON tblMedicationTime.PatientId = tblPatient.Id
	JOIN tblMedicationTracking ON tblMedicationTracking.PatientId = tblPatient.Id
	JOIN tblPEF ON tblPEF.PatientId = tblPatient.Id
	JOIN tblPulse ON tblPulse.PatientId = tblPatient.Id
	JOIN tblSleepWake ON tblSleepWake.PatientId = tblPatient.Id
	JOIN tblTemperature ON tblTemperature.PatientId = tblPatient.Id
	JOIN tblWeight ON tblWeight.PatientId = tblPatient.Id
	WHERE tblPatient.Id = @Id
END
