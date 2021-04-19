using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            Task.Run(async () =>
            {
                var task = await PatientManager.Load();
                IEnumerable<Models.Patient> patients = task;
                Assert.AreEqual(90, patients.ToList().Count);
            });
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var results = await PatientManager.LoadByPatientId(patientId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void LoadByUserIdTest()
        {
            Task.Run(async () =>
            {
                var results = await PatientManager.LoadByUserId(userId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
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

                int results = await PatientManager.Insert(patient);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = PatientManager.Load();
                IEnumerable<Models.Patient> patients = task.Result;
                Models.Patient patient = patients.FirstOrDefault(p => p.FirstName == "newfirstname");
                patient.LastName = "updatedlastname";
                var results = PatientManager.Update(patient);
                Assert.IsTrue(results.Result > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = PatientManager.Load();
                IEnumerable<Models.Patient> patients = task.Result;
                task.Wait();
                Models.Patient patient = patients.FirstOrDefault(p => p.FirstName == "newfirstname");
                var results = PatientManager.Delete(patient.Id, true);
                Assert.IsTrue(results.Result > 0);
            });
        }
    }
}