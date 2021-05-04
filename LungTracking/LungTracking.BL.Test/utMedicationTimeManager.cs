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
    public class utMedicationTimeManager
    {
        Guid medicationId = Guid.Parse("aa596935-5d6f-4a1d-940e-3c3afb6fd9c7");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () =>
            {
                var task = await MedicationTimeManager.Load();
                IEnumerable<Models.MedicationTime> mts = task;
                Assert.AreEqual(300, mts.ToList().Count);
            });
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var results = await MedicationTimeManager.LoadByPatientId(patientId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                MedicationTime mt = new MedicationTime();
                mt.PillTime = DateTime.Parse("2020-04-10 12:00:00");
                mt.MedicationId = medicationId;
                mt.PatientId = patientId;

                int results = await MedicationTimeManager.Insert(mt);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = MedicationTimeManager.Load();
                IEnumerable<Models.MedicationTime> mts = task.Result;
                Models.MedicationTime mt = mts.FirstOrDefault(a => a.MedicationId == medicationId && a.PatientId == patientId);
                mt.PillTime = DateTime.Parse("2020-04-10 13:00:00");
                var results = MedicationTimeManager.Update(mt);
                Assert.IsTrue(results.Result > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = MedicationTimeManager.Load();
                IEnumerable<Models.MedicationTime> mts = task.Result;
                task.Wait();
                Models.MedicationTime mt = mts.FirstOrDefault(a => a.MedicationId == medicationId && a.PatientId == patientId);
                var results = MedicationTimeManager.Delete(mt.Id, true);
                Assert.IsTrue(results.Result > 0);
            });
        }
    }
}
