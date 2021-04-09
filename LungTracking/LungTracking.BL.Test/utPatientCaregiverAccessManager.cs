using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;

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
            List<PatientCaregiverAccess> pcas = new List<PatientCaregiverAccess>();
            pcas = PatientCaregiverAccessManager.Load();
            int expected = 40;
            Assert.AreEqual(expected, pcas.Count);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            PatientCaregiverAccess pcas = new PatientCaregiverAccess();
            pcas = PatientCaregiverAccessManager.LoadById(oldPatientId, oldCaregiverId);
            Assert.IsNotNull(pcas);
        }

        [TestMethod]
        public void InsertTest()
        {
            PatientCaregiverAccess pca = new PatientCaregiverAccess();
            pca.PatientId = patientId;
            pca.CaregiverId = caregiverId;

            int result = PatientCaregiverAccessManager.Insert(pca);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void DeleteTest()
        {
            List<PatientCaregiverAccess> pcas = PatientCaregiverAccessManager.Load();
            PatientCaregiverAccess pca = pcas.Where(p => p.PatientId == patientId && p.CaregiverId == caregiverId).FirstOrDefault();

            int results = PatientCaregiverAccessManager.Delete(pca.PatientId, pca.CaregiverId);
            Assert.IsTrue(results > 0);
        }
    }
}
