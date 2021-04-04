BEGIN 
	INSERT INTO [dbo].[tblEmergency] (Id, EmergencyType, EmergencyMessage, TimeOfDay, PatientId) 
	VALUES
	('fb1546bf-0671-42f6-a99a-9ed025f313ac', 'Blood Sugar', 'Blood sugar too low. Call 911!', '2021-04-04 17:41:00', '976b6cd8-eeba-4643-88ba-914577682ffc')
END