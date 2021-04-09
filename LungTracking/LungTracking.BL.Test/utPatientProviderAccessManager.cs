using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;

namespace LungTracking.BL.Test
{
    [TestClass]
    public class utPatientProviderAccessManager
    {
        // for testing insert record
        Guid providerId = Guid.Parse("5b0b01f2-f225-4b6a-8084-eaa7b64dfb16");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        // for testing existing record
        Guid oldProviderId = Guid.Parse("5b0b01f2-f225-4b6a-8084-eaa7b64dfb16");
        Guid oldPatientId = Guid.Parse("509db332-936f-4dc2-9c02-66d58a8a20f8");

        [TestMethod]
        public void LoadTest()
        {
            List<PatientProviderAccess> ppas = new List<PatientProviderAccess>();
            ppas = PatientProviderAccessManager.Load();
            int expected = 40;
            Assert.AreEqual(expected, ppas.Count);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            PatientProviderAccess ppas = new PatientProviderAccess();
            ppas = PatientProviderAccessManager.LoadById(oldPatientId, oldProviderId);
            Assert.IsNotNull(ppas);
        }

        [TestMethod]
        public void InsertTest()
        {
            PatientProviderAccess ppa = new PatientProviderAccess();
            ppa.PatientId = patientId;
            ppa.ProviderId = providerId;

            int result = PatientProviderAccessManager.Insert(ppa);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void DeleteTest()
        {
            List<PatientProviderAccess> ppas = PatientProviderAccessManager.Load();
            PatientProviderAccess ppa = ppas.Where(p => p.PatientId == patientId && p.ProviderId == providerId).FirstOrDefault();

            int results = PatientProviderAccessManager.Delete(ppa.PatientId, ppa.ProviderId);
            Assert.IsTrue(results > 0);
        }
    }
}
