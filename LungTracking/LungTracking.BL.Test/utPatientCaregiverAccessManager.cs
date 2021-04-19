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
    public class utPatientCaregiverAccessManager
    {
        // for testing insert record
        Guid caregiverId = Guid.Parse("f8cd0f12-04a4-4bb7-afed-425e95018ecc");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        // for testing existing record
        Guid oldCaregiverId = Guid.Parse("79f49d26-fcaa-458f-9a8c-9e98bbce67d5");
        Guid oldPatientId = Guid.Parse("16ee99e5-f8e9-45d3-8621-2435e0b60576");

        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () =>
            {
                var task = await PatientCaregiverAccessManager.Load();
                IEnumerable<Models.PatientCaregiverAccess> pcas = task;
                Assert.AreEqual(300, pcas.ToList().Count);
            });
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            Task.Run(async () =>
            {
                var results = await PatientCaregiverAccessManager.LoadById(oldPatientId, oldCaregiverId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                PatientCaregiverAccess pca = new PatientCaregiverAccess();
                pca.PatientId = patientId;
                pca.CaregiverId = caregiverId;

                int results = await PatientCaregiverAccessManager.Insert(pca);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = PatientCaregiverAccessManager.Load();
                IEnumerable<Models.PatientCaregiverAccess> pcas = task.Result;
                task.Wait();
                Models.PatientCaregiverAccess pca = pcas.FirstOrDefault(p => p.PatientId == patientId && p.CaregiverId == caregiverId);
                var results = PatientCaregiverAccessManager.Delete(pca.Id, true);
                Assert.IsTrue(results.Result > 0);
            });
        }
    }
}