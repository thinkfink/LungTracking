CREATE PROCEDURE [dbo].[spFindPatientAppsAndPills]
	@Id uniqueidentifier
AS
BEGIN
	SELECT 
		FirstName, LastName, Sex, DateOfBirth, StreetAddress, City, State, PhoneNumber,
		tblAppointment.Date AS AppointmentDate, tblAppointment.TimeStart AS AppointmentTimeStart, tblAppointment.TimeEnd AS AppointmentTimeEnd, tblAppointment.Description AS AppointmentDescription, tblAppointment.Location AS AppointmentLocation,
		tblMedicationDetail.MedicationName AS MedicationName, tblMedicationDetail.MedicationDosageTotal AS MedicationDosageTotal, tblMedicationDetail.MedicationDosagePerPill AS MedicationDosagePerPill, tblMedicationDetail.MedicationInstructions AS MedicationInstructions, tblMedicationDetail.NumberOfPills AS MedicationNumberOfPills, tblMedicationDetail.DateFilled AS MedicationDateFilled, tblMedicationDetail.QuantityOfFill AS MedicationQuantityOfFill, tblMedicationDetail.RefillDate AS MedicationRefillDate,
		tblMedicationTime.PillTime AS PillTime, tblMedicationTime.MedicationId AS MedicationTimeMedicationId
	FROM tblPatient	
	JOIN tblAppointment ON tblAppointment.PatientId = tblPatient.Id
	JOIN tblMedicationDetail ON tblMedicationDetail.PatientId = tblPatient.Id
	JOIN tblMedicationTime ON tblMedicationTime.PatientId = tblPatient.Id
	WHERE tblPatient.Id = @Id
END
