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
    public class utBloodPressureManager
    {
        DateTime timeOfDay = DateTime.Parse("2021-04-14 11:00:00");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () =>
            {
                var task = await BloodPressureManager.Load();
                IEnumerable<Models.BloodPressure> appointments = task;
                Assert.AreEqual(300, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var results = await BloodPressureManager.LoadByPatientId(patientId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                BloodPressure bp = new BloodPressure();
                bp.BPsystolic = 120;
                bp.BPdiastolic = 80;
                bp.BeginningEnd = true;
                bp.TimeOfDay = timeOfDay;
                bp.PatientId = patientId;

                int results = await BloodPressureManager.Insert(bp);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = BloodPressureManager.Load();
                IEnumerable<Models.BloodPressure> bps = task.Result;
                Models.BloodPressure bp = bps.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);
                bp.BPsystolic = 110;
                bp.BPdiastolic = 70;
                var results = BloodPressureManager.Update(bp);
                Assert.IsTrue(results.Result > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = BloodPressureManager.Load();
                IEnumerable<Models.BloodPressure> bps = task.Result;
                task.Wait();
                Models.BloodPressure bp = bps.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);
                var results = BloodPressureManager.Delete(bp.Id, true);
                Assert.IsTrue(results.Result > 0);
            });
        }
    }
}
