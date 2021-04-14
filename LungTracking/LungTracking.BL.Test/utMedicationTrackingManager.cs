using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;

namespace LungTracking.BL.Test
{
    [TestClass]
    public class utMedicationTrackingManager
    {
        Guid medicationId = Guid.Parse("aa596935-5d6f-4a1d-940e-3c3afb6fd9c7");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void LoadTest()
        {
            List<MedicationTracking> mts = new List<MedicationTracking>();
            mts = MedicationTrackingManager.Load();
            int expected = 300;
            Assert.AreEqual(expected, mts.Count);
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            List<MedicationTracking> mts = new List<MedicationTracking>();
            mts = MedicationTrackingManager.LoadByPatientId(patientId);
            Assert.IsNotNull(mts);
        }

        [TestMethod]
        public void InsertTest()
        {
            MedicationTracking mt = new MedicationTracking();
            mt.PillTakenTime = DateTime.Parse("2021-04-14 11:00:00");
            mt.MedicationId = medicationId;
            mt.PatientId = patientId;

            int result = MedicationTrackingManager.Insert(mt);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            List<MedicationTracking> mts = MedicationTrackingManager.Load();

            MedicationTracking mt = mts.Where(a => a.MedicationId == medicationId && a.PatientId == patientId).FirstOrDefault();

            mt.PillTakenTime = DateTime.Parse("2021-04-14 12:00:00");

            MedicationTrackingManager.Update(mt);

            MedicationTracking updatedmt = mts.FirstOrDefault(a => a.MedicationId == medicationId && a.PatientId == patientId);

            Assert.AreEqual(mt.PillTakenTime, updatedmt.PillTakenTime);
        }

        [TestMethod]
        public void DeleteTest()
        {
            List<MedicationTracking> mts = MedicationTrackingManager.Load();
            MedicationTracking mt = mts.Where(a => a.MedicationId == medicationId && a.PatientId == patientId).FirstOrDefault();

            int results = MedicationTrackingManager.Delete(mt.Id);
            Assert.IsTrue(results > 0);
        }
    }
}
