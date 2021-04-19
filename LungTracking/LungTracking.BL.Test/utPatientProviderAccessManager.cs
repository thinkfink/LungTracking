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
            Task.Run(async () =>
            {
                var task = await PatientProviderAccessManager.Load();
                IEnumerable<Models.PatientProviderAccess> pcas = task;
                Assert.AreEqual(300, pcas.ToList().Count);
            });
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            Task.Run(async () =>
            {
                var results = await PatientProviderAccessManager.LoadById(oldPatientId, oldProviderId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                PatientProviderAccess pca = new PatientProviderAccess();
                pca.PatientId = patientId;
                pca.ProviderId = providerId;

                int results = await PatientProviderAccessManager.Insert(pca);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = PatientProviderAccessManager.Load();
                IEnumerable<Models.PatientProviderAccess> pcas = task.Result;
                task.Wait();
                Models.PatientProviderAccess pca = pcas.FirstOrDefault(p => p.PatientId == patientId && p.ProviderId == providerId);
                var results = PatientProviderAccessManager.Delete(pca.Id, true);
                Assert.IsTrue(results.Result > 0);
            });
        }
    }
}