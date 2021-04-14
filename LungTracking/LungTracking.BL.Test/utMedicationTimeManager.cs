using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;

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
            List<MedicationTime> mts = new List<MedicationTime>();
            mts = MedicationTimeManager.Load();
            int expected = 300;
            Assert.AreEqual(expected, mts.Count);
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            List<MedicationTime> mts = new List<MedicationTime>();
            mts = MedicationTimeManager.LoadByPatientId(patientId);
            Assert.IsNotNull(mts);
        }

        [TestMethod]
        public void InsertTest()
        {
            MedicationTime mt = new MedicationTime();
            mt.PillTime = TimeSpan.Parse("11:00:00");
            mt.MedicationId = medicationId;
            mt.PatientId = patientId;

            int result = MedicationTimeManager.Insert(mt);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            List<MedicationTime> mts = MedicationTimeManager.Load();

            MedicationTime mt = mts.Where(a => a.MedicationId == medicationId && a.PatientId == patientId).FirstOrDefault();

            mt.PillTime = TimeSpan.Parse("12:00:00");

            MedicationTimeManager.Update(mt);

            MedicationTime updatedmt = mts.FirstOrDefault(a => a.MedicationId == medicationId && a.PatientId == patientId);

            Assert.AreEqual(mt.PillTime, updatedmt.PillTime);
        }

        [TestMethod]
        public void DeleteTest()
        {
            List<MedicationTime> mts = MedicationTimeManager.Load();
            MedicationTime mt = mts.Where(a => a.MedicationId == medicationId && a.PatientId == patientId).FirstOrDefault();

            int results = MedicationTimeManager.Delete(mt.Id);
            Assert.IsTrue(results > 0);
        }
    }
}
