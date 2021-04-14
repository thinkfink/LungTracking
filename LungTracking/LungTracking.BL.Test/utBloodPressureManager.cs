using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;

namespace LungTracking.BL.Test
{
    [TestClass]
    public class utBloodPressureManager
    {
        DateTime timeOfDay = DateTime.Parse("2021-04-14 11:00:00");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void LoadTest()
        {
            List<BloodPressure> bps = new List<BloodPressure>();
            bps = BloodPressureManager.Load();
            int expected = 300;
            Assert.AreEqual(expected, bps.Count);
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            List<BloodPressure> bps = new List<BloodPressure>();
            bps = BloodPressureManager.LoadByPatientId(patientId);
            Assert.IsNotNull(bps);
        }

        [TestMethod]
        public void InsertTest()
        {
            BloodPressure bp = new BloodPressure();
            bp.BPsystolic = 120;
            bp.BPdiastolic = 80;
            bp.BeginningEnd = true;
            bp.TimeOfDay = timeOfDay;
            bp.PatientId = patientId;

            int result = BloodPressureManager.Insert(bp);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            List<BloodPressure> bps = BloodPressureManager.Load();

            BloodPressure bp = bps.Where(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId).FirstOrDefault();

            bp.BPsystolic = 110;
            bp.BPdiastolic = 70;

            BloodPressureManager.Update(bp);

            BloodPressure updatedbp = bps.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);

            Assert.AreEqual(bp.TimeOfDay, updatedbp.TimeOfDay);
        }

        [TestMethod]
        public void DeleteTest()
        {
            List<BloodPressure> bps = BloodPressureManager.Load();
            BloodPressure bp = bps.Where(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId).FirstOrDefault();

            int results = BloodPressureManager.Delete(bp.Id);
            Assert.IsTrue(results > 0);
        }
    }
}
