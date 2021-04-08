using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;

namespace LungTracking.BL.Test
{
    [TestClass]
    public class utPatientManager
    {
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");
        Guid userId = Guid.Parse("a203c2d8-733b-4cee-9bf5-b53d96bc0e16");

        [TestMethod]
        public void LoadTest()
        {
            List<Patient> patients = new List<Patient>();
            patients = PatientManager.Load();
            int expected = 90;
            Assert.AreEqual(expected, patients.Count);
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            Patient patient = new Patient();
            patient = PatientManager.LoadByPatientId(patientId);
            Assert.IsNotNull(patient);
        }

        [TestMethod]
        public void LoadByUserIdTest()
        {
            Patient patient = new Patient();
            patient = PatientManager.LoadByUserId(userId);
            Assert.IsNotNull(patient);
        }

        [TestMethod]
        public void InsertTest()
        {
            Patient patient = new Patient();
            patient.FirstName = "newfirstname";
            patient.LastName = "newlastname";
            patient.Sex = "M";
            patient.DateOfBirth = DateTime.Parse("1980-01-01");
            patient.StreetAddress = "123 Test Street";
            patient.City = "Testopolis";
            patient.State = "WI";
            patient.PhoneNumber = "(555)555-2345";
            patient.UserId = Guid.Parse("a007c7f2-cbd7-48dd-8fd3-adb871b55c25");

            int result = PatientManager.Insert(patient);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            List<Patient> patients = PatientManager.Load();

            Patient patient = patients.Where(p => p.FirstName == "newfirstname").FirstOrDefault();

            patient.LastName = "updatedlastname";

            PatientManager.Update(patient);

            Patient updatedpatient = patients.FirstOrDefault(p => p.FirstName == patient.FirstName);

            Assert.AreEqual(patient.LastName, updatedpatient.LastName);
        }

        [TestMethod]
        public void DeleteTest()
        {
            List<Patient> patients = PatientManager.Load();
            Patient patient = patients.Where(p => p.FirstName == "newfirstname").FirstOrDefault();

            int results = PatientManager.Delete(patient.Id);
            Assert.IsTrue(results > 0);
        }
    }
}
